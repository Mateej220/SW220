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
						var Base = new Markup("[white on black]" + General.Repeat(" ", Console.WindowWidth) + "[/]");

						AnsiConsole.Cursor.SetPosition(0, 0);
						AnsiConsole.Write(Base);

						AnsiConsole.Cursor.SetPosition(2, 0);
						AnsiConsole.Write(new Markup("[white on black]" + SystemVersion + " (" + SystemArch + ")" + "[/]"));

						AnsiConsole.Cursor.SetPosition((Console.WindowWidth / 2) - (HostName.Length / 2), 0);
						AnsiConsole.Write(new Markup("[white on black]" + HostName + "[/]"));

						AnsiConsole.Cursor.SetPosition(Console.WindowWidth - Time.Length, 0);
						AnsiConsole.Write(new Markup("[white on black]" + Time + "[/]"));
					}
					Thread.Sleep(refresh_rate);
				}

				// Reset the break flag for future use //
				Break_InfoPanelOL = false;
				return;
			}	
		}
	}
}