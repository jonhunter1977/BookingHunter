BookingHunter
=============

I play squash at a local club where if you want to book a court you have to drive to the club and check the booking sheet(s).  If a court is available you can then book it there and then.

Living quite a distance from the club this is a pain for me so I've offered to write them a booking system and at the same time use it as an opportunity to learn some C#, dot net, web API and mobile application creation.

The way I'm planning on approaching this at the moment is as follows:

1) Build a DLL with the following layers:

  a) Data access layer

  b) Business logic layer
  
2) Use the Microsoft Web API to offer the business logic layer via RESTFul web services in a variety of data formats (XML, JSON etc)

I'd like to deliver the following functionality:


1) An administration section to create Clubs/Locations.  Facilities (e.g. Squash/Tennis) can be created against a location.  Courts can then be created for these facilities.

2) A website where members can register, view available courts and make bookings

3) A mobile app (maybe HTML 5?) so members can do the same as the website.

4) A touch screen application which will be at the club where members turn up and can mark that they have attended a booking.  They can also make bookings from here as well.


