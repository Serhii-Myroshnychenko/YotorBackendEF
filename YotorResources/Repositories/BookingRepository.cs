using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorContext.Models;
using YotorResources.Contracts;

namespace YotorResources.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly YotorDbContext _yotorDbContext;
        public BookingRepository(YotorDbContext yotorDbContext)
        {
            _yotorDbContext = yotorDbContext;
        }

        public async Task CreateBookingAsync(int? restriction_id, int user_id, int car_id, int? feedback_id, DateTime start_date, DateTime end_date, bool status, int full_price, string start_address, string end_address)
        {
            var booking = new Booking();
            booking.RestrictionId = restriction_id;
            booking.UserId = user_id;
            booking.CarId = car_id;
            booking.FeedbackId = feedback_id;
            booking.StartDate = start_date;
            booking.EndDate = end_date;
            booking.Status = status;
            booking.FullPrice = full_price;
            booking.StartAddress = start_address;
            booking.EndAddress = end_address;
            await _yotorDbContext.AddAsync(booking);
            await _yotorDbContext.SaveChangesAsync();
        }

        public async Task<Booking> GetBookingAsync(int id)
        {
            return await _yotorDbContext.Bookings.Where(b => b.BookingId == id).FirstOrDefaultAsync();
        }

        public async Task<Booking> GetBookingByParamsAsync(DateTime start_date, DateTime end_date, string start_address, string end_address)
        {
            return await _yotorDbContext.Bookings.FirstOrDefaultAsync(b => b.StartDate == start_date & b.EndDate == end_date & b.StartAddress == start_address & b.EndAddress == end_address);
            
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await _yotorDbContext.Bookings.ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int id)
        {
            return await _yotorDbContext.Bookings.Where(b => b.UserId == id).ToListAsync();
        }
    }
}
