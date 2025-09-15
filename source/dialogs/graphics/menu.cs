using Spectre.Console;

namespace SW220
{
	public partial class Dialogs
	{
		// -- Class which is used to build/draw all graphical parts of dialog windows -- //
		public partial class Draw
		{
			public static int MenuHead(int x, int y, int pos_y, int width, Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
				theme ??= new Theme();

				// Style definitions //
				var Frame = new Markup(
					"[white on silver]│ [/]" +
					"[black on silver]┌" +
					General.Repeat("─", width - 6) +
					"[/][white on silver]┐ [/]" +
					"[black on silver]│[/]" +
					"[black on black]  [/]"
				);

				lock (ConsoleLock) // Ensure thread-safe access to the console
				{
					// Draw head of the menu //
					AnsiConsole.Cursor.SetPosition(x, y + pos_y);
					AnsiConsole.Write(Frame);
					pos_y++;
				}

				// Return number of lines that were drawn	//
				// (to be able to calculate next position)	//
				return pos_y;
			}

			public static int MenuBody(int x, int y, int pos_y, int width, int select, Dictionary<string, string> items, Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
				theme ??= new Theme();

				// Style definitions //
				var Frame = new Markup(
					"[white on silver]│ [/]" +
					"[black on silver]│" +
					General.Repeat(" ", width - 6) +
					"[/][white on silver]│ [/]" +
					"[black on silver]│[/]" +
					"[black on black]  [/]"
				);
				var Message = new Markup("");

				// Draw body/items of the menu //
				int drawn = 1;
				lock (ConsoleLock) // Ensure thread-safe access to the console
				{
					foreach (KeyValuePair<string, string> i in items)
					{
						// Each item of menu is drawn separately	//
						// (to be able to highlight selected item)	//
						if (drawn == select)
						{
							Message = new Markup(
							theme.text_highlight_02 + i.Key + "[/]" +
							"[silver on silver]  [/]" +
							theme.text_highlight_01 + i.Value +
							General.Repeat(" ", width - (i.Key.Length + i.Value.Length) - 10) + "[/]"
							);
						}
						else
						{
							Message = new Markup(
							theme.text_backlight + i.Key + "  [/]" +
							theme.text_normal + i.Value + "[/]"
							);
						}

						// Draw item of the menu //
						AnsiConsole.Cursor.SetPosition(x, y + pos_y);
						AnsiConsole.Write(Frame);

						AnsiConsole.Cursor.SetPosition(x + 4, y + pos_y);
						AnsiConsole.Write(Message);

						pos_y++;
						drawn++;
					}
				}

				// Return number of lines that were drawn	//
				// (to be able to calculate next position)	//
				return pos_y;
			}

			public static int MenuBody(int x, int y, int pos_y, int width, Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
				theme ??= new Theme();

				// Style definitions //
				var Frame = new Markup(
					"[white on silver]│ [/]" +
					"[black on silver]│" +
					General.Repeat(" ", width - 6) +
					"[/][white on silver]│ [/]" +
					"[black on silver]│[/]" +
					"[black on black]  [/]"
				);

				lock (ConsoleLock) // Ensure thread-safe access to the console
				{
					// Draw item of the menu //
					AnsiConsole.Cursor.SetPosition(x, y + pos_y);
					AnsiConsole.Write(Frame);

					pos_y++;
				}

				// Return number of lines that were drawn	//
				// (to be able to calculate next position)	//
				return pos_y;
			}

			public static int MenuEnd(int x, int y, int pos_y, int width, Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
				theme ??= new Theme();

				// Style definitions //
				var Frame = new Markup(
					"[white on silver]│ [/]" +
					"[black on silver]└[/]" +
					"[white on silver]" +
					General.Repeat("─", width - 6) +
					"┘[/][black on silver] [/]" +
					"[black on silver]│[/]" +
					"[black on black]  [/]"
				);

				lock (ConsoleLock) // Ensure thread-safe access to the console
				{
					// Draw end of the menu //
					AnsiConsole.Cursor.SetPosition(x, y + pos_y);
					AnsiConsole.Write(Frame);
					pos_y++;
				}

				// Return number of lines that were drawn	//
				// (to be able to calculate next position)	//
				return pos_y;
			}
		}
	}
}