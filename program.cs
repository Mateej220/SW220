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

			Thread OL_Thread = new Thread(() => Dialogs.Overlays.DateTimeOL(OL_PosX, OL_PosY, OL_Width));

			OL_Thread.Start();
			Thread.Sleep(100); // Give overlay some time to start

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
				Console.ReadKey();
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