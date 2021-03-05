using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Model
{
    public class Currency
    {
        [Key] public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int Rate { get; set; }
    }
}