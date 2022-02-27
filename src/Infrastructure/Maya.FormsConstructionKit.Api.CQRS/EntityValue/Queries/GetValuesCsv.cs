using Maya.FormsConstructionKit.Api.Library.Services.DataSource;

namespace Maya.FormsConstructionKit.Api.CQRS.EntityValue.Queries
{
    public class GetValuesCsvQuery : IRequest<csvResult>
    {
        public GetValuesCsvQuery(string entityName, string csvDefinitionId)
        {
            EntityName = entityName;
            CsvDefinitionId = csvDefinitionId;
        }

        public string EntityName { get; }
        public string CsvDefinitionId { get; }
    }

    public class GetValuesCsvHandler : IRequestHandler<GetValuesCsvQuery, csvResult>
    {
        private readonly AppDataContext dataContext;
        private readonly ILogger<GetValuesCsvHandler> logger;

        public GetValuesCsvHandler(AppDataContext dataContext, ILogger<GetValuesCsvHandler> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<csvResult> Handle(GetValuesCsvQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Maya.FormsConstructionKit.Api.Model.Storage.EntityCsvDefinition csvDefinition = new();

                return await Library.Repositories.EntityCsvDefinition.FindOneById(this.dataContext, request.CsvDefinitionId)
                        .BindAsync(async csvDef => { 
                            csvDefinition = csvDef;
                            return await Library.Repositories.EntityDefinition.FindLatestByNameAsync(this.dataContext, request.EntityName);
                        })
                        .BindAsync(async entityDefinition =>
                        {
                            IDataSourceService dataSource = entityDefinition.DataSourceDefinition.Type switch
                            {
                                DataSourceType.Api => throw new NotImplementedException(),
                                DataSourceType.Storage => new StorageDataSourceService(this.dataContext)
                                    .WithLogger(this.logger),
                                _ => throw new NotImplementedException(),
                            };

                            return await dataSource.Read(entityDefinition);
                        })
                        .BindAsync(data =>
                        {
                            var recordsResult = ObjectValueMapper.MapObjectDictionaryToCsvCols(data, csvDefinition.Columns);
                            return Task.FromResult(recordsResult);
                        })
                        .BindAsync(rows =>
                        {
                            var csvResult = Library.Exports.EntityValue.CsvExport.Generate(rows);
                            return Task.FromResult(csvResult);
                        })
                        .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return csvResult.Failed(e);
            }
        }
    }
}
