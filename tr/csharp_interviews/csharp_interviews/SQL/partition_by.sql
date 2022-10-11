select 
	p.ProductID, 
	p.ProductName, 
	c.CategoryName,
	--counting cateogries for each row
	count(c.CategoryID) over (partition by c.CategoryID) as TotalCategories
from Products p
join Categories c on p.CategoryID = c.CategoryID