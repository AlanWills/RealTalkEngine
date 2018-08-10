using BindingsKernel.Objects;
using CelesteEngineEditor.Assets;
using CelesteEngineEditor.Core;
using CelesteEngineEditor.Editors;
using RealTalkEngineEditorLibrary.Intents;
using RealTalkEngineEditorLibrary.Skills;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTalkEngineEditorLibrary.Editors
{
    public class SkillEditorViewModel : EditorViewModel
    {
        #region Properties and Fields
        
        public List<IReference> AvailableIntents
        {
            get { return Project.Current.ProjectAssets.Where(x => (x is ScriptableObjectAsset) && (x as ScriptableObjectAsset).AssetObject is Intent).Select(x => x.AssetObjectInternal).ToList(); }
        }

        /// <summary>
        /// The current selected intent in the intent list.
        /// </summary>
        public Intent SelectedIntent { get; set; }

        public ObservableCollection<Intent> Intents { get; private set; } = new ObservableCollection<Intent>();

        #endregion

        protected override void OnTargetObjectChanged()
        {
            base.OnTargetObjectChanged();

            Skill skill = TargetObject as Skill;

            Intents.Clear();
            foreach (Intent intent in skill.Intents)
            {
                Intents.Add(intent);
            }
        }

        #region Intent Utility Functions

        public void AddIntent()
        {
            if (SelectedIntent != null)
            {
                Skill skill = TargetObject as Skill;
                skill.Intents.Add(SelectedIntent);

                Intents.Add(SelectedIntent);
            }
        }

        #endregion
    }
}