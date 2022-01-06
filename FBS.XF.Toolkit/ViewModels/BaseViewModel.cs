using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using FBS.XF.Toolkit.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using PropertyChanged;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.ViewModels
{
	/// <summary>
	/// Base ViewModel
	/// </summary>
	/// <seealso cref="System.ComponentModel.INotifyDataErrorInfo" />
	[AddINotifyPropertyChangedInterface]
	public class BaseViewModel : INotifyDataErrorInfo, IValidateViewModel
	{
		#region INotifyDataErrorInfo Events
		/// <summary>
		/// Occurs when the validation errors have changed for a property or for the entire entity.
		/// </summary>
		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="BaseViewModel"/> class.
		/// </summary>
		public BaseViewModel()
		{
			Errors = new List<ValidationFailure>();
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Raises the error changed.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		public void RaiseErrorChanged(string propertyName)
		{
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Validates the model.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="model">The model.</param>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns><c>true</c> if valid, <c>false</c> otherwise.</returns>
		public bool ValidateModel<T>(T model, string propertyName = null)
		{
			// Clear errors
			Errors.Clear();

			// Run Validation
			var validationResults = Validator.Validate(new ValidationContext<T>(model));

			// Add errors
			if (propertyName == null)
			{
				// Add all
				foreach (var error in validationResults.Errors)
				{
					Errors.Add(error);
					RaiseErrorChanged(error.PropertyName);
				}
			}
			else
			{
				// Remove old errors for this property
				Errors.Where(er => er.PropertyName.Equals(propertyName)).ToList().ForEach(er => Errors.Remove(er));

				// Add new ones for this property
				validationResults.Errors.Where(er => er.PropertyName.Equals(propertyName)).ToList().ForEach(er => Errors.Add(er));
				RaiseErrorChanged(propertyName);
			}

			return !Errors.Any();
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Adds the error if true.
		/// </summary>
		/// <param name="passedPropertyName">Name of the passed property.</param>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="message">The message.</param>
		/// <param name="isTrue">if set to <c>true</c> [is true].</param>
		protected void AddErrorIfTrue(string passedPropertyName, string propertyName, string message, bool isTrue)
		{
			// Either we are validating all or a specific property
			if (string.IsNullOrWhiteSpace(passedPropertyName) || passedPropertyName.Equals(propertyName))
			{
				// Failed?
				if (isTrue)
				{
					// Yes add error
					Errors.Add(new ValidationFailure(propertyName, message));
					RaiseErrorChanged(propertyName);
					HasErrors = Errors.Any();
				}
				else
				{
					// Is valid, so check for existence of old error?
					var priorError = Errors.FirstOrDefault(e => e.PropertyName.Equals(propertyName));

					if (priorError != null)
					{
						Errors.Remove(priorError);
						RaiseErrorChanged(propertyName);
						HasErrors = Errors.Any();
					}
				}
			}
		}
		#endregion

		#region INotifyDataErrorInfo methods
		/// <summary>
		/// Gets the validation errors for a specified property or for the entire entity.
		/// </summary>
		/// <param name="propertyName">The name of the property to retrieve validation errors for; or <see langword="null" /> or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
		/// <returns>The validation errors for the property or entity.</returns>
		public IEnumerable GetErrors(string propertyName)
		{
			return Errors.Where(er => er.PropertyName.Equals(propertyName));
		}
		#endregion

		#region INotifyDataErrorInfo Properties
		/// <summary>
		/// Gets a value that indicates whether the entity has validation errors.
		/// </summary>
		/// <value><c>true</c> if this instance has errors; otherwise, <c>false</c>.</value>
		public bool HasErrors { get; set; }
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the errors.
		/// </summary>
		/// <value>The errors.</value>
		public List<ValidationFailure> Errors { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is busy.
		/// </summary>
		/// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
		public bool IsBusy { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is loading.
		/// </summary>
		/// <value><c>true</c> if this instance is loading; otherwise, <c>false</c>.</value>
		public bool IsLoading { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is mobile.
		/// </summary>
		/// <value><c>true</c> if this instance is mobile; otherwise, <c>false</c>.</value>
		public bool IsMobile => Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is saving.
		/// </summary>
		/// <value><c>true</c> if this instance is saving; otherwise, <c>false</c>.</value>
		public bool IsSaving { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the validator.
		/// </summary>
		/// <value>The validator.</value>
		public IValidator Validator { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [validate fields].
		/// </summary>
		/// <value><c>true</c> if [validate fields]; otherwise, <c>false</c>.</value>
		public bool ValidateFields { get; set; }
		#endregion

		#region IValidateViewModel Properties
		/// <summary>
		/// Gets or sets the validate command.
		/// </summary>
		/// <value>The validate command.</value>
		public ICommand ValidateCommand { get; set; }
		#endregion
	}
}