﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace Gamekit3D
{
    public class ChomperSMBIdle : SceneLinkedSMB<ChomperBehavior>
    {
        public float minimumIdleGruntTime = 70.0f;
        public float maximumIdleGruntTime = 100.0f;

        protected float remainingToNextGrunt = 60.0f;

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (minimumIdleGruntTime > maximumIdleGruntTime)
                minimumIdleGruntTime = maximumIdleGruntTime;

            remainingToNextGrunt = Random.Range(minimumIdleGruntTime, maximumIdleGruntTime);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);

            remainingToNextGrunt -= Time.deltaTime;
            if (remainingToNextGrunt < 0)
            {
                Debug.Log(remainingToNextGrunt);
                remainingToNextGrunt = Random.Range(minimumIdleGruntTime, maximumIdleGruntTime);
                m_MonoBehaviour.Grunt();
                
                
            }

            m_MonoBehaviour.FindTarget();
            if (m_MonoBehaviour.target != null)
            {
                m_MonoBehaviour.StartPursuit();
            }
        }
        
    }

}