import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { ShopModule } from './shop/shop.module';
import { HomeModule } from './home/home.module';
import {ErrorInterceptor} from './core/interceptors/error-interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    // ShopModule, no longer need here because we are doing lazy loading of shopModule
    HomeModule
  ],
  providers: [ {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}],
  // angular comes with ours own Interceptor anyways and what we're doing here is we're adding this to an array of
  // HTTPS interceptors even though we've only created one of our own.We actually want to add this to a list of HTTP interceptors
  // and what we only to specify is "multi" and we'll need to set this to true so that ours isn't the only HTTP interceptor in
  // that list it's
  bootstrap: [AppComponent]
})
export class AppModule { }


//