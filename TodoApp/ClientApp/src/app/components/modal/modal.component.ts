import { Component, ComponentFactoryResolver, ComponentRef, ElementRef, Type, ViewChild, ViewContainerRef } from '@angular/core';
import { ModalService } from 'src/services/modal.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent {
    @ViewChild("modalTemplate", {read:ViewContainerRef}) modalTemplate;
    modalModel: ModalModel;
    callBackFunction: any;
    componentRef: ComponentRef<Modal>;
    headerText: string;
    constructor(private modalService: ModalService,private cfr: ComponentFactoryResolver,private host: ElementRef<HTMLElement>){}

    ngOnInit(){
        this.modalService.openModalSubject.subscribe((val: ModalModel) => {
            this.modalModel = val;
            this.modalTemplate.clear();
            const componentFactory = this.cfr.resolveComponentFactory(val.Component);
            
            const componentRef: ComponentRef<Modal> = this.modalTemplate.createComponent(componentFactory);
            this.componentRef = componentRef;
            this.componentRef.instance.Data = val.Data;
            this.headerText = val.HeaderText;
        })
    }

    closeModal(){
        this.modalService.closeModalSubject.next(true);
    }

    saveClick(){
        const result = this.componentRef.instance.OnSave();
        this.modalModel.CallBackFunction(result);
        this.closeModal();
    }
    
}

export interface Modal{
    OnSave: Function;
    Data: any;
}

export interface ModalModel{
    Component: Type<Modal>;
    Data?: any;
    CallBackFunction: Function;
    HeaderText: string;
}