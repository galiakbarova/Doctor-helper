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
    public partial class NewInvestigationResult : ContentPage
    {
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String ResultTable = "results";

        private Int32 PatientId { get; set; }

        public NewInvestigationResult(Int32 patientId)
        {
            InitializeComponent();
            PatientId = patientId;
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

        private bool CheckDataFilling()
        {
            return (DateEntry.Text.Length > 0
                && HgbEntry.Text.Length > 0
                && RbcEntry.Text.Length > 0
                && WbcEntry.Text.Length > 0
                && EoEntry.Text.Length > 0
                && BaEntry.Text.Length > 0
                && NeEntry.Text.Length > 0
                && MoEntry.Text.Length > 0
                && AtlEntry.Text.Length > 0
                && PltEntry.Text.Length > 0
                && SoeEntry.Text.Length > 0
                && ColorEntry.Text.Length > 0
                && HctEntry.Text.Length > 0);
        }

        private void AddNewResults()
        {
            try
            {
                var connection = OpenConnection();
                DateTime dateTime = Convert.ToDateTime(DateEntry.Text);

                String commandString = "INSERT INTO " + ResultTable +
                    " (patient_id, investigation_date, hgb, rbc, wbc, eo, ba, ne, mo, atl, plt, soe, color, hct) " +
                    "VALUES (" + PatientId + ", '" +
                    dateTime.ToString("yyyy-MM-dd") + "', " +
                    HgbEntry.Text.Replace(',', '.') + ", " +
                    RbcEntry.Text.Replace(',', '.') + ", " +
                    WbcEntry.Text.Replace(',', '.') + ", " +
                    EoEntry.Text.Replace(',', '.') + ", " +
                    BaEntry.Text.Replace(',', '.') + ", " +
                    NeEntry.Text.Replace(',', '.') + ", " +
                    MoEntry.Text.Replace(',', '.') + ", " +
                    AtlEntry.Text.Replace(',', '.') + ", " +
                    PltEntry.Text.Replace(',', '.') + ", " +
                    SoeEntry.Text.Replace(',', '.') + ", N'" +
                    ColorEntry.Text.Replace(',', '.') + "', " +
                    HctEntry.Text.Replace(',', '.') + ");";

                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
                DisplayAlert("Внимание!", "Результаты успешно добавлены!", "OK");
            }
            catch
            {
                DisplayAlert("Внимание!", "Не удалось подключиться!", "OK");
            }
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            if (CheckDataFilling())
            {
                AddNewResults();
                Navigation.PushAsync(new BloodResults(PatientId));
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            }
            else
                DisplayAlert("Внимание!", "Все поля должны быть заполнены!", "OK");
        }
    }
}