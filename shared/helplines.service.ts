import { Injectable } from '@angular/core';
import { Helplines } from './helplines.model';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HelplinesService {

  formData: Helplines;
  list: Helplines[];
  readonly rootUrl = 'https://localhost:5001/api';

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  refreshList() {
    this.http.get(this.rootUrl + '/HelpLines')
    .toPromise().then(res => this.list = res as Helplines[]);
  }

  postBook(formData: Helplines): Observable<Helplines[]>  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.post<Helplines[]>(this.rootUrl + '/HelpLines', formData, httpOptions);
  }

  putBook(formData: Helplines): Observable<Helplines> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.put<Helplines>(this.rootUrl + '/HelpLines/' + formData.HelpId, formData, httpOptions);
  }

  deleteBook(id: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.delete<number>(this.rootUrl + '/HelpLines/' + id, httpOptions);
   }
}
