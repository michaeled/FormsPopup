using Xamarin.Forms;

namespace FormsPopup.Examples
{
    public class App : Application
    {
        public App()
        {
            /*
             *  Try these:
             *  MainPage = new CodedPopupExample();
             *  MainPage = new CodedSimpleExample();
             *  MainPage = new NavigationPage(new NavigationExample());
             *  MainPage = new CodedPopupExample();
             *  MainPage = new ComplexLayoutExample();
             */

            MainPage = new CodedPopupExample();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
