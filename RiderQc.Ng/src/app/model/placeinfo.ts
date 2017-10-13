export class PlaceInfo {
    lat: number;
    lng: number;
    street_number: string;
    route: string;
    locality: string;
    country: string;
    city: string;
    postalCode: string;
    placeId: string;
    address: string;
    phoneFormat: string;

    constructor(lat?: number, lng?: number, street_number?:string, route?: string, locality?:string, country?: string, city?:string, postalCode?:string, placeId?: string, address?: string, phoneFormat?: string) {
        this.lat = lat;
        this.lng = lng;
        this.street_number = street_number;
        this.route = route;
        this.locality = locality;
        this.country = country;
        this.city = city;
        this.postalCode = postalCode;
        this.placeId = placeId;
        this.address = address;
        this.phoneFormat = phoneFormat;
    }

}
