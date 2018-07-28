using CelesteEngineEditor.Assets;
using RealTalkEngine.StorySystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTalkEngineEditorLibrary.Assets
{
    public class StoryAsset : Asset<Story>
    {
        public StoryAsset(FileInfo file, Story story) :
            base(file, story)
        {
        }

        #region Save Override

        /// <summary>
        /// Saves the story to this asset's file path.
        /// </summary>
        public override void Save()
        {
            AssetObject.Save(FileInfo.FullName, true);
        }

        #endregion
    }
}
