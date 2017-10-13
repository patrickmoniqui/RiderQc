import { Ride } from "../model/ride";
import { User } from "../model/user";

export class Level {
    public LevelId: number;
    public Name: string;
  
    constructor(LevelId?: number, Name?: string) {
      this.LevelId = LevelId;
      this.Name = Name;
    }
}
