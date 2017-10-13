import { Comment } from '../model/comment';
import { User } from '../model/user';
import { Level } from '../model/level';

export interface Ride
{
  RideId: number;
  Title: string ;
  Description: string ;
  CreatorId: number ;
  TrajetId: number ;
  LevelId: number ;
  DateDepart: Date ;
  DateFin: Date; 
  User: User;
  Level: Level;
  Comments: Comment[];
}