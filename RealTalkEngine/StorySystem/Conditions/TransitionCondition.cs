using RealTalkEngine.StorySystem.Transitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.StorySystem.Conditions
{
    [Serializable]
    public abstract class TransitionCondition
    {
        #region Properties and Fields

        /// <summary>
        /// The parent transition this condition is attached to.
        /// </summary>
        public Transition Transition { get; set; }

        #endregion
        
        #region Condition Validation Functions

        /// <summary>
        /// Returns true if the game state is such that this condition is valid.
        /// </summary>
        /// <returns></returns>
        public abstract bool ConditionPasses();

        #endregion
    }
}
