using System;
using System.Threading.Tasks;
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
                double original;

                await popup1.ShowAsync(async p =>
                {
                    original = p.Scale;

                    await Task.WhenAll
                    (
                        p.SectionContainer.RelScaleTo(0.05, 100, Easing.CubicOut),
                        p.SectionContainer.RelScaleTo(-0.05, 105, Easing.CubicOut)
                    )
                    .ContinueWith(r => p.SectionContainer.Scale = original);
                });
            }
            else
            {
                popup1.Show();
            }
        }
    }
}
