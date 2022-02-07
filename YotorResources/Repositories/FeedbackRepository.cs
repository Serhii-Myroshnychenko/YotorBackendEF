using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;
using YotorResources.Contracts;

namespace YotorResources.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly YotorDbContext _yotorDbContext;
        public FeedbackRepository(YotorDbContext yotorDbContext)
        {
            _yotorDbContext = yotorDbContext;
        }

        public async Task CreateFeedbackAsync(int user_id, string name, DateTime date, string text)
        {
            var feedback = new Feedback(user_id, name, date, text);
            await _yotorDbContext.AddAsync(feedback);
            await _yotorDbContext.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await _yotorDbContext.Feedbacks.Where(f => f.FeedbackId == id).FirstOrDefaultAsync();
            if (feedback != null)
            {
                _yotorDbContext.Feedbacks.Remove(feedback);
                await _yotorDbContext.SaveChangesAsync();
            }
        }

        public async Task<Feedback> GetFeedbackAsync(int id)
        {
            return await _yotorDbContext.Feedbacks.Where(f => f.FeedbackId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksAsync()
        {
            return await _yotorDbContext.Feedbacks.ToListAsync();
        }
    }
}
