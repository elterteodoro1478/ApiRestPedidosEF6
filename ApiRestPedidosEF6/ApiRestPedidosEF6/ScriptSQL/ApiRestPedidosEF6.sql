USE [MASTER]
IF NOT EXISTS( SELECT * FROM SYSDATABASES WHERE NAME = 'APIRESTPEDIDOS')
BEGIN   
    CREATE DATABASE [APIRESTPEDIDOS;
    ALTER DATABASE [APIRESTPEDIDOS] SET COMPATIBILITY_LEVEL = 110;
END;
IF  EXISTS( SELECT * FROM SYSDATABASES WHERE NAME = 'APIRESTPEDIDOS')
BEGIN  
     USE [APIRESTPEDIDOS];
END;


GO



USE [APIRESTPEDIDOS]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 14/11/2022 21:19:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[Documento] [varchar](20) NOT NULL,
	[Email] [varchar](255) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 14/11/2022 21:19:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[VrUnitario] [numeric](18, 2) NOT NULL,
	[Quantidade] [numeric](10, 2) NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemPedido]    Script Date: 14/11/2022 21:19:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemPedido](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdItem] [int] NOT NULL,
	[IdPedido] [int] NOT NULL,
	[VrUnitario] [numeric](18, 2) NOT NULL,
	[Quantidade] [numeric](10, 2) NOT NULL,
 CONSTRAINT [PK_ItemPedido] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 14/11/2022 21:19:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[NumeroPedido] [varchar](50) NOT NULL,
	[DtCriacao] [datetime] NOT NULL,
	[VrTotal] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_ItensPedido]    Script Date: 14/11/2022 21:19:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_ItensPedido]
AS
SELECT        dbo.Pedido.NumeroPedido AS [Nº Pedido], CONVERT(varchar(10), dbo.Pedido.DtCriacao, 103) + '  ' + CONVERT(varchar(5), dbo.Pedido.DtCriacao, 108) AS [Dt.Pedido], dbo.Item.Nome AS Item, dbo.ItemPedido.VrUnitario, 
                         dbo.ItemPedido.Quantidade AS Qtde, CONVERT(numeric(18, 2), dbo.ItemPedido.VrUnitario * dbo.ItemPedido.Quantidade) AS VrTotal, dbo.Cliente.Nome AS Cliente, dbo.Pedido.Id AS IdPedido, dbo.Item.Id AS IdItem
FROM            dbo.Item INNER JOIN
                         dbo.ItemPedido ON dbo.Item.Id = dbo.ItemPedido.IdItem INNER JOIN
                         dbo.Pedido ON dbo.ItemPedido.IdPedido = dbo.Pedido.Id INNER JOIN
                         dbo.Cliente ON dbo.Pedido.IdCliente = dbo.Cliente.Id
