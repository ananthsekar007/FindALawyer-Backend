using FindALawyer.Dao;
using FindALawyer.Dao.LawyerDao;

namespace FindALawyer.Services.LawyerService
{
    public interface ILawyerService
    {
        Task<ServiceResponse<ICollection<LawyerWithRatings>>> GetAllLawyersWithRatings();

        Task<bool> IsValidLawyer(int lawyerId);

        Task<ServiceResponse<string>> RateALawyer(Rating ratingInput);
    }
}
