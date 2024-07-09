using PropertyChanged;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Behaviours
{
	/// <summary>
	/// Masked Format Behavior.
	/// Implements the <see cref="Xamarin.Forms.Behavior{Xamarin.Forms.Entry}" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Behavior{Xamarin.Forms.Entry}" />
	public class MaskedFormatBehavior : Behavior<Entry>
	{
		#region UI methods
		/// <summary>
		/// Handles the <see cref="E:EntryTextChanged" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The <see cref="Xamarin.Forms.TextChangedEventArgs"/> instance containing the event data.</param>
		[SuppressPropertyChangedWarnings]
		private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{
			if (sender is Entry entry)
			{
				var text = entry.Text;

				if (string.IsNullOrWhiteSpace(text) || positions == null)
					return;

				if (text.Length > mask.Length)
				{
					entry.Text = text.Remove(text.Length - 1);
					return;
				}

				foreach (var position in positions)
				{
					if (text.Length >= position.Key + 1)
					{
						var value = position.Value.ToString();
						if (text.Substring(position.Key, 1) != value)
							text = text.Insert(position.Key, value);
					}
				}

				if (entry.Text != text)
				{
					entry.Text = text;
				}
			}
		}
		#endregion

		#region Override methods
		/// <summary>
		/// Called when [attached to].
		/// </summary>
		/// <param name="entry">The entry.</param>
		protected override void OnAttachedTo(Entry entry)
		{
			entry.TextChanged += OnEntryTextChanged;
			base.OnAttachedTo(entry);
		}

		/// <summary>
		/// Called when [detaching from].
		/// </summary>
		/// <param name="entry">The entry.</param>
		protected override void OnDetachingFrom(Entry entry)
		{
			entry.TextChanged -= OnEntryTextChanged;
			base.OnDetachingFrom(entry);
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Sets the positions.
		/// </summary>
		private void SetPositions()
		{
			if (string.IsNullOrWhiteSpace(Mask))
			{
				positions = null;
				return;
			}

			var list = new Dictionary<int, char>();
			for (var i = 0; i < Mask.Length; i++)
				if (Mask[i] != 'X')
					list.Add(i, Mask[i]);

			positions = list;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the mask.
		/// </summary>
		/// <value>The mask.</value>
		public string Mask
		{
			get => mask;
			set
			{
				mask = value;
				SetPositions();
			}
		}
		#endregion

		#region Fields
		private string mask = "";
		IDictionary<int, char> positions;
		#endregion
	}
}
