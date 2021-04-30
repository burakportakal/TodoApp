import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent {
  loginForm: FormGroup;
  userNameKey = "UserName";
  passwordKey = "Password";
  loginError: boolean = false;
  error: string = "";
  get UserName(): AbstractControl {
    return this.loginForm.controls[this.userNameKey];
  }

  get Password(): AbstractControl {
    return this.loginForm.controls[this.passwordKey];
  }

  constructor(private userService: UserService, private router: Router) {
    this.loginForm = this.initilizeFormGroup();
  }

  initilizeFormGroup(): FormGroup {
    const userNameControl = new FormControl('', [Validators.required, Validators.maxLength(15), Validators.minLength(5)]);
    const passwordControl = new FormControl('', [Validators.required, Validators.maxLength(15), Validators.minLength(8)]);

    const formGroup = new FormGroup({})
    formGroup.addControl(this.userNameKey, userNameControl);
    formGroup.addControl(this.passwordKey, passwordControl);
    return formGroup;
  }

  login() {
    if (this.loginForm.valid) {
      const request = {
        UserName: this.UserName.value,
        Password: this.Password.value
      }

      this.userService.login(request).subscribe((res: any) => {
        if (res.Result.IsSuccess) {
          this.router.navigateByUrl('');
        }
        else {
          this.loginError = true;
          this.error = res.Result.Error.ErrorText;
        }
      });
    }
  }

  registerClick() {
    this.router.navigateByUrl("login/register");
  }

}
