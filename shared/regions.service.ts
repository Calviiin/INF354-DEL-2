import { Injectable } from '@angular/core';
import { Regions } from './regions.model';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegionsService {

  formData: Regions;
  list: Regions[];
  readonly rootUrl = 'https://localhost:5001/api';

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  refreshList() {
    this.http.get(this.rootUrl + '/Regions')
    .toPromise().then(res => this.list = res as Regions[]);
  }

  postBook(formData: Regions): Observable<Regions[]>  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.post<Regions[]>(this.rootUrl + '/Regions', formData, httpOptions);
  }

  putBook(formData: Regions): Observable<Regions> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.put<Regions>(this.rootUrl + '/Regions/' + formData.RegionId, formData, httpOptions);
  }

  deleteBook(id: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.delete<number>(this.rootUrl + '/Regions/' + id, httpOptions);
   }
}
