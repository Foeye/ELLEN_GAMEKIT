/**
  * @file :AudioSys
  * @brief :BRIEF
  * @details : Detail of this file
  * @author :
  * @version :0.1
  * @date :2025年1月1日
  * @copyright :Foeye
 */

namespace GameKit.Utils {
    public class AudioSys {
        private static AudioMapping mMappingInst;
        
        public static void Setup() {
            mMappingInst = new AudioMapping();
            mMappingInst.Initialize();
        }
        
        public static string GetWwiseEventName(EllenWwiseEvent wwiseEvent) {
            if (mMappingInst == null) {
                Setup();
            }
            return mMappingInst.GetWwiseEventName(wwiseEvent);
        }
    }
}