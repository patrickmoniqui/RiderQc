import { Waypoint } from "./waypoint";

export class TrajetInfo {
    origin: any;
    destination: any;
    waypoints: Waypoint[];
    steps: Steps[];
    duration: string;
    distance: string;

    constructor(origin?: any, destination?: any, waypoints?: Waypoint[], steps?: Steps[], duration?: string, distance?: string) {
        this.origin = origin;
        this.destination = destination;
        if (waypoints) {
            this.waypoints = waypoints;
        } else {
            this.waypoints = new Array<Waypoint>();
        }
        if(steps) {
            this.steps = steps;
        } else {
            this.steps = new Array<Steps>();
        }
        this.duration = duration;
        this.distance = distance;
    }

}

export class Steps {
    instructions: string;
    duration: string;
    distance: string;

    constructor(instructions?: string, duration?: string, distance?: string) {
        this.instructions = instructions;
        this.duration = duration;
        this.distance = distance;
    }
}
