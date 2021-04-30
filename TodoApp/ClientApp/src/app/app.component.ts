import { Component } from '@angular/core';
import { ModalService } from 'src/services/modal.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'TodoApp';
  modalHidden: boolean = true;
  constructor(private userService: UserService, private modalService:ModalService){}

  ngOnInit(){
    this.modalService.openModalSubject.subscribe((val) => {
      this.modalHidden = false;
    })

    this.modalService.closeModalSubject.subscribe(() => {
      this.modalHidden = true;
    })
  }
}
