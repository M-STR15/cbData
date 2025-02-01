﻿namespace cbData.Shared.Models
{
	/// <summary>
	/// Generovaná třída, i datem vytovřeného buildu.
	/// Tato informace slouží pro zobrazení v číla verze. 
	/// </summary>
	public class BuildInfo
	{
		public static string VersionStr { get; set; }
		public static DateTime BuildDate = DateTime.Parse("2025-02-1 22:02:08");

		static BuildInfo()
		{
			var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			if (version != null)
				VersionStr = string.Format("v. {0}.{1}.{2} | b. {3}", version.Major, version.Minor, version.Build, BuildInfo.BuildDate.ToString("yyMMdd"));
		}
	}
}
