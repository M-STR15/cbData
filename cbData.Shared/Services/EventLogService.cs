using cbData.Shared.Models;
using cbData.Shared.Stories;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;

namespace cbData.Shared.Services
{
	public class EventLogService : IDisposable, IEventLogService
	{
		private readonly string _assemblyName;
		private readonly PathsStory _pathStory;
		private readonly string _version;
		public string Path { get; private set; }

		public EventLogService(PathsStory pathsStory)
		{
			_pathStory = pathsStory;
			Path = System.IO.Path.Combine(_pathStory.BaseDirectory, "logs");
			_assemblyName = "cbData";

			var path = $"{Path}\\{_assemblyName}.log";
			_version = BuildInfo.VersionStr;

			Log.Logger = new LoggerConfiguration()
			  .WriteTo.File(
				  path: path,
				  rollingInterval: RollingInterval.Day, // Denní archivace
				  encoding: System.Text.Encoding.UTF8,
				  outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz};[{Level}];{Message:lj}{NewLine}", //, // Vlastní formát
				  restrictedToMinimumLevel: LogEventLevel.Information // Minimální úroveň logování
			  )
			  .CreateLogger();
		}

		~EventLogService()
		{
			Dispose();
		}
		public void Dispose()
		{
			Log.CloseAndFlush();
		}

		/// <summary>
		/// Metoda pro parsování logovacího záznamu.
		/// Předpokládá, že formát logu je "Timestamp;[Level];Message".
		/// Rozdělí záznam na tři části: Timestamp, Level a Message.
		/// Pokud záznam neobsahuje alespoň tři části, vrátí prázdný řetězec.
		/// </summary>
		/// <param name="logLine">Řádek logu, který má být parsován.</param>
		/// <returns>Vrací zprávu z logu jako řetězec.</returns>
		public string ParseLogEntry(string logLine)
		{
			// Předpoklad: Logovací formát je "Timestamp;[Level];Message"
			var parts = logLine.Split(';', 3);  // Rozdělíme na 3 části: Timestamp, Level, a Message
			if (parts.Length < 3)
				return "";

			//string timestamp = parts[0];  // Spojíme datum a čas
			//string level = parts[1].Trim('[', ']');  // Vyčistíme log level
			string message = parts[2];  // Zbytek je zpráva

			return message;
		}

		/// Metoda pro čtení logovacích záznamů z logovacího souboru.
		/// Předpokládá, že formát logu je "Timestamp;[Level];Message".
		/// Načte logovací soubor pro aktuální den, parsuje jednotlivé řádky a deserializuje je do objektů CustomLogEvent.
		/// Pokud záznam neobsahuje platnou zprávu, je ignorován.
		/// </summary>
		/// <returns>Vrací seznam objektů CustomLogEvent.</returns>
		public List<CustomLogEvent> ReadEventLogs()
		{
			var events = new List<CustomLogEvent>();
			string logFilePath = $"{Path}\\{_assemblyName}{DateTime.Now:yyyyMMdd}.log";

			using (FileStream fs = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			using (StreamReader sr = new StreamReader(fs))
			{
				string line;
				if (sr.EndOfStream)
					return events;
				while ((line = sr.ReadLine()) != null)
				{
					var message = ParseLogEntry(line);
					if (message != null && message != "")
						events.Add(JsonConvert.DeserializeObject<CustomLogEvent>(message));
				}
			}

			return events;
		}

		/// <summary>
		/// Zapíše chybovou zprávu do logu.
		/// </summary>
		/// <param name="guid">Identifikátor události.</param>
		/// <param name="message">Zpráva, která má být zapsána do logu.</param>
		public void WriteError(Guid guid, string message) => writeEvent(new CustomLogEvent(guid, message, LogEventLevel.Error, _version));

		/// <summary>
		/// Zapíše fatální chybovou zprávu do logu.
		/// </summary>
		/// <param name="guid">Identifikátor události.</param>
		/// <param name="message">Zpráva, která má být zapsána do logu.</param>
		public void WriteFatal(Guid guid, string message) => writeEvent(new CustomLogEvent(guid, message, LogEventLevel.Fatal, _version));

		public void WriteInformation(Guid guid, string message) => writeEvent(new CustomLogEvent(guid, message, LogEventLevel.Information, _version));

		/// <summary>
		/// Zapíše varovnou zprávu do logu.
		/// </summary>
		/// <param name="guid">Identifikátor události.</param>
		/// <param name="message">Zpráva, která má být zapsána do logu.</param>
		public void WriteWarning(Guid guid, string message) => writeEvent(new CustomLogEvent(guid, message, LogEventLevel.Warning, _version));
		private static void writeEvent(CustomLogEvent customLogEvent)
		{
			var jsonString = JsonConvert.SerializeObject(customLogEvent);
			Log.Write(customLogEvent.Level, jsonString);
		}
	}
}