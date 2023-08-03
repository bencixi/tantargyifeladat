import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Response } from '../models/response';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  constructor(
    private http: HttpClient
  ) { }

  getPerson(): Observable<Response<any>>{
    return this.http.get<Response<any>>(`${environment.baseURL}/api/Person/GetAll`);
  }

  getPersonById(id): Observable<Response<any>>{
    return this.http.get<Response<any>>(`${environment.baseURL}/api/Person/${id}`);
  }

  addPerson(user: any): Observable<Response<any>>{
    return this.http.post<Response<any>>(`${environment.baseURL}/api/Person`, user);
  }

  deletePerson(id): Observable<Response<any>>{
    return this.http.delete<Response<any>>(`${environment.baseURL}/api/Person/${id}`);
  }

}