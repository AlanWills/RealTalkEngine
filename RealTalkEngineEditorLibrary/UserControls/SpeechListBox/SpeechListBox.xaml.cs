using BindingsKernel;
using RealTalkEngine.Alexa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RealTalkEngine.UserControls
{
    /// <summary>
    /// Interaction logic for SpeechListBox.xaml
    /// </summary>
    public partial class SpeechListBox : UserControl
    {
        #region Properties and Fields

        public static readonly DependencyProperty SpeechDependencyProperty = 
            DependencyProperty.Register("Speech", typeof(Speech), typeof(SpeechListBox));

        public Speech Speech
        {
            get { return (Speech)GetValue(SpeechDependencyProperty); }
            set
            {
                SetValue(SpeechDependencyProperty, value);
                SpeechListBoxViewModel.Elements = value.Elements;
            }
        }

        private SpeechListBoxViewModel SpeechListBoxViewModel { get; set; } = new SpeechListBoxViewModel();

        #endregion
        
        public SpeechListBox()
        {
            DataContext = SpeechListBoxViewModel;

            InitializeComponent();
        }
    }
}
