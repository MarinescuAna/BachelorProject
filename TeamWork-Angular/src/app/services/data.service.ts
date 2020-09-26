import { Injectable, Injector } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { environment }  from 'src/environments/environment';
import { from, Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { AlertService } from './alert.service';
import { AppErrorHandler } from 'src/app/handler-error/app-error-handler';
import { BadRequestError } from "src/app/handler-error/bad-request-error";
import { ConflictError } from "src/app/handler-error/conflict-error";
import { ForbiddenError } from "src/app/handler-error/forbidden-error";
import { MethodNotAllowedError } from "src/app/handler-error/method-not-allowed-error";
import { NotFoundError } from "src/app/handler-error/not-found-error";
import { UnauthorizedError } from "src/app/handler-error/unauthorized-error";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class DataService {

  private http: HttpClient;
  private appHandler: AppErrorHandler;
  public alertService: AlertService;
  constructor(
    private injector: Injector,
    private url: string
  ) {
    this.http=this.injector.get(HttpClient);
    this.alertService=this.injector.get(AlertService);
    this.appHandler=this.injector.get(AppErrorHandler);
    this.url=`${environment.baseApiUrl}${url}`;
   }

   getAll<T>(): Observable<T[]>{
     return this.getMany<T>('');
   }

   getMany<T>(path: string): Observable<T[]>{
     const url=`${this.url}/${path}`;
     return this.http.get<[]>(url,httpOptions)
     .pipe(map((response) => {
       return response as T[];
     })).pipe(catchError((error: HttpErrorResponse)=>{
       return this.handleError(error);
     }));
   }

   getOne<T>(url: string):Observable<T>{
      const path=`${this.url}/${url}`;
      return this.http.get(path)
        .pipe(map((response) => {
          return response as T;
        })).pipe(catchError((err: HttpErrorResponse) => {
          return this.handleError(err);
        }));
   }

   post<T>(path: string, data: any): Observable<T>{
     const url =`${this.url}/${path}`;
     const body = JSON.stringify(data);
     return this.http.post(url, body, httpOptions)
        .pipe(map((response)=>{
          return response as T;
        })).pipe(catchError((err: HttpErrorResponse) => {
          return this.handleError(err);
        }));
   }

   // tslint:disable-next-line: typedef
  update(id: number, entity: any) {
    const body = JSON.stringify(entity);
    const url = `${this.url}/${id}`;
    return this.http.put(url, body, httpOptions).pipe(map((response) => {
      return response;
    })).pipe(catchError(
        (err: HttpErrorResponse) => {
          return this.handleError(err);
        }));
  }

  add<T>(entity: any): Observable<T> {
    const body = JSON.stringify(entity);
    const url = `${this.url}`;
    return this.http.post<T>(url, body, httpOptions).pipe(map((response) => {
      return response as T;
    })).pipe(catchError(
      (err: HttpErrorResponse) => {
        return this.handleError(err);
      }));
  }

  delete(id: number): Observable<any> {
    const url = `${this.url}/${id}`;
    return this.http.delete<any>(url, httpOptions).pipe(map((response) => {
      return response;
    })).pipe(catchError(
      (err: HttpErrorResponse) => {
        return this.handleError(err);
      }));
  }

   public handleError(error: any){
    switch (error.status) {
      case 400: {
        throw this.appHandler.handleError(new BadRequestError(error.error));
      }
      case 405: {
        throw this.appHandler.handleError(new MethodNotAllowedError(error.error));
      }
      case 404: {
        throw this.appHandler.handleError(new NotFoundError(error.error));
      }
      case 409: {
        throw this.appHandler.handleError(new ConflictError(error.error));
      }
      case 403: {
        throw this.appHandler.handleError(new ForbiddenError(error.error));
      }
      case 401: {
        throw this.appHandler.handleError(new UnauthorizedError(error.error));
      }
      case 500: {
        this.alertService.showError('The server encountered an internal error and was unable to complete your request. ' +
          'Please contact the administrators and inform them of the time the error occurred and anything you might have done that may have caused the error.');
      }
    }
    this.alertService.showError(error.message, 'Cannot connect to the API');
     return Observable.throw;
   }
   

   

}
