import { Component, OnInit } from '@angular/core';
import { BooksService } from 'src/app/services/books.service';
import * as _ from 'underscore';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  query: any = {};
  queryResult: any = {};
  filter:any;
  filterValue:any;
  filterList = [
    {"title":"Book Name",
    "value":"bookName"},
    {"title":"Category",
    "value":"categoryName"},
    {"title":"Autor Name",
    "value":"authorName"}
  ]
  constructor(private bookService: BooksService) { }
  categoriesName: any;
  ngOnInit() {
    this.getBooks();
  }

  getBooks(){
    this.bookService.getBooks(this.query)
    .subscribe(result => {
      this.queryResult = result;
    });
    this.categoriesName =  _.pluck(this.queryResult.items.categories,'name');
  }
  applyFilter(){
    debugger;
    switch(this.filter){
      case "bookName":this.query.bookName=this.filterValue;
      break;
      case "categoryName":this.query.categoryName=this.filterValue;
      break;
      case "authorName":this.query.authorName=this.filterValue;
      break;
      default:this.query = {};
    }
    this.getBooks();
  }
  onFilterChange(){
    this.filterValue = "";
    this.query = {};
    this.getBooks();
  }
}
