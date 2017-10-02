import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { RidesComponent } from './components/rides/rides.component';
import { RegisterComponent } from './components/register/register.component';
import { TrajetComponent } from './components/trajet/trajet.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'rides',
        pathMatch: 'full'
    },
    {
        path: 'rides',
        component: RidesComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    }
];


@NgModule({
    imports: [RouterModule.forRoot(routes, { enableTracing: true })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
