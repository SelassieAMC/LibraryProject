import { Component, OnInit } from '@angular/core';
import { CategoriesService } from 'src/app/services/categories.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {

  constructor(private  categoryService: CategoriesService,private router: Router) { }
  private readonly PAGE_SIZE = 0;
  query: any ={
    pageSize : this.PAGE_SIZE
  };
  queryResult: any = {};
  ngOnInit() {
    this.getCategories();
  }

  getCategories(){
    this.categoryService.getCategories(this.query)
    .subscribe(result => {
      this.queryResult = result;
    });
  }

}
