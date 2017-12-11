import { AbstractControl } from '@angular/forms';
export class CustomValidation {

    static MatchPassword(AC: AbstractControl) {
        let password = AC.get('password').value; // to get value in input tag
        let confirmPassword = AC.get('passwordConf').value; // to get value in input tag

        if (password != confirmPassword) {
          AC.get('passwordConf').setErrors({ MatchPassword: true })
        } else {
          return null;   
        }
        
    }

    static CheckDateFormat(date: string): any {
      console.log(date);
      var regEx = /^\d{4}-\d{2}-\d{2}$/;

      if (!date.match(regEx)) { // Invalid format
        return { CheckDateFormat: true };
      }
      var d = new Date(date);
      if (!d.getTime()) { // Invalid date (or this could be epoch)
        return { CheckDateFormat: true };
      }
      return null;
    
    }
}
