using cbData.Shared.Models;

namespace cbData.Shared.Services
{
	public interface IEventLogService
	{
		void Dispose();

		string ParseLogEntry(string logLine);

		List<CustomLogEvent> ReadEventLogs();

		void WriteError(Guid guid, string message);

		void WriteFatal(Guid guid, string message);

		void WriteInformation(Guid guid, string message);

		void WriteWarning(Guid guid, string message);
	}
}