CREATE OR REPLACE FUNCTION fn_user_bookings(id VARCHAR(70))
RETURNS TABLE(bookingsid UUID, bookingdate TIMESTAMP WITH TIME ZONE ,title VARCHAR(50),  purpose VARCHAR(200), dates TIMESTAMP WITH TIME ZONE[],guests smallint ,isreviewed boolean ,isapproved BOOLEAN, haspaid BOOLEAN, days SMALLINT,bookingservicesid int ,servicecostsid int , "cost" numeric)
PARALLEL SAFE STABLE
COST 50
ROWS 20 
LANGUAGE 'sql'
AS $$
SELECT b.bookingsid, b.bookingdate, b.title, b.purpose, b.dates, b.guests, b.isreviewed, b.isapproved, b.haspaid, b.days, bs.bookingservicesid, s.servicecostsid, s.cost
FROM bookings b
LEFT JOIN bookingservices bs ON bs.bookingsid = b.bookingsid
LEFT JOIN (
	SELECT c.servicecostsid, c."cost", ROW_NUMBER() OVER(PARTITION BY c.servicesid ORDER BY c.servicecostsid DESC) rn, s.servicename
	FROM servicecosts c
	INNER JOIN services s ON s.servicesid = c.servicesid
	) s ON s.servicecostsid = bs.servicecostsid
WHERE s.rn = 1 AND b.deleted = FALSE AND b.username = id;
$$

