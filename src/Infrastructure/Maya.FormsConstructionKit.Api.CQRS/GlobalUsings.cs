global using MediatR;
global using Maya.FormsConstructionKit.Api.Model.Storage;
global using Microsoft.Extensions.Logging;
global using Maya.FormsConstructionKit.Api.Library.Mappers.Shared.Model;
global using Maya.Ext.Rop;
global using Maya.FormsConstructionKit.Api.Library.Mappers.Storage;

global using entityFormResult = Maya.Ext.Rop.Result<Maya.FormsConstructionKit.Shared.Model.EntityForm, System.Exception>;
global using entitiesFormsResult = Maya.Ext.Rop.Result<System.Collections.Generic.IEnumerable<Maya.FormsConstructionKit.Shared.Model.EntityForm>, System.Exception>;
global using entityValuesResult = Maya.Ext.Rop.Result<Maya.FormsConstructionKit.Shared.Model.EntityData, System.Exception>;
global using entityValueResult = Maya.Ext.Rop.Result<object, System.Exception>;
global using csvResult = Maya.Ext.Rop.Result<string, System.Exception>;
global using csvDefinitionsResult = Maya.Ext.Rop.Result<System.Collections.Generic.IEnumerable<Maya.FormsConstructionKit.Shared.Model.CsvDefinition>, System.Exception>;
global using csvDefinitionResult = Maya.Ext.Rop.Result<Maya.FormsConstructionKit.Shared.Model.CsvDefinition, System.Exception>;
global using unitResult = Maya.Ext.Rop.Result<Maya.Ext.Unit, System.Exception>;
