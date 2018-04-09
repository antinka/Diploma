﻿using System;
using System.Collections.Generic;

namespace GameStore.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime Date { get; set; }

        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }
    }
}