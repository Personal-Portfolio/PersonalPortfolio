using System;

namespace PersonalPortfolio.Shared.Storage
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

    }
}