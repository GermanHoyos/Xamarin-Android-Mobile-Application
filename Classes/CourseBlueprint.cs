using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DegreePlanner.Classes
{
	public class CourseBlueprint
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public int TermFK_Id { get; set; } //foreign key for term
		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string PerformanceAssessment { get; set; }
		public DateTime PerfAssessDate { get; set; }
		public string ObjectiveAssessment { get; set; }
		public DateTime ObjectiveAssessmentDate { get; set; }
		public bool PerfNotify { get; set; }
		public bool ObjNotify { get; set; }

		//additional stuff
		public string InstructorName { get; set; }
		public string InstructorPhone { get; set; }
		public string InstructorEmail { get; set; }
		public string OptionalNotes { get; set; }
		public string PickerSelection { get; set; }


	}
}
