import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TrajetComponent } from './components/trajet/trajet.component';
import { RidesComponent } from './components/rides/rides.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
    { path: '', redirectTo: 'rides', pathMatch: 'full' },
    { path: 'rides', component: RidesComponent },
    { path: 'trajet', component: TrajetComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
