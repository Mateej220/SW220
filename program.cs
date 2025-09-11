namespace SW220
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Program entrance //
			Console.WriteLine("--- SW220 LIB ---");

			int i;
			Dialogs.Draw.Head(6, 4, 48, "About library: SW220");
			i = Dialogs.Draw.BodyMessage(6, 4, 48, new string[] {
				"SW220 is a C# library for building console",
				"applications with advanced user interface.",
				"",
				"It is based on Spectre.Console library",
				"(https://spectreconsole.net/).",
				"",
				"(c) 2025 SW220 contributors"
			});

			//i = Dialogs.Draw.PanelHead(6, 15, 4 - i + 1, 48);
			//i = Dialogs.Draw.PanelBody(6, 15, i, 48, 0);
			Dialogs.Draw.PanelEnd(6, 4, i, 48);

			int x;
			Dialogs.Draw.Head(60, 4, 48, "Menu example:");
			x = Dialogs.Draw.BodyMessage(60, 4, 48, new string[] {
				"Use Up/Down arrow keys to select",
				"an item and Enter to confirm."
			});
			Dictionary<string, string> menu_items = new Dictionary<string, string>()
			{
				{ "I1", "Description of the item" },
				{ "I2", "Description of the item" },
				{ "I3", "Description of the item" },
				{ "I4", "Description of the item" },
				{ "I5", "Description of the item" }
			};

			x = Dialogs.Draw.MenuHead(60, 4, x, 48);
			x = Dialogs.Draw.MenuBody(60, 4, x, 48, 2, menu_items);
			x = Dialogs.Draw.MenuEnd(60, 4, x, 48);
			Dialogs.Draw.PanelEnd(60, 4, x, 48);

			Console.ReadKey();

			// Prefab use 							//
			// (A complete dialog in a single call) //
			string input = Dialogs.Menu(48, "Menu dialog prefab", new string[] {
				"Use Up/Down arrow keys to select",
				"an item and Enter to confirm.",
				"",
				"Press Tab to switch between menu",
				"and panel and Esc to exit dialog."
			}, menu_items);


			// Exit //
			Console.WriteLine(input);
			Console.ReadKey();
			return;
		}
	}
}