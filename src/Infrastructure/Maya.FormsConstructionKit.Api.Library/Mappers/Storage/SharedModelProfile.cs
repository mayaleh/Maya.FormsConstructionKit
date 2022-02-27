using AutoMapper;

namespace Maya.FormsConstructionKit.Api.Library.Mappers.Storage
{
    public class SharedModelProfile : Profile
    {
        public SharedModelProfile()
        {
            this.CreateMap<Maya.FormsConstructionKit.Api.Model.Storage.CsvColumn, FormsConstructionKit.Shared.Model.CsvColumn>()
                .ReverseMap();
            this.CreateMap<EntityCsvDefinition, FormsConstructionKit.Shared.Model.CsvDefinition>()
                .ReverseMap();
        }
    }
}
