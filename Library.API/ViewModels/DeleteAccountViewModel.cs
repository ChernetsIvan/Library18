using System.Web.Mvc;

namespace Library.Web.ViewModels
{
    public class DeleteAccountViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string UserProfileId { get; set; }
        public string Login { get; set; }
        public bool AllowDelete { get; set; }
    }
}