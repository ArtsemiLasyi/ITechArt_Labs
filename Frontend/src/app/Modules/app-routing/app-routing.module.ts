import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminCinemaAddComponent } from 'src/app/Components/admin/admin-cinema-add/admin-cinema-add.component';
import { AdminCinemaInfoComponent } from 'src/app/Components/admin/admin-cinema-info/admin-cinema-info.component';
import { AdminCinemaSearchComponent } from 'src/app/Components/admin/admin-cinema-search/admin-cinema-search.component';
import { AdminCinemaServiceInfoComponent } from 'src/app/Components/admin/admin-cinema-service-info/admin-cinema-service-info.component';
import { AdminCurrencyAddComponent } from 'src/app/Components/admin/admin-currency-add/admin-currency-add.component';
import { AdminCurrencyInfoComponent } from 'src/app/Components/admin/admin-currency-info/admin-currency-info.component';
import { AdminCurrencySearchComponent } from 'src/app/Components/admin/admin-currency-search/admin-currency-search.component';
import { AdminFilmAddComponent } from 'src/app/Components/admin/admin-film-add/admin-film-add.component';
import { AdminFilmInfoComponent } from 'src/app/Components/admin/admin-film-info/admin-film-info.component';
import { AdminFilmSearchComponent } from 'src/app/Components/admin/admin-film-search/admin-film-search.component';
import { AdminHallAddComponent } from 'src/app/Components/admin/admin-hall-add/admin-hall-add.component';
import { AdminHallInfoComponent } from 'src/app/Components/admin/admin-hall-info/admin-hall-info.component';
import { AdminHallSearchComponent } from 'src/app/Components/admin/admin-hall-search/admin-hall-search.component';
import { AdminPanelComponent } from 'src/app/Components/admin/admin-panel/admin-panel.component';
import { AdminServiceAddComponent } from 'src/app/Components/admin/admin-service-add/admin-service-add.component';
import { AdminServiceInfoComponent } from 'src/app/Components/admin/admin-service-info/admin-service-info.component';
import { AdminServiceSearchComponent } from 'src/app/Components/admin/admin-service-search/admin-service-search.component';
import { AdminUserAddComponent } from 'src/app/Components/admin/admin-user-add/admin-user-add.component';
import { NotFoundComponent } from 'src/app/Components/main/notfound/notfound.component';
import { AccountNodeComponent } from 'src/app/Components/user/account/accountnode/accountnode.component';
import { SignInComponent } from 'src/app/Components/user/account/signin/signin.component';
import { SignUpComponent } from 'src/app/Components/user/account/signup/signup.component';
import { CinemaInfoComponent } from 'src/app/Components/user/cinemas/cinema-info/cinema-info.component';
import { CinemasNodeComponent } from 'src/app/Components/user/cinemas/cinemas-node/cinemas-node.component';
import { FilmInfoComponent } from 'src/app/Components/user/films/film-info/film-info.component';
import { FilmsListComponent } from 'src/app/Components/user/films/films-list/films-list.component';
import { FilmsNodeComponent } from 'src/app/Components/user/films/films-node/films-node.component';
import { UserInfoComponent } from 'src/app/Components/user/users/user-info/user-info.component';
import { AdminPanelGuard } from 'src/app/Guards/admin-panel.guard';
import { UserInfoGuard } from 'src/app/Guards/user-info.guard';
import { CinemaSearchRequest } from 'src/app/Requests/CinemaSearchRequest';

const accountRoutes : Routes = [
    { path : 'signin', component : SignInComponent },
    { path : 'signup', component : SignUpComponent }
];

const filmsRoutes : Routes = [
    { path : '', component : FilmsListComponent },
    { path : ':id', component : FilmInfoComponent }
];

const cinemasRoutes : Routes = [
    { path : ':id', component : CinemaInfoComponent }
];

const adminRoutes : Routes = [
    { path : 'users', component : AdminUserAddComponent },
    { path : 'users/current', component : UserInfoComponent },
    { path : 'films', component : AdminFilmAddComponent },
    { path : 'films/search', component : AdminFilmSearchComponent },
    { path : 'films/info/:id', component : AdminFilmInfoComponent },
    { path : 'cinemas', component : AdminCinemaAddComponent },
    { path : 'cinemas/search', component : AdminCinemaSearchComponent },
    { path : 'cinemas/info/:id', component : AdminCinemaInfoComponent },
    { path : 'halls', component : AdminHallAddComponent },
    { path : 'halls/search', component : AdminHallSearchComponent },
    { path : 'halls/info/:id', component : AdminHallInfoComponent },
    { path : 'currencies', component : AdminCurrencyAddComponent },
    { path : 'currencies/search', component : AdminCurrencySearchComponent },
    { path : 'currencies/info/:id', component : AdminCurrencyInfoComponent },
    { path : 'services', component : AdminServiceAddComponent },
    { path : 'services/search', component : AdminServiceSearchComponent },
    { path : 'services/info/:id', component : AdminServiceInfoComponent },
    { path : 'cinemas/info/:cinemaId/services/:id', component : AdminCinemaServiceInfoComponent },
    { path : '', component : UserInfoComponent }
];

const appRoutes: Routes = [
    { path : '', component : FilmsListComponent },
    {
        path : 'admin',
        component : AdminPanelComponent,
        canActivate : [AdminPanelGuard],
        children : adminRoutes 
    },
    { path : 'account', component : AccountNodeComponent, children : accountRoutes },
    { path : 'films', component : FilmsNodeComponent, children : filmsRoutes },
    { path : 'cinemas', component : CinemasNodeComponent, children : cinemasRoutes},
    { path : 'users/current', component : UserInfoComponent, canActivate : [UserInfoGuard] },
    { path : '**', component: NotFoundComponent }
];

@NgModule({
  imports : [RouterModule.forRoot(appRoutes)],
  exports : [RouterModule],
  providers : [
    UserInfoGuard,
    AdminPanelGuard
],
})
export class AppRoutingModule { }