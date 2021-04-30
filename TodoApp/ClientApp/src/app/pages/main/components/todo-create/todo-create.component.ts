import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Modal } from 'src/app/components/modal/modal.component';

@Component({
  selector: 'app-todo-create',
  templateUrl: './todo-create.component.html',
  styleUrls: ['./todo-create.component.scss']
})
export class TodoCreateComponent implements Modal {
  todoText: string = "";
  Data: any;
  constructor() { }

  ngAfterViewInit(){
    if(!_.isNil(this.Data))
      this.todoText = this.Data
  }
  
  OnSave() {
    console.log("save calisti")
    return this.todoText;
  }

}