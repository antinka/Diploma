using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    public class OrderDetail : BaseEntity
    {
        public decimal Price { get; set; }

        [ForeignKey("Game")]
        public Guid GameId { get; set; }

        public virtual Game Game { get; set; }

        [Column(TypeName = "SMALLINT")]
        public short Quantity { get; set; }

        [Column(TypeName = "REAl")]
        public float Discount { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
