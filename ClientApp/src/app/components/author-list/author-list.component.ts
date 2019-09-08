import { Component, OnInit } from '@angular/core';
import { AuthorsService } from 'src/app/services/authors.service';

@Component({
  selector: 'app-author-list',
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.css']
})
export class AuthorListComponent implements OnInit {
  private readonly PAGE_SIZE = 4;
  query: any ={
    pageSize : this.PAGE_SIZE
  };
  queryResult: any = {};
  columns = [
    {"title":"Name","key":"name"},
    {"title":"Last Name","key":"lastName"},
    {"title":"BirthDay","key":"birthDay"},
    {"title":"Details","key":""}]

  constructor(private  authorsService: AuthorsService) {}

  ngOnInit() {
    this.authorsService.getAuthors(this.query)
    .subscribe(result => {
      this.queryResult = result;
      console.log(result);
    });
  }

}
