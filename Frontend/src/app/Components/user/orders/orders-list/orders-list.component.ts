import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderModel } from 'src/app/Models/OrderModel';
import { SeatOrderModel } from 'src/app/Models/SeatOrderModel';
import { OrderSearchRequest } from 'src/app/Requests/OrderSearchRequest';
import { OrderService } from 'src/app/Services/OrderService';
import { SeatOrderService } from 'src/app/Services/SeatOrderService';

@Component({
    selector : 'orders-orders-list',
    templateUrl : './orders-list.component.html',
})
export class OrdersListComponent implements OnInit {

    constructor(
        private orderService : OrderService,
        private seatOrderService : SeatOrderService
    ) { }

    orders : Observable<OrderModel[]> | undefined;
    seatOrders : Observable<SeatOrderModel[]>[] = [];
    pastOrders : boolean = false;

    ngOnInit() {
        this.getOrders();
    }

    getOrders() {
        let request = new OrderSearchRequest();
        request.pastOrders = this.pastOrders;
        this.orders = this.orderService.getOrders(request);
    }

    getOrderInfo(orderId : number) {
        this.seatOrders[orderId] = this.seatOrderService.getSeatOrders(orderId);
    }

    getTime(startDateTime : Date) {
        return new Date(startDateTime).toLocaleTimeString();
    }

    getDate(startDateTime : Date) {
        return new Date(startDateTime).toLocaleDateString();
    }
}