USE [Airport]
------------------------------------------- CREATE TABLE [Plane] ------------------------------------------
CREATE TABLE [dbo].[Plane](
	[ID самолета]        [int] IDENTITY(1,1) NOT NULL,
	[Тип]                [nvarchar](50) NULL,
	[Модель]             [nvarchar](50) NULL,
	[Количество мест]    [int] NULL,
	[Грузоподъемность]   [int] NULL,
	[Дата последнего ТО] [nvarchar](50) NULL,
	CONSTRAINT [PK_Plane] PRIMARY KEY([ID самолета] ASC)
)ON [PRIMARY]

----------------------------------------- CREATE TABLE [Passenger] ----------------------------------------
CREATE TABLE [dbo].[Passenger](
	[Номер паспорта]          [nvarchar](50) NOT NULL,
	[Идентификационный номер] [nvarchar](50) NULL,
	[Орган, выдавший паспорт] [nvarchar](50) NULL,
	[Дата выдачи]             [nvarchar](50) NULL,
	[ФИО]                     [nvarchar](50) NULL,
	CONSTRAINT [PK_Passenger] PRIMARY KEY([Номер паспорта] ASC)
)ON [PRIMARY]

------------------------------------------- CREATE TABLE [Flight] -----------------------------------------
CREATE TABLE [dbo].[Flight](
	[ID авиарейса]      [int] IDENTITY(1,1) NOT NULL,
	[ID самолета]       [int] NULL,
	[Авиакомпания]      [nvarchar](50) NULL,
	[Аэропорт прибытия] [nvarchar](50) NULL,
	[Дата отправления]  [nvarchar](50) NULL,
	[Дата прибытия]     [nvarchar](50) NULL,
	CONSTRAINT [PK_Flight] PRIMARY KEY([ID авиарейса] ASC),
	CONSTRAINT [FK_Flight] FOREIGN KEY([ID самолета]) REFERENCES [dbo].[Plane]([ID самолета])
)ON [PRIMARY]

------------------------------------------- CREATE TABLE [Ticket] -----------------------------------------
CREATE TABLE [dbo].[Ticket](
	[Авиарейс] [int] NOT NULL,
	[Пассажир] [nvarchar](50) NOT NULL,
	CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED ([Авиарейс] ASC, [Пассажир] ASC),
	CONSTRAINT [FK_Ticket] FOREIGN KEY([Авиарейс]) REFERENCES [dbo].[Flight]([ID авиарейса])
)ON [PRIMARY]

-------------------------------------------- CREATE TABLE [User] ------------------------------------------
CREATE TABLE [dbo].[User](
	[Логин]  [nvarchar](50) NOT NULL,
	[Пароль] [nvarchar](50) NULL,
	[ФИО]    [nvarchar](50) NULL,
	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Логин] ASC)
)ON [PRIMARY]

--------------------------------------------- INSERT INTO [User] ------------------------------------------
INSERT INTO [dbo].[User]([Логин], [Пароль], [ФИО])
				 VALUES ('Rgvi7IORqwzbSP122wAnGA==', 'Rgvi7IORqwzbSP122wAnGA==',  'Rgvi7IORqwzbSP122wAnGA==')