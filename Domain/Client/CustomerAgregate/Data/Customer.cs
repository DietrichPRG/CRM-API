﻿using Domain.Interfaces;

namespace Domain.Client.CustomerAgregate.Data
{
    public class Customer : IDomainClientModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BrithDay { get; set; }
        public DateTime Created { get; set; }
        public string Identification { get; set; } = null!;
    }
}
