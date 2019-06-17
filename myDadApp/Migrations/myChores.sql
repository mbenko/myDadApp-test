if not exists (select * from sys.views where object_id = OBJECT_ID(N'[dbo].[v_myChores]'))
exec dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[v_myChores] 
AS 
with myChores_CTE (Id, Owner, Title, Notes, Depth, Sort) as
(
	select Id, Owner, convert(nvarchar(255),Title), Notes, 0 as Depth, Convert(nvarchar(255), Title) as Sort
	from chores s
	where Owner is null
	UNION ALL
	select e.Id, e.OwnerId, 
		convert(nvarchar(255), Replicate('>  ', depth+1) + e.Title), e.Notes, m.Depth + 1, 
		Convert(nvarchar(255), rtrim(sort) + ('>  ' + e.Title))
	from chores e
		INNER JOIN myChores_CTE m 
		on e.OwnerId = m.Id
)
select * from myChores_CTE
'
