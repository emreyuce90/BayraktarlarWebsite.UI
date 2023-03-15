using BayraktarlarWebsite.Shared.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Hunter : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Tabela adı alanı boş geçilemez")]
        public string TabelaAdi { get; set; }
        [Required(ErrorMessage = "Usta adı alanı boş geçilemez")]

        public string UstaAdi { get; set; }
        [Required(ErrorMessage = "İl adı alanı boş geçilemez")]

        public string İl { get; set; }
        [Required(ErrorMessage = "İlçe adı alanı boş geçilemez")]

        public string İlçe { get; set; }
        [Required(ErrorMessage = "Sanayi sitesi adı alanı boş geçilemez")]

        public string SanayiSitesiAdi { get; set; }
        public string Adres { get; set; }
        [Required(ErrorMessage = "Kategori adı alanı boş geçilemez")]

        public string KategoriAdi { get; set; }
        [Required(ErrorMessage = "Not alanı boş geçilemez")]
        public string Not { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string CepTelefonu { get; set; }

    }
}
