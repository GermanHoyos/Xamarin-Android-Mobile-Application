using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DegreePlanner.Classes
{
	public class TermBlueprint
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
