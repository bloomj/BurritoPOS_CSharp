/*
DB Schema for BurritoPOS_CSharp
*/

BEGIN TRY
PRINT ' '
PRINT 'Creating NeatoBurrito database'
PRINT ' '
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = '[neatoburrito]')
	PRINT '  - NeatoBurrito database already exists'
ELSE 
BEGIN
	CREATE DATABASE NeatoBurrito
	PRINT '  - NeatoBurrito database successfully created'
END
END TRY BEGIN CATCH END CATCH
GO

PRINT ' '
PRINT 'Creating burrito table'
PRINT ' '
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF EXISTS (SELECT * FROM [NeatoBurrito].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'burrito')
	PRINT '  - burrito table already exists'
ELSE
BEGIN
	CREATE TABLE [NeatoBurrito].[dbo].[burrito] (
	  [id] [varchar](36) NOT NULL CONSTRAINT [DF_burrito_id] DEFAULT (newid()),
	  [flourTortilla] [bit] NOT NULL CONSTRAINT [DF_burrito_flourTortilla]  DEFAULT ((0)),
	  [chiliTortilla] [bit] NOT NULL CONSTRAINT [DF_burrito_chiliTortilla]  DEFAULT ((0)),
	  [jalapenoCheddarTortilla] [bit] NOT NULL CONSTRAINT [DF_burrito_jalapenoCheddarTortilla]  DEFAULT ((0)),
	  [tomatoBasilTortilla] [bit] NOT NULL CONSTRAINT [DF_burrito_tomatoBasilTortilla]  DEFAULT ((0)),
	  [herbGarlicTortilla] [bit] NOT NULL CONSTRAINT [DF_burrito_herbGarlicTortilla]  DEFAULT ((0)),
	  [wheatTortilla] [bit] NOT NULL CONSTRAINT [DF_burrito_wheatTortilla]  DEFAULT ((0)),
	  [whiteRice] [bit] NOT NULL CONSTRAINT [DF_burrito_whiteRice]  DEFAULT ((0)),
	  [brownRice] [bit] NOT NULL CONSTRAINT [DF_burrito_brownRice]  DEFAULT ((0)),
	  [blackBeans] [bit] NOT NULL CONSTRAINT [DF_burrito_blackBeans]  DEFAULT ((0)),
	  [pintoBeans] [bit] NOT NULL CONSTRAINT [DF_burrito_pintoBeans]  DEFAULT ((0)),
	  [chicken] [bit] NOT NULL CONSTRAINT [DF_burrito_chicken]  DEFAULT ((0)),
	  [beef] [bit] NOT NULL CONSTRAINT [DF_burrito_beef]  DEFAULT ((0)),
	  [hummus] [bit] NOT NULL CONSTRAINT [DF_burrito_hummus]  DEFAULT ((0)),
	  [salsaPico] [bit] NOT NULL CONSTRAINT [DF_burrito_salsaPico]  DEFAULT ((0)),
	  [salsaVerde] [bit] NOT NULL CONSTRAINT [DF_burrito_salsaVerde]  DEFAULT ((0)),
	  [salsaSpecial] [bit] NOT NULL CONSTRAINT [DF_burrito_salsaSpecial]  DEFAULT ((0)),
	  [guacamole] [bit] NOT NULL CONSTRAINT [DF_burrito_guacamole]  DEFAULT ((0)),
	  [lettuce] [bit] NOT NULL CONSTRAINT [DF_burrito_lettuce]  DEFAULT ((0)),
	  [jalapenos] [bit] NOT NULL CONSTRAINT [DF_burrito_jalapenos]  DEFAULT ((0)),
	  [tomatoes] [bit] NOT NULL CONSTRAINT [DF_burrito_tomatos]  DEFAULT ((0)),
	  [cucumber] [bit] NOT NULL CONSTRAINT [DF_burrito_cucumber]  DEFAULT ((0)),
	  [onion] [bit] NOT NULL CONSTRAINT [DF_burrito_onion]  DEFAULT ((0)),
	  [price] [smallmoney] DEFAULT NULL,
	  [orderID] [varchar](36) DEFAULT NULL,
	 CONSTRAINT [PK_burrito] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	PRINT '  - burrito table successfully created'
END
GO
SET ANSI_PADDING OFF

PRINT ' '
PRINT 'Creating customer table'
PRINT ' '
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF EXISTS (SELECT * FROM [NeatoBurrito].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'customer')
	PRINT '  - customer table already exists'
ELSE
BEGIN
	CREATE TABLE [NeatoBurrito].[dbo].[customer] (
	  [id] [varchar](36) NOT NULL CONSTRAINT [DF_customer_id] DEFAULT (newid()),
	  [firstName] varchar(128) DEFAULT NULL,
	  [lastName] varchar(128) DEFAULT NULL,
	  [emailaddress] varchar(256) DEFAULT NULL,
	 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	PRINT '  - customer table successfully created'
END
GO
SET ANSI_PADDING OFF

PRINT ' '
PRINT 'Creating employee table'
PRINT ' '
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF EXISTS (SELECT * FROM [NeatoBurrito].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'employee')
	PRINT '  - employee table already exists'
ELSE
BEGIN
	CREATE TABLE [NeatoBurrito].[dbo].[employee] (
	  [id] [varchar](36) NOT NULL CONSTRAINT [DF_employee_id] DEFAULT (newid()),
	  [firstName] varchar(128) DEFAULT NULL,
	  [lastName] varchar(128) DEFAULT NULL,
	  [isManager] [bit]  NOT NULL CONSTRAINT [DF_employee_isManager]  DEFAULT ((0)),
	 CONSTRAINT [PK_employee] PRIMARY KEY CLUSTERED 
	(
		[employeeID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	PRINT '  - employee table successfully created'
END
GO
SET ANSI_PADDING OFF

PRINT ' '
PRINT 'Creating inventory table'
PRINT ' '
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF EXISTS (SELECT * FROM [NeatoBurrito].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'inventory')
	PRINT '  - inventory table already exists'
ELSE
BEGIN
	CREATE TABLE [NeatoBurrito].[dbo].[inventory] (
	  [id] [varchar](36) NOT NULL CONSTRAINT [DF_inventory_id] DEFAULT (newid()),
	  [flourTortillaQty] [int] NOT NULL CONSTRAINT [DF_inventory_flourTortillaQty]  DEFAULT ((0)),
	  [chiliTortillaQty] [int] NOT NULL CONSTRAINT [DF_inventory_chiliTortillaQty]  DEFAULT ((0)),
	  [jalapenoCheddarTortillaQty] [int] NOT NULL CONSTRAINT [DF_inventory_jalapenoCheddarTortillaQty]  DEFAULT ((0)),
	  [tomatoBasilTortillaQty] [int] NOT NULL CONSTRAINT [DF_inventory_tomatoBasilTortillaQty]  DEFAULT ((0)),
	  [herbGarlicTortillaQty] [int] NOT NULL CONSTRAINT [DF_inventory_herbGarlicTortillaQty]  DEFAULT ((0)),
	  [wheatTortillaQty] [int] NOT NULL CONSTRAINT [DF_inventory_wheatTortillaQty]  DEFAULT ((0)),
	  [whiteRiceQty] [int] NOT NULL CONSTRAINT [DF_inventory_whiteRiceQty]  DEFAULT ((0)),
	  [brownRiceQty] [int] NOT NULL CONSTRAINT [DF_inventory_brownRiceQty]  DEFAULT ((0)),
	  [blackBeansQty] [int] NOT NULL CONSTRAINT [DF_inventory_blackBeansQty]  DEFAULT ((0)),
	  [pintoBeansQty] [int] NOT NULL CONSTRAINT [DF_inventory_pintoBeansQty]  DEFAULT ((0)),
	  [chickenQty] [int] NOT NULL CONSTRAINT [DF_inventory_chickenQty]  DEFAULT ((0)),
	  [beefQty] [int] NOT NULL CONSTRAINT [DF_inventory_beefQty]  DEFAULT ((0)),
	  [hummusQty] [int] NOT NULL CONSTRAINT [DF_inventory_hummusQty]  DEFAULT ((0)),
	  [salsaPicoQty] [int] NOT NULL CONSTRAINT [DF_inventory_salsaPicoQty]  DEFAULT ((0)),
	  [salsaVerdeQty] [int] NOT NULL CONSTRAINT [DF_inventory_salsaVerdeQty]  DEFAULT ((0)),
	  [salsaSpecialQty] [int] NOT NULL CONSTRAINT [DF_inventory_salsaSpecialQty]  DEFAULT ((0)),
	  [guacamoleQty] [int] NOT NULL CONSTRAINT [DF_inventory_guacamoleQty]  DEFAULT ((0)),
	  [lettuceQty] [int] NOT NULL CONSTRAINT [DF_inventory_lettuceQty]  DEFAULT ((0)),
	  [jalapenosQty] [int] NOT NULL CONSTRAINT [DF_inventory_jalapenosQty]  DEFAULT ((0)),
	  [tomatoesQty] [int] NOT NULL CONSTRAINT [DF_inventory_tomatosQty]  DEFAULT ((0)),
	  [cucumberQty] [int] NOT NULL CONSTRAINT [DF_inventory_cucumberQty]  DEFAULT ((0)),
	  [onionQty] [int] NOT NULL CONSTRAINT [DF_inventory_onionQty]  DEFAULT ((0)),
	 CONSTRAINT [PK_inventory] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	PRINT '  - inventory table successfully created'
END
GO
SET ANSI_PADDING OFF

PRINT ' '
PRINT 'Creating orders table'
PRINT ' '
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF EXISTS (SELECT * FROM [NeatoBurrito].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'orders')
	PRINT '  - orders table already exists'
ELSE
BEGIN
	CREATE TABLE [NeatoBurrito].[dbo].[orders] (
	  [id] [varchar](36) NOT NULL CONSTRAINT [DF_orders_id] DEFAULT (newid()),
	  [orderDate] datetime DEFAULT NULL,
	  [totalCost] [smallmoney] DEFAULT NULL,
	  [inventoryID] [varchar](36) DEFAULT NULL,
	  [isComplete] [bit] NOT NULL CONSTRAINT [DF_orders_isComplete]  DEFAULT ((0)),
	  [isSubmitted] [bit] NOT NULL CONSTRAINT [DF_orders_isSubmitted]  DEFAULT ((0)),
	 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
	(
		[orderID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	PRINT '  - orders table successfully created'
END
GO
SET ANSI_PADDING OFF


