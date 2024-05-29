using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Amount { get; set; }
        public string SourceCardNumber { get; set; }
        public string TargetCardNumber { get; set; }
        public string Purpose { get; set; }

        //Ідентифікатор користувача, що ініціював транзакцію
        //public int? SourceUserId { get; set; }

        //Ідентифікатор користувача, що є отримувачем транзакції
        //public int? TargetUserId { get; set; }

        // Навігаційні властивості до користувачів
        public User SourceUser { get; set; }
        public User TargetUser { get; set; }
    }
}
