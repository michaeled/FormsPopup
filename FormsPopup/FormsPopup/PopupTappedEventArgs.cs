using System;
using Xamarin.Forms;

namespace FormsPopup
{
	/// <summary>
	/// Represents the available event data, when the <see cref="FormsPopup.Popup.Tapped"/> event is invoked. 
	/// </summary>
	public class PopupTappedEventArgs : EventArgs
	{
		public PopupSectionType Section { get; set; }
		public View ControlTapped { get; set; }
		public Popup Popup { get; set; }
		public bool IsUserControl { get; set; }


		/// <summary>
		/// Instantiate a new <see cref="PopupTappedEventArgs"/>
		/// </summary>
		/// <param name="popup">The popup that was tapped</param>
		/// <param name="view">The view that was tapped</param>
		/// <returns></returns>
		public static PopupTappedEventArgs Create(Popup popup, View view)
		{
			PopupSectionType housingSectionType;

			var parentView = view.FindParent(ve =>
			{
				var parentSectionType = ve.GetValue(Popup.SectionTypeProperty);
				var currentSection = (PopupSectionType)parentSectionType;

				return currentSection == PopupSectionType.Body
					   || currentSection == PopupSectionType.Footer
					   || currentSection == PopupSectionType.Header;
			});


			if (parentView == null)
			{
				housingSectionType = PopupSectionType.Backdrop;
			}
			else
			{
				housingSectionType = (PopupSectionType)parentView.GetValue(Popup.SectionTypeProperty);
			}


			var controlType = (PopupSectionType)view.GetValue(Popup.SectionTypeProperty);
			var evt = new PopupTappedEventArgs
			{
				Popup = popup,
				ControlTapped = view,
				IsUserControl = controlType == PopupSectionType.NotSet,
				Section = housingSectionType
			};

			return evt;
		}
	}
}