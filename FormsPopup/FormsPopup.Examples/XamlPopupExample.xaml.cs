using System;

namespace FormsPopup.Examples
{
    public partial class XamlPopupExample
    {
        public XamlPopupExample()
        {
            InitializeComponent();
            popup1.Tapped += Popup1_Tapped;
            popup1.Showing += Popup1_Showing;
        }


        private void Popup1_Showing(object sender, PopupShowingEventArgs e)
        {
            if (preventShowing.On)
            {
                e.Cancel = true;
            }
        }


        private void Popup1_Tapped(object sender, PopupTappedEventArgs e)
        {
            if (closeOnAnyTap.On)
            {
                e.Popup.Hide();
            }

            if (displayTappedSection.On)
            {
                DisplayAlert("Information", string.Format("{0} tapped.", e.Section), "OK");
            }
        }


        private void Button_OnClicked(object sender, EventArgs e)
        {
            popup1.Show();
        }
    }
}
