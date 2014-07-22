using AutoMapper;
using Library.API.ViewModels;
using Library.Model.Models;
using Library.Web.ViewModels;

namespace Library.API.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }
        // userProfile.Gender = userProfileVm.Gender == GenderState.Male ? true : false;
        protected override void Configure()
        {
            
            Mapper.CreateMap<UserProfile, UserProfileViewModel>().ForMember(dest=>dest.Gender, pick=>pick.MapFrom(src=>src.Gender == true ? GenderState.Male : GenderState.Female)); 
            Mapper.CreateMap<UserProfile, AccountViewModel>();
            Mapper.CreateMap<Author, AuthorViewModel>()
                .ForMember(dest=>dest.Name, pick =>pick.MapFrom(src=>src.Name + " " + src.LastName));
            Mapper.CreateMap<UserProfile, PersonViewModel>();
            Mapper.CreateMap<Book, FullBookViewModel>();
            Mapper.CreateMap<BookAmount, FullBookViewModel>()
                .ForMember(dest=>dest.BookAmount, pick => pick.MapFrom(src=>src.Amount));
        }
    }
}