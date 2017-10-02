import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class UserService {

    constructor(public http: Http) {

    }

    headers = new Headers({
        'Content-Type': 'application/json'
    });

    register(jsonUser) {
        return this.http.post('http://riderqc.azurewebsites.net/user/register', JSON.stringify(jsonUser));
    }

}
