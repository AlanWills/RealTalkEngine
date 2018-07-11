using RealTalkEngine.StorySystem.Transitions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RealTalkEngine.StorySystem.Conditions
{
    [Serializable]
    public abstract class TransitionCondition
    {
        #region Properties and Fields

        [NonSerialized]
        private Transition m_transition;
        /// <summary>
        /// The parent transition this condition is attached to.
        /// </summary>
        public Transition Transition
        {
            get { return m_transition; }
            set { m_transition = value; }
        }

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
