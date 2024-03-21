using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RazorHotel24.Interfaces;
using RazorHotel24.Models;

namespace RazorHotel24.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private IHotelService _hservice;

        public List<Hotel> AllHotels { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHotelService hotelService)
        {
            _logger = logger;
            _hservice = hotelService;
        }

        public void OnGet()
        {
            try
            {
                AllHotels = _hservice.GetAllHotel();
            } 
            catch (SqlException SqlExp)
            {
                AllHotels = new List<Hotel>();
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
            }
            catch (Exception ex)
            {
                AllHotels = new List<Hotel>();
                ViewData["ErrorMessage"] = "General error: " + ex;
            }
        }
    }
}
