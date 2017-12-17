export class User {


	  UserID: string;
	  Username: string;
	  Password: string;
		Region: string;
		Ville: string;
    DateOfBirth: Date;
		Description: string;
		DpUrl: string;
  
      constructor()
      {
        this.UserID = "";
        this.Username = "";
        this.Password = "";
        this.Region = "";
        this.Ville = "";
        this.DateOfBirth = new Date();
        this.Description = "";
        this.DpUrl = "";
      }
}
