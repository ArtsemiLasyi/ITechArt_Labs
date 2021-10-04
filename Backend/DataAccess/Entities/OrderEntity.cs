using System;

namespace DataAccess.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Price { get; set; }
        public DateTime RegistratedAt { get; set; }
        public bool IsDeleted { get; set; }
        public SessionEntity Session { get; set; } = null!;
    }
}
