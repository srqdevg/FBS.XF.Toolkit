﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FBS.XF.Toolkit.Controls
{
	/// <summary>
	/// Zoom Content View.
	/// </summary>
	public class ZoomContentView : ContentView
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ZoomContentView"/> class.
		/// </summary>
		public ZoomContentView()
		{
			var pinchGesture = new PinchGestureRecognizer();
			pinchGesture.PinchUpdated += OnPinchUpdated;
			GestureRecognizers.Add(pinchGesture);
		}
		#endregion

		#region UI methods
		/// <summary>
		/// Handles the <see cref="E:PinchUpdated" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PinchGestureUpdatedEventArgs"/> instance containing the event data.</param>
		private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
		{
			if (e.Status == GestureStatus.Started)
			{
				// Store the current scale factor applied to the wrapped user interface element,
				// and zero the components for the center point of the translate transform.
				startScale = Content.Scale;
				Content.AnchorX = 0;
				Content.AnchorY = 0;
			}

			if (e.Status == GestureStatus.Running)
			{
				// Calculate the scale factor to be applied.
				currentScale += (e.Scale - 1) * startScale;
				currentScale = Math.Max(1, currentScale);

				// The ScaleOrigin is in relative coordinates to the wrapped user interface element,
				// so get the X pixel coordinate.
				var renderedX = Content.X + xOffset;
				var deltaX = renderedX / Width;
				var deltaWidth = Width / (Content.Width * startScale);
				var originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

				// The ScaleOrigin is in relative coordinates to the wrapped user interface element,
				// so get the Y pixel coordinate.
				var renderedY = Content.Y + yOffset;
				var deltaY = renderedY / Height;
				var deltaHeight = Height / (Content.Height * startScale);
				var originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

				// Calculate the transformed element pixel coordinates.
				var targetX = xOffset - originX * Content.Width * (currentScale - startScale);
				var targetY = yOffset - originY * Content.Height * (currentScale - startScale);

				// Apply translation based on the change in origin.
				Content.TranslationX = targetX.Clamp(-Content.Width * (currentScale - 1), 0);
				Content.TranslationY = targetY.Clamp(-Content.Height * (currentScale - 1), 0);

				// Apply scale factor
				Content.Scale = currentScale;
			}

			if (e.Status == GestureStatus.Completed)
			{
				// Store the translation delta's of the wrapped user interface element.
				xOffset = Content.TranslationX;
				yOffset = Content.TranslationY;
			}
		}
		#endregion

		#region Fields
		private double currentScale = 1;
		private double startScale = 1;
		private double xOffset;
		private double yOffset;
		#endregion
	}
}