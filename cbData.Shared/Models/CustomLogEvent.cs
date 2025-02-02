using Serilog.Events;
using System.Text;

namespace cbData.Shared.Models
{
	/// <summary>
	/// Třída sloužící pro formát JSONu v event logu.
	/// </summary>
	public class CustomLogEvent
	{
		public string Message { get; set; } = "";
		public LogEventLevel Level { get; set; }
		public DateTime Timestamp { get; set; }
		public Guid GuidID { get; set; }
		public string Version { get; set; } = "";

		public CustomLogEvent()
		{ }

		public CustomLogEvent(Guid guidID, string message, LogEventLevel level, string version) : this()
		{
			Timestamp = DateTime.Now;
			Message = message;
			Level = level;
			Version = version;
			GuidID = guidID;
		}
#pragma warning disable IDE0058
#pragma warning disable CA1834
		public string GetFormat()
		{
			var properties = this.GetType().GetProperties();
			StringBuilder sb = new StringBuilder();

			foreach (var property in properties)
			{
				sb.Append("{");
				sb.Append($"{property.Name}");
				sb.Append("} ");
			}
			return sb.ToString();
		}
	}
}