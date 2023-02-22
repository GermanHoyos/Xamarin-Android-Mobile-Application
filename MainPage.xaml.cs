using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DegreePlanner.Pages;
using DegreePlanner.Classes;
using SQLite;

namespace DegreePlanner
{
	public partial class MainPage : ContentPage
	{
		public string nameOfSelectedItem = "";
		public int indexOfSelectedItem;
		public TermBlueprint item;
		public int initialData = 0;
		public int mainPageVisits = 0;
		
		public MainPage()
		{
			InitializeComponent();
		} 

		protected override async void OnAppearing()
		{
			try
			{
				

				if (initialData == 0) { //main page visitation tracker
					loadDelayedData();
				}

				base.OnAppearing();
				fullViewOfTermsCourses.ItemsSource = await App.MyDatabase.ReadTerms();
			}

			catch
			{

			}
		}

		public async void AddTermButton(object sender, EventArgs e)
		{
			//DisplayAlert("Alert", "Clicked", "OK");
			await Navigation.PushAsync(new A_TermAdd());
		}
		public async void fullViewOfTermsCourses_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			item = e.Item as TermBlueprint;
			nameOfSelectedItem = item.Name;
			indexOfSelectedItem = item.Id;
			//await DisplayAlert("ID of term selected = ", indexOfSelectedItem.ToString(), "ok");
			await Navigation.PushAsync(new A_TermUpdate(item,indexOfSelectedItem));
		}

		//demo data loader / notifier of assessments / will only load demo data if no other data exists
		public async void loadDelayedData()
		{
			Task<List<TermBlueprint>> tasks = App.MyDatabase.ReadTerms();
			int tasksNum = tasks.Result.Count;

			Task<List<CourseBlueprint>> cTasks = App.MyDatabase.ReadAllCourses();
			int cTasksNum = cTasks.Result.Count;
			
			if (tasksNum > 0 && cTasksNum > 0 && mainPageVisits < 1)
			{
				var courseList = await App.MyDatabase.ReadAllCourses();
				foreach (CourseBlueprint foundCourse in courseList) {
					if (foundCourse.PerfNotify == true){
						await DisplayAlert("You have a Perfomance Assessment Today in class: ",  foundCourse.Name,"Ok");
					}
					if (foundCourse.ObjNotify == true){
						await DisplayAlert("You have an Objective Assessment Today in class: ",  foundCourse.Name,"Ok");
					}
				}
			}
			//await DisplayAlert("title", tasksNum.ToString(), "ok");
			if (tasksNum < 1 && mainPageVisits < 1){ //check if any terms have been created
			await DisplayAlert("Welcome to WGU Degree Planner","You have no performance or Objective Assessments today","Ok");
				await App.db.CreateTerm(new TermBlueprint{
						Name = "Demo Term: German Hoyos",
						StartDate = DateTime.Now,
						EndDate = DateTime.Now
				});
				Task<List<TermBlueprint>> findDemoTask = App.MyDatabase.ReadTerms();
				int passMeToCourse = findDemoTask.Result[0].Id;
				//await DisplayAlert("passed Id",passMeToCourse.ToString(),"ok");
				await App.MyDatabase.CreateCourse(new Classes.CourseBlueprint{
					Name = "Demo Course WGU",
					TermFK_Id = passMeToCourse,
					StartDate = DateTime.Now.AddDays(1),
					EndDate = DateTime.Now.AddDays(1),
					PerformanceAssessment = "Performance Assessment 6.789",
					PerfAssessDate = DateTime.Now.AddDays(2),
					ObjectiveAssessment = "Objective Assessment 3.567.1",
					ObjectiveAssessmentDate = DateTime.Now.AddDays(2),
					PerfNotify = true,
					ObjNotify = true,
					InstructorName = "Lareun Provost",
					InstructorPhone = "3854288921",
					InstructorEmail = "lauren.provost@wgu.edu",
					OptionalNotes = "Optional Notes",
					PickerSelection = "In Progress"
				});

				initialData++; //stops app from running this every time main page is loaded
				fullViewOfTermsCourses.ItemsSource =  await App.MyDatabase.ReadTerms();
			}
			mainPageVisits++;
		}
	}
}
