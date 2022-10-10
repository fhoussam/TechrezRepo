--procedure with out param, CTE, if and case when

alter procedure myProcedure
	@start_with_1 varchar(200), @start_with_2 varchar(20), @use_first bit = 1,
	@is_total_count_even bit output
	as
	if(@use_first = 1)
	begin
		;WITH start_with_first_cte AS(
		select ProductID, ProductName, CategoryID from Products
		where ProductName like @start_with_1 + '%')
		select * from start_with_first_cte
	end
	else
	begin
		;with start_with_second_cte as(
		select ProductID, ProductName, CategoryID from Products
		where ProductName like @start_with_2 + '%')
		select * from start_with_second_cte
	end
	set @is_total_count_even = case when (@@ROWCOUNT % 2 = 0) then 1 else 0 end
go

declare @is_total_count_even bit
exec myProcedure 'ch', 'lo', 1, @is_total_count_even out
select @is_total_count_even
go

