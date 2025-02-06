namespace cbData.Shared.Models
{
	/// <summary>
	/// Generovaná tøída, i datem vytovøeného buildu.
	/// Tato informace slouží pro zobrazení v èíla verze.
	/// </summary>
	public static class BuildInfo
	{
		public static DateTime BuildDate = DateTime.Parse("2025-02-06 22:46:07");
		public static string VersionStr { get; set; }

		static BuildInfo()
		{
			var assembly = System.Reflection.Assembly.Load(nameof(cbData));
			var version = assembly.GetName().Version;
			if (version != null)
				VersionStr = string.Format("v. {0}.{1}.{2} | b. {3}", version.Major, version.Minor, version.Build, BuildInfo.BuildDate.ToString("yyMMdd"));
		}
	}
}