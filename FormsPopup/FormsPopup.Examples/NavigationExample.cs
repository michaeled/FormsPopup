using Xamarin.Forms;

namespace FormsPopup.Examples
{
    public class NavigationExample : ContentPage
    {
        private readonly ContentPage _childPage;
        private readonly Popup _popup;


        public NavigationExample()
        {
            var btnNavigate = new Button {Text = "Navigate to another Page"};
            var btnPopup = new Button {Text = "Show Popup"};

            _popup = new Popup
            {
                XPositionRequest = 0.5,
                YPositionRequest = 0.2,
                ContentHeightRequest = 0.1,
                ContentWidthRequest = 0.4,
                Padding = 10,

                Body = new ContentView
                {
                    BackgroundColor = Color.White,
                    Content = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.Black,
                        Text = "Hello, World!"
                    }
                }
            };


            // Initialize event handlers
            btnPopup.Clicked += BtnPopup_Clicked;
            btnNavigate.Clicked += BtnNavigate_Clicked;
            _popup.Tapped += Popup_Tapped;


            // Initialize popup view
            var _ = new PopupPageInitializer(this) {_popup};


            // Notice _popup is not added to this ContentPage
            Content = new StackLayout
            {
                Padding = 15,

                Children =
                {
                    btnNavigate,
                    btnPopup
                }
            };


            // Child page that will opened via btnPopup.Clicked
            _childPage = new ContentPage
            {
                Content = new Label
                {
                    Text = "Hello, World!"
                }
            };
        }


        private void Popup_Tapped(object sender, PopupTappedEventArgs e)
        {
            _popup.Hide();
        }



        private void BtnNavigate_Clicked(object sender, System.EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(_childPage, true);
        }


        private void BtnPopup_Clicked(object sender, System.EventArgs e)
        {
            _popup.Show();
        }
    }
}
