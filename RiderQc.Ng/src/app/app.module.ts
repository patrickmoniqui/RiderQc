import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RiderqcService } from './services/riderqc.service';
import { HttpModule } from '@angular/http';
import { Routes } from "@angular/router";
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RidesComponent } from './components/rides/rides.component';
import { RegisterComponent } from './components/register/register.component';
import { TrajetComponent } from './components/trajet/trajet.component';
import { UserComponent } from './components/user/user.component';

@NgModule({
    declarations: [
		AppComponent,
		RidesComponent,
		TrajetComponent,
		RegisterComponent,
		UserComponent

    ],
    imports: [
        RouterModule,
        ReactiveFormsModule,
        BrowserModule,
        HttpModule,
        AppRoutingModule
	],
	  providers: [RiderqcService],
    bootstrap: [AppComponent]
})
export class AppModule { }
