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

        insert into room (id,h_id, seats, price)
  values (1, 1, 2, 1200);
  insert into room (id,h_id, seats, price)
  values (2, 1, 3, 3300);
  insert into room (id,h_id, seats, price)
  values (3, 1, 1, 1300);
  insert into room (id,h_id, seats, price)
  values (4, 1, 4, 6300);
  insert into room (id,h_id, seats, price)
  values (5, 2, 1, 1500);
  insert into room (id,h_id, seats, price)
  values (6, 2, 2, 3500);
  insert into room (id,h_id, seats, price)
  values (7, 2, 3, 5600);
  insert into room (id,h_id, seats, price)
  values (8, 2, 2, 4700);
  insert into room (id,h_id, seats, price)
  values (10, 3, 1, 14800);
  insert into room (id,h_id, seats, price)
  values (11, 3, 2, 10200);
  insert into room (id,h_id, seats, price)
  values (12, 3, 2, 16900);
  insert into room (id,h_id, seats, price)
  values (13, 3, 3, 13800);
  insert into room (id,h_id, seats, price)
  values (14, 3, 3, 20000);
  insert into room (id,h_id, seats, price)
  values (15, 3, 4, 18000);
  insert into room (id,h_id, seats, price)
  values (16, 4, 1, 5000);
  insert into room (id,h_id, seats, price)
  values (17, 4, 2, 8000);
  insert into room (id,h_id, seats, price)
  values (18, 4, 3, 10000);
  insert into room (id,h_id, seats, price)
  values (19, 4, 4, 14000);
  insert into room (id,h_id, seats, price)
  values (20, 4, 5, 15000);

insert into users (id,name, login, password)
  values (1, 'Richard Hendrix', 'piedpiper', 'siliconvalley');
 insert into users (id,name, login, password)
  values (2, 'Tomas Anderson', 'Neo', 'matrix');
 insert into users (id,name, login, password)
  values (3, 'Elliot Alderson', 'mrrobot', 'fucksociety');
 insert into users (id,name, login, password)
  values (4, 'Test User', 'test', '12345');

insert into booking (h_id, idofroom,begindate, enddate, userid)
  values (1, 1,'2021-01-08', '2021-02-08', 1);
  insert into booking (h_id, idofroom,begindate, enddate, userid)
  values (1, 1,'2021-01-09', '2021-02-10', 1);
 insert into booking (h_id, idofroom,begindate, enddate, userid)
  values (1, 1,'2021-05-04', '2021-05-08', 1);
 insert into booking (h_id, idofroom,begindate, enddate, userid)
  values (2, 3,'2021-05-04', '2021-05-08', 2);