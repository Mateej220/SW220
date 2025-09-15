namespace SW220
{
	public class Program
	{
		// Overlay threads //
		public static int OLDT_Width = 40;
		public static int OLDT_PosX = Console.WindowWidth - OLDT_Width - 2;
		public static int OLDT_PosY = 3;

		public static Thread OL_Thread1 = new(() => Dialogs.Overlays.DateTimeOL(OLDT_PosX, OLDT_PosY, OLDT_Width));
		public static Thread OL_Thread2 = new(() => Dialogs.Overlays.InfoPanelOL());
		public static Thread OL_Thread3 = null!; // Performance overlay not implemented on Linux

		static void Main(string[] args)
		{
			// Program entrance //
			Console.WriteLine("--- SW220 LIB ---");

			Dialogs.Info(48, "Welcome to SW220 library!", new string[] {
				"This is a demo application showcasing",
				"the features of the SW220 library.",
				"",
				"Login with username: Administrator",
				"and password: administrator128",
				"",
				"Press OK to continue."
			});

		URepeat:
			if (Dialogs.Input(48, "Login window", ["Username:"], "Next", false) == "Administrator") { }
			else { Dialogs.Info(48, "Login window", ["Invalid username!"]); goto URepeat; }
		PRepeat:
			if (Dialogs.Input(48, "Login window", ["Password:"], "Login", true) == "administrator128") { }
			else { Dialogs.Info(48, "Login window", ["Invalid password!"]); goto PRepeat; }

			OL_Thread2.Start();

		MainMenu:
			string Input = Dialogs.Menu(48, "Main menu", new string[] {
				"Use up and down arrows to navigate",
				"Press Enter to select an item" }, new Dictionary<string, string> {
				{ "A1", "About library" },
				{ "A2", "Thread management"},
				{ "EX", "Exit application" } });

			if (Input == "A1") { MenuItems.About(); goto MainMenu; }
			else if (Input == "A2") { MenuItems.ThreadManagement(); goto MainMenu; }
			else if (Input == "EX") { /* Exit app */ }
			else if (Input == "@exited") { /* Exit app */ }
			else { Console.WriteLine("Invalid input!"); goto MainMenu; }

			// Exit //

			// Wait for the overlay thread to finish peacefully before exiting the program //
			// OL_Thread.Abort(); // (forcefully stops the thread - throws exception around platform support)
			if (OL_Thread1.IsAlive) { Dialogs.Overlays.Break_DateTimeOL = true; OL_Thread1.Join(); }
			if (OL_Thread2.IsAlive) { Dialogs.Overlays.Break_InfoPanelOL = true; OL_Thread2.Join(); }

			return;
		}

		static class MenuItems
		{
			public static void About()
			{
				Dialogs.Info(48, "About library: SW220", new string[] {
					"SW220 is a C# library for building console",
					"application with advanced user interface.",
					"",
					"It is based on Spectre.Console library",
					"(https://spectreconsole.net/)).",
					"",
					"(c) 2025 SW220 contributors"
				});

				return;
			}

			public static void ThreadManagement()
			{
				string DT_Status = OL_Thread1.IsAlive ? "Stop" : "Start";
				string IP_Status = OL_Thread2.IsAlive ? "Stop" : "Start";
				string PC_Status = "----";//OL_Thread3.IsAlive ? "Start" : "Stop";

				var Input = Dialogs.Menu(48, "Thread management", new string[] {
					"Use up and down arrows to navigate",
					"Press Enter to start/stop thread" }, new Dictionary<string, string> {
					{ "DT", DT_Status + " Date & time overlay" },
					{ "IP", IP_Status + " Information panel overlay"},
					{ "PC", PC_Status + " Performance overlay" } });

				if (Input == "DT")
				{
					if (OL_Thread1.IsAlive) { Dialogs.Overlays.Break_DateTimeOL = true; OL_Thread1.Join(); }
					else { OL_Thread1 = new(() => Dialogs.Overlays.DateTimeOL(OLDT_PosX, OLDT_PosY, OLDT_Width)); OL_Thread1.Start(); }
				}
				else if (Input == "IP")
				{
					if (OL_Thread2.IsAlive) { Dialogs.Overlays.Break_InfoPanelOL = true; OL_Thread2.Join(); }
					else { OL_Thread2 = new(() => Dialogs.Overlays.InfoPanelOL()); OL_Thread2.Start(); }
				}
				else if (Input == "PC")
				{
					Dialogs.Info(60, "Thread management", new string[] {
						"Performance overlay is not implemented on Linux.",
						"It would require platform-specific code to",
						"gather performance metrics.",
						"",
						"On Windows, it could be implemented using",
						"the PerformanceCounter class from System.Diagnostics.",
						"",
						"On Linux, a different approach would be needed,",
						"such as reading from /proc/stat or using a",
						"third-party library."
					});

					// Performance overlay not implemented on Linux //
					//if (OL_Thread3.IsAlive) { /* Dialogs.Overlays.Break_PerformanceOL = true; OL_Thread3.Join(); */ }
					//else { /* OL_Thread3.Start(); */ }
				}
				else if (Input == "@exited") { /* Exit to main menu */ }
				else { Console.WriteLine("Invalid input!"); }

				return;
			}
		}
	}
}