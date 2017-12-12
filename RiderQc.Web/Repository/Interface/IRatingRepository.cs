using RiderQc.Web.Entities;

namespace RiderQc.Web.Repository.Interface
{
    public interface IRatingRepository
    {
        UserRating GetUserRatingByUserId(int userId);
    }
}
