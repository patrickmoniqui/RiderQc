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
<<<<<<< Updated upstream:RiderQc.Ng/src/app/services/riderqc.service.ts
        return this.http.get('http://riderqc-api.azurewebsites.net/ride/list')
=======
		return this.http.get('http://riderqc-api.azurewebsites.net/ride/list')
>>>>>>> Stashed changes:NgApp1/src/app/services/riderqc.service.ts
            .map(res => res.json());
    }
}
