import { Component, OnInit, ViewChild, ElementRef, NgZone, Input, Output, EventEmitter } from '@angular/core';
import { GoogleMapsAPIWrapper } from '@agm/core/services/google-maps-api-wrapper';
import { MapsAPILoader } from '@agm/core/services/maps-api-loader/maps-api-loader';
import { Http } from '@angular/http';
import { FormControl } from "@angular/forms";

import { DirectionsMapDirective } from './direction.directive';

//Models
import { TrajetInfo } from "../../model/trajetinfo";
import { PlaceInfo } from "../../model/placeinfo";

declare var google: any;

@Component({
    selector: 'app-map',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

    constructor(private _httpService: Http, private mapsAPILoader: MapsAPILoader, private ngZone: NgZone) { }
    @Input() editable = false;
    @Input() showInstructions = false;
    @Input() waypoints;
    @Input() height;
    @Output() trajetUpdated = new EventEmitter();
    showExpansion: boolean = false;
    google_map_style: any;
    instructions_style: any;
    lat: number = 45.242396;
    lng: number = -74.135377;
    directionsDisplay: any;
    placeInfo: PlaceInfo = new PlaceInfo();
    placeInfoOrigin: PlaceInfo = new PlaceInfo();
    placeInfoDestination: PlaceInfo = new PlaceInfo();
    trajetInfo: TrajetInfo = new TrajetInfo();
    searchControl: FormControl;
    @ViewChild("originSearch") originSearchElementRef: ElementRef;
    @ViewChild("destinationSearch") destinationSearchElementRef: ElementRef;
    @ViewChild(DirectionsMapDirective) vc: DirectionsMapDirective;

    ngOnInit() {
        if (this.showInstructions) {
            this.showExpansion = true;
        }
        this.google_map_style = {
            'height': this.height + "vh"
        };
        this.instructions_style = {
            'max-height': this.height + "vh"
        };
        this.searchControl = new FormControl();
        if (this.waypoints && this.waypoints.length > 0) {
            this.setGivenWaypoints();
        } else {
            this.setCurrentPosition();
        }
        var gmapsApi: GoogleMapsAPIWrapper;
        if (this.vc.directionsDisplay === undefined) {
            this.mapsAPILoader.load().then(() => {
                var self = this;
                this.vc.directionsDisplay = new google.maps.DirectionsRenderer({
                    draggable: this.editable
                });
                this.vc.directionsDisplay.addListener('directions_changed', function () {
                    console.log("Complete: ", self.vc.directionsDisplay);
                    self.setTrajetInfo(self.vc.directionsDisplay.directions.routes[0].legs[0]);
                    self.setWaypoints(self.vc.directionsDisplay.directions.request.waypoints);
                    self.setOrigin(self.vc.directionsDisplay.directions.request.origin);
                    self.setDestination(self.vc.directionsDisplay.directions.request.destination);
                    self.emitValues();
                    console.log(self.vc.directionsDisplay.directions.routes[0].legs[0]);
                });

                if (this.editable) {
                    let autocompleteOrigin = new google.maps.places.Autocomplete(this.originSearchElementRef.nativeElement, {
                        types: ['geocode']  // geocode (cities) (regions) establishment address 
                    });

                    autocompleteOrigin.addListener("place_changed", () => {
                        this.ngZone.run(() => {
                            //get the place result
                            let place = autocompleteOrigin.getPlace();

                            //verify result
                            if (place.geometry === undefined || place.geometry === null) {
                                return;
                            }
                            console.log("Place", place);
                            console.log("lat: ", place.geometry.location.lat());
                            console.log("lng: ", place.geometry.location.lng());
                            //set latitude, longitude and zoom
                            this.placeInfoOrigin.lat = place.geometry.location.lat();
                            this.placeInfoOrigin.lng = place.geometry.location.lng();
                            this.placeInfoOrigin.placeId = place.place_id;
                            this.placeInfoOrigin.address = place.formatted_address;
                            this.placeInfoOrigin.phoneFormat = place.formatted_phone_number;
                            this.getAddressComponentByPlace(place);
                            this.vc.ngOnInit();
                        });
                        this.trajetInfo.origin = { lng: this.placeInfoOrigin.lng, lat: this.placeInfoOrigin.lat };
                        this.vc.ngOnInit();
                    });

                    let autocompleteDestination = new google.maps.places.Autocomplete(this.destinationSearchElementRef.nativeElement, {
                        types: ['geocode']  // geocode (cities) (regions) establishment address 
                    });

                    autocompleteDestination.addListener("place_changed", () => {
                        this.ngZone.run(() => {
                            //get the place result
                            let place = autocompleteDestination.getPlace();

                            //verify result
                            if (place.geometry === undefined || place.geometry === null) {
                                return;
                            }
                            console.log("Place", place);
                            console.log("lat: ", place.geometry.location.lat());
                            console.log("lng: ", place.geometry.location.lng());
                            //set latitude, longitude and zoom
                            this.placeInfoDestination.lat = place.geometry.location.lat();
                            this.placeInfoDestination.lng = place.geometry.location.lng();
                            this.placeInfoDestination.placeId = place.place_id;
                            this.placeInfoDestination.address = place.formatted_address;
                            this.placeInfoDestination.phoneFormat = place.formatted_phone_number;
                            this.getAddressComponentByPlace(place);
                            this.vc.ngOnInit();
                        });
                        this.trajetInfo.destination = { lng: this.placeInfoDestination.lng, lat: this.placeInfoDestination.lat };
                        this.vc.ngOnInit();
                    });
                }
            });
        }
    }
    
    toggleInstructions() {
        this.showInstructions = !this.showInstructions;
        this.vc.ngOnInit();
    }

    deleteWaypoint(index: number) {
        this.trajetInfo.waypoints.splice(index, 1);
        this.vc.ngOnInit();
    }

    setGivenWaypoints() {
        let lat: number = this.waypoints[0].split(", ")[0];
        let lng: number = this.waypoints[0].split(", ")[1];
        console.log("Origin:", lat, ",", lng);
        this.trajetInfo.origin = { lng: +lng, lat: +lat };
        lat = this.waypoints[this.waypoints.length - 1].split(", ")[0];
        lng = this.waypoints[this.waypoints.length - 1].split(", ")[1];
        console.log("Destination:", lat, ",", lng);
        this.trajetInfo.destination = { lng: +lng, lat: +lat };
        console.log("Waypoints length Given Waypoints :", this.waypoints.length);
        for (let i = 1; i <= this.waypoints.length - 2; i++) {
            console.log("Waypoints length Given Waypoints :", this.waypoints.length);
            lat = this.waypoints[i].split(",")[0];
            lng = this.waypoints[i].split(",")[1];
            if (lat != null && lng != null) {
                this.trajetInfo.waypoints[i - 1] = {
                    location: lat + "," + lng,
                    stopover: false
                }
                console.log("trajetInfo waypoint", i, ":", this.trajetInfo.waypoints[i - 1].location);
            }
        }
    }

    setWaypoints(waypoints: any) {
        let i = 0;
        console.log("Waypoints lenght: ", waypoints.length);
        for (let wp of waypoints) {
            if (wp.location.lat != null && wp.location.lng != null) {
                this.trajetInfo.waypoints[i] = {
                    location: wp.location.lat() + "," + wp.location.lng(),
                    stopover: false
                }
            }
            i++;
        }
    }

    setOrigin(origin: any) {
        if (origin.lat != null && origin.lng != null) {
            this.trajetInfo.origin = {
                lat: origin.lat(),
                lng: origin.lng()
            }
        }
    }

    setDestination(destination: any) {
        if (destination.lat != null && destination.lng != null) {
            this.trajetInfo.destination = {
                lat: destination.lat(),
                lng: destination.lng()
            }
        }
    }

    setTrajetInfo(trajetInfo: any) {
        let i = 0;
        for (let step of trajetInfo.steps) {
            this.trajetInfo.steps[i] = {
                instructions: step.instructions,
                duration: step.duration.text,
                distance: step.distance.text
            }
            i++;
        }
        this.trajetInfo.duration = trajetInfo.duration.text;
        this.trajetInfo.distance = trajetInfo.distance.text;
    }

    setCurrentPosition() {
        this.lat = 0;
        this.lng = 0;
        if ("geolocation" in navigator) {
            navigator.geolocation.getCurrentPosition((position) => {
                this.lat = position.coords.latitude;
                this.lng = position.coords.longitude;
            });
        }
    }

    emitValues() {
        var array = [];
        array.push(this.trajetInfo.origin.lat + ", " + this.trajetInfo.origin.lng);
        this.trajetInfo.waypoints.forEach(eachObj => {
            array.push(eachObj.location);
        });
        array.push(this.trajetInfo.destination.lat + ", " + this.trajetInfo.destination.lng);
        console.log(array);
        this.trajetUpdated.emit(array);
    }


    private getAddressComponentByPlace(place) {
        var components = place.address_components;
        var country = null;
        var city = null;
        var postalCode = null;
        var street_number = null;
        var route = null;
        var locality = null;

        for (var i = 0, component; component = components[i]; i++) {
            console.log(component);
            if (component.types[0] == 'country') {
                country = component['short_name'] + ', ' + component['long_name'];
            }
            if (component.types[0] == 'administrative_area_level_1') {
                city = component['long_name'];
            }
            if (component.types[0] == 'postal_code') {
                postalCode = component['short_name'];
            }
            if (component.types[0] == 'street_number') {
                street_number = component['short_name'];
            }
            if (component.types[0] == 'route') {
                route = component['long_name'];
            }
            if (component.types[0] == 'locality') {
                locality = component['short_name'];
            }

        }

        this.placeInfo.country = country;
        this.placeInfo.city = city;
        this.placeInfo.postalCode = postalCode;
        this.placeInfo.street_number = street_number;
        this.placeInfo.route = route;
        this.placeInfo.locality = locality;
    }
}
