import { Injectable } from '@angular/core';
import { Treatments } from './treatments.model';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class TreatmentsService {

  formData: Treatments;
  list: Treatments[];
  readonly rootUrl = 'https://localhost:5001/api';

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  refreshList() {
    this.http.get(this.rootUrl + '/Treatments')
    .toPromise().then(res => this.list = res as Treatments[]);
  }

  postBook(formData: Treatments): Observable<Treatments[]>  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.post<Treatments[]>(this.rootUrl + '/Treatments', formData, httpOptions);
  }

  putBook(formData: Treatments): Observable<Treatments> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.put<Treatments>(this.rootUrl + '/Treatments/' + formData.TratId, formData, httpOptions);
  }

  deleteBook(id: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.delete<number>(this.rootUrl + '/Treatments/' + id, httpOptions);
   }
}
