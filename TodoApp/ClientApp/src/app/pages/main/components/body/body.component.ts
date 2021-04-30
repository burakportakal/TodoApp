import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Category, TaskStatus, Todo } from 'src/app/models/models';
import { ApplicationManager } from 'src/services/application-manager.service';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.scss']
})
export class BodyComponent {
  category: Category;
  todoList: Todo[] = [];
  inProgressList: Todo[] = [];
  completedList: Todo[] = [];
  constructor(private applicationManager: ApplicationManager) { }

  ngOnInit() {
    this.fillTodoList();
  }

  fillTodoList() {
    this.applicationManager.loadTodoSubject.subscribe((category) => {
      if (category) {
        this.category = category;
        this.todoList = [];
        this.inProgressList = [];
        this.completedList = [];

        if (!_.isEmpty(category.TodoList)) {
          category.TodoList.map((e) => {
            if (e.Status == TaskStatus.Todo)
              this.todoList.push(e);
            else if (e.Status == TaskStatus.InProgress)
              this.inProgressList.push(e);
            else if (e.Status == TaskStatus.Completed)
              this.completedList.push(e);
          })
        }
      }
    })
  }
}