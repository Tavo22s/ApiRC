# ApiR

## Features

- SQLServer 2022 - 16.0.1110.1
- NET 8.0
- AspNetCore 6.4.0
- SqlClient 4.8.6
- Dapper 2.1.28

## Instalación
                
1. Configurar Base de Datos
2. Configurar el ConexionString
                
----

### Configurar Base de Datos

> Ejecutar Sentencia por Sentencia.

EL SQLScript se encuentra en ApiR.Data/SQLScripts/DBBanco.sql

![](https://github.com/Tavo22s/ApiRC/blob/master/images/confdb1.png)
![](https://github.com/Tavo22s/ApiRC/blob/master/images/confdb2.png)
![](https://github.com/Tavo22s/ApiRC/blob/master/images/confdb3.png)

### Configurar el ConexionString

![](https://github.com/Tavo22s/ApiRC/blob/master/images/conexionString.png)

> Se cambia de acuerdo a la base de datos.
Esto se encuentra en ApiR/appsettings.json


## Uso
### Otener clientes

![](https://github.com/Tavo22s/ApiRC/blob/master/images/GetAll.png)
Resultado
![](https://github.com/Tavo22s/ApiRC/blob/master/images/GetAllResults.png)

### Registrar Cliente

![](https://github.com/Tavo22s/ApiRC/blob/master/images/PostC.png)
Resultado
![](https://github.com/Tavo22s/ApiRC/blob/master/images/PostCR.png)

### Actualizar Cliente

![](https://github.com/Tavo22s/ApiRC/blob/master/images/PutC.png)
Resultado
![](https://github.com/Tavo22s/ApiRC/blob/master/images/PutCR.png)

### Busqueda

![](https://github.com/Tavo22s/ApiRC/blob/master/images/SearchC.png)
Resultado
![](https://github.com/Tavo22s/ApiRC/blob/master/images/SearchCR.png)

### Registrar Credito

![](https://github.com/Tavo22s/ApiRC/blob/master/images/PostCredito.png)
Resultado
![](https://github.com/Tavo22s/ApiRC/blob/master/images/PostCreditoR.png)


## Restricciones
### Busqueda por Tipo Documento y Nro Documento

![](https://github.com/Tavo22s/ApiRC/blob/master/images/SearchClientQuery.png)

### Actualizacion no incluye Tipo Documento y Nro Documento

![](https://github.com/Tavo22s/ApiRC/blob/master/images/UpdateClientProtect.png)

### Controlar el registro del mismo cliente por Tipo Documento y Nro Documento

![](https://github.com/Tavo22s/ApiRC/blob/master/images/ControlarClientInsert.png)

### Registro de Credito
- Un cliente solo puede tener 3 creditos en paralelon
- Se Debe tener registrado primero al cliente
- El plazo no puede ser mayor de 12 meses

![](https://github.com/Tavo22s/ApiRC/blob/master/images/RegistroCreditoRestriction.png)
