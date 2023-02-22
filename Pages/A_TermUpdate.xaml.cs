using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DegreePlanner.Classes;
using DegreePlanner;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DegreePlanner.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class A_TermUpdate : ContentPage
	{
		int _indexOfSentItem;
		public string _termName = "";
		public int _termIndex;
		public TermBlueprint _item;
		public CourseBlueprint _course;
		public string startDateStr;
		public string endDateStr;		


		public A_TermUpdate(TermBlueprint item, int indexOfSentItem)
		{
			InitializeComponent();
			_item = item;
			_indexOfSentItem = indexOfSentItem;
			xmlTermName.Text = _item.Name;
			startDateStr = _item.StartDate.ToString();
			termUpdateStartDate.Date = DateTime.Parse(startDateStr);
			endDateStr = _item.EndDate.ToString();
			termUpdateEndDate.Date = DateTime.Parse(endDateStr);
		}

		protected override async void OnAppearing(){
			try {
				base.OnAppearing();
				courseViewOfSelectedTerm.ItemsSource = await App.MyDatabase.ReadCourses(_item);
				
			}
			catch {
				
			}
		}
 
		public void UpdateTermSave(object sender, EventArgs e)
		{
			DisplayAlert("Alert", "Clicked", "OK");
			termChangeMethod();
		}
		public async void DeleteTermButton(object sender, EventArgs e)
		{
			//await DisplayAlert("Delete this term and all associated courses?",nameOfSelectedItem, "OK");
			//await DisplayAlert("Item Id = ",indexOfSelectedItem.ToString(), "OK");
			//await App.MyDatabase.DeleteTerm(item);
			//fullViewOfTermsCourses.ItemsSource = await App.MyDatabase.ReadTerms();
			
			await App.MyDatabase.DeleteSpecificCourse(_item);
			await App.MyDatabase.DeleteTerm(_item);
			
			await Navigation.PopAsync();

		}	
		public async void UpdateTermCancel(object sender, EventArgs e)
		{
			//DisplayAlert("Alert", "Clicked", "OK");
			await Navigation.PopAsync();
		}
		public async void AddCourseButton(object sender, EventArgs e)
		{
			//await DisplayAlert("term foreign key = ", _item.Id.ToString(), "ok");
			await Navigation.PushAsync(new B_CourseAdd(_termIndex, _termName,_indexOfSentItem, _item ));
		}
		public async void termChangeMethod()
		{
			_item.Name = xmlTermName.Text;
			_item.StartDate = termUpdateStartDate.Date;
			_item.EndDate = termUpdateEndDate.Date;
			await App.db.UpdateTerm(_item);
			await Navigation.PopAsync();
		}

		public async void courseViewOfSelectedTerm_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			_course = e.Item as CourseBlueprint;
			await Navigation.PushAsync(new B_CourseUpdate(_course));
		}
	}
}