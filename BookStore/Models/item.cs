namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int item_id { get; set; }

        [Required]
        [StringLength(30)]
        public string item_name { get; set; }


        [Range(0, 100000, ErrorMessage = "hey..the money can't be negative")]
        [DataType(DataType.Currency)]
        public int item_price { get; set; }

        public int item_quantity { get; set; }
    }
}
