export class User {


	  UserID: string;
	  Username: string;
	  Password: string;
		Region: string;
		Ville: string;
		DateOfBirth: string;
		Description: string;
		DpUrl: string;


		constructor(UserID?: string, Username?: string, Password?: string, Region?: string, Ville?: string, DateOfBirth?: string, Description?: string, DpUrl?: string) {
			this.UserID = UserID;
			this.Username = Username;
			this.Password = Password;
			this.Region = Region;
			this.Ville = Ville;
			this.DateOfBirth = DateOfBirth;
			this.Description = Description;
			this.DpUrl = DpUrl;
		}

}
