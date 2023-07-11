using FindALawyer.Dao;
using FindALawyer.Dao.LawyerDao;
using FindALawyer.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using FindALawyer.Models;

namespace FindALawyer.Services.LawyerService
{
    public class LawyerServiceImpl : ILawyerService
    {

        private readonly FindALawyerContext _context;

        public LawyerServiceImpl(FindALawyerContext context) { 
            _context = context;
        }

        public async Task<ServiceResponse<ICollection<LawyerWithRatings>>> GetAllLawyersWithRatings()
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

        public async Task<bool> IsValidLawyer(int lawyerId)
        {
            Lawyer existingLawyer = await _context.Lawyer.FindAsync(lawyerId);
            if (existingLawyer == null) return false;
            return true;

        }

        public async Task<ServiceResponse<string>> RateALawyer(Rating ratingInput)
        {
            ServiceResponse<string> serviceResponse = new();

            Feedback existingfeedback = await _context.Feedback.FirstOrDefaultAsync(f => f.ClientId == ratingInput.ClientId && f.LawyerId == ratingInput.LawyerId);

            if(existingfeedback is not null)
            {
                serviceResponse.Error = "You already rated this lawyer!";
                return serviceResponse;
            }

            Feedback newFeedback = new Feedback()
            {
                ClientId = ratingInput.ClientId,
                LawyerId = ratingInput.LawyerId,
                Rating = ratingInput.RatingValue,
                Remarks = ratingInput.Remarks
            };

            await _context.Feedback.AddAsync(newFeedback);
            await _context.SaveChangesAsync();

            serviceResponse.Response = "Lawyer rated successfully!";
            return serviceResponse;

        }
    }
}
