import { User } from '../model/user';

export class Authentification {
    public Id: number;
    public UserId: number;
    public Token: string;
    public IssueDate: Date;
    public ExpirationDate: Date;
    public User: User;
}