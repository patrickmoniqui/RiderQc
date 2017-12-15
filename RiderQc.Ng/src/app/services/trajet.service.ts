import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { environment } from '../../environments/environment';
import 'rxjs/add/operator/map';

//Models
import { Trajet } from '../model/trajet';

//Service
import { UserService } from '../services/user.service';

@Injectable()
export class TrajetService {

    baseUrl: string = environment.BaseUrl;

    constructor(public http: Http, public userService: UserService) {}

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

    add(trajet: any): Observable<number> {
        var trajetId = this.http.post(this.baseUrl + '/trajet', trajet, { headers: this.userService.getBearerAuthHeader() })
            .map(x => x.json());
        return trajetId;
    }
    
    update(trajet: any): Observable<number> {
        var trajetId = this.http.put(this.baseUrl + '/trajet/' + trajet.trajetid, trajet, { headers: this.userService.getBearerAuthHeader() })
            .map(x => x.json());
        return trajetId;
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
