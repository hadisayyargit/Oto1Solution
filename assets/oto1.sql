use khordadn_oto1



/*
select *,g.GeoLocation.ToString() , g.GeoShape.ToString() 
from AddressLocation g
*/

/*
DECLARE @line geometry;
SET @line = geometry::STGeomFromText('LINESTRING(-10 -5, 5 1)', 0);
select @line ,@line.ToString();

DECLARE @line2 geometry;
SET @line2 = geometry::STLineFromText('LINESTRING(-1000 -5, 5 1000)', 0);
select @line2;
--STPointFromText
--STPolyFromText


declare @poly geometry
set @poly=geometry::STGeomFromText('POLYGON((-122.358 47.653 , -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326)
select @poly

declare @poly2 geometry
set @poly2='POLYGON((0 0 , 5 5, 12 3, 0 0, 11 7, 0 0))';
select @poly2

declare @six geometry
declare @nine geometry
set @six='CIRCULARSTRING(0 4, 4 8, 8 4, 4 0,0 4, 1 10,16 15)';
--select @six


set @nine='CIRCULARSTRING(4 0, 0 -4, -4 0, 0 4,4 0, -2 -8 , -10 -6 )';

select  @six, @nine


declare @curve geometry
set @curve='CurveSTRING(0 4, 4 8, 8 4)';
select @curve
*/

/*
INSERT INTO AddressLocation (LocationTitle,geolocation)  
VALUES ('point_geography',geography::STGeomFromText('point(-122.360 47.656)', 4326));  


 --  this is a Equal to up
INSERT INTO AddressLocation (LocationTitle,geolocation)  
VALUES ('point_geography','point(-122.360 47.656)');  

INSERT INTO AddressLocation (LocationTitle,geoshape)  
VALUES ('point_geometry',geometry::STGeomFromText('point(-122.360 47.656)', 4326));  

INSERT INTO AddressLocation (LocationTitle,geoshape)  
VALUES ('line_geometry',geometry::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656 )', 4326));  
  
INSERT INTO AddressLocation (LocationTitle,geoshape)  
VALUES ('polygon_geometry',geometry::STGeomFromText('POLYGON((-122.358 47.653 , -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326));  
GO  
*/

/*
DECLARE @geog1 geography;  
DECLARE @geog2 geography;  
DECLARE @result geography;  
  
SELECT @geog1 = GeogCol1 FROM SpatialTable WHERE id = 1;  
SELECT @geog2 = GeogCol1 FROM SpatialTable WHERE id = 2;  
SELECT @result = @geog1.STIntersection(@geog2);  
SELECT @result.STAsText();  
-----------------
DECLARE @g1 geometry;
DECLARE @g2 geometry;
SET @g1 = geometry::STGeomFromText('LINESTRING(1 1, 5 1, 3 5, 1 1)', 0);
SET @g2 = geometry::STGeomFromText('CIRCULARSTRING(1 1, 3 1, 5 1, 4 3, 3 5, 2 3, 1 1)', 0);
IF @g1.STIsValid() = 1 AND @g2.STIsValid() = 1
  BEGIN
      SELECT @g1.ToString(), @g2.ToString()
      SELECT @g1.STLength() AS [LS Length], @g2.STLength() AS [CS Length]
  END

  */

 /*
  select * from 
  product

declare @i int=9
declare @pname nvarchar(10)
while (@i<100) 
begin
set @pname='P'+CAST(@i as varchar(3))
insert into product([ProductId],[PName],[ProductType],[ManufacturerId],[IsActive])
values(@i,@pname,1,1,1)
set @i=@i+1
end
*/

select * from basicprime
select * from car
select * from vendor
select * from part
select * from customer

select * from customer_car



select * from 
-- delete 
-- truncate table  
CustomerService
where serviceid =98

select * from 
-- truncate table  
CustomerServiceItem

 --dotnet publish -c Release -f:net7.0-android33.0


-- sp_SearchProducts @carid=1  --@productid=1






select [dbo].[fn_GetServiceId](1,1,'2024-10-18',30,0)


sp_GetCustomerServiceItems @serviceid = -1, @customerid=1, @manufacturerid=-1, @vendorid=-1,@ticketdatetime='2000-1-1',@servicedatetime='2000-1-1',@servicestatus=0,@openservices=true



select * from LocationAddress
		

select * from AppUser
select * from Customer
select * from Vendor
select * from Person


select * from Vendor_Part
where PartId=1 and VendorId=1


[dbo].[sp_GetPart] @partid=1, @vendorid=1,@pricedate='2025-03-21'

