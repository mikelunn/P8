using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;

namespace P8.Console
{
    public class DiagnosisQuery : IDisposable
    {
		private readonly P8DbContext _context;
		private readonly string query = @"
						select
						ROW_NUMBER() OVER(ORDER BY m.MemberID ASC) AS RowId,
						m.MemberID as			  'MemberID',
						FirstName as			  'FirstName',
						LastName as				  'LastName',
						MostSevereDiagnosis.ID as 'MostSevereDiagnosisID',
						d.DiagnosisDescription as 'MostSevereDiagnosisDescription',
						dc.DiagnosisCategoryID as 'CategoryID',
						dc.CategoryDescription as 'CategoryDescription',
						dc.CategoryScore as		  'CategoryScore',
					case 
						WHEN 
						dc.DiagnosisCategoryID = MostSevereCategory.ID
						OR dc.DiagnosisCategoryID IS NULL
						THEN 1 ELSE 0 END as	  'IsMostSevereCategory'
	
					from Member m
					LEFT JOIN ( 
								select md.MemberId, dcm.DiagnosisCategoryID, MIN(md.DiagnosisId) ID FROM MemberDiagnosis md
								LEFT JOIN DiagnosisCategoryMap dcm on md.DiagnosisID = dcm.DiagnosisID
								group by md.MemberID, dcm.DiagnosisCategoryID
							  ) as MostSevereDiagnosis on m.MemberID = MostSevereDiagnosis.MemberID
					LEFT JOIN (
								select md.MemberId, MIN(dc.DiagnosisCategoryId) as ID from MemberDiagnosis md 
								Left JOIN DiagnosisCategoryMap dcm on dcm.DiagnosisID = md.DiagnosisID
								Left JOIN DiagnosisCategory dc on dcm.DiagnosisCategoryID = dc.DiagnosisCategoryID
								group by md.MemberID

							  ) as MostSevereCategory on m.MemberID = MostSevereCategory.MemberID
					LEFT JOIN Diagnosis d on MostSevereDiagnosis.ID = d.DiagnosisID
					LEFT JOIN DiagnosisCategory dc on MostSevereDiagnosis.DiagnosisCategoryID = dc.DiagnosisCategoryID
					where m.MemberID = @memberId";
		public DiagnosisQuery()
		{
			var optionsBuilder = new DbContextOptionsBuilder<P8DbContext>();
			optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;DataBase=Pulse8TestDB;Integrated Security=True;Connect Timeout=30");
			_context = new P8DbContext(optionsBuilder.Options);
		}

		public IEnumerable<SevereDiagnosis> Handle(int id)
		{
			var memberId = new SqlParameter("MemberID", id);
			return _context.SevereDiagnoses.FromSql(query, memberId).ToList();
		}
		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
