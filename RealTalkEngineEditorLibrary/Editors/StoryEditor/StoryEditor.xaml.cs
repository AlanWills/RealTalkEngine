using CelesteEngineEditor.Attributes;
using CelesteEngineEditor.Editors;
using DevZest.Windows.Docking;
using RealTalkEngine.StorySystem;
using RealTalkEngineEditorLibrary.Editors;
using RealTalkEngineEditorLibrary.StorySystem;
using RealTalkEngineEditorLibrary.StorySystem.Attributes;
using RealTalkEngineEditorLibrary.StorySystem.NodeViewModels;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace RealTalkEngineEditorLibrary.Editors
{
    /// <summary>
    /// Interaction logic for StoryEditor.xaml
    /// </summary>
    [CustomEditor(typeof(Story), "Story Editor", DockPosition.Document)]
    public partial class StoryEditor : Editor
    {
        #region Properties and Fields

        private StoryEditorViewModel StoryEditorViewModel { get { return ViewModel as StoryEditorViewModel; } }

        #endregion

        public StoryEditor() :
            base(new StoryEditorViewModel())
        {
            InitializeComponent();
            
            ContextMenu contextMenu = new ContextMenu();
            MenuItem createMenuItem = new MenuItem() { Header = "Create" };
            contextMenu.Items.Add(createMenuItem);

            foreach (Type type in NodeViewModelFactory.NodeViewModels)
            {
                NodeViewModelAttribute nodeAttribute = type.GetCustomAttribute<NodeViewModelAttribute>();
                MenuItem nodeMenuItem = new MenuItem() { Header = nodeAttribute.MenuName };
                nodeMenuItem.DataContext = nodeAttribute.NodeType;
                nodeMenuItem.Click += NodeMenuItem_Click;
                createMenuItem.Items.Add(nodeMenuItem);
            }

            // Network swallows the context menu and right button down events
            // so we have to manually open the context menu in the preview event
            Network.ContextMenu = contextMenu;
            Network.PreviewMouseRightButtonDown += (sender, e) => 
            {
                Network.ContextMenu.DataContext = e.GetPosition(Network);
                Network.ContextMenu.IsOpen = true;
            };
        }

        private void NodeMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Type nodeType = (sender as MenuItem).DataContext as Type;
            Point nodePosition = Network.NetworkViewportRegion.Location;
            Point originalMousePosition = (Point)Network.ContextMenu.DataContext;
            nodePosition.X += originalMousePosition.X;
            nodePosition.Y += originalMousePosition.Y;
            StoryEditorViewModel.CreateNode(nodeType, "New " + (sender as MenuItem).Header, nodePosition);
        }
    }
}
