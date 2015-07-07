using Xamarin.Forms;

namespace FormsPopup.Examples
{
    public class CodedSimpleExample : ContentPage
    {
        public CodedSimpleExample()
        {
            var popup = new Popup
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
                        XAlign = TextAlignment.Center,
                        YAlign = TextAlignment.Center,
                        TextColor = Color.Black,
                        Text = "Hello, World!"
                    }
                }
            };

            new PopupPageInitializer(this) {popup};

            var button = new Button {Text = "Show Popup"};
            button.Clicked += (s, e) => popup.Show();

            Content = new StackLayout
            {
                Children = 
                {
					button
				}
            };
        }
    }
}
