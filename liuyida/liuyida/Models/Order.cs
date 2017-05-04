using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        public DateTime DeliveryTime { get; set; }

        [Required]
        public double DeliveryFee { get; set; }

        [Required]
        public DeliveryMethod DeliveryMethod { get; set; }

        public string DeliveryAddress { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double Discount { get; set; }

        [Required]
        public double Paid { get; set; }

        [Required]
        public Status Status { get; set; }

        public string Note { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IList<OrderItem> OrderItems { get; set; }
    }
}