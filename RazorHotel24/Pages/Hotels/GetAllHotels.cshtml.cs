using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RazorHotel24.Interfaces;
using RazorHotel24.Models;

namespace RazorHotel24.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {

        private IHotelService _hotelService;

        public List<Hotel> Hotels { get; set; }

        public GetAllHotelsModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        public void OnGet()
        {
            try
            {
                Hotels = _hotelService.GetAllHotel();
            }
            catch (SqlException SqlExp)
            {
                Hotels = new List<Hotel>();
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
            }
            catch (Exception ex)
            {
                Hotels = new List<Hotel>();
                ViewData["ErrorMessage"] = "General error: " + ex;
            }
        }
    }
}
