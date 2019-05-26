import { Injectable } from '@angular/core';
import { SmokerStat } from './smoker-stat.model';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SomkerStatService {

  formData: SmokerStat;
  list: SmokerStat[];
  readonly rootUrl = 'https://localhost:5001/api';

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  refreshList() {
    this.http.get(this.rootUrl + '/SmokerStatus')
    .toPromise().then(res => this.list = res as SmokerStat[]);
  }

  postBook(formData: SmokerStat): Observable<SmokerStat[]>  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.post<SmokerStat[]>(this.rootUrl + '/SmokerStatus', formData, httpOptions);
  }

  putBook(formData: SmokerStat): Observable<SmokerStat> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.put<SmokerStat>(this.rootUrl + '/SmokerStatus/' + formData.SmokerId, formData, httpOptions);
  }

  deleteBook(id: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.delete<number>(this.rootUrl + '/SmokerStatus/' + id, httpOptions);
   }
}
