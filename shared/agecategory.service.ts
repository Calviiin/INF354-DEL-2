import { Injectable } from '@angular/core';
import { AgeCategory } from './age-category.model';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AgecategoryService {

  formData: AgeCategory;
  list: AgeCategory[];
  readonly rootUrl = 'https://localhost:5001/api';

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  refreshList() {
    this.http.get(this.rootUrl + '/AgeCategory')
    .toPromise().then(res => this.list = res as AgeCategory[]);
  }

  postBook(formData: AgeCategory): Observable<AgeCategory[]>  {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.post<AgeCategory[]>(this.rootUrl + '/AgeCategory', formData, httpOptions);
  }

  putBook(formData: AgeCategory): Observable<AgeCategory> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.put<AgeCategory>(this.rootUrl + '/AgeCategory/' + formData.AgeCatId, formData, httpOptions);
  }

  deleteBook(id: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.delete<number>(this.rootUrl + '/AgeCategory/' + id, httpOptions);
   }
}
