import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { AdminFilmAddComponent } from 'src/app/Components/admin/admin-film-add/admin-film-add.component';
import { AdminFilmSearchComponent } from 'src/app/Components/admin/admin-film-seach/admin-film-search.component';
import { AdminPanelComponent } from 'src/app/Components/admin/admin-panel/admin-panel.component';
import { AdminUserAddComponent } from 'src/app/Components/admin/admin-user-add/admin-user-add.component';
import { AppComponent } from 'src/app/Components/main/app/app.component';
import { FooterComponent } from 'src/app/Components/main/footer/footer.component';
import { HeaderComponent } from 'src/app/Components/main/header/header.component';
import { NotFoundComponent } from 'src/app/Components/main/notfound/notfound.component';
import { AccountNodeComponent } from 'src/app/Components/user/account/accountnode/accountnode.component';
import { SignInComponent } from 'src/app/Components/user/account/signin/signin.component';
import { SignUpComponent } from 'src/app/Components/user/account/signup/signup.component';
import { FilmInfoComponent } from 'src/app/Components/user/films/filminfo/film-info.component';
import { FilmsListComponent } from 'src/app/Components/user/films/films-list/films-list.component';
import { FilmsNodeComponent } from 'src/app/Components/user/films/filmsnode/films-node.component';
import { UserInfoComponent } from 'src/app/Components/user/users/user-info/user-info.component';
import { SignInRequest } from 'src/app/Requests/SignInRequest';
import { AuthInterceptor } from 'src/app/Services/AuthInterceptor';

const accountRoutes : Routes = [
    { path: 'signin', component: SignInComponent },
    { path: 'signup', component: SignUpComponent }
];

const filmsRoutes : Routes = [
    { path: '', component: FilmsListComponent },
    { path: ':id', component: FilmInfoComponent }
];

const adminRoutes : Routes = [
    { path: 'users', component: AdminUserAddComponent },
    { path: 'users/current', component: UserInfoComponent },
    { path: 'films', component: AdminFilmAddComponent },
    { path: 'films/search', component: AdminFilmSearchComponent }
];

const appRoutes: Routes = [
    { path: '', component : FilmsListComponent },
    { path: 'admin', component : AdminPanelComponent, children: adminRoutes },
    { path: 'account', component : AccountNodeComponent, children: accountRoutes },
    { path: 'users/current', component: UserInfoComponent },
    { path: 'films', component : FilmsNodeComponent, children: filmsRoutes },
    { path: '**', component: NotFoundComponent }
];

@NgModule({
    declarations: [
        AppComponent,
        FooterComponent,
        AdminFilmAddComponent,
        AdminUserAddComponent,
        AdminFilmSearchComponent,
        HeaderComponent,
        FilmsListComponent,
        AccountNodeComponent,
        FilmsNodeComponent,
        SignInComponent,
        SignUpComponent,
        UserInfoComponent,
        FilmInfoComponent,
        AdminPanelComponent
    ],
    imports: [
        BrowserModule,
        RouterModule.forRoot(appRoutes),
        FormsModule,
        HttpClientModule
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
