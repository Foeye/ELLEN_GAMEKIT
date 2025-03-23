/**
  * @file :GameEntry
  * @brief :GameEntry Point
  * @details : Detail of this file
  * @author :
  * @version :0.1
  * @date :2025年1月1日
  * @copyright :Foeye
 */

using GameKit.Utils;
using UnityEngine;

namespace GameKit.Meta {
    public class GameEntry : MonoBehaviour{
        public void Awake() {
            AudioSys.Setup();
            DontDestroyOnLoad(this);
        }
    }
}