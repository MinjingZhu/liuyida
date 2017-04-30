﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liuyida.Models
{
    public enum DeliveryMethod
    {
        Pickup = 0,
        Delivery = 1
    }

    public enum PaymentMethod
    {
        Cash = 0,
        Venmo = 1,
        QuickPay = 2,
        Paypal = 3,
        Other = 99
    }

    public enum Status
    {
        Created = 0,
        Packed = 1,
        Delivered = 2,
        Cancelled = 3
    }

    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime DeliveryTime { get; set; }

        public double DeliveryFee { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public string DeliveryAddress { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double Paid { get; set; }

        public Status Status { get; set; }

        public string Note { get; set; }

        public virtual Customer Customer { get; set; }
    }
}