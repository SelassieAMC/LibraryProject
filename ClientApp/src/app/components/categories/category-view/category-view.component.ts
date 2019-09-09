import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoriesService } from 'src/app/services/categories.service';
import { ToastrManager } from 'ng6-toastr-notifications';

@Component({
  selector: 'app-category-view',
  templateUrl: './category-view.component.html',
  styleUrls: ['./category-view.component.css']
})
export class CategoryViewComponent implements OnInit {
  category: any={};

  constructor(private route: ActivatedRoute,
    private router: Router,
    private categoryService: CategoriesService,
    public toasterService : ToastrManager) {
    this.route.params.subscribe(r => {
      this.category.id = +r['id'] || 0;
    });
  }

  ngOnInit() {
    //console.log(this.category);
    if(this.category.id){
      this.categoryService.getCategoryById(this.category.id)
      .subscribe(result =>{
         this.category = result;
      }, err => {
        if(err.status == 404)
        this.toasterService.errorToastr(
          'Category not found',
          'Error',
          {
            toastTimeout:5000,
            animate: 'slideFromTop',
            showCloseButton: false
          });
          this.router.navigate(['category']);
      });
    }
  }
  save(){
    //debugger;
    var result$ = (this.category.id) ? 
      this.categoryService.updateCategory(this.category) : 
      this.categoryService.createCategory(this.category);
    result$.subscribe(res=>{
      this.category = res;
      this.toasterService.successToastr(
        'Data was succesfully saved',
        'Success',
        {
          toastTimeout:5000,
          animate: 'slideFromTop',
          showCloseButton: false
        });
      this.router.navigate(['categories']);
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
    this.categoryService.deleteCategory(this.category.id)
      .subscribe( result => {
        this.toasterService.successToastr(
          'Author was deleted succesfully',
          'Success',
          {
            toastTimeout:5000,
            animate: 'slideFromTop',
            showCloseButton: false
          });
        this.router.navigate(['categories']);
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