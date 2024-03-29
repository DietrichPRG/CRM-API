﻿using Domain.Interfaces;

namespace Domain.Client.ScheduleAgregate.Data
{
    public class Schedule : IDomainClientModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public int IdCustomer { get; set; }
    }
}
