import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';


@NgModule({
  declarations: [PagingHeaderComponent, PagerComponent],
  imports: [
    CommonModule,
    PaginationModule.forRoot() /* 
    we need to add forRoot() here as the pagination module has its own provider's array and those providers need to be injected into our root module at startup.
    So this is effectively acting as a singleton anyway.
    And if we take off forRoot then it won't load with its providers and we'll have errors.
    */
  ],
  exports: [PaginationModule, PagingHeaderComponent,
    PagerComponent]
})
export class SharedModule { }

/*
anything that we might need in other parts of our application that doesn't belong specifically to a feature and isn't necessarily a singleton is just going to go 
inside our shared module and our ngx components and modules are good candidates to go into this one.
*/

/*
we are going to import and export pagination module
 because we're going to be importing our shared module into any feature modules that need the functionality that we're providing inside this shared module
*/

