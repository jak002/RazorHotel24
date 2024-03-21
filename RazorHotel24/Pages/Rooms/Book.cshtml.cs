using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorHotel24.Pages.Rooms
{
    public class BookModel : PageModel
    {
        public int HotelNr { get; set; }
        public string Hname { get; set; }

        public void OnGet(int hotelnr, string hname)
        {
            HotelNr = hotelnr;
            Hname = hname;
        }
    }
}
