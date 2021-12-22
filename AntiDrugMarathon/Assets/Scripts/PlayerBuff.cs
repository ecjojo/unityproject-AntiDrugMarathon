using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuff : MonoBehaviour
{
    public int DeaddrugV=3;

    public static PlayerBuff instance;
    public List<GameObject> BuffIcon;

    //Tip
    public GameObject TipPanel;
    public Text TipPanel_DrugName;
    public Text TipPanel_BuffName, TipPanel_BuffName2;
    public Text TipPanel_BuffDes, TipPanel_BuffDes2;

    int drug01stateCount, drug02stateCount, drug03stateCount, drug04stateCount, drug05stateCount;
    public int PlayerCurStateCount;

    //Addicted State
    public bool drug01Addicted,drug02Addicted,drug03Addicted,drug04Addicted,drug05Addicted;//F3 致幻型

    //BuffVFX
    //F1 亢奮: 速度
    public GameObject RoadFX; 
    //Add Speed 
    //F2 模糊: 畫面
    public GameObject FlakeoutFX;
    //- Speed 
    //F3 致幻: 肉體分離
    public GameObject DissociateFX; 
    public GameObject Seye;//1
    public GameObject Redeye;//5

    public GameObject Deading;

    int F1State, F2State, F3State, F4State, F5State;

    void Start()
    {
        instance = this;

        //Reset public Value
        drug01stateCount = 0;
        drug02stateCount = 0;
        drug03stateCount = 0;
        drug04stateCount = 0;
        drug05stateCount = 0;

        ReDisplayIcon();
    }

    void CheckisDeading()
    {
        if(drug01stateCount == DeaddrugV-1 && drug01Addicted ||
            drug02stateCount == DeaddrugV - 1 && drug02Addicted ||
            drug03stateCount == DeaddrugV - 1 && drug03Addicted ||
            drug04stateCount == DeaddrugV - 1 && drug04Addicted ||
            drug05stateCount == DeaddrugV - 1 && drug05Addicted)
        {
            Deading.SetActive(true);
        }
    }

    void ReDisplayIcon()
    {
        for (int i = 0; i < BuffIcon.Count; i++)
        {
            BuffIcon[i].SetActive(false);
        }
    }

    public void AddDisplayIcon(int State, string drugName, int drugType)
    {
        BuffIcon[State - 1].GetComponent<BuffIconController>().buffName.text = drugName;
        BuffIcon[State - 1].GetComponent<BuffIconController>().DisplaySet(drugType);
        BuffIcon[State - 1].SetActive(true);
    }

    public void EndDisplayIcon(int State)
    {
        for (int i = State; i < PlayerCurStateCount; i++)
        {
            BuffIcon[i].GetComponent<BuffIconController>().buffName.text = BuffIcon[i + 1].GetComponent<BuffIconController>().buffName.text;
            BuffIcon[i].GetComponent<BuffIconController>().drugType = BuffIcon[i + 1].GetComponent<BuffIconController>().drugType;
        }

        if(PlayerCurStateCount>=1)
        {
            PlayerCurStateCount -= 1;
        }
        if (BuffIcon[PlayerCurStateCount]!=null)
        {
            BuffIcon[PlayerCurStateCount].SetActive(false);
        }
    }

    #region Durg01
    //抑制型毒品//安神、止痛、忘憂、去除緊張、感覺鬆弛欣快。F2
    //致幻型毒品//令人產生幻覺，覺得靈魂出竅，與肉體分離；自我境界消失，感覺與宇宙融合。F3
    //手術用麻醉劑//有飄浮及欣快感，產生幻覺以及輕微的解離感
    //長期使用導致記憶力衰退、肌肉功能受損、上癮、心臟受損、失去知覺、幻覺、尿頻及小便失禁、壞腦
    //心癮不明顯 身癮不明顯 耐藥性可建立
    //視覺扭曲，失去痛楚感覺，體重下降，筋疲力竭，被迫害的感覺，記憶力衰退。
    //服用高劑量會出現呼吸緩慢，抽筋，出現攻擊性行為甚至昏迷。
    //高度迷茫，妄想，驚惶，有侵略性，被動。
    public void Durg01()
    {
        CheckisDeading();
        drug01stateCount++;

        if (drug01stateCount > DeaddrugV && drug01Addicted)
        {
            PlayerController.instance.LoseText.text = "服食過量氯胺酮導致死亡";
            PlayerController.instance.playerDead = true;
        }

        TipPanel_BuffName.text = "氯胺酮成癮效果";
        TipPanel_BuffDes.text = "氯胺酮成癮";
        if(!drug01Addicted)
        {
            drug01Addicted = true;
            Invoke("Drug01AddictedOff", 30);
        }
        else
        {
            CancelInvoke("Drug01AddictedOff");
            Invoke("Drug01AddictedOff", 30);
            SpawnManager.instance.itemcount++;
        }
 
        TipPanel_BuffName2.text = "致幻感效果";
        TipPanel_BuffDes2.text = "服用氯胺酮會產生飄浮及欣快感，產生幻覺以及輕微的解離感";
        if (F1State == 0)
        {
            //
            if(!drug05Addicted)
            {
                Seye.SetActive(true);
            }
            DissociateFX.SetActive(true);
            PlayerCurStateCount += 1;
            F1State = PlayerCurStateCount;
            AddDisplayIcon(PlayerCurStateCount, "致幻", 1);
            Invoke("Drug01FXOff", 15);
        }
        else
        {
            CancelInvoke("Drug01FXOff");
            Invoke("Drug01FXOff", 15);
        }

        TipPanel_DrugName.text = "氯胺酮(K仔)危害";
        TipPanel.SetActive(true);
    }

    void Drug01AddictedOff()
    {
        drug01Addicted = false;
    }

    void Drug01FXOff()
    {
        Seye.SetActive(false);
        DissociateFX.SetActive(false);
        EndDisplayIcon(F1State);
        F1State = 0;
    }
    #endregion

    #region Durg02
    //興奮型毒品//精神異常振奮、感覺渾身是勁、充滿信心、思考敏捷、喪失饑餓感覺、不眠不休。F1
    //失眠、激動不安、驚惶及精神錯亂、焦慮及緊張、冰瘡、抑鬱、心臟衰竭、因產生幻覺及被迫害的感覺而引致暴力行為、
    //如服用重劑量，會導致中毒性精神病、抽搐、昏迷、腦出血甚 至死亡
    //心癮強 身癮 比較不強 耐藥性很強
    //中風，腦出血，食慾不振，失眠，血壓高，心悸，妄想，有暴力行為。
    //當這些反應消失，或仍會出現嚴重抑鬱，隨之而來的疲倦，頭痛及失去動力。
    //興奮，焦慮，抑鬱，神志錯亂，暴力行為，停不下來，判斷力差，有幻覺，幻聽及幻像的精神病。

    public void Durg02()
    {
        drug02stateCount++;

        if (drug02stateCount > DeaddrugV && drug02Addicted)
        {
            PlayerController.instance.LoseText.text = "服食過量甲基安非他明導致死亡";
            PlayerController.instance.playerDead = true;
        }

        TipPanel_BuffName.text = "甲基安非他明成癮效果";
        TipPanel_BuffDes.text = "甲基安非他明成癮";
        if (!drug02Addicted)
        {
            drug02Addicted = true;
            Invoke("Drug02AddictedOff", 60);
        }
        else
        {
            CancelInvoke("Drug02AddictedOff");
            Invoke("Drug02AddictedOff", 60);
            SpawnManager.instance.itemcount++;
        }

        //甲基安非他明-焦慮 Buff02_Panic
        TipPanel_BuffName2.text = "亢奮效果";
        TipPanel_BuffDes.text = "服用甲基安非他明會導致失眠、激動不安、驚惶及精神錯亂、焦慮及緊張";
        if (F2State == 0)
        {
            PlayerController.instance.AddSpeeding = true;
            RoadFX.GetComponent<Animator>().Play("Base Layer.RoadFX");
            PlayerCurStateCount += 1;
            F2State = PlayerCurStateCount;
            AddDisplayIcon(PlayerCurStateCount, "亢奮", 2);//ADD speed
            Invoke("Drug02FXOff", 30);
        }
        else
        {
            CancelInvoke("Drug02FXOff");
            Invoke("Drug02FXOff", 30);
        }

        TipPanel_DrugName.text = "甲基安非他明(冰)危害";
        TipPanel.SetActive(true);
    }

    void Drug02AddictedOff()
    {
        drug02Addicted = false;
    }

    void Drug02FXOff()
    {
        PlayerController.instance.AddSpeeding = false;
        RoadFX.GetComponent<Animator>().Play("Base Layer.RoadIdle");
        EndDisplayIcon(F2State);
        F2State = 0;
    }
    #endregion

    #region Durg03
    //抑制型毒品 //安神、止痛、忘憂、去除緊張、感覺鬆弛欣快。F2
    //成癮、昏睡、壓抑呼吸、噁心
    //斷癮症狀 :流眼水、 流鼻涕、 打呵欠、食慾不振、煩躁、震 顫、驚惶、感到寒冷、出汗、 痙攣
    //心癮強 身癮強 耐藥性強
    //營養不良，有受感染危險，肝炎，感染愛滋病，痢疾，破傷風，皮膚膿瘡，血液內有細菌，靜脈閉塞，嘔吐，食慾不振及體重下降，
    //服用過量會出現呼吸緩慢及呼吸量淺，昏迷及死亡。
    //抑鬱，呆滯，身體活動減少，幻覺妄想，情緒不穩，難以集中精神，影響性能力，睡眠失調。

    public void Durg03()
    {
        drug03stateCount++;

        if (drug03stateCount > DeaddrugV && drug03Addicted)
        {
            PlayerController.instance.LoseText.text = "服食過量海洛英導致死亡";
            PlayerController.instance.playerDead = true;
        }

        TipPanel_BuffName.text = "海洛英成癮效果";
        TipPanel_BuffDes.text = "海洛英成癮 : 停止服用海洛英會產生斷戒反應";      
        if (!drug03Addicted)
        {
            drug03Addicted = true;
            AddDisplayIcon(PlayerCurStateCount, "海洛英成癮", 3);
            //Invoke("WithdrawalDurg03", 30);//斷癮症狀
        }
        else
        {
            CancelInvoke("Drug03AddictedOff");
            Invoke("Drug03AddictedOff", 60);
            SpawnManager.instance.itemcount++;
        }

        TipPanel_BuffName2.text = "昏睡效果";
        TipPanel_BuffDes.text = "服用海洛英會導致昏睡、壓抑呼吸感、噁心感";        
        if (F3State == 0)
        {
            FlakeoutFX.SetActive(true);
            PlayerCurStateCount += 1;
            F3State = PlayerCurStateCount;
            AddDisplayIcon(PlayerCurStateCount, "昏睡", 3);
            Invoke("Drug03FXOff", 30);
        }
        else
        {
            CancelInvoke("Drug03FXOff");
            Invoke("Drug03FXOff", 30);
        }

        TipPanel_DrugName.text = "海洛英(白粉)危害";
        TipPanel.SetActive(true);      
    }
    /*
    void WithdrawalDurg03()
    {

    }*/

    void Drug03AddictedOff()
    {
        drug03Addicted = false;
    }

    void Drug03FXOff()
    {
        FlakeoutFX.SetActive(false);
        EndDisplayIcon(F3State);
        F3State = 0;
    }
    #endregion

    #region Durg04
    //興奮型毒品//精神異常振奮、感覺渾身是勁、充滿信心、思考敏捷、喪失饑餓感覺、不眠不休。F1亢奮
    //上癮、呼吸道受損、躁狂、妄想被迫害、影響記憶力、幻覺、精神錯亂、心臟病、鼻腔受損
    //心癮強 身癮強 耐藥性強
    //心跳加速，震顫，頭痛，胸痛，嘔吐，視覺模糊，發燒，飲食失調，體重下降，便秘，肌 肉抽搐，死亡。
    //抑鬱，易怒，激動，精神錯亂的思想，暴力行為，幻覺，情緒起伏大，靜不下來。

    public void Durg04()
    {
        drug04stateCount++;

        if (drug04stateCount > DeaddrugV && drug04Addicted)
        {
            PlayerController.instance.LoseText.text = "服食過量可卡因導致死亡";
            PlayerController.instance.playerDead = true;
        }

        TipPanel_BuffName.text = "可卡因成癮效果"; 
        TipPanel_BuffDes.text = "可卡因成癮";
        if (!drug04Addicted)
        {
            drug04Addicted = true;
            Invoke("Drug04AddictedOff", 60);
        }
        else
        {
            CancelInvoke("Drug04AddictedOff"); 
            Invoke("Drug04AddictedOff", 60);
            SpawnManager.instance.itemcount++;
        }

        //可卡因-幻覺 Buff04_Hallucination
        TipPanel_BuffName2.text = "亢奮效果";
        TipPanel_BuffDes2.text = "服用可卡因會上癮、躁狂、妄想被迫害、影響記憶力、幻覺、精神錯亂、呼吸道受損、鼻腔受損";
        if (F3State == 0)
        {
            PlayerController.instance.AddSpeeding = true;
            RoadFX.GetComponent<Animator>().Play("Base Layer.RoadFX");
            PlayerCurStateCount += 1;
            F4State = PlayerCurStateCount;
            AddDisplayIcon(PlayerCurStateCount, "亢奮", 3);
            Invoke("Drug04FXOff", 30);
        }
        else
        {
            CancelInvoke("Drug04FXOff");
            Invoke("Drug04FXOff", 30);
        }

            TipPanel_DrugName.text = "可卡因危害";
        TipPanel.SetActive(true);
    }

    void Drug04AddictedOff()
    {
        drug04Addicted = false;
    }

    void Drug04FXOff()
    {
        PlayerController.instance.AddSpeeding = false;
        RoadFX.GetComponent<Animator>().Play("Base Layer.RoadIdle");
        EndDisplayIcon(F4State);
        F4State = 0;
    }
    #endregion

    #region Durg05
    //致幻型毒品//令人產生幻覺，覺得靈魂出竅，與肉體分離；自我境界消失，感覺與宇宙融合。F3
    //幻覺、協調障礙、上癮、增加患癌機會、集中力減弱，記憶力及判斷力受損、抑鬱及對別人極度懷疑、容易激動及脾氣暴躁、呼吸系統疾病、長期吸食會有較大傾向嘗試其他毒品
    //心癮很強 身癮不明顯 耐藥性不明顯
    //破壞呼吸系統，心跳加速，食慾增加，口乾，眼睛變紅，瞳孔放大，有大麻的氣味，身體抖顫，欠缺志向。
    //無積極性，易怒，對時間及空間的感覺扭曲，幻覺，性格轉變，不能自制重複動作，即使停止服用一段時間仍會閃過那些反應。

    public void Durg05()
    {
        drug05stateCount++;

        if (drug05stateCount > DeaddrugV && drug05Addicted)
        {
            PlayerController.instance.LoseText.text = "服食過量大麻導致死亡";
            PlayerController.instance.playerDead = true;
        }

        if (drug05stateCount == 1)
        {

            TipPanel_BuffName.text = "出現其他毒品";
            TipPanel_BuffDes.text = "服用大麻後較大傾向嘗試其他毒品";
            GameController.instance.onlySpawnWeed = false;
        }
        else if (drug05stateCount > 1)
        {
            TipPanel_BuffName.text = "大麻成癮效果";
            TipPanel_BuffDes.text = "大麻成癮";
            drug05Addicted = true;
        }

        TipPanel_BuffName2.text = "致幻效果";
        TipPanel_BuffDes2.text = "服用大麻會產生幻覺及協調障礙、集中力減弱，記憶力及判斷力受損";

            if (F5State == 0)
            {
                if (!drug01Addicted)
                {
                    Redeye.SetActive(true);
                }
                DissociateFX.SetActive(true);
                PlayerCurStateCount += 1;
                F5State = PlayerCurStateCount;
                AddDisplayIcon(PlayerCurStateCount, "致幻", 5);
                Invoke("Drug05FXOff", 15);
            }
            else
            {
                CancelInvoke("Drug05FXOff");
                Invoke("Drug05FXOff", 15);
            }

            if (!drug05Addicted)
            {
                drug05Addicted = true;
                Invoke("Drug05AddictedOff", 20);
            }
            else
            {
                CancelInvoke("Drug05AddictedOff");
                Invoke("Drug04AddictedOff", 20);
                SpawnManager.instance.itemcount++;
            }

        TipPanel_DrugName.text = "大麻危害";
        TipPanel.SetActive(true);
    }

    void Drug05AddictedOff()
    {
        drug05Addicted = false;
    }

    void Drug05FXOff()
    {
        Redeye.SetActive(false);
        DissociateFX.SetActive(false);
        EndDisplayIcon(F5State);
        F5State = 0;
    }
    #endregion
}

