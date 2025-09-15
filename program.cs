namespace SW220
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Program entrance //
			Console.WriteLine("--- SW220 LIB ---");

			int OL_Width = 40;
			int OL_PosX = Console.WindowWidth - OL_Width - 2;
			int OL_PosY = 2;

			Thread OL_Thread1 = new(() => Dialogs.Overlays.DateTimeOL(OL_PosX, OL_PosY, OL_Width));
			Thread OL_Thread2 = new(() => Dialogs.Overlays.InfoPanelOL());

			OL_Thread1.Start();
			OL_Thread2.Start();

			Repeat:
			string Input = Dialogs.Menu(48, "Main menu", new string[] {
				"Use up and down arrows to navigate",
				"Press Enter to select an item" }, new Dictionary<string, string> {
				{ "A1", "About library" },
				{ "A2", "Stop date & time overlay"},
				{ "EX", "Exit application" } });

			if (Input == "A1") { MenuItems.About(); goto Repeat; }
			else if (Input == "A2") { Dialogs.Overlays.Break_DateTimeOL = true; goto Repeat; }
			else if (Input == "EX") { /* Exit app */ }
			else if (Input == "@exited") { /* Exit app */ }
			else { Console.WriteLine("Invalid input!"); goto Repeat; }

			// Exit //
			
			// Wait for the overlay thread to finish peacefully before exiting the program //
			// OL_Thread.Abort(); // (forcefully stops the thread - throws exception around platform support)
			Dialogs.Overlays.Break_DateTimeOL = true;
			OL_Thread1.Join();

			Dialogs.Overlays.Break_InfoPanelOL = true;
			OL_Thread2.Join();

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
		}
	}
}