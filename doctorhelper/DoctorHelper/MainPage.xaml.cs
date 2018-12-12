using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorHelper.Doctor;
using DoctorHelper.Patient;
using Xamarin.Forms;

namespace DoctorHelper
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void ButtonDoctor_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new DoctorLogin());
        }

        void ButtonPatient_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PatientLogin());
        }
    }
}
