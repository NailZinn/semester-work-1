create trigger ID_Accounts on Accounts
instead of delete as
begin
	delete from Posts
	where AccountId in (select Id from deleted)
	delete from Accounts
	where Id in (select Id from deleted)
end