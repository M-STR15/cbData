#pragma warning disable CA1822

namespace cbData.Shared.Stories
{
	public class PathsStory
	{
		public string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;
		public string FileDirectory => "cbDataFiles";
		public string JsonBufferName => "bufferData.json";
	}
}