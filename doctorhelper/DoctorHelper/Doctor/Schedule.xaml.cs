﻿using System;
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
    public partial class Schedule : ContentPage
    {
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String ScheduleTable = "schedule";

        private Int32 DoctorId { get; set; }

        public Schedule(Int32 doctorId)
        {
            InitializeComponent();
            DoctorId = doctorId;
            SetScheduleData();
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

        private void SetScheduleData()
        {
            var listView = new ListView();
            var itemsSource = new List<String>();

            try
            {
                var connection = OpenConnection();
                String commandString = "SELECT * FROM " + ScheduleTable + " WHERE doctor_id = " + DoctorId + " AND is_free = 'False';";

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

        private Int32 GetPatientId(string dateTime)
        {
            int ID = -1;
            var date = Convert.ToDateTime(dateTime);

            var connection = OpenConnection();
            String commandString = "SELECT patient_id FROM " + ScheduleTable + " WHERE consultation_date = '"
                + date.ToString("yyyy-MM-dd") + "' AND consultation_time = '" + date.TimeOfDay + "';";
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
            Navigation.PushAsync(new PatientInfo(GetPatientId(dateTime)));
        }
    }
}