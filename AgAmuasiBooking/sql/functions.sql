-- DROP VIEW public.vw_bookings;

CREATE OR REPLACE VIEW public.vw_bookings
 AS
 SELECT b.bookingsid,
    b.bookingdate,
    b.title,
    b.purpose,
    b.dates,
    b.guests,
    b.isreviewed,
    b.isapproved,    b.haspaid,
    b.days,
    bs.bookingservicesid,
    s.servicecostsid,
    s.cost,
    b.username
   FROM bookings b
     LEFT JOIN bookingservices bs ON bs.bookingsid = b.bookingsid
     LEFT JOIN ( SELECT c.servicecostsid,
            c.cost,
            row_number() OVER (PARTITION BY c.servicesid ORDER BY c.servicecostsid DESC) AS rn,
            s_1.servicename
           FROM servicecosts c
             JOIN services s_1 ON s_1.servicesid = c.servicesid) s ON s.servicecostsid = bs.servicecostsid AND s.rn = 1
  WHERE b.deleted = false;

ALTER TABLE public.vw_bookings
    OWNER TO postgres;

-- FUNCTION: public.fn_user_bookings(character varying)

-- DROP FUNCTION IF EXISTS public.fn_user_bookings(character varying);

CREATE OR REPLACE FUNCTION public.fn_user_bookings(
	id character varying)
    RETURNS TABLE(bookingsid uuid, bookingdate timestamp with time zone, title character varying, purpose character varying, dates timestamp with time zone[], guests smallint, isreviewed boolean, isapproved boolean, haspaid boolean, days smallint, bookingservicesid integer, servicecostsid integer, cost numeric) 
    LANGUAGE 'sql'
    COST 50
    STABLE PARALLEL SAFE 
    ROWS 20

AS $BODY$
SELECT bookingsid, bookingdate, title, purpose, dates, guests, isreviewed, isapproved, haspaid, days, bookingservicesid, servicecostsid, cost
FROM vw_bookings 
WHERE username = id;
$BODY$;

ALTER FUNCTION public.fn_user_bookings(character varying)
    OWNER TO postgres;
