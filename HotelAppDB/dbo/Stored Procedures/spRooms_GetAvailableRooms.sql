CREATE PROCEDURE [dbo].[spRooms_GetAvailableRooms]
	@startDate date,
	@endDate date,
	@roomTypeId int
AS
	begin 
	set nocount on;

	select r.*
	from Rooms r
	join RoomTypes t on r.RoomTypeId = t.Id
	where r.RoomTypeId = @roomTypeId
	and
	r.Id not in 
	(
		select b.RoomId
		from Bookings b
		where (@startDate < b.StartDate and @endDate > b.EndDate)
		or (@startDate >= b.StartDate  and @endDate < b.EndDate)
		or(@startDate < b.EndDate)  
	)
	


	end

