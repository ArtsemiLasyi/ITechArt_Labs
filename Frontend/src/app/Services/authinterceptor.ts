import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {

      const token = localStorage.getItem("token");

      if (token) {
        const cloned = request.clone({
          headers: request.headers.set("Authorization",
            "Bearer " + token)
          });
          return next.handle(cloned);
        }
        else {
          return next.handle(request);
      }
  }
}