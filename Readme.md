# Maya.FormsConstructionKit

App for building and using custom forms based on "any" existing API as datasource.

> warning: this code is unoptimized. The main reason of this app was to try on use an OAuth provider to secure the app and try on to use MongoDB as persistent storage of the backend.

## Description

This app is providing ability to build custom forms and then managing its data.

The form is also called entity. So, on defining a form, it is the same as defining an entity.

Every form has components. Thoose components are setted on defining the from.
It is the same as defining the enitity properties.

Nomenclature summary:

- form == entity
- component == property

Imagine than you have an external API called Foo API. This API has an endpont to load a collection of one entity data:

`````HTTP
### Request:
GET https//api.foo.example/v1/car
Accept: */*

### Response content:
[
	{"name": "Natural95 car"},
	{"name": "EL cal"},
	{"name": "Gas cal"},
]
`````

Depend on previeus terminology and the foo API example, our form/entity would be the car and it has only one component/property called name.

## Solution structure

1. Backend API - the project `Maya.FormsConstructionKit.Api` is the server app, that contains the "logical" infrastructure and the 
persistent storage. Access to the data is secured. The authentication mechanism is provided and delegated to OAuth and OIDC provider.

2. Frontend SPA WASM - the project `Maya.FormsConstructionKit.Spa` is an Blazor standalone app. Allows to access from the web browser.
The main and the key feature is, that this app allows to an user to manage its froms definitions and the csv definitions. It 
also contain the common features like managing the data of the forms and export the data using csv exports.
The app is connected to OAuth and OIDC provider to authenticate and then to ability accessing the API as an authorized user.

3. TODO Mobile app - only for common feature purpose (managing data of the forms)

4. Maybe TODO React TS SPA - only for common feature purpose (managing data of the forms)

### Specifications:

- authentication and authorization is delegated to oAuth and OIDC provider (Azure AD, Auth0, Okta...),
- storage is MongoDb,
- backend is .NET API,
- frontend SPA is Blazor WASM standalone, us using [Havit.Blazor](https://github.com/havit/Havit.Blazor/) components on Bootstrap 5,
- TODO Mobile Xamarin Forms or MAUI or Blazor Native or Uno Platform? TBD Learn the differns first...,
- TODO spa in React in Typescript not in JavaScript and now, wash your hands for said that word.

## Usages
[Introduction and documentation](https://mayaleh.github.io/Maya.FormsConstructionKit/)

[♥ Sponsor](https://github.com/sponsors/mayaleh)



# TODOS

- [ ] File input
- [ ] Date input
- [ ] DateTime input
- [ ] For API DataSource, support reading or sending the json data in nested objects. In most cases like reading, the data is not available in the root of the json response.

## Blazor client
- [x] after authorize the user, use the auth token to send authorized requests to the backend API
- [ ] after success created new form, the page is refresehd without the new path/endpoint with the new entity name,
- [ ] on navigate to the entity data first time, the data is loaded and on navigate to othe entity data, nothing is changed on the page, no refresh...,
- [ ] sorting entity data does not work ever

## API backend
- [x] authorize the endpoints
- [x] KeyPropertyName for identification of the values, should be as ID unique
- [x] Add/Update/Delete value. On Add maybe check unique value. On Update/Delete where ID field is the KeyPropertyName on entity definition
- [x] consider of adding "Is Unique" for component textbox settings and recommend to use it for keyProperty
- [x] Export values to CSV (csfFile, ColumsMap) where ColumsMap: [{"propName": "csv position"}]
- [ ] Import values from CSV - always create new rows - (csfFile, ColumsMap)
- [ ] CRUD CSV map template: Allow CRUD Csv Columns Map templates as pre-defined Csv exports / imports: Template: {"name": "custom name", "ColumsMap": [{"propName": "csv position"}]}