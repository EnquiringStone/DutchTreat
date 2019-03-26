import * as _ from "lodash";

export class Order {
    orderId: string;
    orderDate: Date = new Date();
    orderNumber: string;
    items: Array<OrderItem> = new Array<OrderItem>();

    get subtotal(): number {
        return _.sum(_.map(this.items, i => i.unitPrice * i.quantity));
    }
}

export class OrderItem {
    id: string;
    quantity: number;
    unitPrice: number;
    productId: string;
    productCategory: string;
    productSize: string;
    productTitle: string;
    productArtist: string;
    productArtId: string;
}