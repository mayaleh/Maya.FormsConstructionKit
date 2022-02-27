global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Logging;
global using Maya.FormsConstructionKit.Api.Model.Storage;

global using entityDefinitionsResult = Maya.Ext.Rop.Result<System.Collections.Generic.IEnumerable<Maya.FormsConstructionKit.Api.Model.Storage.EntityDefinition>, System.Exception>;
global using entityDefinitionResult = Maya.Ext.Rop.Result<Maya.FormsConstructionKit.Api.Model.Storage.EntityDefinition, System.Exception>;

global using entityCsvDefinitionsResult = Maya.Ext.Rop.Result<System.Collections.Generic.IEnumerable<Maya.FormsConstructionKit.Api.Model.Storage.EntityCsvDefinition>, System.Exception>;
global using entityCsvDefinitionResult = Maya.Ext.Rop.Result<Maya.FormsConstructionKit.Api.Model.Storage.EntityCsvDefinition, System.Exception>;

global using entityValuesResult = Maya.Ext.Rop.Result<System.Collections.Generic.IEnumerable<Maya.FormsConstructionKit.Api.Model.Storage.EntityValue>, System.Exception>;
global using entityValueResult = Maya.Ext.Rop.Result<Maya.FormsConstructionKit.Api.Model.Storage.EntityValue, System.Exception>;