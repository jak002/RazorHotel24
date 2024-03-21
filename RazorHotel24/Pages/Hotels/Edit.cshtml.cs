using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RazorHotel24.Interfaces;
using RazorHotel24.Models;

namespace RazorHotel24.Pages.Hotels
{
    public class EditModel : PageModel
    {
        private IHotelService _hotelService;

        [BindProperty]
        public Hotel HotelToUpdate { get; set; }
        public EditModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public void OnGet(int hotelnr)
        {

            try
            {
                HotelToUpdate = _hotelService.GetHotelFromId(hotelnr);
            }
            catch (SqlException SqlExp)
            {
                HotelToUpdate = new Hotel();
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
            }
            catch (Exception ex)
            {
                HotelToUpdate = new Hotel();
                ViewData["ErrorMessage"] = "General error: " + ex;
            }
        }

        public IActionResult OnPostUpdate()
        {
            try
            {
                _hotelService.UpdateHotel(HotelToUpdate, HotelToUpdate.HotelNr);
                return RedirectToPage("GetAllHotels");
            }
            catch (SqlException SqlExp)
            {
                HotelToUpdate = new Hotel();
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
                return Page();
            }
            catch (Exception ex)
            {
                HotelToUpdate = new Hotel();
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
