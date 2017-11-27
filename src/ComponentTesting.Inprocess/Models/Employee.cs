using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ComponentTesting.Inprocess.Data;

namespace ComponentTesting.Inprocess.Models
{
    [Table("employees")]
    public class Employee: Entity
    {
        [Required]
        public string Name { get; set; }
    }

}