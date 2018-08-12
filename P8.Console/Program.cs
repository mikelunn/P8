
using ConsoleTables;
using P8.Console.Exceptions;
using P8.Console.Utils;
using System;
using System.Linq;
using System.Threading;
using static P8.Console.Utils.Console;

namespace P8.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Console.SetWindowSize(185, 45);
			System.Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
			while (true)
			{
				try
				{
					Write("Enter a member ID to view a member's diagnosis (Or enter 0 to cancel):", false);
					var isNumber = int.TryParse(Read(), out int memberId);
					var canceled = isNumber && memberId == 0;
					if (!isNumber) throw new MemberIdException("Validation Error: Member ID must be a number.");
					if (!canceled)
					{
						var results = Disposable.Using(() => new DiagnosisQuery(), query => query.Handle(memberId)).ToList();

						if (results.Any())
						{
							var count = results.Count();
							var message = count > 1 ? $"Found {count} Diagnoses for Member ID {memberId}" : $"Found 1 Diagnosis for Member ID {memberId}";
							Write(message);
							ConsoleTable.From(results).Write(Format.Alternative);
						}
						else
						{
							Write($"No diagnoses found for member id {memberId}");
						}
					}
					else
					{
						break;
					}
				}
				catch (MemberIdException ex)
				{
					Thread.Sleep(TimeSpan.FromMilliseconds(100));
					Write(ex.Message);
				}
				catch (Exception ex)
				{
					Thread.Sleep(TimeSpan.FromMilliseconds(100));
					Write($"The following exception was thrown during search: {ex.Message}");
				}
			}
		}
		static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
		{
			e.Cancel = true;
			System.Console.WriteLine("Cancel key press....shutting down");
			Environment.Exit(0);
		}
	}
}
