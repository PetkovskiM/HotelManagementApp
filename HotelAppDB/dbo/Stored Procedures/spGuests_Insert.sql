﻿CREATE PROCEDURE [dbo].[spGuests_Insert]

	@firstName varchar(50),
	@lastName varchar(50)
AS
	begin
	set nocount on;

	if not exists (select 1 from dbo.Guests where FirstName = @firstName and LastName = @lastName)
		begin
			insert into Guests(FirstName, LastName)
			values (@firstName, @lastName);
		end

		select [Id], [FirstName], [LastName] 
		from dbo.Guests
		where FirstName = @firstName and LastName = @lastName;

	end

