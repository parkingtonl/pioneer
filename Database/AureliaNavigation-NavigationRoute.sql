USE [AureliaNavigation]
GO
/****** Object:  Table [dbo].[NavigationRoute]    Script Date: 16/04/2018 11:09:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NavigationRoute](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[Route] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Icon] [varchar](10) NOT NULL,
	[ModuleId] [varchar](100) NOT NULL,
	[Nav] [bit] NOT NULL,
	[Title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_NavigationRoute] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[NavigationRoute] ON 

INSERT [dbo].[NavigationRoute] ([Id], [ParentId], [Route], [Name], [Icon], [ModuleId], [Nav], [Title]) VALUES (1, NULL, N'scheduler', N'scheduler', N'education', N'../scheduler/scheduler', 1, N'Scheduler')
INSERT [dbo].[NavigationRoute] ([Id], [ParentId], [Route], [Name], [Icon], [ModuleId], [Nav], [Title]) VALUES (2, NULL, N'newcustomer', N'newcustomer', N'education', N'../newcustomer/newcustomer', 0, N'New customer')
INSERT [dbo].[NavigationRoute] ([Id], [ParentId], [Route], [Name], [Icon], [ModuleId], [Nav], [Title]) VALUES (3, NULL, N'searchcustomers', N'searchcustomers', N'education', N'../searchcustomers/searchcustomers', 0, N'Search customers')
INSERT [dbo].[NavigationRoute] ([Id], [ParentId], [Route], [Name], [Icon], [ModuleId], [Nav], [Title]) VALUES (4, 3, N'#/accordion', N'accordion', N'th', N'../samples/accordion/accordion', 1, N'Accordion')
INSERT [dbo].[NavigationRoute] ([Id], [ParentId], [Route], [Name], [Icon], [ModuleId], [Nav], [Title]) VALUES (6, 3, N'#/qrcode', N'qrcode', N'th', N'../samples/qrcode/qrcode', 1, N'QRcode')
INSERT [dbo].[NavigationRoute] ([Id], [ParentId], [Route], [Name], [Icon], [ModuleId], [Nav], [Title]) VALUES (7, 3, N'#/grid', N'grid', N'th', N'../samples/grid/grid', 1, N'Grid')
INSERT [dbo].[NavigationRoute] ([Id], [ParentId], [Route], [Name], [Icon], [ModuleId], [Nav], [Title]) VALUES (8, NULL, N'counter', N'counter', N'home', N'../counter/counter', 1, N'Counter')
INSERT [dbo].[NavigationRoute] ([Id], [ParentId], [Route], [Name], [Icon], [ModuleId], [Nav], [Title]) VALUES (10, NULL, N'fetch-data', N'fetchdata', N'education', N'../fetchdata/fetchdata', 1, N'Fetch data')
INSERT [dbo].[NavigationRoute] ([Id], [ParentId], [Route], [Name], [Icon], [ModuleId], [Nav], [Title]) VALUES (11, 7, N'nquote', N'nquote', N'home', N'../nquote/quote', 1, N'NQuote')
SET IDENTITY_INSERT [dbo].[NavigationRoute] OFF
