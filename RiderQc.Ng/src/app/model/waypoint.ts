export class Waypoint {
    location: string;
    stopover: boolean;

    constructor(location?: string, stopover?: boolean) {
        this.location = location;
        this.stopover = stopover;
    }

}
