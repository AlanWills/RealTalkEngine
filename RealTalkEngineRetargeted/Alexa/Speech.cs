using BindingsKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTalkEngine.Alexa
{
    public class Speech : ISsml
    {
        /// <summary>
        /// All of the individual elements which comprise the speech which will Alexa will generate for this node.
        /// </summary>
        [Serialize]
        public List<ISsml> Elements { get; } = new List<ISsml>();
    }
}