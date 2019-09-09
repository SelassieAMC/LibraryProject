import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BooksService } from 'src/app/services/books.service';
import { ToastrManager } from 'ng6-toastr-notifications';
import { forkJoin } from 'rxjs/internal/observable/forkJoin';
import { CategoriesService } from 'src/app/services/categories.service';
import { AuthorsService } from 'src/app/services/authors.service';

@Component({
  selector: 'app-book-view',
  templateUrl: './book-view.component.html',
  styleUrls: ['./book-view.component.css']
})
export class BookViewComponent implements OnInit {
  book: any = {
    "author":{},
    "category":[]
  };
  categories: any={};
  authors: any = {};
  emptyFilter: any;

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
      if(this.book.id){
        this.setBook(data[2]);
        this.populateModels();
      }
    }, err => {
      if(err.status == 404)
        this.router.navigate(['']);
    });
  }
  populateModels() {
    //this.categories = data[]
  }
  setBook(book) {
    //
  }
  save(){
    //debugger;
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

}
