using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name{ get; set; }

        public ICollection<User> Users { get; set; }
    }
}
