using AutoMapper;
using Library.API.ViewModels;
using Library.Core.Models;

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
            
            //Mapper.CreateMap<UserModel, UserProfileViewModel>().ForMember(dest=>dest.Gender, pick=>pick.MapFrom(src=>src.Gender == true ? GenderState.Male : GenderState.Female));
            //Mapper.CreateMap<UserModel, AccountViewModel>();
            //Mapper.CreateMap<AuthorModel, AuthorViewModel>()
            //    .ForMember(dest=>dest.Name, pick =>pick.MapFrom(src=>src.Name + " " + src.LastName));
            Mapper.CreateMap<AuthorModel, AuthorViewModel>();
            Mapper.CreateMap<BookModel, BookViewModel>();
        }
    }
}