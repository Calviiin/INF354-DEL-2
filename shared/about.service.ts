import { Injectable } from '@angular/core';
import { About } from './about.model';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AboutService {

  formData: About ;
  list: About[];
  readonly rootUrl = 'https://localhost:5001/api';

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  refreshList() {
    this.http.get(this.rootUrl + '/About')
    .toPromise().then(res => this.list = res as About[]);
  }

  postAbout(formData: About): Observable<About[]>  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.post<About[]>(this.rootUrl + '/About', formData, httpOptions);
  }

  putAbout(formData: About): Observable<About> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.put<About>(this.rootUrl + '/About/' + formData.AboutId, formData, httpOptions);
  }

  deleteAbout(id: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.delete<number>(this.rootUrl + '/About/' + id, httpOptions);
   }
}
