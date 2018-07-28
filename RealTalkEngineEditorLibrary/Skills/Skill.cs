using BindingsKernel;
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

        #endregion
    }
}