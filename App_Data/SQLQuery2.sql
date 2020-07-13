use [D:\projects programs\Visual2017\web asp\publishfan\publishfan\App_DataAN\PUBLISHFAN\APP_DATA\MYDATA.MDF]



create table publishfan_users
(
user_numero varchar(50) primary key ,
firstname varchar(20),
lastname varchar(20),
email varchar(50),
pass_word varchar(30),
age varchar(5),
gender varchar(10)
)
insert into user_articles values ('Nature','this is about Nature','2000')
insert into user_articles values ('Fire','this is about Fire','2000')
select * from publishfan_users
create table user_articles
(
article_title varchar(50),
article_Description varchar(200),
article_id int identity primary key,
user_numero varchar(50) foreign key references publishfan_users(user_numero) 
)

go
use [C:\USERS\AZIZMOBARAK\SOURCE\REPOS\PUBLISHFAN\PUBLISHFAN\APP_DATA\MYDATA.MDF]

go
create table article_details
(
article_id int  foreign key references  user_articles(article_id ),
article_text varchar(4000),
number_likes bigint,
number_comments bigint,
number_visits bigint
)
go
create table article_comments
(
article_id int  foreign key references  user_articles(article_id ),
user_name_ac varchar(30),
comment_text varchar(2000)
)
go
create table user_interest
(
user_numero varchar(50) foreign key references publishfan_users(user_numero) ,
interests varchar(200)
)

SELECT * from publishfan_users ps join user_interest ui on ps.user_numero=ui.user_numero



 
