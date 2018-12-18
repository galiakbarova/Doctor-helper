using System;
using System.Data.SqlClient;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DoctorHelper.Patient
{
    public partial class PatientLogin : ContentPage
    {
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String PatientTable = "patient";
        private readonly String Login = "login";
        private readonly String Password = "password";

        private Int32 UserId = -1;

        public PatientLogin()
        {
            InitializeComponent();
        }

        private SqlConnection OpenConnection()
        {
            String connectionString = "Server = " + DataSourse +
                "; Database = " + DataBase +
                "; User ID = " + User +
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
                String commandString = "SELECT id FROM " + PatientTable + " WHERE " + Login + " = '" + LoginEntry.Text.ToLower()
                    + "' AND " + Password + " = '" + PasswordEntry.Text.ToLower() + "';";
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
            Navigation.PushAsync(new PatientRegister());
        }

        void Login_Clicked(object sender, System.EventArgs e)
        {
            if (CheckUser())
            {
                Navigation.PushAsync(new PatientLK(UserId));
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            }
            else
                DisplayAlert("Внимание!", "Неверный логин или пароль!", "OK");
        }
    }
}
