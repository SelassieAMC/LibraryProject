import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  private readonly bookEndPoint = '/api/books';
  constructor(private http: Http) { }
  
  getBooks(filter){
   return this.http.get(this.bookEndPoint + '?' + this.queryString(filter))
      .pipe(map(res => res.json()));
  }

  getBookById(id){
    return this.http.get(this.bookEndPoint +'/getbook/'+ id)
    .pipe(map(res => res.json()));
  }

  createBook(book){
    return this.http.post(this.bookEndPoint,book)
    .pipe(map(res => res.json()));
  }

  updateBook(book){
    return this.http.put(this.bookEndPoint+'/update/'+book.id, book)
    .pipe(map(res => res.json()));
  }

  deleteBook(id){
    return this.http.delete(this.bookEndPoint+'/remove/'+id);
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
