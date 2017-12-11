import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TrajetComponent } from './components/trajet/trajet.component';
import { RidesComponent } from './components/rides/rides.component';
import { RideDetailsComponent } from './components/rides/details/ride.details.component';
import { RideEditComponent } from './components/rides/edit/ride.edit.component';
import { MyridesComponent } from './components/rides/myrides/myrides.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { LogoffComponent } from './components/logoff/logoff.component';
import { MessagingComponent } from './components/messaging/messaging.component';

const routes: Routes = [
    { path: '', redirectTo: 'rides', pathMatch: 'full' },
    { path: 'rides', component: RidesComponent },
    { path: 'rides/:id', component: RideDetailsComponent },
    { path: 'rides/edit/:id', component: RideEditComponent },
    { path: 'myrides', component: MyridesComponent },
    { path: 'trajet', component: TrajetComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'logoff', component: LogoffComponent },
    { path: 'messaging', component: MessagingComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
