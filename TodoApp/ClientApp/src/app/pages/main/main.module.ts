import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main.component';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { ContainerComponent } from './components/container/container.component';
import { BodyComponent } from './components/body/body.component';
import { TodoContainerComponent } from './components/todo-container/todo-container.component';
import { TodoCreateComponent } from './components/todo-create/todo-create.component';
import {DragDropModule} from '@angular/cdk/drag-drop';
import { MatMenuModule } from '@angular/material/menu';
import {MatIconModule} from '@angular/material/icon';
const routes: Routes = [
  {
    path: '', component: ContainerComponent
  }
]
@NgModule({
  declarations: [
      MainComponent,
      SidebarComponent,
      HeaderComponent,
      ContainerComponent,
      BodyComponent,
      TodoContainerComponent,
      TodoCreateComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    FormsModule,
    CommonModule,
    DragDropModule,
    MatMenuModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: []
})
export class MainModule { }
