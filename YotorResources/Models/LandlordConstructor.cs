using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YotorResources.Models
{
    public class LandlordConstructor
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Введите название организации")]
        public string OrganizationName { get; set; }
    }
}
