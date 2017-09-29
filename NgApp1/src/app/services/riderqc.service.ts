import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class RiderqcService {

    constructor(public http:Http)
    {

    }

    getRides()
    {
        return this.http.get('http://riderqc.azurewebsites.net/ride/list')
            .map(res => res.json());
    }
}
