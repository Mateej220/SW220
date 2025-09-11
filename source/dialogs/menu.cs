using Spectre.Console;

namespace SW220
{
	public partial class Dialogs
	{
		// -- Menu dialog prefab --- //
		public static string Menu(int width, string title, string[] message,
			Dictionary<string, string> items, string txt_confirm = "Confirm", string txt_cancel = "Cancel",
			Theme? theme = null)
		{
			// If there is no defined theme, dialog will use default one.	//
			// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
			theme ??= new Theme();

			// Render space setup //
			Console.BackgroundColor = theme.background;
			Console.Clear();
			AnsiConsole.Cursor.Hide();

			// Variables //
			string Output;

			int StartX = (Console.WindowWidth - width) / 2;
			int StartY = (Console.WindowHeight / 2) - ((message.Count() + items.Count()) / 2) - 2;

			int select = 1;
			int option = 0;

			// Render dialog head and message				//
			// (Part of the dialog which is static)			//
			Draw.Head(StartX, StartY, width, title, theme);
			int Y = Draw.BodyMessage(StartX, StartY, width, message, theme);

			// Main loop									//
			// (Controls the dialog and redraws it)			//
			while (true)
			{
				// Render dialog menu and panel				//
				// (Part of the dialog which is dynamic)	//
				int PosY = Y;

				PosY = Draw.MenuHead(StartX, StartY, PosY, width, theme);
				PosY = Draw.MenuBody(StartX, StartY, PosY, width, select, items, theme);
				PosY = Draw.MenuEnd(StartX, StartY, PosY, width, theme);
				PosY = Draw.PanelHead(StartX, StartY, PosY, width, theme);
				PosY = Draw.PanelBody(StartX, StartY, PosY, width, option, txt_confirm, txt_cancel, theme);
				Draw.PanelEnd(StartX, StartY, PosY, width, theme);

				// Dialog controller						//
				// (Handles user input and controls the		//
				// dialog)									//
				var Input = Console.ReadKey();

				if (Input.Key == ConsoleKey.Enter)
				{
					if (option == 0) { Output = items.ElementAt(select - 1).Key; break; }
					else { Output = "@exited"; break; }
				}
				if (Input.Key == ConsoleKey.Escape)
				{
					Output = "@exited"; break;
				}

				if (Input.Key == ConsoleKey.DownArrow) { if (select < items.Count()) { select++; } else { select = 1; } }
				if (Input.Key == ConsoleKey.UpArrow) { if (select < 2) { select = items.Count(); } else { select--; } }

				if (Input.Key == ConsoleKey.Tab) { switch (option) { case 0: option = 1; break; case 1: option = 0; break; } }
			}

			// Clear console and return output value 		//
			// Enabling cursor								//
			Console.Clear();
			AnsiConsole.Cursor.Show();
			return Output;
		}
	}
}