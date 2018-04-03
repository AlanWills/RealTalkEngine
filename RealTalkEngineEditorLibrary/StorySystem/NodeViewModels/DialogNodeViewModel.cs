﻿using BindingsKernel;
using NodeNetwork.ViewModels;
using ReactiveUI;
using RealTalkEngine.StorySystem.Nodes;
using RealTalkEngineEditorLibrary.StorySystem.Attributes;
using RealTalkEngineEditorLibrary.StorySystem.NodeViews;

namespace RealTalkEngineEditorLibrary.StorySystem.NodeViewModels
{
    [Node("Dialog Node")]
    public class DialogNodeViewModel : BaseNodeViewModel<DialogNode>
    {
        #region Registration

        static DialogNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new DialogNodeView(), typeof(IViewFor<DialogNodeViewModel>));
        }

        #endregion

        #region Properties and Fields

        /// <summary>
        /// The dialog text that is represented by this node.
        /// </summary>
        [Serialize]
        public string Dialog
        {
            get { return Node.Dialog; }
            set
            {
                Node.Dialog = value;
                this.RaisePropertyChanged(nameof(Node.Dialog));
            }
        }

        #endregion

        public DialogNodeViewModel()
        {
            CreateInputPin<NodeInputViewModel>("Input");
            CreateOutputPin<NodeOutputViewModel>("Output");
        }
    }
}