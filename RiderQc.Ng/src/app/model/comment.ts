import { Ride } from "../model/ride";
import { User } from "../model/user";

export class Comment {
    public CommentId: number;
    public ParentId: number;
    public SenderId: number;
    public RideId: number;
    public TrajetId: number;
    public CommentText: string;
    public Blocked: boolean;
    public Vote: number;
    public TimeStamp: Date;
    public ChildComments: Comment[];
    public Comment2: Comment;
    public Ride: Ride;
    public User: User;
}
