using System.ComponentModel.DataAnnotations;

namespace ComponentTesting.Inprocess.Data
{
    public abstract class Entity
    {
        [Key]
        public long Id { get; set; }
 
    }
}