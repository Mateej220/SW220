using Spectre.Console;

namespace SW220
{
	public partial class Dialogs
	{
		// -- Class which is used to build/draw all graphical parts of dialog windows -- //
		public partial class Draw
		{
			// Lock object for thread-safe console access 							//
			// (to prevent multiple threads from writing to console simultaneously) //
			public static readonly object ConsoleLock = new object();

			public static void Head(int x, int y, int width, string title, Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
				theme ??= new Theme();

				// Style definitions //
				var Title = new Markup(theme.text_title + title + "[/]");
				var Frame = new Markup(
					"[white on silver]┌" +
					General.Repeat("─", width - 2) +
					"[/][black on silver]┐[/]"
				);

				lock (ConsoleLock) // Ensure thread-safe access to the console
				{
					// Draw head of the dialog //
					AnsiConsole.Cursor.SetPosition(x, y);
					AnsiConsole.Write(Frame);
					AnsiConsole.Cursor.SetPosition(x, y); AnsiConsole.Cursor.MoveRight(width / 2 - title.Length / 2);
					AnsiConsole.Write(Title);
				}

				return;
			}

			public static int BodyMessage(int x, int y, int width, string[] message, Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
				theme ??= new Theme();

				// Style definitions //
				var Frame = new Markup(
					"[white on silver]│" +
					General.Repeat(" ", width - 2) +
					"[/][black on silver]│[/]" +
					"[black on black]  [/]"
				);
				var Message = new Markup("");

				// Draw body/message of the dialog //
				int pos_y = 1;
				lock (ConsoleLock) // Ensure thread-safe access to the console
				{
					foreach (string i in message)
					{
						// Each line of message is drawn separately 	//
						// (to be able to handle multi-line messages)	//
						Message = new Markup(theme.text_normal + i + "[/]");

						AnsiConsole.Cursor.SetPosition(x, y + pos_y);
						AnsiConsole.Write(Frame);

						AnsiConsole.Cursor.SetPosition(x + 2, y + pos_y);
						AnsiConsole.Write(Message);

						pos_y++;
					}
				}

				// Return number of lines that were drawn	//
				// (to be able to calculate next position)	//
				return pos_y;
			}

			public static int PanelHead(int x, int y, int pos_y, int width, Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
				theme ??= new Theme();

				// Style definitions //
				var Frame = new Markup(
					"[white on silver]├" +
					General.Repeat("─", width - 2) +
					"[/][black on silver]┤[/]" +
					"[black on black]  [/]"
				);

				lock (ConsoleLock) // Ensure thread-safe access to the console
				{
					// Draw panel head of the dialog //
					AnsiConsole.Cursor.SetPosition(x, y + pos_y);
					AnsiConsole.Write(Frame);
					pos_y++;
				}

				// Return number of lines that were drawn	//
				// (to be able to calculate next position)	//
				return pos_y;
			}

			public static int PanelBody(int x, int y, int pos_y, int width, int select, string txt_confirm = "Confirm", string txt_cancel = "Cancel", Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
				theme ??= new Theme();

				// Style definitions //
				var Frame = new Markup(
					"[white on silver]│" +
					General.Repeat(" ", width - 2) +
					"[/][black on silver]│[/]" +
					"[black on black]  [/]"
				);
				var ButtonConfirm = new Markup("");
				var ButtonCancel = new Markup("");

				// Button selection handling			//
				// (which button should be highlighted)	//
				if (select == 0)    // Highlight "Confirm" button	//
				{
					ButtonConfirm = new Markup(theme.text_highlight_01 + "< " + txt_confirm + " >" + "[/]");
					ButtonCancel = new Markup(theme.text_normal + "< " + txt_cancel + " >" + "[/]");
				}
				else if (select == 1)   // Highlight "Cancel" button	//
				{
					ButtonConfirm = new Markup(theme.text_normal + "< " + txt_confirm + " >" + "[/]");
					ButtonCancel = new Markup(theme.text_highlight_01 + "< " + txt_cancel + " >" + "[/]");
				}
				else                    // No button is highlighted	// - pretty useless unless you want to display both buttons as inactive
				{
					ButtonConfirm = new Markup(theme.text_normal + "< " + txt_confirm + " >" + "[/]");
					ButtonCancel = new Markup(theme.text_normal + "< " + txt_cancel + " >" + "[/]");
				}

				lock (ConsoleLock) // Ensure thread-safe access to the console
				{
					// Draw panel body of the dialog //
					AnsiConsole.Cursor.SetPosition(x, y + pos_y);
					AnsiConsole.Write(Frame);

					AnsiConsole.Cursor.SetPosition(x + 10, y + pos_y);
					AnsiConsole.Write(ButtonConfirm);

					AnsiConsole.Cursor.SetPosition(x + width - 10 - ButtonCancel.Length, y + pos_y);
					AnsiConsole.Write(ButtonCancel);
					pos_y++;
				}

				// Return number of lines that were drawn	//
				// (to be able to calculate next position)	//
				return pos_y;
			}

			public static int PanelBody(int x, int y, int pos_y, int width, int select, string txt_okay = "Okay", Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
				theme ??= new Theme();

				// Style definitions //
				var Frame = new Markup(
					"[white on silver]│" +
					General.Repeat(" ", width - 2) +
					"[/][black on silver]│[/]" +
					"[black on black]  [/]"
				);
				var ButtonOkay = new Markup("");

				// Button selection handling			//
				if (select == 0)    // Highlight "Okay" button	//
				{
					ButtonOkay = new Markup(theme.text_highlight_01 + "< " + txt_okay + " >" + "[/]");
				}
				else            	// No button is highlighted	// - pretty useless unless you want to display both buttons as inactive
				{
					ButtonOkay = new Markup(theme.text_normal + "< " + txt_okay + " >" + "[/]");
				}

				lock (ConsoleLock) // Ensure thread-safe access to the console
				{
					// Draw panel body of the dialog //
					AnsiConsole.Cursor.SetPosition(x, y + pos_y);
					AnsiConsole.Write(Frame);

					AnsiConsole.Cursor.SetPosition(x + width / 2 - (ButtonOkay.Length / 2), y + pos_y);
					AnsiConsole.Write(ButtonOkay);
					pos_y++;
				}

				// Return number of lines that were drawn	//
				// (to be able to calculate next position)	//
				return pos_y;
			}

			public static void PanelEnd(int x, int y, int pos_y, int width, Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
				theme ??= new Theme();

				// Style definitions //
				var Frame = new Markup(
					"[white on silver]└" +
					General.Repeat("─", width - 2) +
					"[/][black on silver]┘[/]" +
					"[black on black]  [/]"
				);
				var Shadow = new Markup(
					"[black on black]" +
					General.Repeat(" ", width) +
					"[/]"
				);

				lock (ConsoleLock) // Ensure thread-safe access to the console
				{
					// Draw panel end of the dialog //
					AnsiConsole.Cursor.SetPosition(x, y + pos_y);
					AnsiConsole.Write(Frame);
					pos_y++;

					AnsiConsole.Cursor.SetPosition(x + 2, y + pos_y);
					AnsiConsole.Write(Shadow);
				}

				return;
			}
		}
	}
}