using System;
using System.Collections.Generic;
using System.Text;

namespace P8.Console
{
	public class SevereDiagnosis
	{

		public uint RowId { get; set; }
		public int MemberID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int? MostSevereDiagnosisID { get; set; }
		public string MostSevereDiagnosisDescription { get; set; }
		public int? CategoryID { get; set; }
		public string CategoryDescription { get; set; }
		public int? CategoryScore { get; set; }
		public int IsMostSevereCategory { get; set; }
	}
}
