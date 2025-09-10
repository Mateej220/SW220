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

                // Draw head of the menu //
                AnsiConsole.Cursor.SetPosition(x, y + pos_y);
                AnsiConsole.Write(Frame);
                pos_y++;

                // Return number of lines that were drawn	//
				// (to be able to calculate next position)	//
                return pos_y;
            }
        }
    }
}