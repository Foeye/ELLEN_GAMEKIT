/**
  * @file :LODChecker
  * @brief :BRIEF
  * @details : Detail of this file
  * @author :
  * @version :0.1
  * @date :2025年1月6日
  * @copyright :Foeye
 */

using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace GameKit.Meta {
    public class LODChecker : MonoBehaviour {
        #if UNITY_EDITOR
        [Button("Check LOD")]
        private void LODReplace() {
            var lodGroups = GetComponentsInChildren<LODGroup>();
            foreach (LODGroup lodGroup in lodGroups) {
                var childRenderers = lodGroup.GetComponentsInChildren<Renderer>();
                var lods = lodGroup.GetLODs();
                var lastLODIndex = lods.Length - 1;
                var lastLod = lods[lastLODIndex];
                for (var i = 0; i < lastLODIndex; i++) {
                    foreach (var subRender in lods[i].renderers) {
                        foreach (var childRenderer in childRenderers) {
                            if (subRender == childRenderer) {
                                Object.DestroyImmediate(childRenderer.gameObject);
                            }
                        }
                    }
                }

                lastLod.screenRelativeTransitionHeight = 0.11f;
                var newLods = new LOD[1] { lastLod };
                lodGroup.SetLODs(newLods);
            }
            Selection.activeGameObject = null;
        }
        #endif
    }
}