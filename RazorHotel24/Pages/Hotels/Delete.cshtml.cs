using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RazorHotel24.Interfaces;
using RazorHotel24.Models;

namespace RazorHotel24.Pages.Hotels
{
    public class DeleteModel : PageModel
    {

        private IHotelService _hotelService;

        public Hotel DeleteHotel {  get; set; }

        public DeleteModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public IActionResult OnGet(int hotelnr)
        {
            try
            {
                DeleteHotel = _hotelService.GetHotelFromId(hotelnr);
            }
            catch (SqlException SqlExp)
            {
                DeleteHotel = new Hotel();
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
            }
            catch (Exception ex)
            {
                DeleteHotel = new Hotel();
                ViewData["ErrorMessage"] = "General error: " + ex;
            }
            return Page();
        }

        public IActionResult OnPostDelete(int delNumber)
        {
            try
            {
                _hotelService.DeleteHotel(delNumber);
                return RedirectToPage("GetAllHotels");
            }
            catch (SqlException SqlExp)
            {
                DeleteHotel = new Hotel();
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
                return Page();
            }
            catch (Exception ex)
            {
                DeleteHotel = new Hotel();
                ViewData["ErrorMessage"] = "General error: " + ex;
                return Page();
            }

        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("GetAllHotels");
        }
    }
}
