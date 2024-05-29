using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Banking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Створюємо змінну для поточного користувача
        private User currentUser;
        public MainWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadUserData();
            UpdateCardInfo();
            ShowCards_Click(showCards, null);
        }

        //оновлення даних картки при вході
        private void LoadUserData()
        {
            using (BankContext context = new BankContext())
            {
                // Завантаження користувача з його картками
                currentUser = context.Users.Include("UserCards").FirstOrDefault(u => u.Id == currentUser.Id);
            }

            // Відображення нікнейму
            UsernameTextBlock.Text = $"{currentUser.Name} {currentUser.LastName}";

            // Відображення інформації про картки
            UpdateCardInfo();
        }

        private void UpdateCardInfo()
        {
            // Якщо у користувача є картки, відображаємо інформацію про першу картку
            if (currentUser.UserCards.Any())
            {
                CardInfoTextBlock.Text = $"Картка: {currentUser.UserCards.First().CardNumber}, Баланс: {currentUser.UserCards.First().Balance}";
            }
            else
            {
                CardInfoTextBlock.Text = "Не створено жодної картки";
            }
        }
            

        private void ShowCards_Click(object sender, RoutedEventArgs e)
        {
            lstCards.Items.Clear(); // Очищуємо список перед додаванням нових елементів
            foreach (var card in currentUser.UserCards)
            {
                card.CheckCardStatus(); // Перевіряємо статус кожної картки
                lstCards.Items.Add(card); // Додаємо об'єкт Card до ListBox
            }
        }

        private void TransactionHistory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void createCards_Click(object sender, RoutedEventArgs e)
        {
            

            var newCard = new Card(currentUser);

            using (BankContext context = new BankContext())
            {
                context.Users.Attach(currentUser); // Прикріплюємо поточного користувача до контексту
                // Додаємо картку до контексту
                context.Cards.Add(newCard);
                context.SaveChanges();
            }

            currentUser.UserCards.Add(newCard); // Додаємо нову картку до списку карток користувача
            // Оновлюємо інформацію про картки в інтерфейсі
            UpdateCardInfo();
            MessageBox.Show($"Картка {newCard.CardNumber} успішно створена!");
        }

        private void lstCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Перевірка, чи є обрана карта в списку
            if (lstCards.SelectedItem != null)
            {
                // Отримання вибраної карти
                Card selectedCard = (Card)lstCards.SelectedItem;

                // Відображення інформації про вибрану карту у CardInfoTextBlock
                string status = selectedCard.IsActive ? "Активна" : "Неактивна";
                CardInfoTextBlock.Text = $"Номер картки: {selectedCard.CardNumber}, CVV: {selectedCard.CVV}, Дата закінчення: {selectedCard.ExpiryDate.ToShortDateString()}, Статус: {status}, Баланс: {selectedCard.Balance}";
            }
        }

        private void MakeTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (lstCards.SelectedItem != null)
            {
                Card selectedCard = (Card)lstCards.SelectedItem;
                TransactionWindow transactionWindow = new TransactionWindow(selectedCard, currentUser);
                transactionWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть картку для здійснення транзакції.");
            }
        }
    }
}
