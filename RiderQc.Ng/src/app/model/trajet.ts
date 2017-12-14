import { User } from "../model/user";

export class Trajet {
    public TrajetId: number;
    public Title: string;
    public Description: string;
    public Creator: User;
    public GpsPoints: string[];
  
    constructor(TrajetId?: number, Title?: string, Description?: string, Creator?: User, GpsPoints?: string[]) {
      this.TrajetId = TrajetId;
      this.Title = Title;
      this.Description = Description;
      if (Creator)
          this.Creator = Creator;
      else
          this.Creator = new User();
      if (GpsPoints)
          this.GpsPoints = GpsPoints;
      else
          this.GpsPoints = new Array<string>();
    }
}
