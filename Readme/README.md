# API LAfise - Prueba Tecnica .NET Backend

Este proyecto es una API RESTful desarrollada en ASP.NET Core (.NET 8) con persistencia en SQLite. Simula operaciones basicas de una aplicacion bancaria: clientes, cuentas, depositos, retiros y consultas de transacciones.


Requisitos Tecnicos (segun PDF)

-> Crear perfil del cliente
-> Crear cuenta bancaria (unica por numero)
-> Consultar saldo actual
-> Registrar depositos y retiros (con validacion de saldo)
-> Consultar historial de transacciones y saldo final
-> Uso de SQLite
-> Arquitectura limpia, separada por capas
-> Inyeccion de dependencias (repositorios)
-> Pruebas unitarias (xUnit)
-> Cumplimiento de buenas prcticas y principios SOLID


# Tecnologias utilizadas

-> ASP.NET Core Web API (.NET 8)
-> SQLite
-> ADO.NET
-> xUnit (Pruebas unitarias)
-> Swagger (documentación y pruebas)

# Estructura del proyecto

ApiLafise/
->
L> Controllers/             # Endpoints
L> Models/                  # Entidades
L> Repositories/            # Acceso a datos con ADO.NET
L> BaseDatos/LafiseBD.db    # Base de datos SQLite
L> Program.cs               # Configuracion principal
L> README.md                # Este archivo
L> ApiLafise.Tests/         # Proyecto de pruebas unitarias

# Como ejecutar el proyecto

### 1. Clona el repositorio

```bash
git clone https://github.com/Alexander-Acu/TestBackendLafise.git
```
### 2. Abre el proyecto en Visual Studio

L> Asegurate de tener instalado .NET 8 y SQLite
L> Abre la soluciun `ApiLafise.sln`

### 3. Ejecuta el proyecto
---

## aparecera la ventna de Swagger

Una vez corras el proyecto, ve a:

```
```
Ahi podras probar los endpoints:

L> `POST /api/cliente`
L> `POST /api/cuenta`
L> `GET /api/cuenta/saldo/{numeroCuenta}`
L> `POST /api/transaccion`
L> `GET /api/transaccion/historial/{numeroCuenta}`

---
## Ejecutar pruebas unitarias

## Cobertura de pruebas

Se han implementado pruebas para:

L> Insertar cliente
L> Crear cuenta bancaria
L> Realizar depositos
L> Retiro valido
L> Rechazo de retiro sin fondos
L> Consultar historial y saldo final

---
# Autor
## Alexander (Prueba Tccnica Programador Backend)