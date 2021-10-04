import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { StorageKeyNames } from 'src/app/Constants/StorageKeyNames';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { OrderModel } from 'src/app/Models/OrderModel';
import { SeatOrderModel } from 'src/app/Models/SeatOrderModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { UserModel } from 'src/app/Models/UserModel';
import { OrderSearchRequest } from 'src/app/Requests/OrderSearchRequest';
import { UserRequest } from 'src/app/Requests/UserRequest';
import { AccountStorageService } from 'src/app/Services/AccountStorageService';
import { OrderService } from 'src/app/Services/OrderService';
import { SeatOrderService } from 'src/app/Services/SeatOrderService';
import { UserService } from 'src/app/Services/UserService';

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
}