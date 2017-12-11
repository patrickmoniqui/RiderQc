import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Ride } from '../model/ride';
import { User } from '../model/user';
import { Level } from '../model/level';
import 'rxjs/add/operator/map';

@Injectable()
export class RiderqcService {

    private baseUrl: string = 'http://riderqc-api.azurewebsites.net';

    constructor(public http:Http) {}

    getRides() {
        return this.http.get(`${this.baseUrl}/ride/list`)
            .map(res => res.json());
    }

    getRidesParticipants(rideId: number) {
        let headers = new Headers();
        console.log("rideId, participants: " + rideId);
        headers.append('rideId', rideId + '');
        return this.http.get(`${this.baseUrl}/ride/participant/list`, {headers: headers})
            .map(res => res.json());
    }

    participate(rideId: number) {
        return this.http.get(`${this.baseUrl}/ride/${rideId}/participate`, {headers: this.getHeadersPOST()});
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

    private getHeaders() {
        let headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }

    private getHeadersPOST() {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        return headers;
    }
}
