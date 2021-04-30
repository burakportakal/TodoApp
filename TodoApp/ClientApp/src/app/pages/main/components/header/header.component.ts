import { Component } from '@angular/core';
import * as _ from 'lodash';
import { parseJwt } from 'src/app/common/common-functions';
import { User } from 'src/app/models/models';
import { ApplicationManager } from 'src/services/application-manager.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  searchString: string = "";
  user: User;

  constructor(private userService: UserService, private appManager: ApplicationManager) {
  }

  ngOnInit() {
    this.user = this.userService.userObject;
  }

  onSideIconClick(){
      this.appManager.sideIconClicked.next();
  }

  logout() {
    this.userService.logout();
    window.location.href = "/";
  }

}