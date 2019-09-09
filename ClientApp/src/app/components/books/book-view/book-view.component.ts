import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BooksService } from 'src/app/services/books.service';
import { ToastrManager } from 'ng6-toastr-notifications';
import { forkJoin } from 'rxjs/internal/observable/forkJoin';
import { CategoriesService } from 'src/app/services/categories.service';
import { AuthorsService } from 'src/app/services/authors.service';
import * as _ from 'underscore';

@Component({
  selector: 'app-book-view',
  templateUrl: './book-view.component.html',
  styleUrls: ['./book-view.component.css']
})
export class BookViewComponent implements OnInit {
  book: any = {
    "categories":[]
  };
  categories: any={};
  authors: any = {};
  emptyFilter: any;

  authorsData = [];
  authorKeyword = 'name';
  dropdownCategories = [];
  selectedCategories = [];
  dropdownSettings = {};
  authorId = 0;
  author:any ={};

  constructor(private route: ActivatedRoute,
    private router: Router,
    private bookService: BooksService,
    private categoryService: CategoriesService,
    private authorService: AuthorsService,
    public toasterService : ToastrManager) {
    this.route.params.subscribe(r => {
      this.book.id = +r['id'] || 0;
    });
  }

  ngOnInit() {

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      itemsShowLimit: 8,
      allowSearchFilter: true
    };

    var sources = [
      //get categories
      this.categoryService.getCategories(this.emptyFilter),
      //get authors
      this.authorService.getAuthors(this.emptyFilter)
    ]
    if(this.book.id){
      //get book
      sources.push(this.bookService.getBookById(this.book.id));
    }
    forkJoin(sources).subscribe(data => {
      this.categories = data[0];
      this.authors = data[1];
      this.authorsData = this.authors.items;
      this.dropdownCategories = this.categories.items;

      if(this.book.id){
        this.setBook(data[2]);
        this.populateModels(data[2]);
      }
    }, err => {
      if(err.status == 404)
        this.router.navigate(['']);
    });
  }
  populateModels(book) {
    this.author = book.author;
    this.selectedCategories = book.categories;
    console.log(this.selectedCategories);
  }
  setBook(book) {
    this.book.id = book.id;
    this.book.name = book.name;
    this.book.authorId = book.author.id;
    this.book.isbn = book.isbn;
    this.book.categories = _.pluck(book.categories,'id');
  }
  save(){
    var result$ = (this.book.id) ? 
      this.bookService.updateBook(this.book) : 
      this.bookService.createBook(this.book);
    result$.subscribe(res=>{
      this.book = res;
      this.toasterService.successToastr(
        'Data was succesfully saved',
        'Success',
        {
          toastTimeout:5000,
          animate: 'slideFromTop',
          showCloseButton: false
        });
      this.router.navigate(['books']);
    },error => {
      this.toasterService.errorToastr(
        'Something was fail',
        'Error',
        {
          toastTimeout:5000,
          animate: 'slideFromTop',
          showCloseButton: false
        });
    });
  }

  delete(){
    this.bookService.deleteBook(this.book.id)
      .subscribe( result => {
        debugger;
        this.toasterService.successToastr(
          'Author was deleted succesfully',
          'Success',
          {
            toastTimeout:5000,
            animate: 'slideFromTop',
            showCloseButton: false
          });
        this.router.navigate(['books']);
      },error => {
        debugger;
        this.toasterService.errorToastr(
          'Something was fail',
          'Error',
          {
            toastTimeout:5000,
            animate: 'slideFromTop',
            showCloseButton: false
          });
      });
  }

  onItemSelect(category) {
    this.book.categories.push(category.id);
  }
  onDeSelect(category){
    var index = this.book.categories.indexOf(category.id);
    this.book.categories.splice(index, 1);
    //console.log('unChecked'+categoryId)
  }
  selectEvent(item) {
    this.authorId = item.id;
    this.book.authorId= this.authorId;
  }
}
