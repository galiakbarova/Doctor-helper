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
	public partial class BloodInvestigationResult : ContentPage
    {
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String ResultTable = "results";

        private Int32 InvestigationId { get; set; }
        double Hgb;
        double Rbc;
        double Wbc;
        double Eo;
        double Ba;
        double Ne;
        double Mo;
        double Atl;
        double Plt;
        double Soe;
        string color;
        double Hct;

        public BloodInvestigationResult (int investigationId)
		{
			InitializeComponent ();
            InvestigationId = investigationId;
            SetBloodInvestigationResults();
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

        private void SetBloodInvestigationResults()
        {
            //try
            //{
                var connection = OpenConnection();
                String commandString = "SELECT * FROM " + ResultTable + " WHERE id = " + InvestigationId + ";";

                var command = new SqlCommand(commandString, connection);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateLabel.Text = reader.GetDateTime(2).ToString("dd-MM-yyyy");

                        Hgb = reader.GetDouble(3);
                        HgbLabel.Text = Hgb.ToString();

                        Rbc = reader.GetDouble(4);
                        RbcLabel.Text = Rbc.ToString();

                        Wbc = reader.GetDouble(5);
                        WbcLabel.Text = Wbc.ToString();

                        Eo = reader.GetDouble(6);
                        EoLabel.Text = Eo.ToString();

                        Ba = reader.GetDouble(7);
                        BaLabel.Text = Ba.ToString();

                        Ne = reader.GetDouble(8);
                        NeLabel.Text = Ne.ToString();

                        Mo = reader.GetDouble(9);
                        MoLabel.Text = Mo.ToString();

                        Atl = reader.GetDouble(10);
                        AtlLabel.Text = Atl.ToString();

                        Plt = reader.GetDouble(11);
                        PltLabel.Text = Plt.ToString();

                        Soe = reader.GetDouble(12);
                        SoeLabel.Text = Soe.ToString();

                        color = reader.GetString(13);
                        ColorLabel.Text = color;

                        Hct = reader.GetDouble(14);
                        HctLabel.Text = Hct.ToString();
                    }
                }

                connection.Close();
            //}
            //catch
            //{
            //    DisplayAlert("Внимание!", "Не удалось загрузить результаты!", "OK");
            //}
        }
    }
}