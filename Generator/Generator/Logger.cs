using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	public enum LogLevel { DEBUG, INFO, ERROR }

	public class LogEntry
	{
		public DateTime time { get; set; }
		public LogLevel level { get; set; }
		public string message { get; set; }

		public override string ToString()
		{
			return String.Format("{0}\t{1}\t{2}", time, level, message);
		}

	}

	public delegate void Log(LogLevel level, string message);
}
