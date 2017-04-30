using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liuyida.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public virtual IList<Order> Orders { get; set; }
    }
}