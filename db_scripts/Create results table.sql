USE [doctor_helper]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[results](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[patient_id] [int] NOT NULL,
	[investigation_date] [date] NOT NULL,
	[hgb] [float] NOT NULL,
	[rbc] [float] NOT NULL,
	[wbc] [float] NOT NULL,
	[eo] [float] NOT NULL,
	[ba] [float] NOT NULL,
	[ne] [float] NOT NULL,
	[mo] [float] NOT NULL,
	[atl] [float] NOT NULL,
	[plt] [float] NOT NULL,
	[soe] [float] NOT NULL,
	[color] [varchar] (50) NOT NULL,
	[hct] [float] NOT NULL,
 CONSTRAINT [PK_results] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[results]  WITH CHECK ADD FOREIGN KEY([patient_id])
REFERENCES [dbo].[patient] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO


