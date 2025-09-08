namespace SW220
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Program entrance //
			Console.WriteLine("--- SW220 LIB ---");

			Dialogs.Draw.Head(16, 4, 48, "About SW220");
			Dialogs.Draw.BodyMessage(16, 4, 48, new string[] {
				"SW220 is a C# library for building console",
				"applications with advanced user interface.",
				"",
				"It is based on Spectre.Console library",
				"(https://spectreconsole.net/).",
				"",
				"(c) 2025 SW220 contributors"
			});

			// Exit //
			Console.ReadKey();
			return;
		}
	}
}