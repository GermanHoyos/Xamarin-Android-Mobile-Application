using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DegreePlanner.Classes;
using DegreePlanner.Pages;
using System.Threading.Tasks;

namespace DegreePlanner
{
	public partial class App : Application
	{

		public static SQLiteHelper db;


		public static SQLiteHelper MyDatabase {
			get{
				if(db==null){
					db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyStore.db3"));
				}
			return db;
			}
		}

		public App()
		{
			InitializeComponent();
			var mainPage = new MainPage();

			var navPage = new NavigationPage(mainPage);

			MainPage = navPage;

		}

		protected override void OnStart()
		{

		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{

		}



	}
}
