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
    /// Interaction logic for CreateCardWindow.xaml
    /// </summary>
    public partial class CreateCardWindow : Window
    {
        public CreateCardWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //// Отримання даних з полів введення
            //string cardNumber = txtCardNumber.Text;
            //string expiryDate = txtExpiryDate.Text;

            //// Перевірка введених даних
            //if (!string.IsNullOrEmpty(cardNumber) && !string.IsNullOrEmpty(expiryDate))
            //{
            //    // Створення нової картки
            //    Card newCard = new Card
            //    {
            //        CardNumber = cardNumber,
            //        ExpiryDate = expiryDate

            //    };

            //    // Додавання нової картки до поточного користувача
            //    //currentUser.AddCard(newCard);

            //    // Оповіщення користувача про успішне створення картки
            //    MessageBox.Show("New card created successfully!");
            //}
            //else
            //{
            //    // Виведення повідомлення про неправильно введені дані
            //    MessageBox.Show("Please fill in all fields.");
            //}
        }
    }
}
