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
using System.Windows.Shapes;

namespace Banking
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            LoadUsers();
        }
        private void LoadUsers()
        {
            using (BankContext context = new BankContext())
            {
                var users = context.Users.ToList();
                UsersListBox.ItemsSource = users;
            }
        }

        private void SearchTransactions_Click(object sender, RoutedEventArgs e)
        {
            int userId;
            if (!int.TryParse(UserIdTextBox.Text, out userId))
            {
                MessageBox.Show("Введіть коректний ID користувача.");
                return;
            }

            using (BankContext context = new BankContext())
            {
                var transactions = context.Transactions
                                          .Where(t => t.SourceCardNumber == userId.ToString() || t.TargetCardNumber == userId.ToString())
                                          .ToList();
                TransactionsListBox.ItemsSource = transactions;
            }
        }

        private void AddMoney_Click(object sender, RoutedEventArgs e)
        {
            string cardNumber = CardNumberTextBox.Text;
            decimal amount;

            if (!decimal.TryParse(AmountTextBox.Text, out amount))
            {
                MessageBox.Show("Введіть коректну суму.");
                return;
            }

            using (BankContext context = new BankContext())
            {
                var card = context.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);

                if (card != null)
                {
                    card.Balance += amount;
                    context.SaveChanges();
                    MessageBox.Show($"На картку {cardNumber} додано {amount} грн.");
                }
                else
                {
                    MessageBox.Show("Картку не знайдено.");
                }
            }

        }

        private void TransferMoney_Click(object sender, RoutedEventArgs e)
        {
            string sourceCardNumber = SourceCardTextBox.Text;
            string targetCardNumber = TargetCardTextBox.Text;
            decimal amount;

            if (!decimal.TryParse(TransferAmountTextBox.Text, out amount))
            {
                MessageBox.Show("Введіть коректну суму.");
                return;
            }

            using (BankContext context = new BankContext())
            {
                var sourceCard = context.Cards.FirstOrDefault(c => c.CardNumber == sourceCardNumber);
                var targetCard = context.Cards.FirstOrDefault(c => c.CardNumber == targetCardNumber);

                if (sourceCard != null && targetCard != null)
                {
                    if (sourceCard.Balance >= amount)
                    {
                        sourceCard.Balance -= amount;
                        targetCard.Balance += amount;
                        context.SaveChanges();
                        MessageBox.Show($"Переведено {amount} грн з картки {sourceCardNumber} на картку {targetCardNumber}.");
                    }
                    else
                    {
                        MessageBox.Show("Недостатньо коштів на картці.");
                    }
                }
                else
                {
                    MessageBox.Show("Картки не знайдено.");
                }
            }
        }

        private void UsersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
