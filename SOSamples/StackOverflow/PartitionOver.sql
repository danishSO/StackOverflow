-- How many rows per line id exists in the result? Sequences them per line id

USE [StackOverflow]
GO

/****** Object:  Table [dbo].[RunningCount]    Script Date: 7/6/2022 5:33:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RunningCount]') AND type in (N'U'))
DROP TABLE [dbo].[RunningCount]
GO

/****** Object:  Table [dbo].[RunningCount]    Script Date: 7/6/2022 5:33:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RunningCount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LineId] [int] NULL,
	[Price] [int] NULL
) ON [PRIMARY]
GO



SELECT ROW_NUMBER() over(partition by [LineId] order by [LineId]) as DocId,
      [LineId],
      [Price]
  FROM [StackOverflow].[dbo].[RunningCount] order by [LineId]