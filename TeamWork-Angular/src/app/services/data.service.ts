import { Injectable, Injector } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { environment }  from 'src/environments/environment';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class DataService {

  private http: HttpClient;

  constructor(
    private injector: Injector,
    private url: string
  ) {
    this.http=this.injector.get(HttpClient);
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
     return Observable.throw;
   }
   

   

}
