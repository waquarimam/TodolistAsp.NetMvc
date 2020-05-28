namespace Todolist.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("List")]
    public partial class List
    {
        public int Id { get; set; }

        
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime DueDate { get; set; }

        public int Done {get; set;}
    }
}
