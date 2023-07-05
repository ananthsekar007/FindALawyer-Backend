using FindALawyer.Dao;
using FindALawyer.Dao.LawyerDao;
using FindALawyer.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace FindALawyer.Services.LawyerService
{
    public class LawyerServiceImpl : ILawyerService
    {

        private readonly FindALawyerContext _context;

        public LawyerServiceImpl(FindALawyerContext context) { 
            _context = context;
        }

        public async Task<ServiceResponse<ICollection<LawyerWithRatings>>> getAllLawyersWithRatings()
        {

            ServiceResponse<ICollection<LawyerWithRatings>> serviceResponse = new();

            ICollection<LawyerWithRatings> lawyers = await _context.Lawyer.Include(l => l.Feedbacks).ThenInclude(f => f.Client)
                            .Select(lawyer => new LawyerWithRatings()
                            {
                                LawyerId = lawyer.LawyerId,
                                Name = lawyer.Name,
                                EmailAddress = lawyer.EmailAddress,
                                PhoneNumber = lawyer.PhoneNumber,
                                Qualification = lawyer.Qualification,
                                Type = lawyer.Type,
                                Address = lawyer.Address,
                                CreatedAt = lawyer.CreatedAt,
                                UpdatedAt = lawyer.UpdatedAt,
                                Feedbacks = lawyer.Feedbacks,
                                AverageRating = lawyer.Feedbacks.Any() ? (int)lawyer.Feedbacks.Average(f => f.Rating) : 0
                            })
                            .ToListAsync();

            serviceResponse.Response = lawyers;
            return serviceResponse;
            
        }
    }
}
