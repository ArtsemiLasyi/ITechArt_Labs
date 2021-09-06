import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminFilmAddComponent } from 'src/app/Components/admin/admin-film-add/admin-film-add.component';
import { AdminFilmInfoComponent } from 'src/app/Components/admin/admin-film-info/admin-film-info.component';
import { AdminFilmSearchComponent } from 'src/app/Components/admin/admin-film-search/admin-film-search.component';
import { AdminPanelComponent } from 'src/app/Components/admin/admin-panel/admin-panel.component';
import { AdminUserAddComponent } from 'src/app/Components/admin/admin-user-add/admin-user-add.component';
import { NotFoundComponent } from 'src/app/Components/main/notfound/notfound.component';
import { AccountNodeComponent } from 'src/app/Components/user/account/accountnode/accountnode.component';
import { SignInComponent } from 'src/app/Components/user/account/signin/signin.component';
import { SignUpComponent } from 'src/app/Components/user/account/signup/signup.component';
import { FilmInfoComponent } from 'src/app/Components/user/films/film-info/film-info.component';
import { FilmsListComponent } from 'src/app/Components/user/films/films-list/films-list.component';
import { FilmsNodeComponent } from 'src/app/Components/user/films/filmsnode/films-node.component';
import { UserInfoComponent } from 'src/app/Components/user/users/user-info/user-info.component';
import { AdminPanelGuard } from 'src/app/Guards/admin-panel.guard';
import { UserInfoGuard } from 'src/app/Guards/user-info.guard';

const accountRoutes : Routes = [
    { path: 'signin', component : SignInComponent },
    { path: 'signup', component : SignUpComponent }
];

const filmsRoutes : Routes = [
    { path: '', component : FilmsListComponent },
    { path: ':id', component : FilmInfoComponent }
];

const adminRoutes : Routes = [
    { path: 'users', component : AdminUserAddComponent },
    { path: 'users/current', component : UserInfoComponent },
    { path: 'films', component : AdminFilmAddComponent },
    { path: 'films/search', component : AdminFilmSearchComponent },
    { path: 'films/info/:id', component : AdminFilmInfoComponent },
    { path: '', component : UserInfoComponent }
];

const appRoutes: Routes = [
    { path: '', component : FilmsListComponent },
    {
        path: 'admin',
        component : AdminPanelComponent,
        canActivate : [AdminPanelGuard],
        children : adminRoutes 
    },
    { path: 'account', component : AccountNodeComponent, children : accountRoutes },
    { path: 'films', component : FilmsNodeComponent, children : filmsRoutes },
    { path: 'users/current', component : UserInfoComponent, canActivate : [UserInfoGuard] },
    { path: '**', component: NotFoundComponent }
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