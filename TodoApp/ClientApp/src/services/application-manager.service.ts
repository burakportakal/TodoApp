import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { Category, Todo } from "src/app/models/models";



@Injectable({providedIn: 'root'})
export class ApplicationManager {
    searchSubject: Subject<string> = new Subject();
    loadTodoSubject: Subject<Category> = new Subject();
    sideIconClicked: Subject<any> = new Subject();
    todoMoveSubject: Subject<Todo> = new Subject();
}