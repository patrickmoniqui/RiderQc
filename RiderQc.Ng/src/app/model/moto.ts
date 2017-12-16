export class Moto {


    UserId: number;
    Brand: string;
    Model: string;
    Year: number;
    Type: string;
    


    constructor(UserId?: number, Brand?: string,  Model?: string, Year?: number, Type?: string) {
        
        this.UserId = UserId;
        this.Brand = Brand;
        this.Model = Model;
        this.Year = Year;
        this.Type = Type;
    }

}
