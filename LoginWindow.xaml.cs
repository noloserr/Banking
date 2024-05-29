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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void createAcc_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            bool isAdmin;

            using (BankContext context = new BankContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

                if (user != null)
                {
                    if (user.Email == "admin@gmail.com" && user.Password == "qwerty12345Admin")
                    {
                        // Адміністратор ввійшов
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                    }
                    else
                    {
                        // Звичайний користувач ввійшов
                        MessageBox.Show($"Ви увійшли як {user.Name} {user.LastName}!");
                        //Створюємо вікно з основною інформацією
                        MainWindow mainWindow = new MainWindow(user);
                        mainWindow.Show();
                    }
                }
                else
                {
                    // Неправильні дані входу
                    MessageBox.Show("Неправильний email або пароль!");
                }
            }
        }
    }
}
