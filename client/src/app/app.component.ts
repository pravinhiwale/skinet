import { Component, OnInit} from '@angular/core';
//import {IProduct} from './shared/models/product';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'Skinet';
  
  //products: IProduct[];
  constructor() {
  //constructor(private http: HttpClient) {
//Now this is not best practice and we don't really want to inject HTTP service into our components directly
//but it's much better to use is an angular service 
//
  }
  ngOnInit(): void {
    // this.http.get('http://localhost:5000/api/products?PageSize=50').subscribe((response: IPagination) => {
    //  this.products = response.data;
    // }, error => { console.log(error); });
  }



}
