using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DegreePlanner.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class A_TermDelete : ContentPage
	{
		public A_TermDelete()
		{
			InitializeComponent();
		}

		public void DeleteTermSave(object sender, EventArgs e)
		{
			DisplayAlert("Alert", "Clicked", "OK");
		}

		public void DeleteTermCancel(object sender, EventArgs e)
		{
			DisplayAlert("Alert", "Clicked", "OK");
		}

	}
}