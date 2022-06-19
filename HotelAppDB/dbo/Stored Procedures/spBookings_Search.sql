CREATE PROCEDURE [dbo].[spBookings_Search]
	@lastName varchar (50),
	@startDate date
AS
begin
set nocount on;

select b.Id, b.RoomId,b.StartDate,b.EndDate,b.CheckedIn,b.TotalCost, g.FirstName, g.LastName, r.RoomNumber, r.RoomTypeId, t.Title, t.Description
from dbo.Bookings b
join dbo.Guests g on g.Id = b.GuestId 
join dbo.Rooms r on b.RoomId = r.Id
join dbo.RoomTypes t on r.RoomTypeId = t.Id
where b.StartDate = @startDate and g.LastName = @lastName

end