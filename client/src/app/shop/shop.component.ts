import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IType } from '../shared/models/productType';
import { IBrand } from '../shared/models/brand';
import { ShopParams } from '../shared/models/shopParams';


@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
@ViewChild('search', {static: true}  ) searchTerm: ElementRef;
//we want to access 'search' from the template
/*
In Angular v9 default value of static is false, we want to make it to true, cause the search field is always visible in the componenet its not hidden or visible based on conditions
our input property  is standalone it's just part of our template and it's always going to be available we've not put any conditions here to decide whether or not to show it then we can set the static property equal to true.

*/
products: IProduct[];
brands: IBrand[];
types: IType[];
totalCount = 0;
// brandIdSelected = 0;
// typeIdSelected = 0;
// sortSelected = 'name';
//moved above to a model class

shopParams = new ShopParams();

sortOptions =[
  {name: 'Alphabetical', value: 'name'},
  {name: 'Price:Low to high', value: 'priceAsc'},
  {name: 'Price: High to Low', value: 'priceDesc' }
];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
    // this.shopService.getProducts().subscribe( response =>
    //   {this.products = response.data; }, error => { console.log(error); } );
  }
  
  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe( response =>
      {this.products = response.data ;
       this.shopParams.pageNumber = response.pageIndex;
       this.shopParams.pageSize= response.pageSize;
       this.totalCount = response.count;
      }, error => { console.log(error); } );
  }
  
  getBrands(){
    this.shopService.getBrands().subscribe(response =>
      {this.brands = [{id: 0, name: 'All'}, ...response]; }, error => {console.log(error); });
  }

  getTypes(){
    this.shopService.getTypes().subscribe(response => 
      {this.types = [{id: 0, name : 'All'}, ...response]  ; }, error => {console.log(error); });
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();

  }
  onTypeSelected(typeId: number){
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(sort: string)
  {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any){
   // this.shopParams.pageNumber = event.page;
    //we changed this because well we're not actually getting our page number from the event.page
   // anymore we're still getting it from an event but all we need inside here now is just the event itself which is going to be our page number because that's being supplied by the child component
  if (this.shopParams.pageNumber !== event){
   this.shopParams.pageNumber = event;
   this.getProducts();
  }
}

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.getProducts();
  }
  
  onReset() {
this.searchTerm.nativeElement.value = '';
this.shopParams = new ShopParams();
this.getProducts();
  }
}
