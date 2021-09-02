import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
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
import { FilmsNodeComponent } from 'src/app/Components/user/films/filmsnode/films-node.component';
import { UserInfoComponent } from 'src/app/Components/user/users/user-info/user-info.component';
import { AdminPanelGuard } from 'src/app/Guards/admin-panel.guard';
import { UserInfoGuard } from 'src/app/Guards/user-info.guard';
import { AuthInterceptor } from 'src/app/Services/AuthInterceptor';
import { FilmService } from 'src/app/Services/filmservice';
import { PageService } from 'src/app/Services/pageservice';
import { UserService } from 'src/app/Services/UserService';
import { AppRoutingModule } from '../app-routing/app-routing.module';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { AdminFilmInfoComponent } from 'src/app/Components/admin/admin-film-info/admin-film-info.component';
import { CityService } from 'src/app/Services/cityservice';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

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
        SignInComponent,
        SignUpComponent,
        UserInfoComponent,
        AdminPanelComponent,
        AdminFilmInfoComponent
    ],
    imports : [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        AppRoutingModule,
        ScrollingModule,
        NgbModule
    ],
    providers : [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
        },
        UserService,
        FilmService,
        PageService,
        CityService
    ],
    bootstrap : [AppComponent]
})
export class AppModule { }
