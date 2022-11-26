--ACCOUNTS
create table Accounts(
	Id int not null identity,
	Login varchar(20) not null unique,
	Password varchar(100) not null,
	Email varchar(50) not null unique,
	constraint pk_account_id
	primary key(Id)
)

--POSTS
create table Posts(
	Id int not null identity,
	Date datetime not null,
	Content varchar(1000) not null,
	AccountId int not null,
	constraint pk_posts_id
	primary key(Id),
	constraint fk_posts_to_account_id
	foreign key(AccountId)
	references Accounts(Id)
)

--COMMENTS
create table Comments(
	Id int not null identity,
	Date datetime not null,
	Content varchar(200) not null,
	AccountId int not null,
	PostId int not null,
	constraint pk_comments_id
	primary key(Id),
	constraint fk_comments_to_account_id
	foreign key(AccountId)
	references Accounts(Id)
	on delete cascade,
	constraint fk_post_id
	foreign key(PostId)
	references Posts(Id)
	on delete cascade
)

--EVENTS
create table Events(
	Id int not null identity,
	Date datetime not null,
	Name varchar(50) not null,
	EventType varchar(20) not null,
	TeamA varchar(50) not null,
	TeamB varchar(50) not null,
	constraint pk_events_id
	primary key(Id)
)

--SESSIONS
create table Sessions(
	Id uniqueidentifier not null,
	AccountId int not null,
	Login varchar(20) not null,
	Password varchar(100) not null,
	constraint fk_session_to_account_id
	foreign key(AccountId)
	references Accounts
	on delete cascade
)