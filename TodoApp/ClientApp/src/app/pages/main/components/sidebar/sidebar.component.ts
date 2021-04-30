import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Category } from 'src/app/models/models';
import { ApplicationManager } from 'src/services/application-manager.service';
import { TodoService } from 'src/services/todo.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  categoryList: Category[] = [];
  showAddCategory: boolean = false;
  categoryName: string = "";
  isOpen: boolean = false;
  editingCategory: number = 0;
  @HostListener("body:click", ['$event.target']) documentClick($event: any) {
    if ($event.tagName != "MAT-ICON" && !this.parentElementFinder($event, "app-sidebar")) {
      this.isOpen = false;
    }
  }

  parentElementFinder(element: HTMLElement, elementTag: string) {
    while (element.parentElement != null) {
      if (element.tagName.toUpperCase() == elementTag.toUpperCase()) {
        return true;
      }
      element = element.parentElement;
    }
    return false;
  }

  @ViewChild("sidebar", { read: ElementRef, static: true }) sidebar: ElementRef;

  constructor(private todoService: TodoService, private applicationManager: ApplicationManager) { }

  ngOnInit(): void {
    this.getCategories().pipe((take(1))).subscribe(() => { });
    this.applicationManager.sideIconClicked.subscribe(() => {
      this.isOpen = !this.isOpen;
    })
  }

  getCategories(): Observable<void> {
    this.categoryList = [];
    return new Observable((subscriber) => {
      this.todoService.getCategories().pipe(take(1)).subscribe((res) => {
        if (res.Result.IsSuccess) {
          this.categoryList = res.Categories
          this.applicationManager.loadTodoSubject.next(res.Categories[0]);
        }
        subscriber.next();
      })
    })
  }

  openCategory(category: Category) {
    this.getCategories().pipe(take(1)).subscribe(() => {
      this.applicationManager.loadTodoSubject.next(category);
    })
  }

  saveCategory() {
    this.todoService.addCategory(this.categoryName).subscribe(() => {
      this.showAddCategory = false;
      this.categoryName = "";
      this.getCategories().toPromise();
    });

  }

  onEditClick(category: Category) {
    this.editingCategory = category.CategoryId;

    _.defer(() => {
      const input: HTMLElement = document.getElementById(`category_${category.CategoryId}`);
      input.focus();
    })
  }

  updateCategory(category) {
    this.todoService.updateCategory(category).toPromise();
  }

  onBlur(event, category: Category) {

    this.editingCategory = 0;
    if (!_.isNil(event.target) && !_.isEmpty(event.target.value)) {
      category.Name = event.target.value;
    }
    this.updateCategory(category);
  }

  onDeleteClick(category: Category) {
    this.todoService.deleteCategory(category).subscribe(res => {
      if (res.Result.IsSuccess) {
        this.getCategories().toPromise();;
      }
    });
  }

}