import { Component, OnInit } from '@angular/core';
import { BookService } from 'src/app/shared/book.service';
import { Book } from 'src/app/shared/book.model';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { debug } from 'util';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  dataSaved = false;

  constructor(private service: BookService, private toastr: ToastrService) { }

  ngOnInit() {
    this.service.refreshList();
    this.resetForm();
  }

  populateForm(boo: Book) {
    this.service.formData = boo;
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.resetForm();
    }
    this.service.formData = {
      Id: null,
      Name: '',
      Description: '',
      Author: '',
      ReleaseYear: '',
      Price: null,
    },
    this.dataSaved = false;
  }

  onSubmit(form: NgForm) {
    if (form.value.Id == null) {
      this.insertRecord(form);
    } else {
      this.updateRecord(form);
    }
    this.dataSaved = false;
  }

  insertRecord(form: NgForm) {
    this.service.postBook(form.value).subscribe(res => {
      this.dataSaved = true;
      this.toastr.success('Inserted successfully', 'Book Record');
      this.resetForm(form);
      this.service.refreshList();
    });
  }


  updateRecord(form: NgForm) {
    this.service.putBook(form.value).subscribe(res => {
      this.dataSaved = true;
      this.toastr.info('Updated successfully', 'Book Record');
      this.resetForm(form);
      this.service.refreshList();
    });

  }

  onDelete(id: number) {
    if (confirm('Are you sure to delete this record?')) {
      this.service.deleteBook(id).subscribe(res => {
        this.service.refreshList();
        this.toastr.warning('Deleted successfully', 'Book Record');
      });
    }
  }



}
