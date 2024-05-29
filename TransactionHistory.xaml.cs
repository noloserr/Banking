using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for TransactionHistory.xaml
    /// </summary>
    public partial class TransactionHistory : Window
    {
        private User currentUser;
        public TransactionHistory(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadTransactions();
        }
        private void LoadTransactions()
        {
            using (BankContext context = new BankContext())
            {
                // Завантаження транзакцій користувача
                var userTransactions = context.Transactions
                    .Where(t => t.SourceUserId == currentUser.Id || t.TargetUserId == currentUser.Id)
                    .Include(t => t.SourceUser)
                    .Include(t => t.TargetUser)
                    .ToList();

                // Відображення транзакцій в ListBox або інший відповідний контрол
                TransactionListBox.ItemsSource = userTransactions;
            }
        }

    }
}
