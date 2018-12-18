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
    public partial class Schedules : ContentPage
    {
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String ScheduleTable = "schedule";

        private Int32 PatientId { get; set; }
        private Int32 DoctorId { get; set; }

        public Schedules(Int32 patientId, Int32 doctorId)
        {
            InitializeComponent();
            PatientId = patientId;
            DoctorId = doctorId;
            SetDatesListView();

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

        private void SetDatesListView()
        {
            var listView = new ListView();
            var itemsSource = new List<String>();

            try
            {
                var connection = OpenConnection();
                String commandString = "SELECT * FROM " + ScheduleTable + " WHERE doctor_id = " + DoctorId + " AND is_free = 'True';";

                var command = new SqlCommand(commandString, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string dateTime = reader.GetDateTime(2).Date.ToString("dd.MM.yyyy") +
                            " " + reader.GetTimeSpan(3).ToString();
                        itemsSource.Add(dateTime);
                    }
                }

                connection.Close();
            }
            catch
            {
                DisplayAlert("Внимание!", "Не удалось подключиться!", "OK");
            }

            listView.ItemsSource = itemsSource;
            listView.ItemTapped += OnItemTapped;
            ContentField.Children.Add(listView);
        }

        private void RegistrateTime(DateTime dateTime)
        {
            try
            {
                var connection = OpenConnection();

                string date = dateTime.Date.ToString("yyyy-MM-dd");
                string time = dateTime.TimeOfDay.ToString();

                String commandString = "UPDATE " + ScheduleTable +
                        " SET is_free = 'False', patient_id = " + PatientId + " WHERE doctor_id = " + DoctorId
                        + " AND consultation_date = '" + date + "' AND consultation_time = '" + time + "';";

                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
                DisplayAlert("Внимание!", "Вы успешно записались к врачу!", "OK");
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 1]);
            }
            catch
            {
                DisplayAlert("Внимание!", "Не удалось подключиться!", "OK");
            }
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = (ListView)sender;
            string dateTime = selectedItem.SelectedItem.ToString();
            RegistrateTime(Convert.ToDateTime(dateTime));
        }
    }
}