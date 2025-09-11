using Spectre.Console;

namespace SW220
{
    public partial class Dialogs
    {
        public static void Info(int width, string title, string[] message, bool panel = true, Theme? theme = null)
        {
            // If there is no defined theme, dialog will use default one.	//
            // (New instance of "Theme" [file: source/dialogs/theme.cs] )	//
            theme ??= new Theme();

            // Render space setup //
            Console.BackgroundColor = theme.background;
            Console.Clear();
            AnsiConsole.Cursor.Hide();

            // Variables //
            int StartX = (Console.WindowWidth - width) / 2;
            int StartY = (Console.WindowHeight / 2) - ((message.Count()) / 2) - 2;

            // Render dialog head and message				//
            // (Part of the dialog which is static)			//
            Draw.Head(StartX, StartY, width, title, theme);
            int Y = Draw.BodyMessage(StartX, StartY, width, message, theme);

            // Render dialog panel (if enabled)			//
            if (panel)
            {
                Y = Draw.PanelHead(StartX, StartY, Y, width, theme);
                Y = Draw.PanelBody(StartX, StartY, Y, width, 0, "Okay", theme);
                Draw.PanelEnd(StartX, StartY, Y, width, theme);
            }
            else
            {
                Draw.PanelEnd(StartX, StartY, Y, width, theme);
            }

            // Wait for user input before closing the dialog	//
            // (User needs to press Enter or Esc key)		    //
            while (true)
            {
                var Input = Console.ReadKey();

                if (Input.Key == ConsoleKey.Enter) { break; }
                if (Input.Key == ConsoleKey.Escape) { break; }
            }

            // Clear console and show cursor			//
            Console.Clear();
            AnsiConsole.Cursor.Show();
            return;
        }
    }
}