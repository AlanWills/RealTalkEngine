using BindingsKernel;
using NodeNetwork.ViewModels;
using ReactiveUI;
using RealTalkEngine.StorySystem.Nodes;
using RealTalkEngineEditorLibrary.StorySystem.Attributes;
using RealTalkEngineEditorLibrary.StorySystem.NodeViews;
using System.Windows;
using System.Reactive;
using RealTalkEngineEditorLibrary.StorySystem.Interfaces;

namespace RealTalkEngineEditorLibrary.StorySystem.NodeViewModels
{
    [NodeViewModel(typeof(SpeechNode), "Speech Node")]
    public class SpeechNodeViewModel : NodeViewModel, INodeViewModel
    {
        #region Registration

        static SpeechNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new SpeechNodeView(), typeof(IViewFor<SpeechNodeViewModel>));
        }

        #endregion

        #region Properties and Fields

        /// <summary>
        /// The underlying node object which we will be manipulating through this view model.
        /// </summary>
        public SpeechNode Node { get; }

        /// <summary>
        /// The name of the node being manipulated via this view model.
        /// </summary>
        public new string Name
        {
            get { return Node.Name; }
            set
            {
                Node.Name = value;
                base.Name = value;
            }
        }

        #endregion

        public SpeechNodeViewModel(SpeechNode speechNode)
        {
            CelDebug.AssertNotNull(speechNode);
            Node = speechNode;

            // Have to set the name on the base view model otherwise it will not get refreshed in the UI when we create this view model
            base.Name = speechNode.Name;

            // Have to set the position on the base view model otherwise it will not get refreshed in the UI when we create this view model
            Position = new Point((int)speechNode.Position.X, (int)speechNode.Position.Y);

            // The node view model's position can be changed in the network, so we add a callback here to update the node's position too when that happens
            Changed.Subscribe(Observer.Create((IReactivePropertyChangedEventArgs<IReactiveObject> e) =>
            {
                if (e.PropertyName == nameof(Position))
                {
                    Node.Position.X = (float)Position.X;
                    Node.Position.Y = (float)Position.Y;
                }
            }));

            CreateInputPin<NodeInputViewModel>("Input");
            CreateOutputPin<NodeOutputViewModel>("Output");
        }
        
        #region Pin Utility Functions

        /// <summary>
        /// Create an instance of the inputted type and add it to this node's inputs.
        /// Assign it the inputted name and then return it.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public K CreateInputPin<K>(string name) where K : NodeInputViewModel, new()
        {
            K pin = new K();
            pin.Name = name;

            Inputs.Add(pin);
            return pin;
        }

        /// <summary>
        /// Create an instance of the inputted type and add it to this node's outputs.
        /// Assign it the inputted name and then return it.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public K CreateOutputPin<K>(string name) where K : NodeOutputViewModel, new()
        {
            K pin = new K();
            pin.Name = name;

            Outputs.Add(pin);
            return pin;
        }

        #endregion
    }
}