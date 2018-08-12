
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace P8.Console
{
	public partial class P8DbContext : DbContext
	{
		public P8DbContext(DbContextOptions<P8DbContext> options) : base(options) { }

		public virtual DbSet<SevereDiagnosis> SevereDiagnoses { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SevereDiagnosis>()
				.HasKey(c => c.RowId);
			base.OnModelCreating(modelBuilder);
		}
	}
}
