using System.Linq;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Controls
{
	public class LoadingView : ContentView
	{
		private const double OpacityLevelWhenOverlayed = 0.3;
		private const int OpacityLevelWhenFullyVisible = 1;

		// This can be any view type you like just make sure it is the type you use in your target page
		private static readonly VisualElement DefaultGrid = new Grid { AutomationId = "defaultGrid" };
		public static readonly BindableProperty MainPanelProperty = BindableProperty.Create("MainPanel", typeof(VisualElement), typeof(Page), DefaultGrid);

		public VisualElement MainPanel => (VisualElement) GetValue(MainPanelProperty);

		public LoadingView()
		{
			ControlTemplate = (ControlTemplate) Application.Current.Resources.FirstOrDefault(x => x.Key == "LoadingControlTemplate").Value;

			PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == "IsVisible")
				{
					MainPanel.Opacity = IsVisible ? OpacityLevelWhenOverlayed : OpacityLevelWhenFullyVisible;

					if (Application.Current.MainPage != null)
					{
						Application.Current.MainPage.IsEnabled = !IsVisible;
					}
				}
			};
		}
	}
}
