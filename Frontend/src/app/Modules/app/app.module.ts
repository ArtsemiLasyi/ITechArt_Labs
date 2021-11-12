import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorHandler, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AdminFilmAddComponent } from 'src/app/Components/admin/admin-film-add/admin-film-add.component';
import { AdminFilmSearchComponent } from 'src/app/Components/admin/admin-film-search/admin-film-search.component';
import { AdminPanelComponent } from 'src/app/Components/admin/admin-panel/admin-panel.component';
import { AdminUserAddComponent } from 'src/app/Components/admin/admin-user-add/admin-user-add.component';
import { AppComponent } from 'src/app/Components/main/app/app.component';
import { FooterComponent } from 'src/app/Components/main/footer/footer.component';
import { HeaderComponent } from 'src/app/Components/main/header/header.component';
import { AccountNodeComponent } from 'src/app/Components/user/account/accountnode/accountnode.component';
import { SignInComponent } from 'src/app/Components/user/account/signin/signin.component';
import { SignUpComponent } from 'src/app/Components/user/account/signup/signup.component';
import { FilmInfoComponent } from 'src/app/Components/user/films/film-info/film-info.component';
import { FilmsListComponent } from 'src/app/Components/user/films/films-list/films-list.component';
import { FilmsNodeComponent } from 'src/app/Components/user/films/films-node/films-node.component';
import { UserInfoComponent } from 'src/app/Components/user/users/user-info/user-info.component';
import { AuthInterceptor } from 'src/app/Interceptors/AuthInterceptor';
import { FilmService } from 'src/app/Services/FilmService';
import { PageService } from 'src/app/Services/pageservice';
import { UserService } from 'src/app/Services/UserService';
import { AppRoutingModule } from '../app-routing/app-routing.module';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { AdminFilmInfoComponent } from 'src/app/Components/admin/admin-film-info/admin-film-info.component';
import { CityService } from 'src/app/Services/CityService';
import { GlobalErrorHandler } from 'src/app/ErrorHandlers/GlobalErrorHandler';
import { HttpErrorInterceptor } from 'src/app/Interceptors/HttpErrorInterceptor';
import { CinemaService } from 'src/app/Services/CinemaService';
import { HallService } from 'src/app/Services/HallService';
import { CinemaInfoComponent } from 'src/app/Components/user/cinemas/cinema-info/cinema-info.component';
import { HallsListComponent } from 'src/app/Components/user/halls/halls-list/halls-list.component';
import { StoreModule } from '@ngrx/store';
import { cityReducer } from 'src/app/Reducers/city.reducer';
import { CinemasNodeComponent } from 'src/app/Components/user/cinemas/cinemas-node/cinemas-node.component';
import { HallViewDialogComponent } from 'src/app/Components/user/halls/hall-view-dialog/hall-view-dialog.component';
import { MatDialog, MatDialogModule, MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';
import { AdminCinemaAddComponent } from 'src/app/Components/admin/admin-cinema-add/admin-cinema-add.component';
import { AdminCinemaSearchComponent } from 'src/app/Components/admin/admin-cinema-search/admin-cinema-search.component';
import { AdminCinemaInfoComponent } from 'src/app/Components/admin/admin-cinema-info/admin-cinema-info.component';
import { AdminHallAddComponent } from 'src/app/Components/admin/admin-hall-add/admin-hall-add.component';
import { AdminHallInfoComponent } from 'src/app/Components/admin/admin-hall-info/admin-hall-info.component';
import { AdminHallSearchComponent } from 'src/app/Components/admin/admin-hall-search/admin-hall-search.component';
import { AdminHallConstructorDialogComponent } from 'src/app/Components/admin/admin-hall-constructor-dialog/admin-hall-constructor-dialog.component';
import { StorageService } from 'src/app/Services/StorageService';
import { AccountStorageService } from 'src/app/Services/AccountStorageService';
import { CurrencyService } from 'src/app/Services/CurrencyService';
import { ServiceService } from 'src/app/Services/ServiceService';
import { CinemaServiceService } from 'src/app/Services/CinemaServiceService';
import { AdminCurrencyAddComponent } from 'src/app/Components/admin/admin-currency-add/admin-currency-add.component';
import { AdminCurrencyInfoComponent } from 'src/app/Components/admin/admin-currency-info/admin-currency-info.component';
import { AdminCurrencySearchComponent } from 'src/app/Components/admin/admin-currency-search/admin-currency-search.component';
import { AdminServiceAddComponent } from 'src/app/Components/admin/admin-service-add/admin-service-add.component';
import { AdminServiceInfoComponent } from 'src/app/Components/admin/admin-service-info/admin-service-info.component';
import { AdminServiceSearchComponent } from 'src/app/Components/admin/admin-service-search/admin-service-search.component';
import { AdminCinemaServiceAddComponent } from 'src/app/Components/admin/admin-cinema-service-add/admin-cinema-service-add.component';
import { AdminCinemaServiceListComponent } from 'src/app/Components/admin/admin-cinema-service-list/admin-cinema-service-list.component';
import { AdminCinemaServiceInfoComponent } from 'src/app/Components/admin/admin-cinema-service-info/admin-cinema-service-info.component';
import { AdminSeatTypeAddComponent } from 'src/app/Components/admin/admin-seat-type-add/admin-seat-type-add.component';
import { AdminSeatTypeInfoComponent } from 'src/app/Components/admin/admin-seat-type-info/admin-seat-type-info.component';
import { AdminSeatTypeSearchComponent } from 'src/app/Components/admin/admin-seat-type-search/admin-seat-type-search.component';
import { SeatTypeService } from 'src/app/Services/SeatTypeService';
import { SeatService } from 'src/app/Services/SeatService';
import { HallSizeService } from 'src/app/Services/HallSizeService';
import { DrawingService } from 'src/app/Services/DrawingService';
import { SessionService } from 'src/app/Services/SessionService';
import { OrderService } from 'src/app/Services/OrderService';
import { SeatOrderService } from 'src/app/Services/SeatOrderService';
import { OrdersListComponent } from 'src/app/Components/user/orders/orders-list/orders-list.component';
import { SeatTypePriceService } from 'src/app/Services/SeatTypePriceService';
import { SessionSeatService } from 'src/app/Services/SessionSeatService';
import { AdminSessionAddComponent } from 'src/app/Components/admin/admin-session-add/admin-session-add.component';
import { AdminSessionInfoComponent } from 'src/app/Components/admin/admin-session-info/admin-session-info.component';
import { AdminSessionSearchComponent } from 'src/app/Components/admin/admin-session-search/admin-session-search.component';
import { DateTimeService } from 'src/app/Services/DateTimeService';
import { SessionsListComponent } from 'src/app/Components/user/sessions/sessions-list/sessions-list.component';
import { MakeOrderDialogComponent } from 'src/app/Components/user/orders/make-order-dialog/make-order-dialog.component';

@NgModule({
    declarations : [
        AppComponent,
        FooterComponent,
        AdminUserAddComponent,
        HeaderComponent,
        FilmsListComponent,
        FilmInfoComponent,
        AdminFilmAddComponent,
        AdminFilmSearchComponent,
        AccountNodeComponent,
        FilmsNodeComponent,
        CinemasNodeComponent,
        SignInComponent,
        SignUpComponent,
        UserInfoComponent,
        OrdersListComponent,
        AdminPanelComponent,
        MakeOrderDialogComponent,
        AdminFilmInfoComponent,
        CinemaInfoComponent,
        HallsListComponent,
        SessionsListComponent,
        HallViewDialogComponent,
        AdminCinemaAddComponent,
        AdminCinemaSearchComponent,
        AdminCinemaInfoComponent,
        AdminHallAddComponent,
        AdminHallInfoComponent,
        AdminHallSearchComponent,
        AdminHallConstructorDialogComponent,
        AdminCurrencyAddComponent,
        AdminCurrencyInfoComponent,
        AdminCurrencySearchComponent,
        AdminServiceAddComponent,
        AdminServiceInfoComponent,
        AdminServiceSearchComponent,
        AdminCinemaServiceAddComponent,
        AdminCinemaServiceListComponent,
        AdminCinemaServiceInfoComponent,
        AdminSeatTypeAddComponent,
        AdminSeatTypeInfoComponent,
        AdminSeatTypeSearchComponent,
        AdminSessionAddComponent,
        AdminSessionInfoComponent,
        AdminSessionSearchComponent
    ],
    entryComponents : [
        HallViewDialogComponent
    ],
    imports : [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        AppRoutingModule,
        ScrollingModule,
        MatDialogModule,
        BrowserAnimationsModule,
        StoreModule.forRoot({ city: cityReducer })
    ],
    providers : [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: HttpErrorInterceptor, 
            multi: true
        },
        GlobalErrorHandler,
        UserService,
        FilmService,
        PageService,
        CityService,
        CinemaService,
        HallService,
        CurrencyService,
        ServiceService,
        SessionService,
        OrderService,
        SessionSeatService,
        SeatOrderService,
        SeatService,
        HallSizeService,
        DrawingService,
        CinemaServiceService,
        SeatTypePriceService,
        StorageService,
        SeatTypeService,
        DateTimeService,
        AccountStorageService,
        {
            provide: ErrorHandler,
            useClass: GlobalErrorHandler,
        },
        {
            provide: MAT_DIALOG_DEFAULT_OPTIONS,
            useValue: {hasBackdrop: true}
        }
    ],
    bootstrap : [AppComponent]
})
export class AppModule { }
