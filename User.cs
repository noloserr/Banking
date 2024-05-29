using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Card> UserCards { get; set; } // Додано список карток                                                      
        public ICollection<Transaction> SentTransactions { get; set; } 
        public ICollection<Transaction> ReceivedTransactions { get; set; } 

        public User()
        {
            UserCards = new List<Card>();
            SentTransactions = new List<Transaction>();
            ReceivedTransactions = new List<Transaction>();
        }

        // Метод для створення нової картки
        public void AddCard(Card card)
        {
            UserCards.Add(card);
        }
    }
}
