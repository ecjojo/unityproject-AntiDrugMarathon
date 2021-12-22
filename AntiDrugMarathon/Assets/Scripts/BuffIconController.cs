using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconController : MonoBehaviour
{
    public Text buffName;
    public int drugType;

    public void DisplaySet(int drugtype)
    {
        drugType = drugtype;
        switch (drugtype)
        {
            case 0:
                GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                return;
            case 1:
                GetComponent<Image>().color =new Color32(255, 230, 255, 255);//Pink
                return;
            case 2:
                GetComponent<Image>().color = new Color32(230, 255, 225, 255);//Blue
                return;
            case 3:
                GetComponent<Image>().color = new Color32(255, 230, 230, 255);//Red
                return;
            case 4:
                GetComponent<Image>().color = new Color32(255, 255, 230, 255);//Yellow
                return;
            case 5:
                GetComponent<Image>().color = new Color32(230, 255, 230, 255);//Green
                return;
        }
    }
}
