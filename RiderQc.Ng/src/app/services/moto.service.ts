import { Injectable } from '@angular/core';

//Model
import { Moto } from '../model/moto';

//autres
import { environment } from '../../environments/environment';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';



@Injectable()
export class MotoService {
    baseUrl: string = environment.BaseUrl;
    constructor(public http: Http)
    {

    }

    createMoto(jsonMoto,token:string)
    {
        let body = JSON.stringify(jsonMoto);
        let headers = new Headers({
            'Content-Type': 'application/json',
            'Authorization' : 'Bearer ' + token,
        });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.baseUrl + "/moto", body, options)
            .map((response: Response) => {
                console.log(response);
            return response;
        }).catch(this.handleError);
    }



    private handleError(error: Response) {
        console.error(error.json());
        return Observable.throw(error.json().Message || 'Server error');
    }
    /*
    register(jsonUser) {
        let body = JSON.stringify(jsonUser);
        let headers = new Headers({
            'Content-Type': 'application/json',
            'username': btoa(jsonUser.Username),
            'password': jsonUser.Password
        });
        let options = new RequestOptions({ headers: headers });
        return this.http.post(`${this.baseUrl}/user/register`, body, options)
            .map((response: Response) => {
                return response;
            }).catch(this.handleError);

    }
    */
}
