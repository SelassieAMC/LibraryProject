import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrManager  } from 'ng6-toastr-notifications';
import { AuthorsService } from 'src/app/services/authors.service';

@Component({
  selector: 'app-author-view',
  templateUrl: './author-view.component.html',
  styleUrls: ['./author-view.component.css']
})
export class AuthorViewComponent implements OnInit {
  author: any = {};

  constructor( private route: ActivatedRoute,
    private router: Router,
    private authorService: AuthorsService,
    public toasterService : ToastrManager) {
    this.route.params.subscribe(r => {
      this.author.id = +r['id'] || 0;
    });
  }

  ngOnInit() {
    if(this.author.id){
      this.authorService.getAuthorById(this.author.id)
      .subscribe(result =>{
         this.author = result;
      }, err => {
        if(err.status == 404)
        this.toasterService.errorToastr(
          'Author not found',
          'Error',
          {
            toastTimeout:5000,
            animate: 'slideFromTop',
            showCloseButton: false
          });
          this.router.navigate(['author']);
      });
    }
  }
  save(){
    //debugger;
    var result$ = (this.author.id) ? 
      this.authorService.updateAuthor(this.author) : 
      this.authorService.createAuthor(this.author);
    result$.subscribe(res=>{
      this.author = res;
      this.toasterService.successToastr(
        'Data was succesfully saved',
        'Success',
        {
          toastTimeout:5000,
          animate: 'slideFromTop',
          showCloseButton: false
        });
      this.router.navigate(['authors']);
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
    this.authorService.deleteAuthor(this.author.id)
      .subscribe( result => {
        this.toasterService.successToastr(
          'Author was deleted succesfully',
          'Success',
          {
            toastTimeout:5000,
            animate: 'slideFromTop',
            showCloseButton: false
          });
        this.router.navigate(['authors']);
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
