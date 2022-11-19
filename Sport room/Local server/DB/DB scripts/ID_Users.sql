create trigger ID_Users on Users
instead of delete as
begin
	delete from Posts
	where UserId in (select Id from deleted)
	delete from Users
	where Id in (select Id from deleted)
end