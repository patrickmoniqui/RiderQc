import { Injectable } from '@angular/core';
import { Http } from "@angular/http/src";
import { Observable } from "rxjs/Observable";

@Injectable()
export class GoogleService {

    private baseUrl: string = 'https://maps.googleapis.com/maps/api/js?callback=initMap&key=';
    private apiKey: string = 'AIzaSyCrXlv7ZZWk5yoav8hM-afN6NvDOiKSpuM';

    constructor(private http: Http) {
    }

    /*getAll(): Observable<[]> {
        let category$ = this.http
            .get(`${this.baseUrl}/model.category`, { headers: this.getHeaders() })
            .map(mapCategorys)
            .catch(handleError);
        return category$;
    }

    private getHeaders() {
        let headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }

    mapCategorys(response: Response): Category[] {
        return response.json().map(toCategory);
    }


    mapCategory(response: Response): Category {
        return toCategory(response.json());
    }

    toCategory(r: any): Category {
        let category = <Category>({
            id: r.id,
            tag: r.tag
        });
        console.log('Parsed Category:', category);
        return category;
    }*/

    handleError(error: any) {
        let errorMsg = error.message || `Yikes! There was was a problem with our hyperdrive device and we couldn't retrieve your data!`
        console.error(errorMsg);

        return Observable.throw(errorMsg);
    }

}
