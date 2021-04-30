import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({ providedIn: "root" })
export class ModalService {
    openModalSubject: Subject<any> = new Subject();
    closeModalSubject: Subject<any> = new Subject();
    constructor() {

    }
    
}