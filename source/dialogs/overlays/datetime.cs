using System.Globalization;

namespace SW220
{
	public partial class Dialogs
	{
		public partial class Overlays
		{
			public static bool Break_DateTimeOL = false;

			// -- Date and time dialog overlay prefab --- //
			public static void DateTimeOL(int x, int y, int width = 40, string title = "Date & time overlay", Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
				theme ??= new Theme();

				var culture = new CultureInfo("en-US");
				string[] content;
				int Y;

				while (Break_DateTimeOL == false)
				{
					Thread.Sleep(250); 

					content = [
						$"{DateTime.Now.ToString("dddd dd 'of the' MMMM yyyy", culture)}",
    					$"{DateTime.Now:HH:mm}"
					];

					Draw.Head(x, y, width, title, theme);
					Y = Draw.BodyMessage(x, y, width, content, theme);
					Draw.PanelEnd(x, y, Y, width, theme);

					Thread.Sleep(1000 * 5);
				}

				// Draws the dialog last time with "STOP" at the title to indicate that the thread has ended. //
				content = [
					$"{DateTime.Now.ToString("dddd dd 'of the' MMMM yyyy", culture)}",
					$"{DateTime.Now:HH:mm}"
				];

				Draw.Head(x, y, width, title + " (STOP)", theme);
				Y = Draw.BodyMessage(x, y, width, content, theme);
				Draw.PanelEnd(x, y, Y, width, theme);

				Break_DateTimeOL = false;
				return;
			}
		}
	}
}