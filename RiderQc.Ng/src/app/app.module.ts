import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { UserService } from './services/user.service';
import { RideService } from './services/ride.service';
import { CommentService } from './services/comment.service';
import { MessageService } from './services/message.service';

import { LoadingModule } from 'ngx-loading';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';
import { SocialLoginModule, AuthServiceConfig } from "angular4-social-login";
import { GoogleLoginProvider, FacebookLoginProvider } from "angular4-social-login";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RidesComponent } from './components/rides/rides.component';
import { RideDetailsComponent } from './components/rides/details/ride.details.component';
import { RideEditComponent } from './components/rides/edit/ride.edit.component';
import { TrajetComponent } from './components/trajet/trajet.component';
import { RegisterComponent } from './components/register/register.component';
import { DirectionsMapDirective } from './components/trajet/direction.directive';
import { LoginComponent } from './components/login/login.component';
import { UserComponent } from './components/user/user.component';
import { CommentComponent } from './components/comment/comment.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LogoffComponent } from './components/logoff/logoff.component';
import { NavbarLoginComponent } from './components/login/navbar-login/navbar-login.component';
import { MyridesComponent } from './components/rides/myrides/myrides.component';
import { SingleComponent } from './components/rides/single/single.component';
import { MessagingComponent } from './components/messaging/messaging.component';

let config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider("874199628639-s0dgl716v5lvj3qvac8n89clbdc66rk8.apps.googleusercontent.com")
  },
  {
    id: FacebookLoginProvider.PROVIDER_ID,
    provider: new FacebookLoginProvider("935318439942778")
  }
]);

export function provideConfig() {
  return config;
}

@NgModule({
  declarations: [
    AppComponent,
    RidesComponent,
    RideDetailsComponent,
    RideEditComponent,
    TrajetComponent,
    RegisterComponent,
    DirectionsMapDirective,
    LoginComponent,
    UserComponent,
    CommentComponent,
    NavbarComponent,
    LogoffComponent,
    NavbarLoginComponent,
    MyridesComponent,
    SingleComponent,
    MessagingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpModule,
    FormsModule,
    LoadingModule,
    ReactiveFormsModule,
    AgmCoreModule.forRoot({
      libraries: ["places"],
      apiKey: 'AIzaSyCrXlv7ZZWk5yoav8hM-afN6NvDOiKSpuM'
    }),
    SocialLoginModule
  ],
  providers: [
    UserService,
    RideService,
    {
      provide: AuthServiceConfig,
      useFactory: provideConfig
    },
    CommentService,
    MessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
