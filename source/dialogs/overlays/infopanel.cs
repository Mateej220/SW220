using System.Globalization;
using Spectre.Console;

namespace SW220
{
	public partial class Dialogs
	{
		public partial class Overlays
		{
			public static bool Break_InfoPanelOL = false;

			// -- Information panel dialog overlay prefab --- //
			public static void InfoPanelOL(int refresh_rate = 500)
			{
				// System information //
				string SystemVersion = Environment.OSVersion.ToString();
				string SystemArch = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
				string HostName = Environment.MachineName;

				while (Break_InfoPanelOL == false)
				{
					lock (Draw.ConsoleLock) // Ensure thread-safe access to the console
					{
						var Time = DateTime.Now.ToString("HH:mm:ss");
						var Content = new Markup(
							"[white on black] " +
							SystemVersion + " (" + SystemArch + ")" +
							General.Repeat(" ", Console.WindowWidth / 2 - (SystemVersion.Length + SystemArch.Length + 4) - (HostName.Length / 2)) +
							HostName +
							General.Repeat(" ", Console.WindowWidth / 2 - (HostName.Length / 2) - Time.Length - 1) +
							Time + " [/]"
						);

						AnsiConsole.Cursor.SetPosition(0, 0);
						AnsiConsole.Write(Content);
					}
					Thread.Sleep(refresh_rate);
				}

				return;
			}	
		}
	}
}