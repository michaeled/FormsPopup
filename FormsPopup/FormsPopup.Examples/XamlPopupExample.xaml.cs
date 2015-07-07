using System;
using Xamarin.Forms;

namespace FormsPopup.Examples
{
    public partial class XamlPopupExample
    {
        public XamlPopupExample()
        {
            InitializeComponent();
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


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (showingAnimation.On)
            {
                await popup1.ShowAsync(async p =>
                {
                    await p.RelScaleTo(0.05, 90, Easing.CubicOut);
                    await p.RelScaleTo(-0.05, 80, Easing.CubicOut);
                });
            }
            else
            {
                popup1.Show();
            }
        }
    }
}
