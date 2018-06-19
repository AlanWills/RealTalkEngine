using BindingsKernel;
using BindingsKernel.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RealTalkEngine.Alexa
{
    public class Speech : ScriptableObject
    {
        #region Properties and Fields

        /// <summary>
        /// All of the individual elements which comprise the speech which will Alexa will generate for this node.
        /// </summary>
        [Serialize]
        public List<ISsml> Elements { get; } = new List<ISsml>();

        #endregion
    }
}