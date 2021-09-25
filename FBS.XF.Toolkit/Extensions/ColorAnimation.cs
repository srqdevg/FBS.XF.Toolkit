using System;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Extensions
{
	public class ColorAnimation : Animation
	{
		readonly Easing _easing;
		readonly Action _finished;
		readonly Action<double> _step;

		public ColorAnimation()
		{
			_easing = Easing.Linear;
			_step = f => { };
		}

		public ColorAnimation(Action<Color> callback, Color fromColor, Color toColor, Easing easing = null, Action finished = null)
		{
			_easing = easing ?? Easing.Linear;
			_finished = finished;

			//Func<double, double> transform = AnimationExtensions.Interpolate(start, end);
			Color transform(double t) => Color.FromRgba(
				fromColor.R + t * (toColor.R - fromColor.R),
				fromColor.G + t * (toColor.G - fromColor.G),
				fromColor.B + t * (toColor.B - fromColor.B),
				fromColor.A + t * (toColor.A - fromColor.A));

			_step = f => callback(transform(f));
		}

		public new void Commit(IAnimatable owner, string name, uint rate = 16, uint length = 250, Easing easing = null, Action<double, bool> finished = null,
			Func<bool> repeat = null)
		{
			owner.Animate(name, this, rate, length, easing, finished, repeat);
		}

		public new Action<double> GetCallback()
		{
			Action<double> result = f =>
			{
				_step(_easing.Ease(f));
				////foreach (Animation animation in _children)
				////{
				////	if (animation._finishedTriggered)
				////		continue;

				////	double val = Math.Max(0.0f, Math.Min(1.0f, (f - animation._beginAt) / (animation._finishAt - animation._beginAt)));

				////	if (val <= 0.0f) // not ready to process yet
				////		continue;

				////	Action<double> callback = animation.GetCallback();
				////	callback(val);

				////	if (val >= 1.0f)
				////	{
				////		animation._finishedTriggered = true;
				////		if (animation._finished != null)
				////			animation._finished();
				////	}
				////}
			};
			return result;
		}
	}
}
