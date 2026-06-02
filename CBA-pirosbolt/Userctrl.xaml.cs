
using CBA_pirosbolt.Model;
using CBA_pirosbolt.Service;
using SQLite;
using System.Windows;

namespace CBA_pirosbolt
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {

            string felhasznalonevInput = loginUserTxt.Text;
            string jelszoInput = Password.HashPassword(loginPasswordText.Password);

            if (!string.IsNullOrEmpty(loginUserTxt.Text) || !string.IsNullOrEmpty(loginPasswordText.Password))
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                {
                    var user = connection.Table<Felhasznalo>().FirstOrDefault(u => u.FelhasznaloNev == felhasznalonevInput);

                    if (user != null)
                    {

                        if (user.Jelszo == jelszoInput)
                        {
                            // Sikeres belépés esetén, megnyitjuk főmenüt. 
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Belépés megtagadva!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Belépés megtagadva!");
                    }


                }
            }
            else
            {
                MessageBox.Show("Felhasználónév és jelszó megadása kötelező! ");
            }
        }
    }
}
