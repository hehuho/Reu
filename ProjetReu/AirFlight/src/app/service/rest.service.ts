import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators'

import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class RestService {
  baseurl = 'http://localhost:5001';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  constructor(private http : HttpClient) { }



datePost(data : FormGroup) : Observable <any>{
  return this.http.post<any>(this.baseurl +'/api/reservation/returnList', JSON.stringify(data), this.httpOptions).pipe(
  catchError(this.errorHandl));


}

ReservePost(data: FormGroup): Observable<any> {
  return this.http.post<any>(this.baseurl + '/api/reservation/submit', JSON.stringify(data), this.httpOptions).pipe(
      catchError(this.errorHandl));


  }







errorHandl(error) {
  let errorMessage = '';
  if(error.error instanceof ErrorEvent) {
    // Get client-side error
    errorMessage = 'client ', error.error.message;
  } else {
    // Get server-side error
    errorMessage = `server Error Code: ${error.status}\n Message: ${error.message}`;
  }
  console.log(errorMessage);
  return throwError(errorMessage);
}

}
