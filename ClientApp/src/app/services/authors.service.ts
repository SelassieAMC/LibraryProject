import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from "rxjs/operators"; 

@Injectable({
  providedIn: 'root'
})
export class AuthorsService {
  private readonly authorEndPoint = '/api/authors';

  constructor(private http: Http) { }
  
  getAuthors(filter){
   return this.http.get(this.authorEndPoint + '?' + this.queryString(filter))
      .pipe(map(res => res.json()));
  }

  getAuthorById(id){
    return this.http.get(this.authorEndPoint +'/getauthor/'+ id)
    .pipe(map(res => res.json()));
  }

  createAuthor(author){
    return this.http.post(this.authorEndPoint,author)
    .pipe(map(res => res.json()));
  }

  updateAuthor(author){
    return this.http.put(this.authorEndPoint+'/update/'+author.id, author)
    .pipe(map(res => res.json()));
  }

  deleteAuthor(id){
    return this.http.delete(this.authorEndPoint+'/remove/'+id)
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
