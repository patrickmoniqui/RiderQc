import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Ride } from '../model/ride';
import { Level } from '../model/level';

//service
import { UserService } from '../services/user.service';

import { environment } from '../../environments/environment';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class RideService {

  baseUrl: string = environment.BaseUrl;

    constructor(public http:Http, public UserService:UserService) {}

    getRideById(id: number): Observable<Ride> {
      let ride$ = this.http
        .get(`${this.baseUrl}/ride/${id}`)
        .map(res => res.json());
      return ride$;
    }

    getRides() {
        return this.http.get(`${this.baseUrl}/ride/list`)
            .map(res => res.json());
    }

    getMyRides(username: string) {
      return this.http.get(`${this.baseUrl}/ride/myrides?username=` + username)
        .map(res => res.json());
    }
  
    details(id: number): Observable<Ride> {
        let ride$ = this.http
            .get(`${this.baseUrl}/ride/${id}`)
            .map(res => res.json());
        return ride$;
    }

    levelList() {
        let levels$ = this.http
            .get(`${this.baseUrl}/level/list`)
            .map(res => res.json());
        return levels$;
    }

    participate(rideId: number) {
      console.log("participating to ride: " + rideId);
      let _headers: Headers = this.UserService.getBearerAuthHeader();

      return this.http
        .get(`${this.baseUrl}/ride/${rideId}/participate`, { headers: _headers });
    }

    removeParticipate(rideId: number) {
      let _headers: Headers = this.UserService.getBearerAuthHeader();

      return this.http
        .delete(`${this.baseUrl}/ride/${rideId}/participate`, { headers: _headers });
    }

    private getHeaders() {
        let headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }
}
