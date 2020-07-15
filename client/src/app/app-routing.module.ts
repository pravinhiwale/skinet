import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ShopComponent } from './shop/shop.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';


const routes: Routes = [
  {path: '', component: HomeComponent},
  //{path: 'shop', component: ShopComponent},
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule) },
  //{path: 'shop/:id', component: ProductDetailsComponent},
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

/*
all of these routes are effectively loaded when we first start our application and also if we take a
look at our app module then all of our modules are declared there  and are loaded when our app module loads because we're
importing these and this is okay.
But what we can do is tell angular not to load everything straight away.

And if a user just browses to the home page and then disappears somewhere else ,do we really want to
load up the shop module at the same time. Well of course not.
so we need to do lazy loading to prevent that

shop module is only going to be activated and loaded when we access the shop path.



*/
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  /*
  we already know that when we use forroot this means that this is going to be added to our root module which is our app module 
  and that's where these routes are contained.
  */
  exports: [RouterModule]
})
export class AppRoutingModule { }
