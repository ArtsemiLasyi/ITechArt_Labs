import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from 'src/app/Components/main/app/app.component';
import { FooterComponent } from 'src/app/Components/main/footer/footer.component';
import { HeaderComponent } from 'src/app/Components/main/header/header.component';
import { NotFoundComponent } from 'src/app/Components/main/notfound/notfound.component';
import { AccountNodeComponent } from 'src/app/Components/user/account/accountnode/accountnode.component';
import { SignInComponent } from 'src/app/Components/user/account/signin/signin.component';
import { SignUpComponent } from 'src/app/Components/user/account/signup/signup.component';
import { FilmInfoComponent } from 'src/app/Components/user/films/filminfo/filminfo.component';
import { FilmsListComponent } from 'src/app/Components/user/films/filmslist/filmslist.component';
import { FilmsNodeComponent } from 'src/app/Components/user/films/filmsnode/filmsnode.component';
import { UserInfoComponent } from 'src/app/Components/user/users/userinfo/userinfo.component';
import { SignInRequest } from 'src/app/Requests/SignInRequest';

const accountRoutes: Routes = [
  { path: 'signin', component: SignInComponent },
  { path: 'signup', component: SignUpComponent }
];

const filmsRoutes: Routes = [
  { path: '', component: FilmsListComponent },
  { path: ':id', component: FilmInfoComponent }
];

const appRoutes: Routes = [
  { path: '', component : FilmsListComponent },
  { path: 'account', component : AccountNodeComponent, children: accountRoutes },
  { path: 'users:id', component: UserInfoComponent },
  { path: 'films', component : FilmsNodeComponent, children: filmsRoutes },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    FilmsListComponent,
    AccountNodeComponent,
    FilmsNodeComponent,
    SignInComponent,
    SignUpComponent,
    UserInfoComponent,
    FilmInfoComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
