namespace SW220
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Program entrance //
			Console.WriteLine("--- SW220 LIB ---");

			int i;
			Dialogs.Draw.Head(16, 4, 48, "About library: SW220");
			i = Dialogs.Draw.BodyMessage(16, 4, 48, new string[] {
				"SW220 is a C# library for building console",
				"applications with advanced user interface.",
				"",
				"It is based on Spectre.Console library",
				"(https://spectreconsole.net/).",
				"",
				"(c) 2025 SW220 contributors"
			});

			i = Dialogs.Draw.PanelHead(16, 15, 4 - i + 1, 48);
			i = Dialogs.Draw.PanelBody(16, 15, i, 48, 0);
			Dialogs.Draw.PanelEnd(16, 15, i, 48);


			// Exit //
			Console.ReadKey();
			return;
		}
	}
}