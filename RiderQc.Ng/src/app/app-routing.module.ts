import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TrajetComponent } from './components/trajet/trajet.component';
//import { UserComponent } from './components/user/user.component';
import { RidesComponent } from './components/rides/rides.component';

const routes: Routes = [
    { path: '', redirectTo: 'rides', pathMatch: 'full' },
    { path: 'rides', component: RidesComponent },
    { path: 'trajet', component: TrajetComponent }//,
    //{ path: 'user', component: UserComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
