using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RazorHotel24.Interfaces;
using RazorHotel24.Models;
using RazorHotel24.Services;
using System.Security.Cryptography;

namespace RazorHotel24.Pages.Hotels
{
    public class CreateModel : PageModel
    {
        private IHotelService _hotelservice;

        [BindProperty]
        public Hotel NewHotel { get; set; }

        public CreateModel(IHotelService hotelService)
        {
            _hotelservice = hotelService;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            try
            {
                _hotelservice.CreateHotel(NewHotel);
                return RedirectToPage("GetAllHotels");
            }
            catch (SqlException SqlExp)
            {
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "General error: " + ex;
                return Page();
            }
        }
    }
}
