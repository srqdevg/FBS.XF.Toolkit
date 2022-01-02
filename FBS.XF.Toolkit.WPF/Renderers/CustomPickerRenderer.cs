using System;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.WPF.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using Size = System.Windows.Size;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace FBS.XF.Toolkit.WPF.Renderers
{
	internal class CustomPickerRenderer : PickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.IsEditable = false;
				Control.Loaded += ControlOnLoaded;
			}
		}

		private void ControlOnLoaded(object sender, RoutedEventArgs e)
		{
			if (((CustomPicker) Element).SizeToFit)
			{
				SetWidthFromItems(Control);
			}
		}

		protected override void Dispose(bool disposing)
		{
			Control.Loaded -= ControlOnLoaded;

			base.Dispose(disposing);
		}

		private static void SetWidthFromItems(ComboBox comboBox)
		{
			double comboBoxWidth = 19;// comboBox.DesiredSize.Width;

			// Create the peer and provider to expand the comboBox in code behind. 
			ComboBoxAutomationPeer peer = new ComboBoxAutomationPeer(comboBox);
			IExpandCollapseProvider provider = (IExpandCollapseProvider) peer.GetPattern(PatternInterface.ExpandCollapse);
			EventHandler eventHandler = null;
			eventHandler = delegate
			{
				if (comboBox.IsDropDownOpen &&
				    comboBox.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
				{
					double width = 0;
					foreach (var item in comboBox.Items)
					{
						ComboBoxItem comboBoxItem = comboBox.ItemContainerGenerator.ContainerFromItem(item) as ComboBoxItem;
						comboBoxItem.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
						if (comboBoxItem.DesiredSize.Width > width)
						{
							width = comboBoxItem.DesiredSize.Width;
						}
					}
					comboBox.Width = comboBoxWidth + width;
					// Remove the event handler. 
					comboBox.ItemContainerGenerator.StatusChanged -= eventHandler;
					comboBox.DropDownOpened -= eventHandler;
					provider.Collapse();
				}
			};
			comboBox.ItemContainerGenerator.StatusChanged += eventHandler;
			comboBox.DropDownOpened += eventHandler;
			// Expand the comboBox to generate all its ComboBoxItem's. 
			provider.Expand();
		}
    }
}
