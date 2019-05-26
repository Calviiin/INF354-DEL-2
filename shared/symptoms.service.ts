import { Injectable } from '@angular/core';
import { Symptoms } from './symptoms.model';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SymptomsService {

  formData: Symptoms;
  list: Symptoms[];
  readonly rootUrl = 'https://localhost:5001/api';

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  refreshList() {
    this.http.get(this.rootUrl + '/Symptoms')
    .toPromise().then(res => this.list = res as Symptoms[]);
  }

  postBook(formData: Symptoms): Observable<Symptoms[]>  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.post<Symptoms[]>(this.rootUrl + '/Symptoms', formData, httpOptions);
  }

  putBook(formData: Symptoms): Observable<Symptoms> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.put<Symptoms>(this.rootUrl + '/Symptoms/' + formData.SympId, formData, httpOptions);
  }

  deleteBook(id: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.delete<number>(this.rootUrl + '/Symptoms/' + id, httpOptions);
   }
}
