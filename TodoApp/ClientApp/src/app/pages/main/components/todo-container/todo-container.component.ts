import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { MatMenuTrigger } from '@angular/material/menu';
import * as _ from 'lodash';
import { ModalModel } from 'src/app/components/modal/modal.component';
import { Category, TaskStatus, Todo } from 'src/app/models/models';
import { ApplicationManager } from 'src/services/application-manager.service';
import { ModalService } from 'src/services/modal.service';
import { TodoService } from 'src/services/todo.service';
import { TodoCreateComponent } from '../todo-create/todo-create.component';

@Component({
  selector: 'app-todo-container',
  templateUrl: './todo-container.component.html',
  styleUrls: ['./todo-container.component.scss']
})
export class TodoContainerComponent {
  private _todoList: Todo[];
  @Input() get todoList(): Todo[] {
    return this._todoList;
  }
  set todoList(todoList: Todo[]) {
    this._todoList = todoList;
  }

  private _category: Category;
  @Input() get category(): Category {
    return this._category;
  }
  set category(category: Category) {
    this._category = category;
  }

  editingTodo: number = 0;
  @Input() headerText: string;

  constructor(private todoService: TodoService, private applicationManager: ApplicationManager, private modalService: ModalService) { }

  ngOnInit() {
    this.applicationManager.todoMoveSubject.subscribe((todo) => {
      if (this.getTaskStatus() == todo.Status)
        this.todoList.push(todo);
    });
  }


  onEditClick(todo: Todo) {
    this.editingTodo = todo.TaskId;

    _.defer(() => {
      const input: HTMLElement = document.getElementById(`input_${todo.TaskId}`);
      input.focus();
    })
  }

  onBlur(event, todo: Todo) {
    console.log(event);
    this.editingTodo = 0;
    if (!_.isNil(event.target) && !_.isEmpty(event.target.value)) {
      todo.Name = event.target.value;
    }
    this.updateTodo(todo);
  }

  onDeleteClick(todo: Todo) {
    this.todoService.deleteTodo(todo).subscribe(res => {
      if (res.Result.IsSuccess) {
        todo.IsDeleted = true;
      }
    });
  }

  updateTodo(todo: Todo) {
    this.todoService.updateTodo(todo, this.category.CategoryId).subscribe(res => {
      if (!res.Result.IsSuccess) {
        
      }
    });
  }

  updateStatus(todo: Todo) {
    this.applicationManager.todoMoveSubject.next(todo);
    _.remove(this.todoList, e => e.TaskId == todo.TaskId);
    this.updateTodo(todo);
  }


  updateStatusPrevious(todo: Todo) {
    if (todo.Status != TaskStatus.Todo) {
      if (todo.Status == TaskStatus.InProgress)
        todo.Status = TaskStatus.Todo;
      else if (todo.Status == TaskStatus.Completed)
        todo.Status = TaskStatus.InProgress;

      this.todoService.updateTodo(todo, this.category.CategoryId).subscribe(res => {
        if (res.Result.IsSuccess) {
          this.todoService.getCategory(this.category.CategoryId).subscribe((res) => {
            if (res.Result.IsSuccess)
              this.applicationManager.loadTodoSubject.next(res.CategoryObj);

          })
        }
      });
    }
  }

  getTaskStatus() {
    if (this.headerText == "Todo")
      return TaskStatus.Todo;
    else if (this.headerText == "In Progress")
      return TaskStatus.InProgress;
    else
      return TaskStatus.Completed;
  }

  openModal() {
    const modal: ModalModel = {
      Component: TodoCreateComponent,
      HeaderText: this.category.Name,
      CallBackFunction: (data) => {

        this.todoService.addTodo(data, this.category.CategoryId, this.getTaskStatus()).subscribe(() => {
          this.todoService.getCategory(this.category.CategoryId).subscribe((res) => {
            if (res.Result.IsSuccess)
              this.applicationManager.loadTodoSubject.next(res.CategoryObj);

          })
        });
      }
    }
    this.modalService.openModalSubject.next(modal);
  }

  openEditModal(todo: Todo) {
    const modal: ModalModel = {
      Component: TodoCreateComponent,
      Data: todo.Name,
      HeaderText: "Update Todo",
      CallBackFunction: (data) => {
        todo.Name = data;
        this.todoService.updateTodo(todo, this.category.CategoryId).subscribe(() => {
          this.todoService.getCategory(this.category.CategoryId).subscribe((res) => {
            if (res.Result.IsSuccess)
              this.applicationManager.loadTodoSubject.next(res.CategoryObj);

          })
        });
      }
    }
    this.modalService.openModalSubject.next(modal);
  }
  drop(event: CdkDragDrop<string[]>) {
    console.log(event.distance);
    const data = event.item.data as Todo;
    const tempStatus = data.Status;

    if (event.distance.x > 250 && event.distance.x <= 450) {
      if (data.Status == TaskStatus.Todo)
        data.Status = TaskStatus.InProgress
      else if (data.Status == TaskStatus.InProgress)
        data.Status = TaskStatus.Completed
    }
    else if (event.distance.x > 450 && event.distance.x < 700) {
      if (data.Status == TaskStatus.Todo)
        data.Status = TaskStatus.Completed
    }
    else if (event.distance.x > 450 && event.distance.x < 700) {
      if (data.Status == TaskStatus.Todo)
        data.Status = TaskStatus.Completed
    }
    else if (event.distance.x < -250 && event.distance.x >= -450) {
      if (data.Status == TaskStatus.InProgress)
        data.Status = TaskStatus.Todo
      else if (data.Status == TaskStatus.Completed)
        data.Status = TaskStatus.InProgress
    }
    else if (event.distance.x < -450 && event.distance.x >= -700) {
      if (data.Status == TaskStatus.Completed)
        data.Status = TaskStatus.Todo
    }
    if (data.Status != tempStatus)
      this.updateStatus(data);
  }
}