using AutoMapper;
using Library.API.ViewModels;
using Library.Core.Models;

namespace Library.API.Mappers
{

    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<UserProfileViewModel, UserProfile>()
            //    .ForMember(dest => dest.Gender, pick => pick.MapFrom(src => src.Gender == GenderState.Male ? true : false));
            //Mapper.CreateMap<AuthorViewModel, Author>()
            //    .ForMember(dest => dest.Name, pick => pick.MapFrom(src => src.Name.Split(' ')[0]))
            //    .ForMember(dest => dest.LastName, pick => pick.MapFrom(src => src.Name.Split(' ')[1]));
            //Mapper.CreateMap<BookViewModel, Book>();
            //Mapper.CreateMap<BookAuthorViewModel, BookAuthor>();
            //Mapper.CreateMap<UserBookViewModel, UserBook>();
            Mapper.CreateMap<AuthorViewModel, AuthorModel>();
            Mapper.CreateMap<BookViewModel, BookModel>();
        }
    }
}