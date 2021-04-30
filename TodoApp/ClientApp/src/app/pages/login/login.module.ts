import { NgModule } from '@angular/core';
import { LoginComponent } from './login.component';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { RegisterFormComponent } from './components/register-form/register-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path:'', component: LoginFormComponent },
  { path: 'register', component: RegisterFormComponent}
]

@NgModule({
  declarations: [
   LoginComponent,
   LoginFormComponent,
   RegisterFormComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    CommonModule
  ],
  providers: [],
  bootstrap: []
})
export class LoginModule { }
