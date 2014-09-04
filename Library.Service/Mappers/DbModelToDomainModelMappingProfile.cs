using AutoMapper;
using Library.Core.Models;
using Library.Model.Models;
using Library.Domain.Models;

namespace Library.Service.Mappers
{

    public class DbModelToDomainModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DbModelToDomainModelMappings"; }
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
            Mapper.CreateMap<BookQrCode, QrCodeModel>();
            //Mapper.CreateMap<UserProfile, UserModel>();
        }
    }
}