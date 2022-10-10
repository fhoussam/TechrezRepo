--function example : returns stock for a given product id

alter function get_product_stock(@product_id int)
returns int 
as
begin
	declare @result int = 0;
	select @result = p.UnitsInStock from products p where p.ProductID =  @product_id
	return @result 
end
go

select p.ProductName, dbo.get_product_stock(p.ProductID) as Stock from Products p
order by 2 desc