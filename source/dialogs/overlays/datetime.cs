using System.Globalization;

namespace SW220
{
	public partial class Dialogs
	{
		public partial class Overlays
		{
			public static bool Break_DateTimeOL = false;

			// -- Date and time dialog overlay prefab --- //
			public static void DateTimeOL(int x, int y, int width = 40, string title = "Date & time overlay", int refresh_rate = 1000, Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
				theme ??= new Theme();

				// Culture info for date formatting //
				var culture = new CultureInfo("en-US");
				string[] content;
				int Y;

				// Render overlay loop 										//
				// (Redraws the overlay every [refresh_rate] milliseconds)	//
				while (Break_DateTimeOL == false)
				{
					lock (Draw.ConsoleLock)
					{
						content = [
							$"{DateTime.Now.ToString("dddd dd 'of the' MMMM yyyy", culture)}",
							$"{DateTime.Now:HH:mm:ss}"
						];

						Draw.Head(x, y, width, title, theme);
						Y = Draw.BodyMessage(x, y, width, content, theme);
						Draw.PanelEnd(x, y, Y, width, theme);
					}
					Thread.Sleep(refresh_rate);
				}

				// Draws the dialog last time with "STOP" at the title to indicate that the thread has ended. //
				content = [
					$"{DateTime.Now.ToString("dddd dd 'of the' MMMM yyyy", culture)}",
					$"{DateTime.Now:HH:mm:ss}"
				];

				lock (Draw.ConsoleLock) // Ensure thread-safe access to the console
				{
					Draw.Head(x, y, width, title + " (STOP)", theme);
					Y = Draw.BodyMessage(x, y, width, content, theme);
					Draw.PanelEnd(x, y, Y, width, theme);
				}

				// Reset the break flag for future use //
				Break_DateTimeOL = false;
				return;
			}
		}
	}
}