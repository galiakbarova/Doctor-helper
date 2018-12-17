using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorHelper.Patient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Hospitals : ContentPage
    {
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String HospitalTable = "hospital";

        private Int32 UserId { get; set; }

        public Hospitals(Int32 userId)
        {
            InitializeComponent();
            UserId = userId;
            SetPickerItems();
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

        private void SetPickerItems()
        {
            try
            {
                var connection = OpenConnection();
                String commandString = "SELECT name FROM " + HospitalTable + ";";
                var command = new SqlCommand(commandString, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HospitalsPicker.Items.Add(reader.GetString(0));
                    }
                }
                connection.Close();
            }
            catch
            {
                DisplayAlert("Внимание!", "Не удалось подключиться!", "OK");
            }
        }

        private bool CheckHospitalSelection()
        {
            if (HospitalsPicker.SelectedItem == null)
                return false;
            return true;
        }

        private int GetHospitalId()
        {
            int ID = -1;
            var connection = OpenConnection();
            String commandString = "SELECT id FROM " + HospitalTable + " WHERE name = N'"
                + HospitalsPicker.SelectedItem.ToString() + "';";
            var command = new SqlCommand(commandString, connection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    ID = reader.GetInt32(0);
                }
            }
            connection.Close();

            return ID;
        }

        private void ChooseHospitalButton_Clicked(object sender, EventArgs e)
        {
            if (CheckHospitalSelection())
            {
                Navigation.PushAsync(new Doctors(UserId, GetHospitalId()));
            }
            else
                DisplayAlert("Внимание!", "Выберите клинику!", "OK");
        }
    }
}