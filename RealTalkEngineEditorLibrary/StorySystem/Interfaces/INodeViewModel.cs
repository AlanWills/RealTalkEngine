using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTalkEngineEditorLibrary.StorySystem.Interfaces
{
    public interface INodeViewModel
    {
        #region Properties and Fields

        /// <summary>
        /// The name of the node represented via this view model.
        /// </summary>
        string Name { get; set; }

        #endregion
    }
}
