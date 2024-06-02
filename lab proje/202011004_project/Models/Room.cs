using System;
using System.Collections.Generic;

namespace loginDemo.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
    
}
