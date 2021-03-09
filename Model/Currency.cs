using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Model
{
    public class Currency
    {
        public string Name { get; set; }

        [Key] public string Code { get; set; }

        public int Rate { get; set; }
    }
}