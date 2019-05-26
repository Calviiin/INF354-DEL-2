import { Injectable } from '@angular/core';
import { Book } from './book.model';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  formData: Book;
  list: Book[];
  readonly rootUrl = 'https://localhost:5001/api';

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  refreshList() {
    this.http.get(this.rootUrl + '/Books')
    .toPromise().then(res => this.list = res as Book[]);
  }

  postBook(formData: Book): Observable<Book[]> {
    return this.http.post<Book[]>(this.rootUrl + '/Books', formData);
  }

  putBook(formData: Book): Observable<Book> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.put<Book>(this.rootUrl + '/Books/' + formData.Id, formData, httpOptions);
  }

  deleteBook(id: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };
    return this.http.delete<number>(this.rootUrl + '/Books/' + id, httpOptions);
   }

}
