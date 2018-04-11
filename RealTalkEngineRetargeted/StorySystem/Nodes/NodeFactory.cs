using BindingsKernel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RealTalkEngine.StorySystem.Nodes
{
    public static class NodeFactory
    {
        #region Properties and Fields

        private static List<Type> NodesImpl { get; set; } = new List<Type>();

        private static ReadOnlyCollection<Type> nodes;
        /// <summary>
        /// All of the available nodes loaded into the application.
        /// Call LoadNodes to refresh this collection.
        /// </summary>
        public static ReadOnlyCollection<Type> Nodes
        {
            get
            {
                if (nodes == null)
                {
                    nodes = new ReadOnlyCollection<Type>(NodesImpl);
                    LoadNodes();
                }

                return nodes;
            }
        }
        
        #endregion

        /// <summary>
        /// Find all the nodes within this assembly and store their type information.
        /// </summary>
        public static void LoadNodes()
        {
            NodesImpl.Clear();

            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            NodesImpl.AddRange(executingAssembly.GetTypes().Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(BaseNode))));
        }

        #region Factory Functions

        /// <summary>
        /// Use the inputted node type anme to create a node from all the registered nodes.
        /// Returns null if no type exists for the inputted node type name.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static BaseNode CreateNode(string nodeType)
        {
            Type type = Nodes.FirstOrDefault(x => x.Name == nodeType);
            if (type != null)
            {
                return Activator.CreateInstance(type) as BaseNode;
            }

            CelDebug.Fail("No node type for inputted node type " + nodeType);
            return null;
        }

        #endregion
    }
}
