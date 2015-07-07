using Xamarin.Forms;

namespace FormsPopup.Examples
{
    public class CodedPopupExample : ContentPage
    {
        private readonly SwitchCell _closeOnAnyTap;
        private readonly SwitchCell _preventShowing;
        private readonly SwitchCell _displayTappedSection ;
        private readonly Popup _popup1;


        public CodedPopupExample()
        {
            _closeOnAnyTap = new SwitchCell {Text = "Close on any tap", On = true};
            _preventShowing = new SwitchCell { Text = "Prevent popup from showing" };
            _displayTappedSection = new SwitchCell {Text = "Display the tapped section's name"};

            var showPopup = new Button {Text = "Show Popup"};

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
                }
            };

            // Required to be instantiated before the popup is added to ContentPage.Content
            var popupInit = new PopupPageInitializer(this) { _popup1 };

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
                                _displayTappedSection
                            }
                        },
                    },

                    showPopup
                }
            };


            _popup1.Tapped += Popup1_Tapped;
            _popup1.Showing += Popup1_Showing;
            showPopup.Clicked += ShowPopup_Clicked;
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
                DisplayAlert("Information", string.Format("{0} tapped.", e.Section), "OK");
            }
        }


        private void ShowPopup_Clicked(object sender, System.EventArgs e)
        {
            _popup1.Show();
        }
    }
}
