using Alexa.NET.Response.Ssml;
using CelesteEngineEditor.ViewModels;
using System.Collections.Generic;

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
