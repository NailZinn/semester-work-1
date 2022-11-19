--USERS
create table Users(
	Id int not null identity,
	Login varchar(20) not null unique,
	Password varchar(16) not null,
	Email varchar(50) not null unique,
	TelephoneNumber varchar(12) not null unique,
	constraint pk_user_id
	primary key(Id)
)

--POSTS
create table Posts(
	Id int not null identity,
	Date date not null,
	Content varchar(1000) not null,
	UserId int not null,
	constraint pk_posts_id
	primary key(Id),
	constraint fk_posts_to_user_id
	foreign key(UserId)
	references Users(Id)
)

--COMMENTS
create table Comments(
	Id int not null identity,
	Date date not null,
	Content varchar(200) not null,
	UserId int not null,
	PostId int not null,
	constraint pk_comments_id
	primary key(Id),
	constraint fk_comments_to_user_id
	foreign key(UserId)
	references Users(Id)
	on delete cascade,
	constraint fk_post_id
	foreign key(PostId)
	references Posts(Id)
	on delete cascade
)

--EVENTS
create table Events(
	Id int not null identity,
	Date date not null,
	Name varchar(50) not null,
	EventType varchar(20) not null,
	TeamA varchar(50) not null,
	TeamB varchar(50) not null,
	constraint pk_events_id
	primary key(Id)
)