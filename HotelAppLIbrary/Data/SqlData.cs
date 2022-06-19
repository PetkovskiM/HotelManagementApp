using HotelAppLIbrary.Databases;
using HotelAppLIbrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelAppLIbrary.Data
{
    public class SqlData : IDatabaseData
    {
        private readonly ISqlDataAccess _db;
        private const string connectionStringName = "SqlDb";
        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        public List<RoomTypeModel> GetAvailableRoomsTypes(DateTime startDate,
                                                          DateTime endDate)
        {
            var rooms = new List<RoomTypeModel>();
            rooms = _db.LoadData<RoomTypeModel, dynamic>("dbo.spRoomTypes_GetAvailableTypes",
                                                          new { startDate, endDate },
                                                          connectionStringName,
                                                          true);
            return rooms;
        }

        public void BookGuests(string firstName,
                               string lastName,
                               DateTime startDate,
                               DateTime endDate,
                               int roomTypeId)
        {
            GuestModel guest = _db.LoadData<GuestModel, dynamic>("dbo.spGuests_Insert",
                                                                 new { firstName, lastName },
                                                                 connectionStringName,
                                                                 true).First();

            var rooms = _db.LoadData<RoomModel, dynamic>("dbo.spRooms_GetAvailableRooms",
                                                         new { startDate, endDate, roomTypeId },
                                                         connectionStringName,
                                                         true);

            var roomType = _db.LoadData<RoomTypeModel, dynamic>("select * from dbo.RoomTypes where Id = @id",
                                                                 new { id = roomTypeId },
                                                                 connectionStringName,
                                                                 false).First();

            var timeStaying = endDate.Date.Subtract(startDate.Date);
            int days = timeStaying.Days;

            var totalCost = days * roomType.Price;

            _db.SaveData("dbo.spBookings_Insert",
                          new
                          {
                              roomId = rooms.First().Id,
                              guestId = guest.Id,
                              startDate = startDate,
                              endDate = endDate,
                              totalCost = totalCost
                          },
                          connectionStringName,
                          true);

        }

        public List<BookingFullModel> SearchBookings(string lastName)
        {
            return _db.LoadData<BookingFullModel, dynamic>("dbo.spBookings_Search",
                                                 new { startDate = DateTime.Now.Date, lastName },
                                                 connectionStringName,
                                                 true);


        }

        public void CheckInGuest(int bookingId)
        {
            _db.SaveData("dbo.spBookings_CheckIn",
                         new { Id = bookingId },
                         connectionStringName,
                         true);
        }

        public RoomTypeModel GetRoomTypeById(int id)
        {
            
            return _db.LoadData<RoomTypeModel, dynamic>("sp_RoomTypes_GetById", new {id }, connectionStringName, true).FirstOrDefault();
        }
    }
}
