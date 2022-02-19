using System.Windows.Input;

namespace FBS.XF.Toolkit.Interfaces
{
	/// <summary>
	/// IValidate ViewModel
	/// </summary>
	interface IValidateViewModel
	{
		#region Public methods
		/// <summary>
		/// Gets or sets the validate command.
		/// </summary>
		/// <value>The validate command.</value>
		ICommand ValidateCommand { get; set; }
		#endregion
	}
}
