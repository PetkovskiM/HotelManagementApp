CREATE PROCEDURE [dbo].[spRoomTypes_GetAvailableTypes]
	@startDate date,
	@endDate date
AS
begin 
	set nocount on;

	select t.Id, t.Title, t.Description, t.Price
from Rooms r
join RoomTypes t on r.RoomTypeId = t.Id
where r.Id not in 
(
	select b.RoomId
	from Bookings b
	where (@startDate < b.StartDate and @endDate > b.EndDate)
	or (@startDate >= b.StartDate  and @endDate < b.EndDate)
	or(@startDate < b.EndDate)  
)
group by t.Id, t.Title, t.Description, t.Price

end
