using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Genre")]
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(24)]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
