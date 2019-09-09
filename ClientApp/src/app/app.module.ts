import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { AuthorListComponent } from './components/authors/author-list/author-list.component';
import { CategoryListComponent } from './components/categories/category-list/category-list.component';
import { BookListComponent } from './components/books/book-list/book-list.component';
import { AuthorsService } from './services/authors.service';
import { AuthorViewComponent } from './components/authors/author-view/author-view.component';
import { ToastrModule } from 'ng6-toastr-notifications';
import { PaginationComponent } from './components/pagination/pagination.component';
import { CategoryViewComponent } from './components/categories/category-view/category-view.component';
import { BookViewComponent } from './components/books/book-view/book-view.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import {AutocompleteLibModule} from 'angular-ng-autocomplete';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AuthorListComponent,
    AuthorViewComponent,
    BookListComponent,
    CategoryListComponent,
    PaginationComponent,
    CategoryViewComponent,
    BookViewComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    NgMultiSelectDropDownModule.forRoot(),
    AutocompleteLibModule,
    RouterModule.forRoot([
      { path: '', component: BookListComponent, pathMatch: 'full' },
      { path: 'authors', component: AuthorListComponent },
      { path: 'author/:id', component: AuthorViewComponent},
      { path: 'author', component: AuthorViewComponent},
      { path: 'categories', component: CategoryListComponent },
      { path: 'category/:id', component: CategoryViewComponent},
      { path: 'category', component: CategoryViewComponent},
      { path: 'books', component: BookListComponent },
      { path: 'book/:id', component: BookViewComponent},
      { path: 'book', component: BookViewComponent}
    ])
  ],
  providers: [AuthorsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
