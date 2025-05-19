using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakani.DA.Interfaces
{
    public interface IFeedback
    {
        Task<Feedback> AddFeedback(Feedback feedback);
        Task<bool> DeleteFeedback(Guid feedbackId);
        Task<Feedback> GetFeedbackById(Guid feedbackId);
        Task<IEnumerable<Feedback>> GetFeedbacksByApartmentId(Guid apartmentId);
        Task<IEnumerable<Feedback>> GetFeedbacksByUserId(string userId);
        Task<bool> UpdateFeedback(Feedback feedback);
        Task<bool> FeedbackExists(Guid feedbackId);
        Task<bool> FeedbackExistsByUserIdAndApartmentId(string userId, Guid apartmentId);
        Task<Feedback> GetFeedbackByUserIdAndApartmentId(string userId, Guid apartmentId);
        Task<IEnumerable<Feedback>> GetAllFeedbacks();
        Task<IEnumerable<Feedback>> GetAllFeedbacksByApartmentId(Guid apartmentId);
        Task<IEnumerable<Feedback>> GetAllFeedbacksByUserId(string userId);

    }
}
