drop database custom_zfile;
create database custom_zfile;
use custom_zfile;

create table user(
id int not null auto_increment primary key,
username varchar(100),
password varchar(100),
creation_time datetime
);

create table drive(
id int not null auto_increment primary key,
name varchar(100),
net_address varchar(100),
creation_time datetime,
creator_id int,
password varchar(100) default "",
foreign key(creator_id) references user(id)
);

create table drive_allow_user(
drive_id int,
user_id int,
allow_read int,
allow_modify int,
allow_download int,
allow_upload int,
primary key (drive_id, user_id),
foreign key (drive_id) references drive(id),
foreign key (user_id) references user(id)
);

insert into user values(null, "root", "root", NOW());
insert into user values(null, "guest", "guest", NOW());
insert into drive values(null, "Default", "127.0.0.1", NOW(), 1, "");
insert into drive_allow_user values(1, 2, 1, 1, 1, 1);
