using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM
{
    public class PaymentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Food { get; set; }

        public string Food1 { get; set; }

        public string PaymentMethod { get; set; }

        public int Total {  get; set; }
    }
}
