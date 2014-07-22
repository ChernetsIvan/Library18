using AutoMapper;
using Library.API.ViewModels;
using Library.Model.Models;
using Library.Web.ViewModels;
using System.Collections.Generic;

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
            Mapper.CreateMap<UserProfileViewModel, UserProfile>()
                .ForMember(dest => dest.Gender, pick => pick.MapFrom(src => src.Gender == GenderState.Male ? true : false));
            Mapper.CreateMap<AuthorViewModel, Author>()
                .ForMember(dest => dest.Name, pick => pick.MapFrom(src => src.Name.Split(' ')[0]))
                .ForMember(dest => dest.LastName, pick => pick.MapFrom(src => src.Name.Split(' ')[1]));
            Mapper.CreateMap<FullBookViewModel, Book>();
            Mapper.CreateMap<BookAuthorViewModel, BookAuthor>();
            Mapper.CreateMap<UserBookViewModel, UserBook>();
        }
    }
}