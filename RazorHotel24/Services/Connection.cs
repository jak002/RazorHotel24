using Microsoft.AspNetCore.DataProtection;

namespace RazorHotel24.Services
{
    public class Connection
    {
        protected String connectionString = Secret.ConnectionString;
    }
}
