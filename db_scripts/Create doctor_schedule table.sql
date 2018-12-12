USE [doctor_helper]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[doctor_schedule](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[doctor_id] [int] NOT NULL,
	[consultation_date] [date] NOT NULL,
	[consultation_time] [time](7) NOT NULL,
	[is_free] [bit] NOT NULL,
	[patient_id] [int],
 CONSTRAINT [PK_doctor_schedule] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[doctor_schedule]  WITH CHECK ADD FOREIGN KEY([doctor_id])
REFERENCES [dbo].[doctor] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[doctor_schedule]  WITH CHECK ADD FOREIGN KEY([patient_id])
REFERENCES [dbo].[patient] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO


