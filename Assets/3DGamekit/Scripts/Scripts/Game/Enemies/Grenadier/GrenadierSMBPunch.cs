using System.Collections;
using System.Collections.Generic;
using GameKit.Utils;
using UnityEngine;


namespace Gamekit3D
{
    public class GrenadierSMBPunch : SceneLinkedSMB<GrenadierBehaviour>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (m_MonoBehaviour.punchAudioPlayer)
            {
                m_MonoBehaviour.punchAudioPlayer.PlayRandomClip();
                AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Grenadier_Punch_Attack), m_MonoBehaviour.gameObject);
            }
                
        }
    }
}