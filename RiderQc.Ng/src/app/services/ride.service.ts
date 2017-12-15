import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { environment } from '../../environments/environment';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

//Models
import { Ride } from '../model/ride';
import { Level } from '../model/level';

//Service
import { UserService } from '../services/user.service';

@Injectable()
export class RideService {

  baseUrl: string = environment.BaseUrl;

  constructor(public http: Http, public userService: UserService) { }

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

    add(ride: any): Observable<number> {
        var rideId = this.http.post(this.baseUrl + '/ride', ride, { headers: this.userService.getBearerAuthHeader() })
            .map(x => x.json());
        return rideId;
    }

    update(ride: any): Observable<number> {
        var rideId = this.http.put(this.baseUrl + '/ride/' + ride.rideid, ride, { headers: this.userService.getBearerAuthHeader() })
            .map(x => x.json());
        return rideId;
    }

    levelList() {
        let levels$ = this.http
            .get(`${this.baseUrl}/level/list`)
            .map(res => res.json());
        return levels$;
    }

    participate(rideId: number) {
      console.log("participating to ride: " + rideId);
      let _headers: Headers = this.userService.getBearerAuthHeader();

      return this.http
        .get(`${this.baseUrl}/ride/${rideId}/participate`, { headers: _headers });
    }

    removeParticipate(rideId: number) {
      let _headers: Headers = this.userService.getBearerAuthHeader();

      return this.http
        .delete(`${this.baseUrl}/ride/${rideId}/participate`, { headers: _headers });
    }

    private getHeaders() {
        let headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }
}
