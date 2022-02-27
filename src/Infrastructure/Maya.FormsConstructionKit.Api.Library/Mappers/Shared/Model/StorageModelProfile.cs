
using AutoMapper;

namespace Maya.FormsConstructionKit.Api.Library.Mappers.Shared.Model
{
    public class StorageModelProfile : Profile
    {
        public StorageModelProfile()
        {
            this.CreateMap<FormsConstructionKit.Shared.Model.CsvColumn, Maya.FormsConstructionKit.Api.Model.Storage.CsvColumn>()
                .ReverseMap();
            this.CreateMap<FormsConstructionKit.Shared.Model.CsvDefinition, EntityCsvDefinition>()
                .ReverseMap();
        }
    }
}
