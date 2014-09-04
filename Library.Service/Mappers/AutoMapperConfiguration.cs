using AutoMapper;

namespace Library.Service.Mappers
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainModelToDbModelMappingProfile>();
                x.AddProfile<DbModelToDomainModelMappingProfile>();
            });
        }
    }
}