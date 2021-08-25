using UnityEngine;

namespace Microgames.Objectives
{
    public class ObjectiveBase : MonoBehaviour
    {
        protected bool IsComplete { get; set; }

        protected float ElapsedTime { get; set; }

        private Microgame _parentMicrogame;

        protected void CompleteObjective()
        {
            IsComplete = true;
            _parentMicrogame.StartNextObjective();
        }
        public void SetParentMicrogame(Microgame parent) =>
            _parentMicrogame = parent;
    }
}
