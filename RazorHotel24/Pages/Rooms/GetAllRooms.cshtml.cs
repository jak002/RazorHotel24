using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RazorHotel24.Interfaces;
using RazorHotel24.Models;
using RazorHotel24.Services;

namespace RazorHotel24.Pages.Rooms
{
    public class GetAllRoomsModel : PageModel
    {

        private IRoomService _roomService;

        public List<Room> Rooms { get; set; }

        public string Name { get; set; }

        public GetAllRoomsModel(IRoomService roomService)
        {
            _roomService = roomService;
        }
        public void OnGet(int cid, string hname)
        {
            Name = hname;
            try
            {
                Rooms = _roomService.GetAllRoom(cid);
            }
            catch (SqlException SqlExp)
            {
                Rooms = new List<Room>();
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
            }
            catch (Exception ex)
            {
                Rooms = new List<Room>();
                ViewData["ErrorMessage"] = "General error: " + ex;
            }
        }
    }
}
