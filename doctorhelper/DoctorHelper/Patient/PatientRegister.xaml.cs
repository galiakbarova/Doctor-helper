using System;
using System.Data.SqlClient;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DoctorHelper.Patient
{
    public partial class PatientRegister : ContentPage
    {
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String PatientTable = "patient";
        private readonly String Surname = "surname";
        private readonly String Name = "name";
        private readonly String Patronymic = "patronymic";
        private readonly String Login = "login";
        private readonly String Password = "password";

        public PatientRegister()
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

        private void AddNewPatient()
        {
            try
            {
                var connection = OpenConnection();
                String commandString = "INSERT INTO " + PatientTable +
                    "(" + Surname + ", " + Name + ", " + Patronymic + ", " + Login + ", " + Password + ") " +
                    "VALUES ('" + SurnameEntry.Text.ToLower() + "', '" +
                    NameEntry.Text.ToLower() + "', '" +
                    PatronymicEntry.Text.ToLower() + "', '" +
                    LoginEntry.Text.ToLower() + "', '" +
                    PasswordEntry.Text.ToLower() + "')";

                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            catch
            {
                DisplayAlert("Внимание!", "Не удалось добавить нового пользователя!", "OK");
            }
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (CheckDataFilling())
                if (CheckPasswordSimilarity())
                {
                    AddNewPatient();
                    var ID = GetPatientId();
                    if (ID != -1)
                        Navigation.PushAsync(new PatientLK(ID));
                }
                else
                    DisplayAlert("Внимание!", "Пароли не совпадают!", "OK");
            else
                DisplayAlert("Внимание!", "Все поля должны быть заполнены!", "OK");
        }

        private int GetPatientId()
        {
            var ID = -1;
            var connection = OpenConnection();

            String queryString = "SELECT id FROM " + PatientTable + " WHERE " + Login + " = '" + LoginEntry.Text.ToLower()
                    + "' AND " + Password + " = '" + PasswordEntry.Text.ToLower() + "';";

            var command = new SqlCommand(queryString, connection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                    ID = reader.GetInt32(0);
            }

            return ID;
        }

        private bool CheckDataFilling()
        {
            if ((SurnameEntry.Text.Length > 0)
                && (NameEntry.Text.Length > 0)
                && (PatronymicEntry.Text.Length > 0)
                && (LoginEntry.Text.Length > 0)
                && (PasswordEntry.Text.Length > 0))
                return true;
            return false;
        }


        private bool CheckPasswordSimilarity()
        {
            return PasswordEntry.Text == RepeatPasswordEntry.Text;
        }
    }
}
