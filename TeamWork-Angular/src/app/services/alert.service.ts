import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';



@Injectable()
export class AlertService {

  private config;

  constructor(private service: ToastrService) { 
    this.config={
      tapToDismiss: true,
      preventDuplicates: true,
      positionClass : 'toast-bottom-right',
      onActivateTick: true,
      enableHtml: true,
      timeOut: 10000,
      progressBar: true
    };
  }

  showSucces(message: string, title?: string): void{
    this.service.success(message, title || 'Success', this.config);
  }

  showError(message: string, title?: string): void{
    this.service.error(message, title || 'Error', this.config);
  }

  showInfo(message: string, title?: string): void{
    this.service.info(message, title || 'Info', this.config);
  }

  showWarning(message: string, title?: string): void{
    this.service.warning(message, title || 'Warning', this.config);
  }
}
