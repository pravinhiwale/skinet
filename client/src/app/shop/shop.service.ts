import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IPagination } from '../shared/models/pagination';
import { IBrand } from '../shared/models/brand';
import {IType} from '../shared/models/productType';
import {map} from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl  = 'http://localhost:5000/api/';

  constructor(private http: HttpClient ) {}

  // getProducts(brandId?: number, typeId?: number,sort?: string) {

/*
typeScript Classes can be used as classes themselves that we can create new instances of but we can also use them as types
so we don't need to create a new instance of this.
This is just going to be used as the type fully shopParams in this case.
*/
    getProducts(shopParams: ShopParams) {

  let params = new HttpParams();

  if (shopParams.brandId !== 0 ){
    params = params.append('brandId', shopParams.brandId.toString());
  }
  if (shopParams.typeId !== 0 ){
    params = params.append('typeId', shopParams.typeId.toString());
  }
  if (shopParams.search){
    params=params.append('search', shopParams.search);
  }
  // if(shopParams.sort)
  // {
  params = params.append('sort', shopParams.sort);
  params = params.append('pageIndex', shopParams.pageNumber.toString());
  params = params.append('pageIndex', shopParams.pageSize.toString());
  // }

  return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params}) // ;
  .pipe(
    map(response => response.body) ); // response.body is the pagination object

  /* we've done something different inside our shop service here.
  What we're doing is we're observing a response. And this is going to give us the http response instead of the body of the response which is what
   does automatically if we use this way of getting the data (without oberserve i mean),  because we're saying we're observing the response here
   We actually need to project this data into our actual response. we need to extract body out of this
   And what we could do to achieve this is remove the semicolon and make use of pipe and map the respoonse or rather manipulate the observable and to project it into IPagination object


  */
  }

  getBrands() {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  getTypes() {
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }

}
// This is an angular service

/*
ervices need to be decorated with the injectable decorator and in here we've got some configuration
about where this is provided in,  now Our app module (app.module.ts) is our roots module of our application and this is the module that's responsible for
bootstrapping and initializing our application.
Now angular services are considered as singletons and they're initialized when our application starts.
And this 'providedin' root means that it's actually provided in our app module but we don't need to
add it to the providers array in app.module.ts because it's already providedin and you can swap root for app.module.ts  here theoretically we don't need to
This is standard and this is initialized when our application starts.

So our services are singletons which means they're always available as long as our app is available

They're not like components where angular is going to initialize them and then destroy them as soon as we move away from the component.
This is always going to be available for it's an excellent place to hold datathat we need to share across the application
and it's also a very good place to inject our HTTP service and go and make our API calls to go and retrieve data and then we can consume the data from our service

*/
