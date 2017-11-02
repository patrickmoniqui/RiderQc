using System.Collections;
using System.ComponentModel.DataAnnotations;
namespace RiderQc.Web.Helpers
{
    
    public class GpsCoordinateValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                if(list.Count >= 2)
                {
                    return true;
                }
            }
            return false;
        }
    }
}