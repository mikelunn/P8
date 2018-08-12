using System;
using System.Collections.Generic;
using System.Text;

namespace P8.Console.Exceptions
{
	public class MemberIdException : Exception
	{
		public MemberIdException()
		{
		}

		public MemberIdException(string message)
			: base(message)
		{
		}

		public MemberIdException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
