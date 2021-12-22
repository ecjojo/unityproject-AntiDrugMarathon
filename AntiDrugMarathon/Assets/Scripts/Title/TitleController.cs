using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//[System.Serializable]
public class LevelData : MonoBehaviour
{
    public static int curLevel=0;
    /*
    public enum CurLevelSet
    {
        Easy, Normal, Difficult
    }
    */
}

public class TitleController : MonoBehaviour
{
    public AudioSource buttonClickAudio;

    public GameObject StartButton;

    public GameObject MainMeunPanel;
    public GameObject LevelPanel;
    public GameObject TutorialPanel;
    public GameObject IntroducePanel;
    public GameObject iPanel;

    public void Button_MainMeun()
    {
        buttonClickAudio.Play();
        StartButton.SetActive(false);
        MainMeunPanel.SetActive(true);
        LevelPanel.SetActive(false);
        TutorialPanel.SetActive(false);
        IntroducePanel.SetActive(false);
        iPanel.SetActive(false);
    }

    public void Button_LevelPanel()
    {
        buttonClickAudio.Play();
        MainMeunPanel.SetActive(false);
        LevelPanel.SetActive(true);
    }

    #region LevelSetup
    public void Button_LevelPanel_EasyGameSet()
    {
        buttonClickAudio.Play();
        //
        Debug.Log("EasyLevel");
        LevelData.curLevel = 0;
        SceneManager.LoadScene("GameScene");
    }

    public void Button_LevelPanel_NormalGameSet()
    {
        buttonClickAudio.Play();
        //
        Debug.Log("NormalLevel");
        LevelData.curLevel = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void Button_LevelPanel_DifficultGameSet()
    {
        buttonClickAudio.Play();
        //
        Debug.Log("DifficulLevel");
        LevelData.curLevel = 2;
        SceneManager.LoadScene("GameScene");
    }
    #endregion

    public void Button_TutorialPanel()
    {
        buttonClickAudio.Play();
        MainMeunPanel.SetActive(false);
        TutorialPanel.SetActive(true);
    }

    public void Button_IntroducePanel()
    {
        buttonClickAudio.Play();
        MainMeunPanel.SetActive(false);
        IntroducePanel.SetActive(true);
        DrugMeun.SetActive(true);
        DrugBook.SetActive(false);
        DrugText01.SetActive(false);
        DrugText02.SetActive(false);
        DrugText03.SetActive(false);
        DrugText04.SetActive(false);
        DrugText05.SetActive(false);
    }
    public GameObject DrugMeun;
    public GameObject DrugBook;
    public GameObject DrugText01,DrugText02,DrugText03, DrugText04, DrugText05;

    #region DrugBook
    public void Button_Drug01()
    {
        buttonClickAudio.Play();
        DrugMeun.SetActive(false);
        DrugBook.SetActive(true);
        DrugText01.SetActive(true);
    }

    public void Button_Drug02()
    {
        buttonClickAudio.Play();
        DrugMeun.SetActive(false);
        DrugBook.SetActive(true);
        DrugText02.SetActive(true);
    }

    public void Button_Drug03()
    {
        buttonClickAudio.Play();
        DrugMeun.SetActive(false);
        DrugBook.SetActive(true);
        DrugText03.SetActive(true);
    }

    public void Button_Drug04()
    {
        buttonClickAudio.Play();
        DrugMeun.SetActive(false);
        DrugBook.SetActive(true);
        DrugText04.SetActive(true);
    }

    public void Button_Drug05()
    {
        buttonClickAudio.Play();
        DrugMeun.SetActive(false);
        DrugBook.SetActive(true);
        DrugText05.SetActive(true);
    }

    #endregion

    public void Button_iPanel()
    {
        buttonClickAudio.Play();
        MainMeunPanel.SetActive(false);
        iPanel.SetActive(true);
    }

    public void Button_QuitGame()
    {
        buttonClickAudio.Play();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
    Application.Quit();
    #endif
    }
}
