import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Ride } from '../model/ride';
import { Level } from '../model/level';
import { environment } from '../../environments/environment';
import 'rxjs/add/operator/map';

@Injectable()
export class RideService {

  baseUrl: string = environment.BaseUrl;

    constructor(public http:Http) {}

    getRides() {
        return this.http.get(`${this.baseUrl}/ride/list`)
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

    private getHeaders() {
        let headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }
}
