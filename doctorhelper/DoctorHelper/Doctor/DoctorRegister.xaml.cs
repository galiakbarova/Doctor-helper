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
        private readonly String ScheduleTable = "schedule";
        private readonly String Surname = "surname";
        private readonly String Name = "name";
        private readonly String Patronymic = "patronymic";
        private readonly String HospitalId = "hospital_id";
        private readonly String HospitalAddress = "address";
        private readonly String Login = "login";
        private readonly String Password = "password";
        private readonly String MondayShift = "mondayshift";
        private readonly String TuesdayShift = "tuesday_shift";
        private readonly String WednesdayShift = "wednesday_shift";
        private readonly String ThursdayShift = "thursday_shift";
        private readonly String FridayShift = "friday_shift";

        public DoctorRegister()
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

        private void AddNewDoctor()
        {
            try
            {
                var connection = OpenConnection();
                String commandString = "INSERT INTO " + DoctorTable +
                    "(" + Surname + ", " + Name + ", " + Patronymic + ", "
                    + HospitalId + ", " + Login + ", " + Password + ", "
                    + MondayShift + ", " + TuesdayShift
                    + ", " + WednesdayShift + ", " + ThursdayShift
                    + ", " + FridayShift + ") " +
                    "VALUES ('" + SurnameEntry.Text.ToLower() + "', '" +
                    NameEntry.Text.ToLower() + "', '" +
                    PatronymicEntry.Text.ToLower() + "', " +
                    "(SELECT id FROM " + HospitalTable + " WHERE " + HospitalAddress + " = N'" + HospitalEntry.Text.ToLower() + "'), '" +
                    LoginEntry.Text.ToLower() + "', '" +
                    PasswordEntry.Text.ToLower() + "', " +
                    Convert.ToInt32(MondayShiftEntry.Text) + ", " +
                    Convert.ToInt32(TuesdayShiftEntry.Text) + ", " +
                    Convert.ToInt32(WednesdayShiftEntry.Text) + ", " +
                    Convert.ToInt32(ThursdayShiftEntry.Text) + ", " +
                    Convert.ToInt32(FridayShiftEntry.Text) + ");";

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
                    if (CheckShifts())
                    {
                        AddNewDoctor();
                        var ID = GetDoctorId();
                        if (ID != -1)
                            Navigation.PushAsync(new DoctorLK(ID));
                    }
                    else
                        DisplayAlert("Внимание!", "Номер смены может быть 1 или 2!", "OK");
                else
                    DisplayAlert("Внимание!", "Пароли не совпадают!", "OK");
            else
                DisplayAlert("Внимание!", "Все поля должны быть заполнены!", "OK");
        }

        private int GetDoctorId()
        {
            var ID = -1;
            var connection = OpenConnection();

            String queryString = "SELECT id FROM " + DoctorTable + " WHERE " + Login + " = '" + LoginEntry.Text.ToLower()
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
                && (HospitalEntry.Text.Length > 0)
                && (LoginEntry.Text.Length > 0)
                && (PasswordEntry.Text.Length > 0)
                && (MondayShiftEntry.Text.Length > 0)
                && (TuesdayShiftEntry.Text.Length > 0)
                && (WednesdayShiftEntry.Text.Length > 0)
                && (ThursdayShiftEntry.Text.Length > 0)
                && (FridayShiftEntry.Text.Length > 0))
                return true;
            return false;
        }

        private bool CheckShifts()
        {
            if ((Convert.ToInt32(MondayShiftEntry.Text) == 1 || Convert.ToInt32(MondayShiftEntry.Text) == 2)
                && (Convert.ToInt32(TuesdayShiftEntry.Text) == 1 || Convert.ToInt32(TuesdayShiftEntry.Text) == 2)
                && (Convert.ToInt32(WednesdayShiftEntry.Text) == 1 || Convert.ToInt32(WednesdayShiftEntry.Text) == 2)
                && (Convert.ToInt32(ThursdayShiftEntry.Text) == 1 || Convert.ToInt32(ThursdayShiftEntry.Text) == 2)
                && (Convert.ToInt32(FridayShiftEntry.Text) == 1 || Convert.ToInt32(FridayShiftEntry.Text) == 2))
                return true;
            return false;
        }

        private bool CheckPasswordSimilarity()
        {
            return PasswordEntry.Text == RepeatPasswordEntry.Text;
        }

        public string GetInitialConsultationTime(DateTime date)
        {
            switch ((int)date.Date.DayOfWeek)
            {
                case 0:
                    if (MondayShiftEntry.Text == "1")
                        return "08:00";
                    else
                        return "13:00";
                case 1:
                    if (TuesdayShiftEntry.Text == "1")
                        return "08:00";
                    else
                        return "13:00";
                case 2:
                    if (WednesdayShiftEntry.Text == "1")
                        return "08:00";
                    else
                        return "13:00";
                case 4:
                    if (ThursdayShiftEntry.Text == "1")
                        return "08:00";
                    else
                        return "13:00";
                default:
                    return "00:00";
            }
        }

        private void MakeSchedule()
        {
            var connection = OpenConnection();
            var currentDate = DateTime.Now.Date;

            for (int i = 0; i < 5; i++)
            {
                String queryString = "INSERT INTO " + ScheduleTable
                    + "(doctor_id, consultation_date, consultation_time, is_free) VALUES ("
                    + GetDoctorId() + ", " + ");";

                var command = new SqlCommand(queryString, connection);
            }

        }
    }
}
