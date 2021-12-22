using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrugController : MonoBehaviour
{
    public GameObject model;
    public Text drugname;

    public List<GameObject> drugmodel;
    public List<Material> drugmaterial;

    public int drugtype;

    public void Start()
    {
        Resettype();
    }

    public void Resettype()
    {
        if(GameController.instance.onlySpawnWeed)
        {
            drugtype = 5;
        }
        else
        {
            /*
            if(PlayerBuff.instance.drug01Addicted)
            {
                drugtype = 1;
            }
            else if(PlayerBuff.instance.drug02Addicted)
            {
                drugtype = 2;
            }
            else if (PlayerBuff.instance.drug03Addicted)
            {
                drugtype = 3;
            }
            else if (PlayerBuff.instance.drug04Addicted)
            {
                drugtype = 4;
            }
            else
            {*/
                drugtype = Random.Range(1, 5);
            //}
            
        }

        model = drugmodel[drugtype-1];
        this.gameObject.GetComponent<Renderer>().material = drugmaterial[drugtype-1];

        switch (drugtype)
        {
            case 1:
                drugname.text = "氯胺酮";
                Debug.Log("氯胺酮");
                return;
            case 2:
                drugname.text = "甲基安非他明";
                Debug.Log("甲基安非他明");
                return;
            case 3:
                drugname.text = "海洛英";
                Debug.Log("海洛英");
                return;
            case 4:
                drugname.text = "可卡因";
                Debug.Log("可卡因");
                return;
            case 5:
                drugname.text = "大麻";
                Debug.Log("大麻");
                return;
        }

    }
}
