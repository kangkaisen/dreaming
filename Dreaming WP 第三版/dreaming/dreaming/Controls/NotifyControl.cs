using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using dreaming.Common;
using dreaming.ControlHelp;

namespace dreaming.Controls
{
    public sealed class NotifyControl : Control
    {
        private TextBlock textBlockStatus;
        private Grid LayoutRoot;

        private string labelText;
        public NotifyControl()
        {
            this.DefaultStyleKey = typeof(NotifyControl);
        }
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            textBlockStatus = GetTemplateChild("textBlockStatus") as TextBlock;
            LayoutRoot = GetTemplateChild("LayoutRoot") as Grid;
            InitializeProgressType();
        }

        public string Text
        {
            get
            {
                return labelText;
            }
            set
            {
                labelText = value;
            }
        }



        internal Popup ChildWindowPopup
        {
            get;
            private set;
        }

        private static Frame RootVisual
        {
            get
            {
                return Window.Current == null ? null : Window.Current.Content as Frame;
            }
        }

        public  Page Page
        {
            get { return RootVisual.GetVisualDescendants().OfType<Page>().FirstOrDefault(); }
        }

        public bool IsOpen
        {
            get
            {
                return ChildWindowPopup != null && ChildWindowPopup.IsOpen;
            }
        }

        public void Show()
        {
            if (ChildWindowPopup == null)
            {
                ChildWindowPopup = new Popup();
                ChildWindowPopup.Child = this;
                ChildWindowPopup.IsLightDismissEnabled = true;

            }

            ChildWindowPopup.IsOpen = true;



        }

        public  void Hide()
        {

            if (ChildWindowPopup != null)
            {
                ChildWindowPopup.IsOpen = false;
            }
            
        }




        private void InitializeProgressType()
        {
            LayoutRoot.Width = HelpMethods.GetWindowsWidth();
            textBlockStatus.Visibility = Visibility.Visible;
            textBlockStatus.FontSize = 18;
            textBlockStatus.Text = Text;
            textBlockStatus.TextWrapping = TextWrapping.Wrap;
        }
    }
}
