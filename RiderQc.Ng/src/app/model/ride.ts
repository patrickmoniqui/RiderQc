import { Comment } from '../model/comment';
import { User } from '../model/user';
import { Level } from '../model/level';

export class Ride {
  RideId: number;
  Title: string ;
  Description: string ;
  CreatorId: number ;
  TrajetId: number ;
  LevelId: number ;
  DateDepart: Date ;
  DateFin: Date; 
  Creator: User;
  Level: Level;
  Comments: Comment[];
  Participants: string[];

  constructor(RideId?: number, Title?: string, Description?: string, CreatorId?: number, TrajetId?: number, LevelId?: number, DateDepart?: Date, DateFin?: Date) {
      this.RideId = RideId;
      this.Title = Title;
      this.Description = Description;
      this.CreatorId = CreatorId;
      this.TrajetId = TrajetId;
      this.LevelId = LevelId;
      this.DateDepart = DateDepart;
      this.DateFin = DateFin;
  }
}
