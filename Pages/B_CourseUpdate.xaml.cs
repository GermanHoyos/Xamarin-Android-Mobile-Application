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
	public partial class B_CourseUpdate : ContentPage
	{
		CourseBlueprint _course;
		public string startDateStr;
		public string endDateStr;
		public string perfDateVar;
		public string objDateVar;		


		public B_CourseUpdate(CourseBlueprint courseSent)
		{
			InitializeComponent();

			_course = courseSent;
			titleInput.Text = _course.Name;
			startDateStr = _course.StartDate.ToString();
			endDateStr = _course.EndDate.ToString();
			perfDateVar = _course.PerfAssessDate.ToString();
			objDateVar = _course.ObjectiveAssessmentDate.ToString();
			CourseStartDatePicker.Date = DateTime.Parse(startDateStr);
			CourseEndDatePicker.Date = DateTime.Parse(endDateStr);
			xNamePerfAssess.Text = _course.PerformanceAssessment;
			xNamePerfAssessDueDate.Date = DateTime.Parse(perfDateVar);
			xNameObjAssessDueDate.Date = DateTime.Parse(objDateVar);
			xNameInstructorName.Text = _course.InstructorName;
			xNameInstructorPhone.Text = _course.InstructorPhone;
			xNameInstructorEmail.Text = _course.InstructorEmail;
			xNameOptionalNotes.Text = _course.OptionalNotes;
			xNamePerfNotify.IsToggled = _course.PerfNotify;
			xNameObjNotify.IsToggled = _course.ObjNotify;
			xNameStatus.SelectedItem = _course.PickerSelection;
		}

		//save course
        private void Button_Clicked(object sender, EventArgs e)
        {
			courseUpdateMethod();
        }

		//delete course
        private async void Button_Clicked_3(object sender, EventArgs e)
        {
			
			await App.MyDatabase.DeleteCourse(_course);	
			await Navigation.PopAsync();
        }

		//cancel
		private async void Button_Clicked_1(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}
			
		//update course
		public async void courseUpdateMethod(){
			_course.Name = titleInput.Text;
			_course.StartDate = CourseStartDatePicker.Date;
			_course.EndDate = CourseEndDatePicker.Date;
			_course.PerformanceAssessment = xNamePerfAssess.Text;
			_course.PerfAssessDate = xNamePerfAssessDueDate.Date;
			_course.ObjectiveAssessment = xNameObjAssess.Text;
			_course.ObjectiveAssessmentDate = xNameObjAssessDueDate.Date;
			_course.PerfNotify = xNamePerfNotify.IsToggled;
			_course.ObjNotify = xNameObjNotify.IsToggled;
			_course.InstructorName = xNameInstructorName.Text;
			_course.InstructorPhone = xNameInstructorPhone.Text;
			_course.InstructorEmail = xNameInstructorEmail.Text;
			_course.OptionalNotes = xNameOptionalNotes.Text;
			_course.PickerSelection = xNameStatus.SelectedItem.ToString();

			await App.db.UpdateCourses(_course);
			await Navigation.PopAsync();
		}

		private void Button_Clicked_2(object sender, EventArgs e)
		{
			DisplayAlert("Would you like to","share","ok");
		}
	}
}