using BindingsKernel;
using RealTalkEngineEditorLibrary.Intents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTalkEngineEditorLibrary.Skills
{
    public class Skill : ScriptableObject
    {
        #region Serialized Fields

        /// <summary>
        /// The name of this skill that will appear on the Amazon management console.
        /// </summary>
        [Serialize]
        public string SkillName { get; set; }

        /// <summary>
        /// The intents associated with this skill.
        /// </summary>
        [Serialize]
        public List<Intent> Intents { get; private set; } = new List<Intent>();

        #endregion
    }
}