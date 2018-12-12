using System;
using System.Data.SqlClient;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DoctorHelper.Doctor
{
    public partial class DoctorLogin : ContentPage
    {
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String DoctorTable = "doctor";
        private readonly String Login = "login";
        private readonly String Password = "password";

        private Int32 UserId = -1;

        public DoctorLogin()
        {
            InitializeComponent();
        }

        private SqlConnection OpenConnection()
        {
            String connectionString = "Data Source = " + DataSourse +
                "; Initial Catalog = " + DataBase +
                "; Persist Security Info = true; User ID = " + User +
                "; Password = " + DbPassword;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        private bool CheckUser()
        {
            try
            {
                var connection = OpenConnection();
                String commandString = "SELECT id FROM " + DoctorTable + " WHERE " + Login + " = " + LoginEntry.Text.ToLower()
                    + " AND " + Password + " = " + PasswordEntry.Text.ToLower();
                var command = new SqlCommand(commandString, connection);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        UserId = reader.GetInt32(0);
                }
                connection.Close();
                return UserId != -1;
            }
            catch
            {
                DisplayAlert("Внимание!", "Не удалось подключиться!", "OK");
                return false;
            }
        }

        void Reg_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new DoctorRegister());
        }

        void Login_Clicked(object sender, System.EventArgs e)
        {
            if (CheckUser())
                Navigation.PushAsync(new DoctorLK(UserId));
            else
                DisplayAlert("Внимание!", "Неверный логин или пароль!", "OK");
        }
    }
}
