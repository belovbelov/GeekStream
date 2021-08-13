using GeekStream.Core.Entities;

namespace GeekStream.Core.ViewModels.Account
{
    public class EditAccount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public FilePath Avatar { get; set; }
    }
}