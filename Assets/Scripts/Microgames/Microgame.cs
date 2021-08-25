using System;
using System.Collections.Generic;
using Microgames.Objectives;
using UnityEngine;
using UnityEngine.Events;

namespace Microgames
{
    public class Microgame : MonoBehaviour
    {
        private float _timer;
        
        [Header("Objectives")]
        [SerializeField] 
        private ObjectiveBase[] _objectives;
        
        [Header("Settings")] 
        [SerializeField]
        private bool _randomizeObjectives;

        private int _activeObjectiveIndex = 0;
        public MicrogameCompleteEvent MicrogameCompleted;
    
        // Start is called before the first frame update
        void Start()
        {
            _objectives[0].GetComponent<ObjectiveBase>().SetParentMicrogame(this);
            for (int i = 1; i < _objectives.Length; i++)
            {
                _objectives[i].GetComponent<ObjectiveBase>().SetParentMicrogame(this);
                _objectives[i].gameObject.SetActive(false);
            }
        }

        public void StartNextObjective()
        {
            _objectives[_activeObjectiveIndex].gameObject.SetActive(false);
            
            if(_randomizeObjectives)
            {
                throw new NotImplementedException();
            }
            else
            {
                _activeObjectiveIndex++;
                if (_activeObjectiveIndex != _objectives.Length)
                    _objectives[_activeObjectiveIndex].gameObject.SetActive(true);
                else
                {
                    MicrogameCompleted.Invoke(this);
                }
            }
        }
    }

    [System.Serializable]
    public class MicrogameCompleteEvent : UnityEvent<Microgame>
    {
        
    }
}