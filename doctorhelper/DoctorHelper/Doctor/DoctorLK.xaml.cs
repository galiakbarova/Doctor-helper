﻿using System;
using System.Data.SqlClient;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DoctorHelper.Doctor
{
    public partial class DoctorLK : ContentPage
    {
        private readonly String DataSourse = "104.248.240.225";
        private readonly String User = "sa";
        private readonly String DbPassword = "Unya7532";
        private readonly String DataBase = "doctor_helper";
        private readonly String DoctorTable = "doctor";
        private readonly String HospitalTable = "hospital";
        private readonly String DataBaseId = "id";

        private Int32 UserId { get; set; }

        public DoctorLK(int userId)
        {
            InitializeComponent();
            UserId = userId;
            SetUserData();
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

        private void SetUserData()
        {
            try
            {
                var connection = OpenConnection();
                String commandString = "SELECT * FROM " + DoctorTable + " WHERE " + DataBaseId + " = " + UserId + ";";
                var command = new SqlCommand(commandString, connection);
                var hospital_id = -1;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SurnameLabel.Text = reader.GetString(1);
                        NameLabel.Text = reader.GetString(2);
                        PatronymicLabel.Text = reader.GetString(3);
                        hospital_id = reader.GetInt32(4);
                    }
                }

                commandString = "SELECT * FROM " + HospitalTable + " WHERE " + DataBaseId + " = " + hospital_id + ";";
                command = new SqlCommand(commandString, connection);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        HospitalNameLabel.Text = reader.GetString(1);
                        HospitalAddressLabel.Text = reader.GetString(2);
                    }
                }

                connection.Close();
            }
            catch
            {
                DisplayAlert("Внимание!", "Не удалось подключиться!", "OK");
            }
        }

        void ShowSchedulePage_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Schedule(UserId));
        }
    }
}
