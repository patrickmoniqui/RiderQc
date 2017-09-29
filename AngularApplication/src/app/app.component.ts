import { Component, OnInit, ViewChild, Injectable } from '@angular/core';
import { GoogleMapsAPIWrapper } from '@agm/core/services/google-maps-api-wrapper';
import { MapsAPILoader } from '@agm/core/services/maps-api-loader/maps-api-loader';
import { Http } from '@angular/http'
import { DirectionsMapDirective } from './direction'
declare var google: any;

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
    constructor(private _httpService: Http, private mapsAPILoader: MapsAPILoader) { }
    apiValues: string[] = [];
    lat: number = 45.242396;
    lng: number = -74.135377;
    origin: any;
    destination: any;
    directionsDisplay: any;
    geocodedWaypoints: any;
    legs: any;
    @ViewChild(DirectionsMapDirective) vc: DirectionsMapDirective;
    waypoints: waypoint[] = [
        {
            location: "45.256173,-74.137887",
            stopover: false
        }
    ]
    markers: marker[] = [
        {
            lat: 45.242396,
            lng: -74.135377,
            label: '1',
            draggable: true
        },
        {
            lat: 45.256173,
            lng: -74.137887,
            label: '2',
            draggable: true
        },
        {
            lat: 45.269456,
            lng: -74.165114,
            label: '3',
            draggable: true
        }
    ]

    ngOnInit() {
        this.origin = { longitude: this.markers[0].lng, latitude: this.markers[0].lat };
        this.destination = { longitude: this.markers[2].lng, latitude: this.markers[2].lat };
        var gmapsApi: GoogleMapsAPIWrapper;
        if (this.vc.directionsDisplay === undefined) {
            this.mapsAPILoader.load().then(() => {
            var self = this;
            this.vc.directionsDisplay = new google.maps.DirectionsRenderer({
                draggable: true
            });
            this.vc.directionsDisplay.addListener('directions_changed', function () {
                self.setGeocodedWaypoints(self.vc.directionsDisplay.directions.geocoded_waypoints);
                self.setLegs(self.vc.directionsDisplay.directions.routes[0].legs[0]);
                console.log(self.vc.directionsDisplay.directions.routes[0].legs[0]);
                console.log("Complete: ", self.vc.directionsDisplay);
            });
            });
        }
    }

    clickedMarker(label: string, index: number) {
        console.log(`clicked the marker: ${label || index}`)
    }

    deleteMarker(index: number) {
        /*this.markers.splice(index, 1);

        for (var i = 0; i < this.markers.length; i++) {
            if (i >= index) {
                this.markers[i].label = (i + 1) + '';
            }
        }*/
        this.waypoints.splice(index, 1);
    }

    deleteGeocodedWaypoint(index: number) {
        this.geocodedWaypoints.splice(index, 1);
    }

    setLegs(legs: any) {
        this.legs = legs;
    }

    setGeocodedWaypoints(geocodedWaypoints: any) {
        this.geocodedWaypoints = geocodedWaypoints;
        console.log(this.geocodedWaypoints);
    }

    mapClicked($event: any) {
        this.markers.push({
            lat: $event.coords.lat,
            lng: $event.coords.lng,
            label: (this.markers.length + 1) + '',
            draggable: true
        });
    }

    markerDragEnd(m: marker, index: number, $event: any) {
        console.log('dragEnd', m, $event);
        m.lat = $event.coords.lat;
        m.lng = $event.coords.lng;
        this.markers[index] = m;
    }

}

interface waypoint {
    location: string;
    stopover: boolean;
}

interface marker {
    lat: number;
    lng: number;
    label?: string;
    draggable: boolean;
}
