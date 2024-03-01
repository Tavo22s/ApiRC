

CREATE DATABASE BancoDB COLLATE SQL_Latin1_General_CP1_CI_AS;

Use BancoDB;

CREATE TABLE CLIENTE(
	IdCliente INT PRIMARY KEY IDENTITY,
	TipoDocumento VARCHAR(100) NOT NULL,
	NroDocumento INT NOT NULL,
	Apellidos NVARCHAR(50),
	Nombres NVARCHAR(50),
	NombreCompleto NVARCHAR(100),
	FechaNacimiento DATE,
	LugarNacimiento NVARCHAR(100),
	PaisResidencia NVARCHAR(100)
)

GO

CREATE TABLE CREDITO(
	IdCredito INT PRIMARY KEY IDENTITY,
	IdCliente INT REFERENCES CLIENTE(IdCliente),
	Plazo INT,
	Monto DECIMAL,
	FrecuenciaPago VARCHAR(50) DEFAULT 'Mensual',
	Producto VARCHAR(50) CHECK (Producto IN('Consumo', 'Pyme')),
	FechaDesembolso DATE,
	FechaPrimeraCuota DATE,
	DiaPagoMensual INT
)
GO

CREATE PROCEDURE SP_REGISTRARCLIENTE(
	@TipoDocumento VARCHAR(100),
	@NroDocumento INT,
	@Apellidos NVARCHAR(50),
	@Nombres NVARCHAR(50),
	@NombreCompleto NVARCHAR(100),
	@FechaNacimiento DATE,
	@LugarNacimiento NVARCHAR(100),
	@PaisResidencia NVARCHAR(100)
)
AS BEGIN
	IF NOT EXISTS (SELECT 1 FROM CLIENTE WHERE TipoDocumento = @TipoDocumento AND NroDocumento = @NroDocumento)
	BEGIN
		INSERT INTO CLIENTE(TipoDocumento, NroDocumento, Apellidos, Nombres, NombreCompleto, FechaNacimiento, LugarNacimiento, PaisResidencia)
		VALUES(@TipoDocumento, @NroDocumento, @Apellidos, @Nombres, @NombreCompleto, @FechaNacimiento, @LugarNacimiento, @PaisResidencia)
	END
END

GO

CREATE PROCEDURE SP_EDITARCLIENTE(
	@IdCliente INT,
	@Apellidos NVARCHAR(50) = NULL,
	@Nombres NVARCHAR(50) = NULL,
	@NombreCompleto NVARCHAR(100),
	@FechaNacimiento DATE,
	@LugarNacimiento NVARCHAR(100),
	@PaisResidencia NVARCHAR(100)
)
AS BEGIN
	SET NOCOUNT ON;
	UPDATE CLIENTE
	SET
		Apellidos = ISNULL(@Apellidos, Apellidos),
		Nombres = ISNULL(@Nombres, Nombres),
		NombreCompleto = ISNULL(@NombreCompleto, NombreCompleto),
		FechaNacimiento = ISNULL(@FechaNacimiento, FechaNacimiento),
		LugarNacimiento = ISNULL(@LugarNacimiento, LugarNacimiento),
		PaisResidencia = ISNULL(@PaisResidencia, PaisResidencia)
	WHERE IdCliente = @IdCliente;
END

GO

CREATE PROCEDURE SP_REGISTRARCREDITO(
	@IdCliente INT,
	@Plazo INT,
	@Monto DECIMAL,
	@Producto VARCHAR(50),
	@FechaDesembolso DATE,
	@FechaPrimeraCuota DATE
)
AS BEGIN
	IF NOT EXISTS (SELECT 1 FROM CLIENTE WHERE IdCliente = @IdCliente)
	BEGIN
		RETURN;
	END

	IF (SELECT COUNT(*) FROM CREDITO WHERE IdCliente = @IdCliente) >= 3
	BEGIN
		RETURN;
	END

	IF @Plazo > 12 OR @Plazo < 1
	BEGIN
		RETURN;
	END

	DECLARE @DiaPagoMensual INT;
	SET @DiaPagoMensual = DAY(@FechaPrimeraCuota);

	INSERT INTO CREDITO (IdCliente, Plazo, Monto, Producto, FechaDesembolso, FechaPrimeraCuota, DiaPagoMensual)
	VALUES(@IdCliente, @Plazo, @Monto, @Producto, @FechaDesembolso, @FechaPrimeraCuota, @DiaPagoMensual);
END

GO