using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string OwnerName { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public string CVV {  get; set; }
        public int UserId { get; set; } // Зовнішній ключ до користувача
        public User User { get; set; } // Навігаційна властивість для користувача

        // Порожній конструктор для Entity Framework
        public Card() { }
        public Card(User owner)
        {
            OwnerName = $"{owner.Name} {owner.LastName}";
            Balance = 0; // Початковий баланс картки
            // Генерація номера та CVV-коду
            CardNumber = GenerateCardNumber();
            CVV = GenerateCVV();
            // Автоматично встановлюємо строк дії картки на 4 роки
            ExpiryDate = DateTime.Now.AddYears(4);
            //Зберігаємо айді нашого користувача
            UserId = owner.Id;
            User = owner;
            // Картка активна після створення
            IsActive = true;
        }

        // Метод для генерації унікального номера картки
        private string GenerateCardNumber()
        {
            Random random = new Random();
            StringBuilder cardNumber = new StringBuilder();

            // Генерація номера картки у форматі 16 цифр
            for (int i = 0; i < 16; i++)
            {
                cardNumber.Append(random.Next(0, 10));
            }

            return cardNumber.ToString();
        }
        // Метод для перевірки активності картки
        public void CheckCardStatus()
        {
            if (DateTime.Now > ExpiryDate)
            {
                IsActive = false;
            }
        }
        //генеруємо рандомний CVV-код для карти
        private string GenerateCVV()
        {
            Random random = new Random();
            StringBuilder cvv = new StringBuilder();

            // Генерація CVV-коду у форматі 3 цифр
            for (int i = 0; i < 3; i++)
            {
                cvv.Append(random.Next(0, 10));
            }

            return cvv.ToString();
        }
    }
}
