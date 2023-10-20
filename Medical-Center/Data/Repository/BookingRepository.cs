using Medical_Center.Data.Models;
using Medical_Center.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Medical_Center.Data.Repository
{
    public class BookingRepository : IRepo<Payment>
    {
        private readonly ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Payment entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            IQueryable<Payment> query = _db.Bookings;

            var result = await query
                                    .OrderBy(payment => payment.Id)
                                    .ToListAsync();

            return result;
        }

        public async Task<Payment> GetOneAsync(int id, bool tracked = true)
        {
            IQueryable<Payment> query = _db.Bookings;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            var result = await query
                                    .OrderBy(payment => payment.Id)
                                    .FirstOrDefaultAsync(payment => payment.Id == id);

            return result;
        }

        public async Task RemoveAsync(Payment entity)
        {
            _db.Remove(entity);
        }

        public async Task UpdateAsync(Payment entity)
        {
            _db.Update(entity);
        }
    }
}
