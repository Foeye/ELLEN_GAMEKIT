/**
  * @file :AudioMappingEdit
  * @brief :BRIEF
  * @details : Detail of this file
  * @author :
  * @version :0.1
  * @date :2025年1月1日
  * @copyright :Foeye
 */

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace GameKit.Utils.Editor {
    public class AudioMappingEdit : OdinEditorWindow {
        [MenuItem("音频/音频映射表")]
        private static void OpenWindow() {
            var window = GetWindow<AudioMappingEdit>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.Show();
        }
        
        [OdinSerialize, HideLabel]
        private AudioMapping mAudioMapping = new AudioMapping();
    }
}