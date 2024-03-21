using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RazorHotel24.Interfaces;
using RazorHotel24.Models;

namespace RazorHotel24.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private IRoomService _roomService;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public Room NewRoom { get; set; }

        [BindProperty]
        public RoomType RoomType { get; set; }

        [BindProperty]
        public int HotelId { get; set; }

        public CreateModel(IRoomService roomService)
        {
            _roomService = roomService;
        }


        public void OnGet(int id, string hname)
        {
            HotelId = id;
            Name = hname;
        }

        public IActionResult OnPost()
        {
            try
            {
                switch (RoomType)
                {
                    case RoomType.Single:
                        NewRoom.Types = 'S';
                        break;
                    case RoomType.Double:
                        NewRoom.Types = 'D';
                        break;
                    case RoomType.Family:
                        NewRoom.Types = 'F';
                        break;
                }
                NewRoom.HotelNr = HotelId;
                _roomService.CreateRoom(NewRoom.HotelNr, NewRoom);
                return RedirectToPage("GetAllRooms", new { cid = NewRoom.HotelNr, hname = Name });
            }
            catch (SqlException SqlExp)
            {
                NewRoom = new Room();
                RoomType = RoomType.Single;
                ViewData["ErrorMessage"] = "Database error: " + SqlExp;
                return Page();
            }
            catch (Exception ex)
            {
                NewRoom = new Room();
                RoomType = RoomType.Single;
                ViewData["ErrorMessage"] = "General error: " + ex;
                return Page();

            }
        }
    }
}
