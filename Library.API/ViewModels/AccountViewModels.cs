using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Library.API.ViewModels
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string UserName { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string UserName { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }

    public class AccountViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string UserProfileId { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        
        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}
