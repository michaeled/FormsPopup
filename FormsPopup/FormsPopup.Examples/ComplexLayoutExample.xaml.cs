using System;

namespace FormsPopup.Examples
{
    public partial class ComplexLayoutExample
    {
        public ComplexLayoutExample()
        {
            InitializeComponent();
        }

        
        private void Button_OnClicked(object sender, EventArgs e)
        {
            popup1.Show();
        }


        private void Popup1_Tapped(object sender, PopupTappedEventArgs e)
        {
            popup1.Hide();
        }
    }
}
