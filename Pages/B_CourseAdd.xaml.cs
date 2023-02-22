using DegreePlanner.Classes;
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
	public partial class B_CourseAdd : ContentPage
	{
		int _termIndex;
		public string _termName;
		TermBlueprint _term;

		public B_CourseAdd(int termIndex, string termName, int sent, TermBlueprint term)
		{
			InitializeComponent();
			this._termIndex = sent;
			this._termName = termName;
			this._term = term;
		}

		//save button
		private async void Button_Clicked(object sender, EventArgs e)
		{

			if (this._term.StartDate > CourseStartDatePicker.Date)
			{
				await DisplayAlert("Date Conflict","Your course start date cannot be before your term start date","ok");
				return;		
			}
			if (CourseStartDatePicker.Date > CourseEndDatePicker.Date)
			{
				await DisplayAlert("Date Conflict","Your course end date cannot be before your course start date","ok");
				return;
			}		

			await App.MyDatabase.CreateCourse(new Classes.CourseBlueprint{
				Name = titleInput.Text,
				TermFK_Id = this._termIndex,
				StartDate = CourseStartDatePicker.Date,
				EndDate = CourseEndDatePicker.Date,
				PerformanceAssessment = xNamePerfAssess.Text,
				PerfAssessDate = xNamePerfAssessDueDate.Date,
				ObjectiveAssessment = xNameObjAssess.Text,
				ObjectiveAssessmentDate = xNameObjAssessDueDate.Date,
				PerfNotify = xNamePerfNotify.IsToggled,
				ObjNotify = xNameObjNotify.IsToggled,
				InstructorName = xNameInstructorName.Text,
				InstructorPhone = xNameInstructorPhone.Text,
				InstructorEmail = xNameInstructorEmail.Text,
				OptionalNotes = xNameOptionalNotes.Text,
				PickerSelection = xNameStatus.SelectedItem.ToString()
			
			});
			await Navigation.PopAsync();
			
		}

		//cancel button
		private async void Button_Clicked_1(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}
	}
}