import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  private readonly categoryEndPoint = '/api/categories';

  constructor(private http: Http) { }
  
  getCategories(filter){
   return this.http.get(this.categoryEndPoint + '?' + this.queryString(filter))
      .pipe(map(res => res.json()));
  }

  getCategoryById(id){
    return this.http.get(this.categoryEndPoint +'/getcategory/'+ id)
    .pipe(map(res => res.json()));
  }

  createCategory(category){
    return this.http.post(this.categoryEndPoint,category)
    .pipe(map(res => res.json()));
  }

  updateCategory(category){
    return this.http.put(this.categoryEndPoint+'/update/'+category.id, category)
    .pipe(map(res => res.json()));
  }

  deleteCategory(id){
    return this.http.delete(this.categoryEndPoint+'/remove/'+id)
    .pipe(map(res => res.json()));
  }

  queryString(obj){
    var parts = [];
    for (var property in obj){
      var value = obj[property];
      if(value != null && value!= undefined){
        parts.push(encodeURIComponent(property)+'='+encodeURIComponent(value));
      }
    }
    return parts.join('&');
  }
}
