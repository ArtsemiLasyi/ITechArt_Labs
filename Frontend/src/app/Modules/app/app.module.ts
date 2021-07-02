import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AdminPanelComponent } from 'src/app/Components/admin/admin-panel/admin-panel.component';
import { AdminUserAddComponent } from 'src/app/Components/admin/admin-user-add/admin-user-add.component';
import { AppComponent } from 'src/app/Components/main/app/app.component';
import { FooterComponent } from 'src/app/Components/main/footer/footer.component';
import { HeaderComponent } from 'src/app/Components/main/header/header.component';
import { AccountNodeComponent } from 'src/app/Components/user/account/accountnode/accountnode.component';
import { SignInComponent } from 'src/app/Components/user/account/signin/signin.component';
import { SignUpComponent } from 'src/app/Components/user/account/signup/signup.component';
import { FilmsListComponent } from 'src/app/Components/user/films/films-list/films-list.component';
import { FilmsNodeComponent } from 'src/app/Components/user/films/filmsnode/films-node.component';
import { UserInfoComponent } from 'src/app/Components/user/users/user-info/user-info.component';
import { AdminPanelGuard } from 'src/app/Guards/admin-panel.guard';
import { UserInfoGuard } from 'src/app/Guards/user-info.guard';
import { AuthInterceptor } from 'src/app/Services/AuthInterceptor';
import { UserService } from 'src/app/Services/UserService';
import { AppRoutingModule } from '../app-routing/app-routing.module';

@NgModule({
    declarations : [
        AppComponent,
        FooterComponent,
        AdminUserAddComponent,
        HeaderComponent,
        FilmsListComponent,
        AccountNodeComponent,
        FilmsNodeComponent,
        SignInComponent,
        SignUpComponent,
        UserInfoComponent,
        AdminPanelComponent
    ],
    imports : [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        AppRoutingModule
    ],
    providers : [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
        },
        UserInfoGuard,
        AdminPanelGuard,
        UserService
    ],
    bootstrap : [AppComponent]
})
export class AppModule { }
