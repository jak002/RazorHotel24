using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorHotel24.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorHotel24.Services.Tests
{
    [TestClass()]
    public class HotelServiceTests
    {
        [TestMethod()]
        public void CreateHotelTest()
        {

            //Setup
            HotelService hotelService = new HotelService();
            
            //Test
            int countBefore = hotelService.GetAllHotel().Count();
            hotelService.CreateHotel(new(999, "Testhotel", "Testvej"));
            int countAfter = hotelService.GetAllHotel().Count();

            //Assert
            Assert.AreEqual(countBefore+1,countAfter);
        }
    }
}