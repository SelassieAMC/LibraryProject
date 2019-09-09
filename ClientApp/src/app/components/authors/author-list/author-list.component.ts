import { Component, OnInit } from '@angular/core';
import { AuthorsService } from 'src/app/services/authors.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-author-list',
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.css']
})
export class AuthorListComponent implements OnInit {
  private readonly PAGE_SIZE = 0;
  query: any ={
    pageSize : this.PAGE_SIZE
  };
  queryResult: any = {};

  constructor(private  authorsService: AuthorsService,private router: Router) {}

  ngOnInit() {
    //debugger;
    this.getAuthors();
  }

  getAuthors(){
    this.authorsService.getAuthors(this.query)
    .subscribe(result => {
      this.queryResult = result;
    });
  }

  createAuthor(){
    this.router.navigate(['author']);
  }

}
