using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public AudioSource buttonClickAudio;

    public bool isGamePlaying = true;
    public bool onlySpawnWeed = true;

    public void Start()
    {
        instance = this;
    }

    public void Button_BackTitle()
    {
        buttonClickAudio.Play();
        SceneManager.LoadScene("TitleScene");     
    }

    public void Button_Replay()
    {
        buttonClickAudio.Play();
        SceneManager.LoadScene("GameScene");
    }

    public void Button_GameStart()
    {
        buttonClickAudio.Play();
        isGamePlaying = true;
        PlayerController.instance.CharAnim.GetComponent<Animator>().Play("Base Layer.run");
        PlayerController.instance.Walking.Play();
    }

}
