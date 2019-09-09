import { Component, OnInit } from '@angular/core';
import { BooksService } from 'src/app/services/books.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  query: any = {};
  queryResult: any = {};

  constructor(private bookService: BooksService) { }

  ngOnInit() {
    this.getBooks();
  }

  getBooks(){
    this.bookService.getBooks(this.query)
    .subscribe(result => {
      this.queryResult = result;
    });
  }
}
