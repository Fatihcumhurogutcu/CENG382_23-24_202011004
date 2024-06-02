using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using loginDemo.Models;
using loginDemo.Data;

namespace loginDemo.Pages
{
    public class AddReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddReservationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Room Room { get; set; } = new Room(); 

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Rooms.Add(Room);
            await _context.SaveChangesAsync();

            return RedirectToPage("ListReservations");
        }
    }
}
