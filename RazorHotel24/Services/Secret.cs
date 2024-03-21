namespace RazorHotel24.Services
{
    public class Secret
    {
        private static string _connectionString = ""; /*@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";*/

        public static string ConnectionString { get { return _connectionString; } }
    }
}
