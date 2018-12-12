USE [doctor_helper]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[patient](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[surname] [varchar](50) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[patronymic] [varchar](50) NOT NULL,
	[login] [varchar](50) NOT NULL,
	[password] [varchar](100) NOT NULL,
 CONSTRAINT [PK_patient] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


