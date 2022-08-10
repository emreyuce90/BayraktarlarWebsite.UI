namespace BayraktarlarWebsite.UI.Models
{
    public class UserInfoViewModel
    {
        public bool  IsAdmin { get; set; }
        public string Username { get; set; }
        public int Unclosed { get; set; }
        public int Closed { get; set; }
        public int Remainder { get; set; }
        public int CountAssigneds { get; set; }

    }
}
