using Microsoft.Data.SqlClient;
using RazorHotel24.Interfaces;
using RazorHotel24.Models;
using System.Data;

namespace RazorHotel24.Services
{
    public class HotelService : Connection, IHotelService
    {
        private string queryString = "SELECT Hotel_No, Name, Address FROM Hotel";
        private string getSql = "SELECT Hotel_No, Name, Address FROM Hotel WHERE Hotel_No = @ID";
        private string getName = "SELECT Hotel_No, Name, Address FROM Hotel WHERE Name = '@Name'";
        private string insertSql = "INSERT INTO Hotel VALUES (@ID, @Navn, @Adresse)";
        private string deleteSql = "DELETE FROM Hotel WHERE Hotel_No = @ID";
        private string updateSql = "UPDATE Hotel SET Name = @Navn, Address = @Adresse WHERE Hotel_No = @ID";
        public bool CreateHotel(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine("General fejl" + ex.Message);
                    throw ex;
                }
                finally
                {

                }
            }
            //return false;
        }

        public Hotel DeleteHotel(int hotelNr)
        {
            Hotel hotel = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    hotel = GetHotelFromId(hotelNr);
                    if (hotel != null)
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
                catch (Exception ex) 
                { 
                    Console.WriteLine("General fejl" + ex.Message);
                    throw ex;
                }
                finally
                {

                }
            }
            return hotel;
        }

        public List<Hotel> GetAllHotel()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int hotelNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine("General fejl" + ex.Message);
                    throw ex;
                }
                finally
                {

                }
            }
            return hoteller;
        }

        public Hotel GetHotelFromId(int hotelNr)
        {
            Hotel hotel = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(getSql, connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                    }
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex) { Console.WriteLine("General fejl" + ex.Message);
                    throw ex;
                }
                finally
                {

                }
            }
            return hotel;
        }

        public List<Hotel> GetHotelsByName(string name)
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(getName, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int hotelNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
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
            return hoteller;
        }

        public bool UpdateHotel(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
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
