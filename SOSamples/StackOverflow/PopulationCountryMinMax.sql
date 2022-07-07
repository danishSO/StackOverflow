USE [StackOverflow]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountryPopulation]') AND type in (N'U'))
DROP TABLE [dbo].[CountryPopulation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CountryPopulation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Continent] [nvarchar](max) NULL,
	[Population] [int] NULL,
 CONSTRAINT [PK_CountryPopulation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [StackOverflow]
GO
SET IDENTITY_INSERT [dbo].[CountryPopulation] ON 
GO
INSERT [dbo].[CountryPopulation] ([ID], [Name], [Continent], [Population]) VALUES (1, N'C1', N'Asia', 100)
GO
INSERT [dbo].[CountryPopulation] ([ID], [Name], [Continent], [Population]) VALUES (2, N'C2', N'Asia', 200)
GO
INSERT [dbo].[CountryPopulation] ([ID], [Name], [Continent], [Population]) VALUES (3, N'C3', N'Asia', 300)
GO
INSERT [dbo].[CountryPopulation] ([ID], [Name], [Continent], [Population]) VALUES (4, N'C4', N'Europe', 100)
GO
INSERT [dbo].[CountryPopulation] ([ID], [Name], [Continent], [Population]) VALUES (5, N'C5', N'Europe', 200)
GO
INSERT [dbo].[CountryPopulation] ([ID], [Name], [Continent], [Population]) VALUES (6, N'C6', N'Europe', 300)
GO
INSERT [dbo].[CountryPopulation] ([ID], [Name], [Continent], [Population]) VALUES (7, N'C7', N'Africa', 100)
GO
INSERT [dbo].[CountryPopulation] ([ID], [Name], [Continent], [Population]) VALUES (8, N'C8', N'Africa', 200)
GO
INSERT [dbo].[CountryPopulation] ([ID], [Name], [Continent], [Population]) VALUES (9, N'C9', N'Africa', 200)
GO
SET IDENTITY_INSERT [dbo].[CountryPopulation] OFF
GO


-- Get data we need 

SELECT	MinMax.Continent, 
		ForLeast.[name] AS LeastPopulous, 
		ForMost.[name] AS MostPopulous
FROM	CountryPopulation ForMost JOIN 
		CountryPopulation ForLeast JOIN
		(	SELECT	DISTINCT Continent, 
					MIN([population]) OVER(PARTITION BY continent) AS LeastPopulation,
					MAX([population]) OVER(PARTITION BY continent) AS MaxPopulation
			FROM	CountryPopulation) MinMax
			
			ON ForLeast.Continent = MinMax.Continent AND ForLeast.[Population] = MinMax.LeastPopulation 
			ON ForMost.Continent = MinMax.Continent AND ForMost.[Population] = MinMax.MaxPopulation