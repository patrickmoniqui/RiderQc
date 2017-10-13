import { GoogleMapsAPIWrapper } from '@agm/core/services/google-maps-api-wrapper';
import { Directive, Input } from '@angular/core';
declare var google: any;



@Directive({
    selector: 'agm-directions',
    providers: []
})
export class DirectionsMapDirective {
    @Input() origin;
    @Input() destination;
    @Input() waypoints;
    @Input() directionsDisplay;

    constructor(private gmapsApi: GoogleMapsAPIWrapper) { }
    ngOnInit() {
        this.gmapsApi.getNativeMap().then(map => {
            var directionsService = new google.maps.DirectionsService;
            this.directionsDisplay.setMap(map);
            var directionsDisplay = this.directionsDisplay;
            console.log("directive origin:", this.origin);
            console.log("directive destination:", this.destination);
            directionsService.route({
                origin: this.origin,
                destination: this.destination,
                waypoints: this.waypoints,
                optimizeWaypoints: true,
                travelMode: 'DRIVING'
            }, function (response, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    directionsDisplay.setDirections(response);
                } else {
                    window.alert('Directions request failed due to ' + status);
                }
            });

        });
    }
}
