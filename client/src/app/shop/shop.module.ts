import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';
//import { RouterModule } from '@angular/router';
import { ShopRoutingModule } from './shop-routing.module';



@NgModule({
  declarations: [ShopComponent, ProductItemComponent, ProductDetailsComponent],
  imports: [
    CommonModule,
    SharedModule,
    //RouterModule
    ShopRoutingModule
  ]
  //, exports: [ShopComponent]  //need to do this so as to import into app module
//after doing lazy loading we no longer need to export this cause app module is no longer responsible for loading this particular component
})
export class ShopModule { }
