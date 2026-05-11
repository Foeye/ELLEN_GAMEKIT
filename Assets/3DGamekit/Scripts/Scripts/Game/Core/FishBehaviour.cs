/**
  * @file :FishBehaviour
  * @brief :BRIEF
  * @details : Detail of this file
  * @author :六队
  * @version :0.1
  * @date :2025年9月23日
  * @copyright :Foeye
 */

using GameKit.Utils;
using UnityEngine;

namespace Gamekit3D {
    public class FishBehaviour : MonoBehaviour{
        public void OnLeapUp() {
            Debug.Log("【WWise client】Fish Leap Up Sound");
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Fish_Leap), gameObject);
        }

        public void OnPlungeInto() {
            Debug.Log("【WWise client】Fish plunge into Sound");
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Fish_Plunge), gameObject);
        }
    }
}