using FractalPainting.Infrastructure.Common;
using FractalPainting.Solved.Step09.Infrastructure.UiActions;

namespace FractalPainting.Solved.Step09.App.Actions
{
	public class PaletteSettingsAction : IUiAction
	{
		private readonly Palette palette;

	    public PaletteSettingsAction(Palette palette)
	    {
	        this.palette = palette;
	    }

		public string Category => "Настройки";
		public string Name => "Палитра...";
		public string Description => "Цвета для рисования фракталов";

		public void Perform()
		{
			SettingsForm.For(palette).ShowDialog();
		}
	}
}