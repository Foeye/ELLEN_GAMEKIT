using System.Collections;
using System.Collections.Generic;
using GameKit.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Gamekit3D
{
    [RequireComponent(typeof(Collider))]
    public class InteractOnTrigger : MonoBehaviour
    {
        public LayerMask layers;
        public UnityEvent OnEnter, OnExit;
        new Collider collider;
        public InventoryController.InventoryChecker[] inventoryChecks;

        void Reset()
        {
            layers = LayerMask.NameToLayer("Everything");
            collider = GetComponent<Collider>();
            collider.isTrigger = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (0 != (layers.value & 1 << other.gameObject.layer))
            {
                ExecuteOnEnter(other);
            }
        }
        public void WeaponPickUpSound()
        {
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Weapon_Stick_Pickup), gameObject); 
        }

        public void HealthCrateOpenSound()
        {
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.HealthBoxes_Open), gameObject);
        }

        public void PrePadStepSound()
        {
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Pressure_Pad_Enter), gameObject);
        }

         public void PrePad2StepSound()
        {
            AkSoundEngine.PostEvent("Pressure_Pad_02_Enter", gameObject);
        }

        public void PrePadExitSound()
        {
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Pressure_Pad_Exit), gameObject);
        }


        public void EllenVoice01()
        {
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Ellen_Voice_01), gameObject);
        }
        public void EllenVoice02()
        {
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Ellen_Voice_02), gameObject);
        }
        public void EllenVoice03()
        {
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Ellen_Voice_03), gameObject);
        }
        public void EllenVoice04()
        {
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Ellen_Voice_04), gameObject);
        }
         public void EllenVoice04b()
        {
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Ellen_Voice_04b), gameObject);
        }
        public void EllenVoice05()
        {
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Ellen_Voice_05), gameObject);
        }


        public void SwitchOnSound()
        {
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Switch_Activated), gameObject);
        }


        protected virtual void ExecuteOnEnter(Collider other)
        {
            OnEnter.Invoke();
            for (var i = 0; i < inventoryChecks.Length; i++)
            {
                inventoryChecks[i].CheckInventory(other.GetComponentInChildren<InventoryController>());
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (0 != (layers.value & 1 << other.gameObject.layer))
            {
                ExecuteOnExit(other);
            }
        }

        protected virtual void ExecuteOnExit(Collider other)
        {
            OnExit.Invoke();
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
        }

        void OnDrawGizmosSelected()
        {
            //need to inspect events and draw arrows to relevant gameObjects.
        }

    } 
}
