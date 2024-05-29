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
    /// Interaction logic for transaction.xaml
    /// </summary>
    public partial class TransactionWindow : Window
    {
        private Card sourceCard;
        private User currentUser;
        public TransactionWindow(Card selectedCard, User user)
        {
            InitializeComponent();
            sourceCard = selectedCard;
            currentUser = user;
            SourceCardTextBox.Text = sourceCard.CardNumber;

        }

        private void ExecuteTransaction_Click(object sender, RoutedEventArgs e)
        {
            decimal amount;
            if (!decimal.TryParse(AmountTextBox.Text, out amount))
            {
                MessageBox.Show("Введіть коректну суму.");
                return;
            }

            string targetCardNumber = TargetCardNumberTextBox.Text;
            string purpose = PurposeTextBox.Text;

            if (string.IsNullOrWhiteSpace(targetCardNumber) || string.IsNullOrWhiteSpace(purpose))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля.");
                return;
            }

            using (BankContext context = new BankContext())
            {
                var realSourse = context.Cards.FirstOrDefault(c => c.CardNumber == sourceCard.CardNumber);
                var targetCard = context.Cards.FirstOrDefault(c => c.CardNumber == targetCardNumber);
                if (targetCard == null || realSourse == null)
                {
                    MessageBox.Show("Картка одержувача не знайдена або відпарвника не знайдена");
                    return;
                }

                if (sourceCard.Balance < amount)
                {
                    MessageBox.Show("Недостатньо коштів.");
                    return;
                }

                
                realSourse.Balance -= amount;
                targetCard.Balance += amount;

                var transaction = new Transaction
                {
                    Timestamp = DateTime.Now,
                    Amount = amount,
                    SourceCardNumber = sourceCard.CardNumber,
                    TargetCardNumber = targetCardNumber,
                    Purpose = purpose
                    //SourceUserId = realSourse.User.Id,
                    //TargetUserId = targetCard.User.Id

                };

                context.Transactions.Add(transaction);
                context.SaveChanges();

                MessageBox.Show("Транзакцію виконано успішно.");
                Close();
            }
        }
    }
}
