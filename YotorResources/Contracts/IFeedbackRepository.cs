using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;

namespace YotorResources.Contracts
{
    public interface IFeedbackRepository
    {
        Task<IEnumerable<Feedback>> GetFeedbacksAsync();
        Task<Feedback> GetFeedbackAsync(int id);
        Task CreateFeedbackAsync(int user_id, string name, DateTime date, string text);
        Task DeleteFeedbackAsync(int id);
    }
}
