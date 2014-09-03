using AutoMapper;
using Library.Model.Models;
using Library.Core.Models;

namespace Library.Service.Mappers
{

    public class DbModelToBusinessModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DbModelToBusinessModelMappings"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<UserProfileViewModel, UserProfile>()
            //    .ForMember(dest => dest.Gender, pick => pick.MapFrom(src => src.Gender == GenderState.Male ? true : false));
            Mapper.CreateMap<Author, AuthorModel>()
                .ForMember(dest => dest.Name, pick => pick.MapFrom(src => src.Name + " " + src.LastName));
            Mapper.CreateMap<Book, BookModel>();
            Mapper.CreateMap<BookAmount, BookModel>()
                .ForMember(dest => dest.BookAmount, pick => pick.MapFrom(src => src.Amount));
            //Mapper.CreateMap<UserProfile, UserModel>();
        }
    }
}