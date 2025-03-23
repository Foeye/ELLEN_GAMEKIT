using System.Collections;
using System.Collections.Generic;
using GameKit.Utils;
using Gamekit3D;
using UnityEngine;
using UnityEngine.Playables;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gamekit3D
{
    public class StartUI : MonoBehaviour
    {
        public bool alwaysDisplayMouse;
        public GameObject pauseCanvas;
        public GameObject optionsCanvas;
        public GameObject controlsCanvas;
        public GameObject audioCanvas;

        protected bool m_InPause;
        protected PlayableDirector[] m_Directors;

        void Start()
        {
            if (!alwaysDisplayMouse)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            /*AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Music_Gameplay), gameObject);*/
            m_Directors = FindObjectsOfType<PlayableDirector> ();
        }

        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
        }

        public void ExitPause()
        {
            m_InPause = true;
            SwitchPauseState();
        }

        public void RestartLevel()
        {
            m_InPause = true;
            SwitchPauseState();
            SceneController.RestartZone();
        }

        public void ClickSound(){  //普通ui点击声
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Menu_Buttons_Click_Normal),gameObject);
        }

        public void MouseOverSound() {  //鼠标空转UI声
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Menu_Buttons_Mouse_Over), gameObject);
        }

        public void StartClickSound() {  //ui开始游戏点击声
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Menu_Buttons_Click_Start), gameObject);
        }

        public void ExitClickSound() {  //ui退出游戏点击声
            AkSoundEngine.PostEvent(AudioSys.GetWwiseEventName(EllenWwiseEvent.Menu_Buttons_Click_Exit), gameObject);
        }


        void Update()
        {
            if (PlayerInput.Instance != null && PlayerInput.Instance.Pause)
            {
                SwitchPauseState();
            }
        }

        protected void SwitchPauseState()
        {
            if (m_InPause && Time.timeScale > 0 || !m_InPause && ScreenFader.IsFading)
                return;

            if (!alwaysDisplayMouse)
            {
                Cursor.lockState = m_InPause ? CursorLockMode.Locked : CursorLockMode.None;
                Cursor.visible = !m_InPause;
            }

            for (int i = 0; i < m_Directors.Length; i++)
            {
                if (m_Directors[i].state == PlayState.Playing && !m_InPause)
                {
                    m_Directors[i].Pause ();
                }
                else if(m_Directors[i].state == PlayState.Paused && m_InPause)
                {
                    m_Directors[i].Resume ();
                }
            }
            
            if(!m_InPause)
                CameraShake.Stop ();

            if (m_InPause)
                PlayerInput.Instance.GainControl();
            else
                PlayerInput.Instance.ReleaseControl();

            Time.timeScale = m_InPause ? 1 : 0;

            if (pauseCanvas)
                pauseCanvas.SetActive(!m_InPause);

            if (optionsCanvas)
                optionsCanvas.SetActive(false);

            if (controlsCanvas)
                controlsCanvas.SetActive(false);

            if (audioCanvas)
                audioCanvas.SetActive(false);

            m_InPause = !m_InPause;
        }
    }
}
