import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RiderqcService } from './services/riderqc.service';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RidesComponent } from './components/rides/rides.component';
import { RideDetailsComponent } from './components/rides/details/ride.details.component';
import { RideEditComponent } from './components/rides/edit/ride.edit.component';
import { TrajetComponent } from './components/trajet/trajet.component';
import { RegisterComponent } from './components/register/register.component';
import { DirectionsMapDirective } from './components/trajet/direction.directive';
import { LoginComponent } from './components/login/login.component';

@NgModule({
    declarations: [
        AppComponent,
        RidesComponent,
        RideDetailsComponent,
        RideEditComponent,
        TrajetComponent,
        RegisterComponent,
        DirectionsMapDirective,
        LoginComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        AgmCoreModule.forRoot({
            libraries: ["places"],
            apiKey: 'AIzaSyCrXlv7ZZWk5yoav8hM-afN6NvDOiKSpuM'
        })
    ],
    providers: [RiderqcService],
    bootstrap: [AppComponent]
})
export class AppModule { }
