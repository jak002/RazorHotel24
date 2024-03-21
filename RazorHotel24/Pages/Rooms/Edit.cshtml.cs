using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RazorHotel24.Interfaces;
using RazorHotel24.Models;
using RazorHotel24.Services;

namespace RazorHotel24.Pages.Rooms
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Room EditRoom { get; set; }

        [BindProperty]
        public RoomType RoomType { get; set; }

        private IRoomService _roomService;

        [BindProperty]
        public string Name { get; set; }

        public EditModel(IRoomService roomService)
        {
            _roomService = roomService;
        }


        public void OnGet(int hotelnr, int roomnr, string hname)
        {
            Name = hname;
            try
            {
                EditRoom = _roomService.GetRoomFromId(roomnr, hotelnr);
                switch(EditRoom.Types) 
                {
                    case 'S':
                        RoomType = RoomType.Single;
                        break;
                    case 'D':
                        RoomType = RoomType.Double;
                        break;
                    case 'F':
                        RoomType = RoomType.Family;
                        break;
                }
            }
            catch (SqlException SqlExp)
            {
                EditRoom = new Room();
                RoomType = RoomType.Single;
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
            }
            catch (Exception ex)
            {
                EditRoom = new Room();
                RoomType = RoomType.Single;
                ViewData["ErrorMessage"] = "General error: " + ex;
            }
        }

        public IActionResult OnPostUpdate()
        {
            try
            {
                switch (RoomType)
                {
                    case RoomType.Single:
                        EditRoom.Types = 'S';
                        break;
                    case RoomType.Double:
                        EditRoom.Types = 'D';
                        break;
                    case RoomType.Family:
                        EditRoom.Types = 'F';
                        break;
                }
                _roomService.UpdateRoom(EditRoom, EditRoom.RoomNr, EditRoom.HotelNr);
                return RedirectToPage("GetAllRooms", new { cid = EditRoom.HotelNr, hname = Name });
            }
            catch (SqlException SqlExp)
            {
                EditRoom = new Room();
                RoomType = RoomType.Single;
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
                return Page();
            }
            catch (Exception ex)
            {
                EditRoom = new Room();
                RoomType = RoomType.Single;
                ViewData["ErrorMessage"] = "General error: " + ex;
                return Page();
            }

        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("GetAllRooms", new {cid = EditRoom.HotelNr, hname = Name});
        }
    }
}
