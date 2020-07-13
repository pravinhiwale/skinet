import { Component, OnInit, InjectableProvider } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {IProduct} from './models/product';
import { IPagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'Skinet';
  products: IProduct[];
  constructor(private http: HttpClient) {

  }
  ngOnInit(): void {
    this.http.get('http://localhost:5000/api/products?PageSize=50').subscribe((response: IPagination) => {
     this.products = response.data;
    }, error => { console.log(error); });
  }

}