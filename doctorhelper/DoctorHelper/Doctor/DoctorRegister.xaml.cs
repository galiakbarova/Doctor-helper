using System;
using System.Data.SqlClient;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DoctorHelper.Doctor
{
    public partial class DoctorRegister : ContentPage
    {
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String DoctorTable = "doctor";
        private readonly String HospitalTable = "hospital";
        private readonly String Surname = "surname";
        private readonly String Name = "name";
        private readonly String Patronymic = "patronymic";
        private readonly String HospitalId = "hospital_id";
        private readonly String HospitalAddress = "address";
        private readonly String Login = "login";
        private readonly String Password = "password";

        public DoctorRegister()
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

        private void AddNewDoctor()
        {
            try
            {
                var connection = OpenConnection();
                String commandString = "INSERT INTO " + DoctorTable +
                    "(" + Surname + ", " + Name + ", " + Patronymic + ", " +
                    HospitalId + ", " + Login + ", " + Password + ") " +
                    "VALUES (" + SurnameEntry.Text.ToLower() + ", " +
                    NameEntry.Text.ToLower() + ", " +
                    PatronymicEntry.Text.ToLower() + ", " +
                    "(SELECT FROM " + HospitalTable + " id WHERE " + HospitalAddress + " = " + HospitalEntry.Text.ToLower() + "), " +
                    LoginEntry.Text.ToLower() + ", " +
                    PasswordEntry.Text.ToLower() + ")";

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
                    AddNewDoctor();
                else
                    DisplayAlert("Внимание!", "Пароли не совпадают!", "OK");
            else
                DisplayAlert("Внимание!", "Все поля должны быть заполнены!", "OK");
        }

        private bool CheckDataFilling()
        {
            if ((SurnameEntry.Text.Length > 0)
                && (NameEntry.Text.Length > 0)
                && (PatronymicEntry.Text.Length > 0)
                && (HospitalEntry.Text.Length > 0)
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
