import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { matchValidator } from 'src/app/validators/custom-validators';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss']
})
export class RegisterFormComponent {
  registerForm: FormGroup;
  userNameKey = "UserName";
  firstNameKey = "FistName";
  lastNameKey = "LastName";
  emailKey = "Email";
  passwordKey = "Password";
  passwordRepeatKey = "PasswordRepeat";
  registerError: boolean = false;
  registerSuccess: boolean = false;
  error: string = "";
  get UserName(): AbstractControl {
    return this.registerForm.controls[this.userNameKey];
  }

  get FirstName(): AbstractControl {
    return this.registerForm.controls[this.firstNameKey];
  }

  get LastName(): AbstractControl {
    return this.registerForm.controls[this.lastNameKey];
  }

  get Email(): AbstractControl {
    return this.registerForm.controls[this.emailKey];
  }

  get Password(): AbstractControl {
    return this.registerForm.controls[this.passwordKey];
  }

  get PasswordRepeat(): AbstractControl {
    return this.registerForm.controls[this.passwordRepeatKey];
  }

  constructor(private userService: UserService, private router: Router) {
    this.registerForm = this.initilizeFormGroup();
  }

  initilizeFormGroup(): FormGroup {
    const userNameControl = new FormControl('', [Validators.required,Validators.maxLength(15),Validators.minLength(5)]);
    const firstNameControl = new FormControl('', [Validators.required,Validators.maxLength(15),Validators.minLength(5)]);
    const lastNameControl = new FormControl('', [Validators.required,Validators.maxLength(15),Validators.minLength(5)]);
    const emailControl = new FormControl('', [Validators.required,Validators.maxLength(50),Validators.minLength(10)]);
    const passwordControl = new FormControl('', );
    const passwordRepeatControl = new FormControl('', );
    passwordControl.setValidators([Validators.required,Validators.maxLength(15),Validators.minLength(8)]);
    passwordRepeatControl.setValidators([Validators.required,Validators.maxLength(15),Validators.minLength(8),matchValidator(passwordControl)]);
    const formGroup = new FormGroup({});
    formGroup.addControl(this.userNameKey,userNameControl);
    formGroup.addControl(this.firstNameKey,firstNameControl);
    formGroup.addControl(this.lastNameKey,lastNameControl);
    formGroup.addControl(this.emailKey,emailControl);
    formGroup.addControl(this.passwordKey,passwordControl);
    formGroup.addControl(this.passwordRepeatKey,passwordRepeatControl);
    return formGroup;
  }
  signInClick(){
     this.router.navigateByUrl('login');
  }
  register() {
    if (this.registerForm.valid) {
      const request = {
        UserName: this.UserName.value,
        FirstName: this.FirstName.value,
        LastName: this.LastName.value,
        Email: this.Email.value,
        Password: this.Password.value
      }

      this.userService.register(request).subscribe((res: any) => {
        if (res.Result.IsSuccess) {
          this.registerSuccess = true;
        }
        else {
          this.registerError = true;
          this.error = res.Result.Error.ErrorText;
        }
      });
    }
  }
}
