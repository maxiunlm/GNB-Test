# GNB-Test
## Consigna

El webservice debe tener un método desde el cuál se pueda obtener el listado de todas las transacciones. Otro método con el que obtener todos los rates.

Y por último una página web a la que se le pase un SKU, y muestre un listado con todas las transacciones de ese producto en EUR, y la suma total de todas esas transacciones, también en EUR.

Además, es necesario un plan B en caso que el webservice del que obtenemos la información no esté disponible. Para ello, la aplicación debe persistir la información cada vez que la obtenemos (eliminando y volviendo a insertar los nuevos datos). Puedes utilizar el sistema que prefieras para ello, por ejemplo, en una base de datos relacional.

## Requisitos

 * Puedes utilizar cualquier framework y cualquier librería de terceros.
  >* El Servicio usa netcoreapp2.2
  >* El Cliente implemente un React.Js 16.8.6 
 * Recuerda que pueden faltar algunas conversiones, deberás calcularlas mediante la información que tengas.
 * Separación de responsabilidades en distintas capas
  >* Es Servicio  es un sistema cuatri-capas (Controlller-Service-Business-Data).
  >* El Cliente es bicapa (Presenter-Client).
 * Implementación de log de error y manejo de excepciones en cada capa.
  >* El Log saldrá por consola, dado que no puedo configurar carpetas en máquinas ajenas.
  >* El manejo de Exceptions y la Auditoría se hacm mediante los Filters de tipo:
  >>* ExceptionFilterAttribute
  >>* ActionFilterAttribute
 * Tener en cuenta los principios SOLID y correcta capitalización del código.
  >* A travez del uso de Interfaces, la división de capas, la delegación de responsabilidades y demás practicas de la programación Orientada a Objetos considero haber cumplido con este Objetivo.
 * Uso de Inyección de dependencias.
  >* El servicio utiliza la Inyección de Dependencias (vía Constructor) y la Inversión de Control del .NET Core, mediante el package 'Microsoft.Extensions.DependencyInjection'
 * Tests unitarios.
  >* Para el Servicio, implementé los Unit Tests mediante MSTest, la herramienta que propone el mismo .NET Core. Además del uso de Moq como herramienta de Mocking.
  >* En el cliente los tests están desarrollados con Jasmine y Jest  

## Puntos extra (No obligatorios)

 * Estamos tratando con divisas, intenta no utilizar números con coma flotante.
  >* En el Servidor, todo número con coma, es del tipo de dato DECIMAL, que es mucho más preciso que el tipo DOUBLE.
 * Después de cada conversión, el resultado debe ser redondeado a dos decimales usando el redondeo Bank
  >* Entiendo haber resuelto esto al hacer que el Redondeo de los números considere que los números con 3 decimales, tomen por ejemplo >= que 0.005 como 0.01 y si es < que 0.005 entonces es un 0 (cero)

--------------------------------------------------------------------------------------------------------------------

# Instalación

 1. Instalar MongoDB con Docker (o localmente, como usted prefiera), en el puerto '27017'
```bash
docker pull mongo
docker run -d -p 27017:27017  --hostname my-mongo --name maxi-mongo mongo
```

Si bien con esto  es suficiente para descargar, instalar y ejecutar una imagen (Docker) de MongoDB, posteriormente se podrá correr esta imagen con el comando:

```bash
docker run --rm -d -p 27017:27017/tcp mongo:latest
```

 2. Crear un Carpeta, con nombre a elección del usuario (por ej. 'GNB-Examen-MaximilianoGauna'), ingresar a esta y ejecutar el comando:
```bash
$ git clone git://github.com/maxiunlm/GNB-Test.git
```
 3. Desde la línea de comandos, posicionarse en la carpeta dinde se haya descargado el Sistema completo desde el GIT.
 4. Seguir los pasos de 'Ejecutar el proyecto' de 'Web Service (Web Api)' y de 'Web Client' en ese orden.

----------------------------------------------------------------------------------------------------------

## Web Service (Web Api)

Se trata de una Web Api desarrollada en .Net Core, que utiliza como repositorio de datos (Base de datos), una instancia de MongoDb.
Está orientada a objetos y se encuentra dividida en Capas:
 1. Controller: Tiene los Métodos de la WebApi, funciona a modo de Presenter.
 2. Service: Es la capa que se encarga de abstraer las reglas de negocio de la capa Business.
 3. Business: En esta capa se encuentra toda la lógica del sistema.
 4. Data: Es la DAL (Data Access Layer), y es la capa encargada de:
  i. Hacer de Cliente para las Web Apis:
    * http://quiet-stone-2094.herokuapp.com/rates.json
    * http://quiet-stone-2094.herokuapp.com/transactions.json
  ii. Administrar la Conexión y el CRUD (ABM) contra el MongoDb.

### Ejecutar Pruebas (Tests)
```bash
dotnet test ./GnbApiRestful/Tests/Tests.csproj
```
 
### Ejecutar Porjecto
```bash
dotnet run --project ./GnbApiRestful/Webapi/Webapi.csproj
```

### Abrir en el Navegador Web
  * Rates: http://localhost:5000/api/Rate
  * Transactions: http://localhost:5000/api/Transaction
  * Sku List: http://localhost:5000/api/Sku
  * SKU: http://localhost:5000/api/Sku/summary/skuId (skuId: puede salir de uno de los valores de 'Sku List')

----------------------------------------------------------------------------------------------------------

## Web Client

Es una aplicación Web (Frontend) desarrollada con React.Js (corriendo sobre Node.js), que consume el Servicio (WebApi) de este examen.
Esta orientada a objetos y se encuentra dividida en Capas:
 1. Presenter: Son los componentes React.js de la UI.
 2. Client: Son los clientes que se encargan de la comunicación con el Servicio (la WebApi).

### Ejecutar Pruebas (Tests)
En la carpeta del Proyecto Cliente (./GnbWebClient/gnbwebclient/), ejecutar el comando:

```bash
yarn test
```
#### ó
```bash
npm test
```

### Ejecutar el proyecto
En la carpeta del Proyecto Cliente (./GnbWebClient/gnbwebclient/), ejecutar el comando:
 
```bash
yarn start
```
#### ó
```bash
npm start
```

### Abrir en el Navegador Web
http://localhost:3000/

# Licencia
[MIT](https://choosealicense.com/licenses/mit/)
