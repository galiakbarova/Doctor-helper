using System;
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
        private Int32 DoctorId { get; set; }

		public Schedule (Int32 doctorId)
		{
			InitializeComponent ();
            DoctorId = doctorId;
		}
	}
}