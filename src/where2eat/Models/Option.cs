using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace where2eat.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string OptionName { get; set; }
        public string OptionDescription { get; set; }
        public string OptionContributor { get; set; }
        public decimal OptionValue { get; set; }
    }
}
