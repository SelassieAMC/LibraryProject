import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { AuthorListComponent } from './components/author-list/author-list.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { BookListComponent } from './components/book-list/book-list.component';
import { AuthorsService } from './services/authors.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AuthorListComponent,
    BookListComponent,
    CategoryListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: BookListComponent, pathMatch: 'full' },
      { path: 'books', component: BookListComponent },
      { path: 'authors', component: AuthorListComponent },
      { path: 'categories', component: CategoryListComponent }
    ])
  ],
  providers: [AuthorsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
