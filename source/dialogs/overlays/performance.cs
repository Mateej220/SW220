using System.Diagnostics;

namespace SW220
{
	public partial class Dialogs
	{
		public partial class Overlays
		{
			public static bool Break_PerformanceOL = false;

			// -- Performance dialog overlay prefab --- //
			public static void PerformanceOL(int x, int y, int width = 35, int refresh_rate = 500, Theme? theme = null)
			{
				// If there is no defined theme, dialog will use default one.	//
				// (New instance of "Theme" [file: source/dialogs/theme.cs]	)	//
				theme ??= new Theme();

				// I had an idea of drawing simple performance stats here, but since PerformanceCounter
				// is not supported on Linux, I will leave this overlay unimplemented for now.
				// I would prefer to come with a cross-platform solution, but I don't have one at the moment.
				// (And time kinda pressures me atm...)
			}
		}
	}
}