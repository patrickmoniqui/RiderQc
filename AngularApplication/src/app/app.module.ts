import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AgmCoreModule } from '@agm/core';

import { DirectionsMapDirective } from './direction'

@NgModule({
  declarations: [
      AppComponent,
      DirectionsMapDirective,
  ],
  imports: [
      BrowserModule,
      FormsModule,
      HttpModule,
      AgmCoreModule.forRoot({
          apiKey: 'AIzaSyCrXlv7ZZWk5yoav8hM-afN6NvDOiKSpuM'
      })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
