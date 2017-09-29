import { GoogleMapsAPIWrapper } from '@agm/core/services/google-maps-api-wrapper';
import { Directive, Input } from '@angular/core';
import { AppComponent } from './app.component';
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
            directionsService.route({
                origin: { lat: this.origin.latitude, lng: this.origin.longitude },
                destination: { lat: this.destination.latitude, lng: this.destination.longitude },
                waypoints: this.waypoints,
                optimizeWaypoints: true,
                travelMode: 'DRIVING'
            }, function (response, status) {
                if (status === 'OK') {
                    directionsDisplay.setDirections(response);
                } else {
                    window.alert('Directions request failed due to ' + status);
                }
            });

        });
    }
}
