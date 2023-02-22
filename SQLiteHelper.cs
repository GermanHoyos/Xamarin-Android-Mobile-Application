using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using DegreePlanner.Classes;
using System.Data;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Data.Common;

namespace DegreePlanner
{
	public class SQLiteHelper
	{
		public readonly SQLiteAsyncConnection db;

		public Task<List<CourseBlueprint>> courses; 
		public CourseBlueprint course;
		
		public  SQLiteHelper(string dbPath) {
			
			//establish connection and create tables if the do not already exist:
			db = new SQLiteAsyncConnection(dbPath);
			db.CreateTableAsync<TermBlueprint>();
			db.CreateTableAsync<CourseBlueprint>();
			courses = db.Table<CourseBlueprint>().ToListAsync();
			
			//must delete courses first due to foriegn key constraint:
			//DeleteAllCourses<CourseBlueprint>(); 
			//DeleteAllItems<TermBlueprint>();
			
			//inserts Demo data:
			//termInsertMethod();
		}

		//TERMS CRUD*********************************************
		public Task<int> DeleteAllItems<T>(){
			return db.DeleteAllAsync<TermBlueprint>();
		}

		public Task<int> CreateTerm(TermBlueprint term){
			return db.InsertAsync(term);
		}

		public Task<List<TermBlueprint>>ReadTerms(){
			return db.Table<TermBlueprint>().ToListAsync();
		}

		public Task <int> UpdateTerm(TermBlueprint term){
			return db.UpdateAsync(term);
		}

		public Task <int> DeleteTerm(TermBlueprint term){
			return db.DeleteAsync(term);
		}

		public Task <int> CountTerms(){
			return db.ExecuteAsync("select count(*) from TermBlueprint;");
		}

		//COURSES CRUD*********************************************
		public Task<int> CreateCourse(CourseBlueprint course){
			return db.InsertAsync(course);
		}

		public Task<List<CourseBlueprint>>ReadCourses(TermBlueprint term){
			return db.Table<CourseBlueprint>().Where(courses => courses.TermFK_Id == term.Id).ToListAsync();
		}

		public Task <int> UpdateCourses(CourseBlueprint course){
			return db.UpdateAsync(course);
		}

		//delete a specifc course
		public Task <int> DeleteCourse(CourseBlueprint course){
			return db.DeleteAsync(course);
		}

		//delete "ASSOCIATED" courses:
		public Task <int> DeleteSpecificCourse(TermBlueprint term){
			return db.ExecuteAsync("delete from CourseBlueprint where TermFK_Id = " + term.Id + ";");
		}
		
		//delete all courses:
		public Task<int> DeleteAllCourses<T>(){
			return db.DeleteAllAsync<CourseBlueprint>();
		}

		public Task<List<CourseBlueprint>>FindAssessments(TermBlueprint term){
			return db.Table<CourseBlueprint>().Where(courses => courses.PerfNotify == true).ToListAsync();
		}

		public Task<List<CourseBlueprint>>ReadAllCourses(){
			return db.Table<CourseBlueprint>().ToListAsync();
		}

		
	}
}
