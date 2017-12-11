import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Trajet } from '../model/trajet';
import 'rxjs/add/operator/map';

@Injectable()
export class TrajetService {

    private baseUrl: string = 'http://riderqc-api.azurewebsites.net';

    constructor(public http:Http) {}

    getTrajets() {
        let trajets$ = this.http
            .get(`${this.baseUrl}/trajet/list`)
            .map(res => res.json());
        return trajets$;
    }

    details(id: number): Observable<Trajet> {
        let ride$ = this.http
            .get(`${this.baseUrl}/trajet/${id}`)
            .map(res => res.json());
        return ride$;
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
