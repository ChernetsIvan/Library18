using AutoMapper;
using Library.Model.Models;
using Library.Core.Models;

namespace Library.Service.Mappers
{
    public class BusinessModelToDbModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "BusinessModelToDbModelMappings"; }
        }
        // userProfile.Gender = userProfileVm.Gender == GenderState.Male ? true : false;
        protected override void Configure()
        {
            
            //Mapper.CreateMap<UserModel, UserProfile>().ForMember(dest=>dest.Gender, pick=>pick.MapFrom(src=>src.Gender == true ? GenderState.Male : GenderState.Female));
            //Mapper.CreateMap<UserModel, AccountViewModel>();
            Mapper.CreateMap<AuthorModel, Author>()
                .ForMember(dest => dest.Name, pick => pick.MapFrom(src => src.Name.Split(' ')[0]))
                .ForMember(dest => dest.LastName, pick => pick.MapFrom(src => src.Name.Split(' ')[1]));

            Mapper.CreateMap<BookModel, Book>();
            Mapper.CreateMap<BookModel, BookAmount>()
                .ForMember(dest => dest.Amount, pick => pick.MapFrom(src => src.BookAmount));

        }
    }
}