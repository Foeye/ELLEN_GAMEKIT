/**
  * @file :AudioMapping
  * @brief :BRIEF
  * @details : Detail of this file
  * @author :Foeye
  * @version :0.1
  * @date :2024年12月31日
  * @copyright :Foeye
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;

namespace GameKit.Utils {
    [Serializable]
    public class AudioMapping {
        
        #region Collpsed GUI

        [InfoBox("以下内容为Wwise音频映射表，用于记录游戏中的音频事件与Wwise中的音频事件的对应关系。\n" +
                 "如果发现声音无法播放请仔细检查【Wwise 事件名称】是否和Wwise工程中的Event相匹配", 
            SdfIconType.Alarm, IconColor = "#C95344")]
        #if UNITY_EDITOR
        [TableList(AlwaysExpanded = true), LabelText("Wwise 音频映射"), HideLabel, OnValueChanged(nameof(OnItemChanged))]
        #endif
        public List<WwiseAudioMappingItem> WwiseAudioMappingItems;
        #endregion
        
        #if UNITY_EDITOR
        [OnInspectorInit]
        private void OnInitialized() {
            Initialize();
            foreach (var wwiseAudioMappingItem in WwiseAudioMappingItems) {
                wwiseAudioMappingItem.RegisterAnyValueChange(OnItemChanged);
            }
        }
        private async void OnItemChanged(bool needCompile) {
            try {
                if (needCompile) {
                    await Task.Run(() => AudioGameKit.GenerateEnumFromJson(WwiseAudioMappingItems));
                }
                await Task.Run(() => AudioGameKit.WriteEnumJson(WwiseAudioMappingItems));
                AssetDatabase.Refresh();
            }
            catch (Exception e) {
                Debug.LogError(e);
            }
        }
        #endif
        
        public void Initialize() {
            var mappingFile = Resources.Load(AudioConst.MappingFileName) as TextAsset;
            if (mappingFile != null) {
                WwiseAudioMappingItems ??= new List<WwiseAudioMappingItem>();
                var readList = JsonConvert.DeserializeObject<List<WwiseAudioMappingItem>>(mappingFile.text);
                if (WwiseAudioMappingItems.Count != readList.Count) {
                    WwiseAudioMappingItems = readList;
                }
                AudioGameKit.GenerateEnumFromJson(WwiseAudioMappingItems);
            }
        }
        
        public string GetWwiseEventName(EllenWwiseEvent wwiseEvent) {
            if (!Enum.IsDefined(typeof(EllenWwiseEvent), wwiseEvent)) {
                return "Unknown";
            }
            return WwiseAudioMappingItems[(int) wwiseEvent].WwiseEventName;
        }
    }

    [Serializable]
    public class WwiseAudioMappingItem {
        [VerticalGroup("Wwise 事件说明"), HideLabel, OnValueChanged(nameof(OnAnyValueChangeWithoutComplie))]
        public string Description = "这是一个摘要";

        [VerticalGroup("Wwise 事件名称"), HideLabel, OnValueChanged(nameof(OnAnyValueChangeWithComplie))]
        public string WwiseEventName = ("EventName_" + Guid.NewGuid()).Replace("-", "_");

        [VerticalGroup("挂载类型"), HideLabel, OnValueChanged(nameof(OnAnyValueChangeWithoutComplie))]
        public string BindingType = "SE";

        [VerticalGroup("挂载归属"), HideLabel, OnValueChanged(nameof(OnAnyValueChangeWithoutComplie))]
        public string BindingImpl = "Global";

        private Action<bool> mCallExternal;

        private void OnAnyValueChangeWithoutComplie() {
            mCallExternal?.Invoke(false);
        }
        
        private void OnAnyValueChangeWithComplie() {
            mCallExternal?.Invoke(true);
        }
        
        public void RegisterAnyValueChange(Action<bool> call) {
            mCallExternal -= call;
            mCallExternal += call;
        }
    }
}