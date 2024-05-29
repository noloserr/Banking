using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        // Функція для перевірки правильності формату електронної пошти за допомогою регулярних виразів
        private bool IsValidEmail(string email)
        {
            // Використовуємо простий регулярний вираз для перевірки формату електронної пошти
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            return Regex.IsMatch(email, pattern);
        }



        private void regUser_Click(object sender, RoutedEventArgs e)
        {
            //Проводимо операції в безпечому режимі
            using(BankContext context = new BankContext())
            {
                //read the info from the textboxes
                string firstName = txtFirstName.Text;
                string secondName = txtSecondName.Text;
                string surname = txtSurname.Text;
                string mail = txtMail.Text;
                string pass = txtPassword.Text;

                // Перевірка на наявність даних у полях
                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(secondName) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(pass))
                {
                    MessageBox.Show("Заповніть всі поля!");
                    return;
                }

                // Перевірка правильності формату електронної пошти
                if (!IsValidEmail(mail))
                {
                    MessageBox.Show("Неправильний формат електронної пошти!");
                    return;
                }

                // Перевірка наявності користувача з такою електронною поштою
                var existingUser = context.Users.FirstOrDefault(u => u.Email == mail);
                if (existingUser != null)
                {
                    MessageBox.Show("Акаунт з такою електронною поштою вже існує!");
                    return;
                }

                var user = new User
                {
                    Name = firstName,
                    LastName = secondName,
                    Surname = surname,
                    Email = mail,
                    Password = pass
                };

                context.Users.Add(user);
                context.SaveChanges();
                MessageBox.Show($"Новий профіль {firstName} успішно створений!");
                Close();
            }
            
        }
    }
}
