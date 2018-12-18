using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorHelper.Doctor
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PatientInfo : ContentPage
    {
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String PatientTable = "patient";

        private Int32 PatientId { get; set; }

		public PatientInfo (Int32 patientId)
		{
			InitializeComponent ();
            PatientId = patientId;
            SetPatientInfo();
        }

        private void SetPatientInfo()
        {
            try
            {
                var connection = OpenConnection();
                String commandString = "SELECT * FROM " + PatientTable + " WHERE id = " + PatientId + ";";
                var command = new SqlCommand(commandString, connection);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SurnameLabel.Text = reader.GetString(1);
                        NameLabel.Text = reader.GetString(2);
                        PatronymicLabel.Text = reader.GetString(3);
                    }
                }
                connection.Close();
            }
            catch
            {
                DisplayAlert("Внимание!", "Не удалось подключиться!", "OK");
            }
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

        void ShowAnalyze_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Patient.BloodResults(PatientId));
        }
    }
}