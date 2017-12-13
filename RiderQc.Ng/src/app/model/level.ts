import { Ride } from "../model/ride";
import { User } from "../model/user";

export class Level {
    public Id: number;
    public Name: string;
  
    constructor(Id?: number, Name?: string) {
      this.Id = Id;
      this.Name = Name;
    }
}
