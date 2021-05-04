insert into hotel (id,name, city, description,isFreeRooms)
  values (1, 'KazanHotel', 'Kazan', 'nice hotel in the middle of old Kazan', true);
 insert into hotel (id,name, city, description,isFreeRooms)
  values (2, 'Regina', 'Kazan', 'expensive and luxury hotel', true);
 insert into hotel (id,name, city, description,isFreeRooms)
  values (3, 'Metropol', 'Moscow', 'one of the best hoels in the world', true);
 insert into hotel (id,name, city, description,isFreeRooms)
  values (4, 'MoyHostel', 'Moscow', 'cheap and cozy hotel on Krasnaya Presnya', true);
 insert into hotel (id,name, city, description,isFreeRooms)
  values (5, 'Marabu', 'Kazan', 'hotel for large families', true);

insert into room (id, h_id, seats, price, isFree)
  values (1, 1, 2, 1200, true);

insert into users (id,name, login, password)
  values (1, 'Richard Hendrix', 'piedpiper', 'siliconvalley');
 insert into users (id,name, login, password)
  values (2, 'Tomas Anderson', 'Neo', 'matrix');
 insert into users (id,name, login, password)
  values (3, 'Elliot Alderson', 'mrrobot', 'fucksociety');
 insert into users (id,name, login, password)
  values (4, 'Test User', 'test', '12345');


insert into booking (idofroom,begindate, enddate, userid)
  values (1, '2021-01-08', '2021-02-08', 1);