import { Injectable } from '@angular/core';
import { Gender } from './gender.model';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenderService {

  formData: Gender;
  list: Gender[];
  readonly rootUrl = 'https://localhost:5001/api';

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  refreshList() {
    this.http.get(this.rootUrl + '/Gender')
    .toPromise().then(res => this.list = res as Gender[]);
  }

  postBook(formData: Gender): Observable<Gender[]>  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.post<Gender[]>(this.rootUrl + '/Gender', formData, httpOptions);
  }

  putBook(formData: Gender): Observable<Gender> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.put<Gender>(this.rootUrl + '/Gender/' + formData.GenId, formData, httpOptions);
  }

  deleteBook(id: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.delete<number>(this.rootUrl + '/Gender/' + id, httpOptions);
   }
}
