USE [doctor_helper]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[doctor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[surname] [varchar](50) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[patronymic] [varchar](50) NOT NULL,
	[hospital_id] [int] NOT NULL,
	[login] [varchar](50) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[monday_shift] [int] NOT NULL,
	[tuesday_shift] [int] NOT NULL,
	[wednesday_shift] [int] NOT NULL,
	[thursday_shift] [int] NOT NULL,
	[friday_shift] [int] NOT NULL,
 CONSTRAINT [PK_doctor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[doctor]  WITH CHECK ADD FOREIGN KEY([hospital_id])
REFERENCES [dbo].[hospital] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[doctor]  WITH CHECK ADD  CONSTRAINT [CK_doctor_friday] CHECK  (([friday_shift]>=(1) AND [friday_shift]<=(2)))
GO

ALTER TABLE [dbo].[doctor] CHECK CONSTRAINT [CK_doctor_friday]
GO

ALTER TABLE [dbo].[doctor]  WITH CHECK ADD  CONSTRAINT [CK_doctor_monday] CHECK  (([monday_shift]>=(1) AND [monday_shift]<=(2)))
GO

ALTER TABLE [dbo].[doctor] CHECK CONSTRAINT [CK_doctor_monday]
GO

ALTER TABLE [dbo].[doctor]  WITH CHECK ADD  CONSTRAINT [CK_doctor_thursday] CHECK  (([thursday_shift]>=(1) AND [thursday_shift]<=(2)))
GO

ALTER TABLE [dbo].[doctor] CHECK CONSTRAINT [CK_doctor_thursday]
GO

ALTER TABLE [dbo].[doctor]  WITH CHECK ADD  CONSTRAINT [CK_doctor_tuesday] CHECK  (([tuesday_shift]>=(1) AND [tuesday_shift]<=(2)))
GO

ALTER TABLE [dbo].[doctor] CHECK CONSTRAINT [CK_doctor_tuesday]
GO

ALTER TABLE [dbo].[doctor]  WITH CHECK ADD  CONSTRAINT [CK_doctor_wednesday] CHECK  (([wednesday_shift]>=(1) AND [wednesday_shift]<=(2)))
GO

ALTER TABLE [dbo].[doctor] CHECK CONSTRAINT [CK_doctor_wednesday]
GO


