using BayraktarlarWebsite.Entities.Dtos;

namespace BayraktarlarWebsite.UI.Models
{
    public class EmployeeLetDetails
    {
        public LetListDto Lets { get; set; }
        //Çalışma yılı
        public int WorkYear { get; set; }
        //Bu yıl izin hakkı
        public int ThisYearLetRight { get; set; }
        //Geçen yıl izin hakkı
        public int LastYearLetRight { get; set; }
        //Kalan izin hakkı
        public int Balance { get; set; }
        //Kullanılan izinler
        public int UsedLets { get; set; }
        public int ThisYearLetUsed { get; set; }
        //Geçen yıl izin hakkı
        public int LastYearLetUsed { get; set; }

        
    }
}
