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
	public partial class BloodResults : ContentPage
	{
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String ResultTable = "results";

        private Int32 PatientId { get; set; }

		public BloodResults (Int32 patientId)
		{
			InitializeComponent ();
            PatientId = patientId;
            SetBloodInvestigationResultsList();
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

        private void SetBloodInvestigationResultsList()
        {
            var listView = new ListView();
            var itemsSource = new List<String>();

            try
            {
                var connection = OpenConnection();
                String commandString = "SELECT investigation_date FROM " + ResultTable + " WHERE patient_id = " + PatientId + ";";

                var command = new SqlCommand(commandString, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string dateTime = reader.GetDateTime(0).Date.ToString("dd.MM.yyyy");
                        itemsSource.Add(dateTime);
                    }
                }

                connection.Close();
            }
            catch
            {
                DisplayAlert("Внимание!", "Не удалось загрузить результаты!", "OK");
            }

            listView.ItemTapped += OnItemTapped;
            listView.ItemsSource = itemsSource;
            ContentField.Children.Add(listView);
        }

        private Int32 GetInvestigationId(string dateTime)
        {
            int ID = -1;

            var date = Convert.ToDateTime(dateTime);

            var connection = OpenConnection();
            String commandString = "SELECT id FROM " + ResultTable + " WHERE investigation_date = '"
                + date.ToString("yyyy-MM-dd") + "';";
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

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = (ListView)sender;
            string dateTime = selectedItem.SelectedItem.ToString();
            Navigation.PushAsync(new BloodInvestigationResult(GetInvestigationId(dateTime)));
        }

        private void AddResultButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewInvestigationResult(PatientId));
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        }
    }
}