GO
/****** Object:  View [dbo].[VW_Pedido]    Script Date: 14/11/2022 21:19:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_Pedido]
AS
SELECT        dbo.Pedido.NumeroPedido AS [Nº Pedido], dbo.Cliente.Nome AS Cliente, CONVERT(VARCHAR(10), dbo.Pedido.DtCriacao, 103) + '  ' + CONVERT(VARCHAR(5), dbo.Pedido.DtCriacao, 108) AS [Dt.Pedido], dbo.Pedido.VrTotal, 
                         dbo.Pedido.IdCliente, dbo.Pedido.Id AS IdPedido
FROM            dbo.Cliente INNER JOIN
                         dbo.Pedido ON dbo.Cliente.Id = dbo.Pedido.IdCliente
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 
GO
INSERT [dbo].[Cliente] ([Id], [Nome], [Documento], [Email]) VALUES (1, N'LK Elteronica Ltda', N'003838162589', N'rt@bol.com.br')
GO
INSERT [dbo].[Cliente] ([Id], [Nome], [Documento], [Email]) VALUES (2, N'Elter teodoro', N'003838162589', N'rtw@bol.com.br')
GO
SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[Item] ON 
GO
INSERT [dbo].[Item] ([Id], [Nome], [VrUnitario], [Quantidade]) VALUES (1, N'Mala de Aço 15mm', CAST(32.17 AS Numeric(18, 2)), CAST(100.00 AS Numeric(10, 2)))
GO
INSERT [dbo].[Item] ([Id], [Nome], [VrUnitario], [Quantidade]) VALUES (2, N'Capa de K12', CAST(11.09 AS Numeric(18, 2)), CAST(32.00 AS Numeric(10, 2)))
GO
INSERT [dbo].[Item] ([Id], [Nome], [VrUnitario], [Quantidade]) VALUES (3, N'Fio 6mm', CAST(3.52 AS Numeric(18, 2)), CAST(32.00 AS Numeric(10, 2)))
GO
INSERT [dbo].[Item] ([Id], [Nome], [VrUnitario], [Quantidade]) VALUES (4, N'Parafuso M16', CAST(0.82 AS Numeric(18, 2)), CAST(512.00 AS Numeric(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[Item] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemPedido] ON 
GO
INSERT [dbo].[ItemPedido] ([Id], [IdItem], [IdPedido], [VrUnitario], [Quantidade]) VALUES (15, 1, 4, CAST(3.00 AS Numeric(18, 2)), CAST(4.00 AS Numeric(10, 2)))
GO
INSERT [dbo].[ItemPedido] ([Id], [IdItem], [IdPedido], [VrUnitario], [Quantidade]) VALUES (16, 2, 4, CAST(12.00 AS Numeric(18, 2)), CAST(39.00 AS Numeric(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[ItemPedido] OFF
GO
SET IDENTITY_INSERT [dbo].[Pedido] ON 
GO
INSERT [dbo].[Pedido] ([Id], [IdCliente], [NumeroPedido], [DtCriacao], [VrTotal]) VALUES (4, 1, N'GT313', CAST(N'2022-11-14T19:37:23.380' AS DateTime), CAST(480.00 AS Numeric(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Pedido] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Cliente_NomeDoc]    Script Date: 14/11/2022 21:19:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Cliente_NomeDoc] ON [dbo].[Cliente]
(
	[Nome] ASC,
	[Documento] ASC
)WITH (FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Pedido_NumeroPedido]    Script Date: 14/11/2022 21:19:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Pedido_NumeroPedido] ON [dbo].[Pedido]
(
	[NumeroPedido] ASC
)WITH (FILLFACTOR = 90) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Item] ADD  DEFAULT ((0)) FOR [VrUnitario]
GO
ALTER TABLE [dbo].[Item] ADD  DEFAULT ((1)) FOR [Quantidade]
GO
ALTER TABLE [dbo].[ItemPedido] ADD  DEFAULT ((0)) FOR [VrUnitario]
GO
ALTER TABLE [dbo].[ItemPedido] ADD  DEFAULT ((1)) FOR [Quantidade]
GO
ALTER TABLE [dbo].[Pedido] ADD  DEFAULT (getdate()) FOR [DtCriacao]
GO
ALTER TABLE [dbo].[Pedido] ADD  DEFAULT ((0)) FOR [VrTotal]
GO
ALTER TABLE [dbo].[ItemPedido]  WITH NOCHECK ADD  CONSTRAINT [FK_ItemPedido_Item] FOREIGN KEY([IdItem])
REFERENCES [dbo].[Item] ([Id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ItemPedido] CHECK CONSTRAINT [FK_ItemPedido_Item]
GO
ALTER TABLE [dbo].[ItemPedido]  WITH NOCHECK ADD  CONSTRAINT [FK_ItemPedido_Pedido] FOREIGN KEY([IdPedido])
REFERENCES [dbo].[Pedido] ([Id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[ItemPedido] CHECK CONSTRAINT [FK_ItemPedido_Pedido]
GO
ALTER TABLE [dbo].[Pedido]  WITH NOCHECK ADD  CONSTRAINT [FK_Pedido_Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([Id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Cliente]
GO
/****** Object:  Trigger [dbo].[SetDelVrTotalPedido]    Script Date: 14/11/2022 21:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[SetDelVrTotalPedido] 
   ON  [dbo].[ItemPedido]
   AFTER  DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Declare  @IdPedido1 Integer = 0
	
    SET @IdPedido1 = (SELECT IdPedido FROM DELETEd)
	
	UPDATE Pedido 
	SET VrTotal = ISNULL( (SELECT SUM( ISNULL(VrUnitario,0 ) *  ISNULL(Quantidade,0 )) FROM ItemPedido WHERE  IdPedido = @IdPedido1 ),0)
	WHERE Id = @IdPedido1
END
GO
ALTER TABLE [dbo].[ItemPedido] ENABLE TRIGGER [SetDelVrTotalPedido]
GO
/****** Object:  Trigger [dbo].[SetUPVrTotalPedido]    Script Date: 14/11/2022 21:19:55 ******/
GO
CREATE TRIGGER [dbo].[SetUPVrTotalPedido] 
   ON  [dbo].[ItemPedido]
   AFTER  INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

     Declare  @IdPedido2 Integer= 0
	
    SET @IdPedido2 = (SELECT IdPedido FROM inserted)
	
	UPDATE Pedido 
	SET VrTotal =  ISNULL((SELECT SUM( ISNULL(VrUnitario,0 ) *  ISNULL(Quantidade,0 )) FROM ItemPedido WHERE  IdPedido = @IdPedido2 ),0)
	WHERE Id = @IdPedido2
END
GO
ALTER TABLE [dbo].[ItemPedido] ENABLE TRIGGER [SetUPVrTotalPedido]
GO


