using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DegreePlanner.Classes;
using DegreePlanner.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DegreePlanner.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class A_TermAdd : ContentPage
	{
		public A_TermAdd()
		{
			InitializeComponent();
		}
		
		public void AddTermSave(object sender, EventArgs e)
		{
			//DisplayAlert("Alert", "Clicked", "OK");
			termInsertMethod();
		}

		public async void AddTermCancel(object sender, EventArgs e)
		{
			//DisplayAlert("Alert", "Clicked", "OK");
			await Navigation.PopAsync();
		}

		async void termInsertMethod()
		{
			if (TermStartDatePicker.Date > TermEndDatePicker.Date)
			{
				await DisplayAlert("Alert","Your end date connot before your start date","ok");
				return;
			}
			
			await App.db.CreateTerm(new TermBlueprint{
				Name = titleInput.Text,
				StartDate = TermStartDatePicker.Date,
				EndDate = TermEndDatePicker.Date
			});
			await Navigation.PopAsync();
		}


	}
}