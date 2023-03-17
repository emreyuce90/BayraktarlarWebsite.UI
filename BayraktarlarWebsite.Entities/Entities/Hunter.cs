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
        public Hunter()
        {
            HunterProducts = new List<HunterProduct>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Tabela adı alanı boş geçilemez")]
        public string TabelaAdi { get; set; }

        [Required(ErrorMessage = "Usta adı alanı boş geçilemez")]
        public string UstaAdi { get; set; }


        [Required(ErrorMessage = "Sanayi sitesi adı alanı boş geçilemez")]
        public string SanayiSitesiAdi { get; set; }

        public string Adres { get; set; }


        [Required(ErrorMessage = "Not alanı boş geçilemez")]
        public string Not { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime?  ModifiedDate { get; set; }

        [MinLength(11,ErrorMessage ="Cep telefonu alanı 11 haneli olmalıdır")]
        [DataType(DataType.PhoneNumber)]
        public string CepTelefonu { get; set; }

        public int? ConsumptionPerYear { get; set; }
        public int? WasteBarrel { get; set; }
        public Town Town { get; set; }
        public int? TownId { get; set; }
        public District District { get; set; }
        public int? DistrictId { get; set; }
        public List<HunterProduct> HunterProducts{ get; set; }

    }
}
