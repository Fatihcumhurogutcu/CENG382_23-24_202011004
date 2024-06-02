using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using loginDemo.Models;
using loginDemo.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace loginDemo.Pages
{
    public class ListReservationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ListReservationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Room> Rooms { get; set; } = new List<Room>(); // Varsayılan değer atandı
        public IList<Room> AvailableRooms { get; set; } = new List<Room>(); // Varsayılan değer atandı

        [BindProperty(SupportsGet = true)]
        public DateTime? SearchStartTime { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? SearchEndTime { get; set; }

        public async Task OnGetAsync()
        {
            Rooms = await _context.Rooms.Include(r => r.Reservations).ToListAsync();

            if (SearchStartTime.HasValue && SearchEndTime.HasValue)
            {
                AvailableRooms = await _context.Rooms
                    .Include(r => r.Reservations)
                    .Where(r => !r.Reservations.Any(res => res.StartTime < SearchEndTime && res.EndTime > SearchStartTime))
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostReserveAsync(int roomId, DateTime startTime, DateTime endTime)
        {
            var userId = User.Identity.Name;

            var reservation = new Reservation
            {
                RoomId = roomId,
                UserId = userId,
                StartTime = startTime,
                EndTime = endTime,
                IsCancelled = false
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCancelAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                reservation.IsCancelled = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
