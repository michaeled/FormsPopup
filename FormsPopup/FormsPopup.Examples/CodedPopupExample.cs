using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsPopup.Examples
{
    public class CodedPopupExample : ContentPage
    {
        private readonly SwitchCell _closeOnAnyTap;
        private readonly SwitchCell _preventShowing;
        private readonly SwitchCell _displayTappedSection;
        private readonly SwitchCell _showingAnimation;
        private readonly Popup _popup1;


        public CodedPopupExample()
        {
            _closeOnAnyTap = new SwitchCell {Text = "Close on any tap", On = true};
            _preventShowing = new SwitchCell { Text = "Prevent popup from showing" };
            _displayTappedSection = new SwitchCell {Text = "Display the tapped section's name"};
            _showingAnimation = new SwitchCell { Text = "Turn on 'Showing' animation" };

            var showPopup = new Button {Text = "Show Popup"};

            var closeButton = new Button
            {
                Text = "Close",
                TextColor = Color.FromHex("#D37E00"),
                BackgroundColor = Color.White,
            };

            _popup1 = new Popup
            {
                XPositionRequest = 0.5,
                YPositionRequest = 0.5,
                ContentWidthRequest = 0.8,
                ContentHeightRequest = 0.5,

                Header = new ContentView
                {
                    Padding = 10,
                    BackgroundColor = Color.FromHex("#3399FF"),
                    Content = new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof (Label)),
                        TextColor = Color.White,
                        Text = "Simple popup"
                    }
                },

                Body = new ContentView
                {
                    Padding = 10,
                    BackgroundColor = Color.White,
                    Content = new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof (Label)),
                        TextColor = Color.Gray,
                        Text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                    }
                },

                Footer = new ContentView
                {
                    BackgroundColor = Color.White,

                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Children = { closeButton }
                    }
                }
            };


            Content = new StackLayout
            {
                Children =
                {
                    new TableView
                    {
                        Intent = TableIntent.Settings,
                        Root = new TableRoot
                        {
                            new TableSection("Coded Popup Example")
                            {
                                _closeOnAnyTap,
                                _preventShowing,
                                _displayTappedSection,
                                _showingAnimation
                            }
                        },
                    },

                    showPopup
                }
            };

			// Required to be instantiated before the popup is added to ContentPage.Content
			new PopupPageInitializer(this) { _popup1 };


			_popup1.Tapped += Popup1_Tapped;
            _popup1.Showing += Popup1_Showing;
            showPopup.Clicked += ShowPopup_Clicked;
            closeButton.Clicked += CloseButton_Clicked;
        }


        private async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await _popup1.HideAsync(async p =>
            {
                await p.FadeTo(0, 250, Easing.Linear);
                p.Opacity = 1;
            });
        }


        private void Popup1_Showing(object sender, PopupShowingEventArgs e)
        {
            if (_preventShowing.On)
            {
                e.Cancel = true;
            }
        }


        private void Popup1_Tapped(object sender, PopupTappedEventArgs e)
        {
            if (_closeOnAnyTap.On)
            {
                e.Popup.Hide();
            }

            if (_displayTappedSection.On)
            {
                DisplayAlert("Information", $"{e.Section} tapped.", "OK");
            }
        }


        private async void ShowPopup_Clicked(object sender, EventArgs e)
        {
            if (_showingAnimation.On)
            {
                double original;

                await _popup1.ShowAsync(async p =>
                {
                    original = p.Scale;

                    await Task.WhenAll
                    (
                        p.SectionContainer.RelScaleTo(0.05, 100, Easing.CubicOut),
                        p.SectionContainer.RelScaleTo(-0.05, 105, Easing.CubicOut)
                    ).ContinueWith(c =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            p.SectionContainer.Scale = original;
                        });
                    });
                });
            }
            else
            {
                _popup1.Show();
            }
        }
    }
}