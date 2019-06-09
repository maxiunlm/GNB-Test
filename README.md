# GNB-Test


## Web Client

Es una aplicación Web (Frontend) desarrollada con React.Js (corriendo sobre Node.js), que consume el Servicio (WebApi) de este examen.

### Abrir en el Navegador Web
http://localhost:3000/

#### Creación del proyecto
```bash
npx create-react-app gnbwebclient
cd gnbwebclient
yarn start
```
#### ó
```bash
npm start
```

### Agregar packages

 * yarn add react-select
 * yarn add bootstrap

----------------------------------------------------------------------------------------------------------

## Web Service (Web Api)

Se trata de una Web Api desarrollada en .Net Core, que utiliza como repositorio de datos (Base de datos), una instancia de MongoDb.
  
### Ejecutar Porjecto
```bash
dotnet run --project ./GnbApiRestful/Webapi/Webapi.csproj
```
### Abrir en el Navegador Web
  * Rates: http://localhost:5000/api/Rate
  * Transactions: http://localhost:5000/api/Transaction
  * SKU: http://localhost:5000/api/Sku/summary/skuId
  * Sku List: http://localhost:5000/api/Sku

### Instalar MongoDB con Docker
```bash
docker pull mongo
docker run -d -p 27017:27017  --hostname my-mongo --name maxi-mongo mongo
```

### Crear Servicio

#### Crear proyectos
```bash
dotnet new webapi -o ./GnbApiRestful/Webapi
dotnet new classlib -o ./GnbApiRestful/Service
dotnet new classlib -o ./GnbApiRestful/Business
dotnet new classlib -o ./GnbApiRestful/Data
dotnet new mstest -o ./GnbApiRestful/Tests
```

#### Creación de la Solution
```bash
dotnet new sln -o ./GnbApiRestful
dotnet sln ./GnbApiRestful/GnbApiRestful.sln add ./GnbApiRestful/Webapi/Webapi.csproj ./GnbApiRestful/Service/Service.csproj ./GnbApiRestful/Business/Business.csproj ./GnbApiRestful/Data/Data.csproj ./GnbApiRestful/Tests/Tests.csproj
```

#### Agregar referencias entre proyectos
```bash
dotnet add ./GnbApiRestful/Webapi/Webapi.csproj reference ./GnbApiRestful/Service/Service.csproj
dotnet add ./GnbApiRestful/Service/Service.csproj reference ./GnbApiRestful/Business/Business.csproj
dotnet add ./GnbApiRestful/Business/Business.csproj reference ./GnbApiRestful/Data/Data.csproj
dotnet add ./GnbApiRestful/Tests/Tests.csproj reference ./GnbApiRestful/Webapi/Webapi.csproj
dotnet add ./GnbApiRestful/Tests/Tests.csproj reference ./GnbApiRestful/Service/Service.csproj
dotnet add ./GnbApiRestful/Tests/Tests.csproj reference ./GnbApiRestful/Business/Business.csproj
dotnet add ./GnbApiRestful/Tests/Tests.csproj reference ./GnbApiRestful/Data/Data.csproj
```

#### Creación de la Estructura de Capas
```bash
mkdir ./GnbApiRestful/Data/DAL
mkdir ./GnbApiRestful/Business/Logic
mkdir ./GnbApiRestful/Service/Services
mkdir ./GnbApiRestful/Tests/Data
mkdir ./GnbApiRestful/Tests/Business
mkdir ./GnbApiRestful/Tests/Service
mkdir ./GnbApiRestful/Tests/Webapi
mkdir ./GnbApiRestful/Tests/Data/DAL
mkdir ./GnbApiRestful/Tests/Business/Logic
mkdir ./GnbApiRestful/Tests/Service/Services
mkdir ./GnbApiRestful/Tests/Webapi/Controllers
```

#### Agregar packages
```bash
dotnet add ./GnbApiRestful/Webapi/Webapi.csproj package jwt
dotnet add ./GnbApiRestful/Webapi/Webapi.csproj package nlog
dotnet add ./GnbApiRestful/Webapi/Webapi.csproj package NLog.Extensions.Logging
dotnet add ./GnbApiRestful/Webapi/Webapi.csproj package Nlog.Web.AspNetcore
dotnet add ./GnbApiRestful/Webapi/Webapi.csproj package unity
dotnet add ./GnbApiRestful/Webapi/Webapi.csproj package Unity.Interception.NetCore
dotnet add ./GnbApiRestful/Webapi/Webapi.csproj package automapper
dotnet add ./GnbApiRestful/Webapi/Webapi.csproj package Microsoft.AspNetCore.Cors
dotnet add ./GnbApiRestful/Tests/Tests.csproj package mvc
dotnet add ./GnbApiRestful/Tests/Tests.csproj package moq
dotnet add ./GnbApiRestful/Tests/Tests.csproj package jwt
dotnet add ./GnbApiRestful/Tests/Tests.csproj package nlog
dotnet add ./GnbApiRestful/Tests/Tests.csproj package NLog.Extensions.Logging
dotnet add ./GnbApiRestful/Tests/Tests.csproj package Nlog.Web.AspNetcore
dotnet add ./GnbApiRestful/Tests/Tests.csproj package unity
dotnet add ./GnbApiRestful/Tests/Tests.csproj package Unity.Interception.NetCore
dotnet add ./GnbApiRestful/Tests/Tests.csproj package automapper
dotnet add ./GnbApiRestful/Tests/Tests.csproj package Microsoft.AspNetCore.Mvc.Core
dotnet add ./GnbApiRestful/Tests/Tests.csproj package Microsoft.Extensions.Http
dotnet add ./GnbApiRestful/Data/Data.csproj package entityframework
dotnet add ./GnbApiRestful/Data/Data.csproj package mongocsharpdriver
dotnet add ./GnbApiRestful/Data/Data.csproj package Newtonsoft.Json
dotnet add ./GnbApiRestful/Data/Data.csproj package Microsoft.Extensions.Http
dotnet add ./GnbApiRestful/Business/Business.csproj package Newtonsoft.Json
dotnet add ./GnbApiRestful/Business/Business.csproj package automapper
dotnet add ./GnbApiRestful/Service/Service.csproj package Newtonsoft.Json
dotnet add ./GnbApiRestful/Service/Service.csproj package automapper
```

## Licencia
[MIT](https://choosealicense.com/licenses/mit/)
