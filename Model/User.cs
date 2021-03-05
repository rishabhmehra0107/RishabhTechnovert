﻿using System;
using System.ComponentModel.DataAnnotations;
using static Bank.Model.Constants;
namespace Bank.Model
{
    public class User
    {
        [Key] public int Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public UserType Type { get; set; }
    }
}