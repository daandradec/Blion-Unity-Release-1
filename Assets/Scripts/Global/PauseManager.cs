using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

    private CanvasGroup canvasGroup;
    public GameObject pausePanel;
    public Text pausePanelText;
    private bool pauseFlag = false;

    private void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();    
    }

    public void PauseWithTextParameter(string text)
    {
        this.SetPanelText(text);
        this.Pause();
    }

    private void SetPanelText(string text)
    {
        pausePanelText.text = text;
    }
    
    public void Pause()
    {
        pauseFlag = !pauseFlag;
        pausePanel.gameObject.SetActive(pauseFlag);
        canvasGroup.interactable = !pauseFlag;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
    
    public void Quit()
    {
        #if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
        #else 
        Application.Quit();
        #endif
    }
}