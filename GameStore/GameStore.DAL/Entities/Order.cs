using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.DAL.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
