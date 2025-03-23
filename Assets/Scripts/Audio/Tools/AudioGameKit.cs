/**
  * @file :AudioGameKit
  * @brief :BRIEF
  * @details : Detail of this file
  * @author :
  * @version :0.1
  * @date :2025年1月1日
  * @copyright :Foeye
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace GameKit.Utils {
    public static class AudioGameKit {
        public static void GenerateEnumFromJson(List<WwiseAudioMappingItem> items) {
            if (items == null) {
                return;
            }

            var enumBuilder = new StringBuilder();
            enumBuilder.AppendLine("namespace GameKit.Utils {");
            enumBuilder.AppendLine("    public enum EllenWwiseEvent {");

            foreach (var item in items) {
                enumBuilder.AppendLine($"        {item.WwiseEventName},");
            }

            enumBuilder.AppendLine("    }");
            enumBuilder.AppendLine("}");

            var enumCode = enumBuilder.ToString();
            var outputFilePath = Path.Combine(Application.dataPath, AudioConst.AudioEnumFilePath);
            File.WriteAllText(outputFilePath, enumCode);
        }
        
        public static void WriteEnumJson(List<WwiseAudioMappingItem> items) {
            var json = JsonConvert.SerializeObject(items, Formatting.Indented);
            var outputFilePath = Path.Combine(Application.dataPath, AudioConst.MappingFilePath);
            File.WriteAllText(outputFilePath, json);
        }
    }
}