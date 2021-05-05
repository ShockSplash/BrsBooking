    create table hotel(
  id serial primary key,
  name varchar(40) not null,
  city varchar(40) not null,
  description varchar(200) not null,
  isFreeRooms bool not null);

create table room(
  id serial primary key,
  h_id integer references hotel(id) on delete cascade,
  seats integer not null,
  price float not null);

create table users(
  id serial primary key,
  name varchar(60) not null,
  login varchar(60) not null,
  password varchar(60) not null);
  
  create table booking(
  id serial primary key,
  idOfRoom integer references room(id) on delete cascade,
  beginDate date not null,
  endDate date not null,
  userId integer references users(id) on delete cascade
  );