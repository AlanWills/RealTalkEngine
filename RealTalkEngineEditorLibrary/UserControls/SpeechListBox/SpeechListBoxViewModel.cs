using CelesteEngineEditor.ViewModels;
using RealTalkEngine.Alexa;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTalkEngine.UserControls
{
    public class SpeechListBoxViewModel : Notifier
    {
        #region Properties and Fields

        public List<ISsml> Elements { get; set; } = new List<ISsml>();
        
        #endregion

        public SpeechListBoxViewModel()
        {
            Elements.Add(new Sentence("Test"));
            Elements.Add(new Sentence("Test"));
        }
    }
}
