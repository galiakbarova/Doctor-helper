using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;

namespace DoctorHelper.Doctor
{
    public partial class ShowPatients : ContentPage
    {
        public ShowPatients()
        {
            InitializeComponent();

            var listView = new ListView();
            listView.ItemsSource = new String[]
            {
                "Василий Петрович",
                "Петр Васильевич",
                "Алла Евгеньевна",
                "Попик"
            };

            listView.ItemTapped += OnItemTapped;

            ContentField.Children.Add(listView);

        }

        public void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var showPatientData = new ShowPatientAnalyze();
            var selectedItem = (ListView)sender;
            showPatientData.BindingContext = new PatientModel
            {
                PatientName = selectedItem.SelectedItem.ToString()
            };
            Navigation.PushAsync(showPatientData);
        }
    }
}
