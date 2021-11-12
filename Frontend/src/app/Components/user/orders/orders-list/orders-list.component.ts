import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderModel } from 'src/app/Models/OrderModel';
import { SeatOrderModel } from 'src/app/Models/SeatOrderModel';
import { OrderSearchRequest } from 'src/app/Requests/OrderSearchRequest';
import { DateTimeService } from 'src/app/Services/DateTimeService';
import { OrderService } from 'src/app/Services/OrderService';
import { SeatOrderService } from 'src/app/Services/SeatOrderService';

@Component({
    selector : 'orders-orders-list',
    templateUrl : './orders-list.component.html',
})
export class OrdersListComponent implements OnInit {

    constructor(
        private orderService : OrderService,
        private seatOrderService : SeatOrderService,
        private dateTimeService : DateTimeService
    ) { }

    orders : Observable<OrderModel[]> | undefined;
    seatOrders : Observable<SeatOrderModel[]>[] = [];
    pastOrders : boolean = false;

    ordersNumber : number = 0;

    ngOnInit() {
        this.getOrders();
    }

    getOrders() {
        let request = new OrderSearchRequest();
        request.pastOrders = this.pastOrders;
        this.orders = this.orderService.getOrders(request);
        this.orders.subscribe(
            (orders : OrderModel[]) => {
                this.ordersNumber = orders.length;
            }
        )
    }

    getOrderInfo(orderId : number) {
        this.seatOrders[orderId] = this.seatOrderService.getSeatOrders(orderId);
    }

    getTime(startDateTime : Date) {
        return this.dateTimeService.getTime(startDateTime);
    }

    getDate(startDateTime : Date) {
        return this.dateTimeService.getDate(startDateTime);
    }
}