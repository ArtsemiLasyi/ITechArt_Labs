import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { OrderModel } from "../Models/OrderModel";
import { PriceModel } from "../Models/PriceModel";
import { OrderRequest } from "../Requests/OrderRequest";
import { OrderSearchRequest } from "../Requests/OrderSearchRequest";

@Injectable()
export class OrderService {
    
    constructor(private http : HttpClient){ }

    addOrder(order : OrderRequest) {         
        return this.http.post(ApiUrls.Orders, order); 
    }

    editOrder(id : number, order : OrderRequest) {         
        return this.http.put(
            `${ApiUrls.Orders}/${id}`,
            order
        ); 
    }

    getOrders(request : OrderSearchRequest) {
        return this.http.get<OrderModel[]>(
            `${ApiUrls.Orders}`, {
                params : { pastOrders : request.pastOrders} 
            }
        );
    }

    getOrder(id : number) {
        return this.http.get<OrderModel>(`${ApiUrls.Orders}/${id}`);
    }

    calculateSum(order : OrderRequest) {
        return this.http.post<PriceModel>(`${ApiUrls.Orders}/sum`, order);
    }
}