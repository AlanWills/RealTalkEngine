using CelesteEngineEditor.Editors;
using RealTalkEngineEditorLibrary.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTalkEngineEditorLibrary.Editors
{
    public class SkillEditorViewModel : EditorViewModel
    {
        #region Properties and Fields

        public Skill Skill { get { return TargetObject as Skill; } }

        public string SkillName
        {
            get { return Skill?.SkillName; }
            set { Skill.SkillName = value; }
        }

        #endregion
    }
}