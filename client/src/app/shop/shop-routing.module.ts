import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ShopComponent } from './shop.component';
import { ProductDetailsComponent } from './product-details/product-details.component';

const routes: Routes =[
  {path: '', component: ShopComponent},
  {path: ':id', component: ProductDetailsComponent}
];

@NgModule({
  declarations: [],
  imports: [
    //CommonModule
    RouterModule.forChild(routes)
    //this forChild means that these routes are not available in our app module 
    //and are only going to be available in our shop module and for this for child will pass in our routes that we have here
  ],
  exports: [RouterModule]
})
export class ShopRoutingModule { }
