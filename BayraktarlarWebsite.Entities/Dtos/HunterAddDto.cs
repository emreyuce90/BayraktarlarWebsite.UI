using BayraktarlarWebsite.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Dtos
{
    public class HunterAddDto
    {

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

        [MinLength(11, ErrorMessage = "Cep telefonu alanı 11 haneli olmalıdır")]
        [MaxLength(11, ErrorMessage = "Cep telefonu alanı 11 haneli olmalıdır")]
        [DataType(DataType.PhoneNumber)]
        public string CepTelefonu { get; set; }

        public int? ConsumptionPerYear { get; set; }
        public int? WasteBarrel { get; set; }

        public List<Town> Towns { get; set; }

        public int TownId { get; set; }
        public List<District> Districts { get; set; }
        public int DistrictId { get; set; }


        public bool Oil { get; set; }
        public bool Filter { get; set; }
        public bool Battery { get; set; }

    }
}
