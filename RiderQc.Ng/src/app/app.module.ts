import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RiderqcService } from './services/riderqc.service';
import { HttpModule } from '@angular/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RidesComponent } from './components/rides/rides.component';
import { TrajetComponent } from './components/trajet/trajet.component';

@NgModule({
  declarations: [
    AppComponent,
    RidesComponent,
    TrajetComponent
  ],
  imports: [
    BrowserModule,
      AppRoutingModule,
      HttpModule
  ],
  providers: [RiderqcService],
  bootstrap: [AppComponent]
})
export class AppModule { }
