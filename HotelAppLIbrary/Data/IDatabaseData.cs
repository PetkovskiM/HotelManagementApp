using HotelAppLIbrary.Models;
using System;
using System.Collections.Generic;

namespace HotelAppLIbrary.Data
{
    public interface IDatabaseData
    {
        void BookGuests(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId);
        void CheckInGuest(int bookingId);
        List<RoomTypeModel> GetAvailableRoomsTypes(DateTime startDate, DateTime endDate);
        RoomTypeModel GetRoomTypeById(int id);
        List<BookingFullModel> SearchBookings(string lastName);
    }
}