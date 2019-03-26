import { Component, OnInit } from "@angular/core";
import { DataService } from '../shared/data.service';
import { Product } from '../shared/product';

@Component({
    selector: "product-list",
    templateUrl: "product.list.component.html",
    styleUrls: [ "product.list.component.css" ]
})

export class ProductList implements OnInit {

    constructor(private data: DataService) {

    }

    public products: Product[] = [];

    ngOnInit(): void {
        this.data.loadProducts().subscribe(success => {
            if (success) {
                this.products = this.data.products;
            }
        });
        
    }

    addProduct(product: Product) {
        this.data.AddToOrder(product);
    }
}