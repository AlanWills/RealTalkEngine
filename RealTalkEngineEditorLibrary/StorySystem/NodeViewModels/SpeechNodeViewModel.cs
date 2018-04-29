using NodeNetwork.ViewModels;
using ReactiveUI;
using RealTalkEngine.StorySystem.Nodes;
using RealTalkEngineEditorLibrary.StorySystem.Attributes;
using RealTalkEngineEditorLibrary.StorySystem.NodeViews;

namespace RealTalkEngineEditorLibrary.StorySystem.NodeViewModels
{
    [NodeViewModel(typeof(SpeechNode), "Speech Node")]
    public class SpeechNodeViewModel : BaseNodeViewModel<SpeechNode>
    {
        #region Registration

        static SpeechNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new SpeechNodeView(), typeof(IViewFor<SpeechNodeViewModel>));
        }

        #endregion
        
        public SpeechNodeViewModel(SpeechNode dialogNode) :
            base(dialogNode)
        {
            CreateInputPin<NodeInputViewModel>("Input");
            CreateOutputPin<NodeOutputViewModel>("Output");
        }
    }
}