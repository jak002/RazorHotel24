using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RazorHotel24.Interfaces;
using RazorHotel24.Models;
using RazorHotel24.Pages.Hotels;
using RazorHotel24.Services;

namespace RazorHotel24.Pages.Rooms
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        private IRoomService _roomService;

        public Room DeleteRoom {  get; set; }

        public DeleteModel(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public void OnGet(int hotelnr, int roomnr, string hname)
        {
            Name = hname;
            try
            {
                DeleteRoom = _roomService.GetRoomFromId(roomnr, hotelnr);
            }
            catch (SqlException SqlExp)
            {
                DeleteRoom = new Room();
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
            }
            catch (Exception ex)
            {
                DeleteRoom = new Room();
                ViewData["ErrorMessage"] = "General error: " + ex;
            }
        }

        public IActionResult OnPostDelete(int delNumber, int delRoom)
        {
            try
            {
                _roomService.DeleteRoom(delRoom, delNumber);
                return RedirectToPage("GetAllRooms", new { cid = delNumber, hname = Name });
            }
            catch (SqlException SqlExp)
            {
                DeleteRoom = new Room();
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
                return Page();
            }
            catch (Exception ex)
            {
                DeleteRoom = new Room();
                ViewData["ErrorMessage"] = "Database error: " + ex;
                return Page();
            }

        }

        public IActionResult OnPostCancel(int delNumber)
        {
            return RedirectToPage("GetAllRooms", new { cid = delNumber, hname = Name });
        }
    }
}
