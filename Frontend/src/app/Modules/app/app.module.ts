import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import * as Config from '../../../../config.json';

import { AppComponent } from '../../Components/app/app.component';
import { FooterComponent } from 'src/app/Components/footer/footer.component';
import { HeaderComponent } from 'src/app/Components/header/header.component';
import { FilmsListComponent } from 'src/app/Components/filmslist/filmslist.component';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    FilmsListComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
