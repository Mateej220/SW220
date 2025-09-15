using Spectre.Console;

namespace SW220
{
	public partial class Dialogs
	{
		// -- Input box dialog prefab --- //
		public static string Input(int width, string title, string[] message, bool password = true, int limit = 0, Theme? theme = null)
		{
			// If there is no defined theme, dialog will use default one.					//
			// (New instance of "Theme" [file: source/dialogs/theme.cs] )					//
			theme ??= new Theme();

			// If there is no defined limit, input length will be limited by dialog width 	//
			if (limit == 0) { limit = width - 6; }

			// Render space setup //
			Console.BackgroundColor = theme.background;
			Console.Clear();

			// Variables //
			string Output = "";

			int StartX = (Console.WindowWidth - width) / 2;
			int StartY = (Console.WindowHeight / 2) - ((message.Count()) / 2) - 2;

			// Render dialog head and message				//
			// (Part of the dialog which is static)			//
			Draw.Head(StartX, StartY, width, title, theme);
			int Y = Draw.BodyMessage(StartX, StartY, width, message, theme);
			Y = Draw.MenuHead(StartX, StartY, Y, width, theme);
			Y = Draw.MenuBody(StartX, StartY, Y, width, theme);
			Draw.MenuEnd(StartX, StartY, Y, width, theme);
			Draw.PanelEnd(StartX, StartY, Y + 1, width, theme);

			// Input box setup								//
			ConsoleKeyInfo Key;
			lock (Draw.ConsoleLock) { AnsiConsole.Cursor.SetPosition(StartX + 3 + Output.Length, StartY + Y - 1);}
			while (true)
			{
				Key = Console.ReadKey(intercept: true);

				lock (Draw.ConsoleLock)
				{
					if (Key.Key == ConsoleKey.Enter) { break; }
					else if (Key.Key == ConsoleKey.Escape) { Output = "@exited"; break; }
					else if (Key.Key == ConsoleKey.Backspace && Output.Length > 0)
					{
						Output = Output.Substring(0, Output.Length - 1);
						// Move cursor back, overwrite with space, move back again
						AnsiConsole.Cursor.SetPosition(StartX + 3 + Output.Length, StartY + Y - 1);
						AnsiConsole.Write(new Markup("[black on silver] [/]"));
						AnsiConsole.Cursor.SetPosition(StartX + 3 + Output.Length, StartY + Y - 1);
					}
					else if (!char.IsControl(Key.KeyChar) && Output.Length < (limit))
					{
						Output += Key.KeyChar;
						if (password) { AnsiConsole.Write(new Markup("[black on silver]*[/]")); }
						else { AnsiConsole.Write(new Markup("[black on silver]" + Key.KeyChar + "[/]")); }
					}
				}
			}

			Console.WriteLine(Output);
			Console.ReadKey();

			return Output;
		}
	}
}