import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RiderqcService } from './services/riderqc.service';
import { HttpModule } from '@angular/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RidesComponent } from './components/rides/rides.component';
import { UserComponent } from './components/user/user.component';

@NgModule({
  declarations: [
    AppComponent,
	  RidesComponent,
    UserComponent
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
