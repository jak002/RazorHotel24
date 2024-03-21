using Microsoft.Data.SqlClient;
using RazorHotel24.Interfaces;
using RazorHotel24.Models;
using System.Data;

namespace RazorHotel24.Services
{
    public class RoomService : Connection, IRoomService
    {

        private string getSql = "SELECT Types, Price FROM Room WHERE Hotel_No = @HID AND Room_No = @RID";
        private string getAllSql = "SELECT Room_No,  Types, Price FROM Room WHERE Hotel_No = @HID";
        private string insertSql = "INSERT INTO Room VALUES (@RID, @HID, @Type, @Price)";
        private string deleteSql = "DELETE FROM Room WHERE Room_No = @RID AND Hotel_No = @HID";
        private string updateSql = "UPDATE Room SET Types = @Type, Price = @Price WHERE Room_No = @RID AND Hotel_No = @HID";
        public bool CreateRoom(int hotelNr, Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@HID", hotelNr);
                    command.Parameters.AddWithValue("@RID", room.RoomNr);
                    command.Parameters.AddWithValue("@Type", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex) { Console.WriteLine("General fejl" + ex.Message); throw ex; }
                finally
                {

                }
            }
            //return false;
        }

        public Room DeleteRoom(int roomNr, int hotelNr)
        {
            Room foundroom = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@HID", hotelNr);
                    command.Parameters.AddWithValue("@RID", roomNr);
                    foundroom = GetRoomFromId(roomNr, hotelNr);
                    if (foundroom != null)
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex) { Console.WriteLine("General fejl" + ex.Message); throw ex; }
                finally
                {

                }
            }
            return foundroom;
        }

        public List<Room> GetAllRoom(int hotelNr)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(getAllSql, connection);
                    command.Parameters.AddWithValue("@HID", hotelNr);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int roomNr = reader.GetInt32("Room_No");
                        char roomType = reader.GetString("Types").First();
                        double roomPrice = reader.GetDouble("Price");
                        Room room = new Room(roomNr, roomType, roomPrice, hotelNr);
                        rooms.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex) { Console.WriteLine("General fejl" + ex.Message); throw ex; }
                finally
                {

                }
            }
            return rooms;
        }

        public Room GetRoomFromId(int roomNr, int hotelNr)
        {
            Room foundroom = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(getSql, connection);
                    command.Parameters.AddWithValue("@HID", hotelNr);
                    command.Parameters.AddWithValue("@RID", roomNr);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        char roomType = reader.GetString("Types").First();
                        double roomPrice = reader.GetDouble("Price");
                        foundroom = new Room(roomNr, roomType, roomPrice, hotelNr);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex) { Console.WriteLine("General fejl" + ex.Message); throw ex; }
                finally
                {

                }
            }
            return foundroom;
        }

        public bool UpdateRoom(Room room, int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@RID", roomNr);
                    command.Parameters.AddWithValue("@HID", hotelNr);
                    command.Parameters.AddWithValue("@Type", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex) { Console.WriteLine("General fejl" + ex.Message); throw ex; }
                finally
                {

                }
            }
            //return false;
        }
    }
}
