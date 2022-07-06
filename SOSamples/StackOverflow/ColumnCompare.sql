-- Compare dates in 3 columns and use the most recent one in one date column. did not create table for this one
SELECT TOP 1	Machines.[Id],
				Machines.[Location],
				(	SELECT MAX(LastUpdateDate) 
					FROM (VALUES (Machines.[Date]), (Systems.[Date]), (Laptops.[Date])) AS UpdateDate(LastUpdateDate)) AS LastUpdateDate
FROM			dbo.ISD_machines Machines 
	INNER JOIN	dbo.ISD_systems Systems
					ON a.HOSTNAME = b.SYSTEM_NAME
	INNER JOIN	dbo.ISD_laptops Laptops
					ON a.HOSTNAME = c.[NAME] 
ORDER BY LastUpdateDate desc