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
    public partial class Doctors : ContentPage
    {
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String DoctorTable = "doctor";

        private Int32 PatientId { get; set; }
        private Int32 HospitalId { get; set; }

        public Doctors(Int32 patientId, Int32 hospitalId)
        {
            InitializeComponent();
            PatientId = patientId;
            HospitalId = hospitalId;
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
                String commandString = "SELECT * FROM " + DoctorTable + " WHERE hospital_id = " + HospitalId + ";";
                var command = new SqlCommand(commandString, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string doctorName = reader.GetString(1) + " "
                            + reader.GetString(2) + " "
                            + reader.GetString(3);
                        DoctorsPicker.Items.Add(doctorName);
                    }
                }
                connection.Close();
            }
            catch
            {
                DisplayAlert("Внимание!", "Не удалось подключиться!", "OK");
            }
        }

        private int GetDoctorId()
        {
            int ID = -1;
            var connection = OpenConnection();
            string[] name = (DoctorsPicker.SelectedItem.ToString()).Split(' ');
            String commandString = "SELECT id FROM " + DoctorTable + " WHERE surname = N'"
                + name[0] + "';";
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

        private bool CheckDoctorSelection()
        {
            if (DoctorsPicker.SelectedItem == null)
                return false;
            return true;
        }

        private void ChooseDoctorButton_Clicked(object sender, EventArgs e)
        {
            if (CheckDoctorSelection())
            {
                Navigation.PushAsync(new Schedules(PatientId, GetDoctorId()));
            }
            else
                DisplayAlert("Внимание!", "Выберите врача!", "OK");
        }
    }
}