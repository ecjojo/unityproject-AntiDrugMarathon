using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gDefine
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    Rigidbody rb;

    LevelData levelData;
    public AudioSource jumpsound; public AudioSource movesound; public AudioSource badbadsound;

    public SpawnManager spawnManager;
    public GameObject CharAnim;

    //Movement
    Vector2 m_screenPos = new Vector2();
    Vector3 targetPosition;
    float targetX;
    public float curSpeed;
    public float moveSpeed = 10f;
    public Vector3 jump;
    public float jumpForce = 1f;
    bool isGrounded;
    public Text UI_DisplaySpeed;
    public AudioSource Walking;
    public bool AddSpeeding;
    int AddSpeedState;

    //Game Main
    public float runCount; //<--------------------check Win
    public Text runCount_UI;
    public GameObject ResultPanel;
    public Text runResult_UI;
    public GameObject heartFX;

    public bool playerDead = false;

    //Result Time
    float gameTimer;
    int gameTimer_Minute;//分
    int gameTimer_Second;
    public Text ResultTimer;
    public Text LoseText;

    public GameObject WinPanel;
    public GameObject LosePanel;

    float WinTarget;
    public Text WinTargetUI;

    public List<string> HeartTip;
    public Text HeartTipUI;   

    public void Awake()
    {
        switch (LevelData.curLevel) //Controller Level
        {
            case 0:
                moveSpeed = 15;
                WinTarget = 10;
                WinTargetUI.text = "10";
                return;
            case 1:
                moveSpeed = 20;
                WinTarget = 26;
                WinTargetUI.text = "26";
                return;
            case 2:
                moveSpeed = 25;
                WinTarget = 42;
                WinTargetUI.text = "42";
                return;
        }

        targetPosition = new Vector3(0, 2, 0);
        runCount = 0; runCount_UI.text = runCount + "公里";
        curSpeed = moveSpeed;
    }

    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();

        playerDead = false;
        AddingTip();
    }

    void AddingTip()
    {
        //
        HeartTip.Add("管有危險藥物最高罰款$1,000,000及監禁7年");
        HeartTip.Add("吸食、吸服、服食或注射危險藥物最高罰款$1,000,000及監禁7年");
        HeartTip.Add("販運毒品最高罰款$5,000,000及終身監禁");
        HeartTip.Add("製造毒品最高罰款$5,000,000及終身監禁");
        HeartTip.Add("藏有吸毒工具最高罰款10,000及監禁3年");
        HeartTip.Add("長期使用氯胺酮(K仔)會導致記憶力衰退");
        HeartTip.Add("長期使用氯胺酮(K仔)會導致肌肉功能受損");
        HeartTip.Add("長期使用氯胺酮(K仔)會導致心臟受損");
        HeartTip.Add("長期使用氯胺酮(K仔)會導致尿頻及小便失禁");
        HeartTip.Add("長期使用氯胺酮(K仔)會導致壞腦");
        HeartTip.Add("甲基安非他明(冰)會導致心臟衰竭");
        HeartTip.Add("甲基安非他明(冰)會導致被迫害的感覺而引致暴力行為");
        HeartTip.Add("甲基安非他明(冰)會導致中毒性精神病");
        HeartTip.Add("甲基安非他明(冰)會導致腦出血甚至死亡");
        HeartTip.Add("甲基安非他明(冰)會導致冰瘡");
        HeartTip.Add("海洛英(白粉)斷癮症狀會導致痙攣");
        HeartTip.Add("可卡因會導致鼻腔及呼吸道受損");
        HeartTip.Add("可卡因會影響記憶力");
        HeartTip.Add("可卡因會導致精神錯亂、妄想被迫害感");
        HeartTip.Add("可卡因會導致心臟病");
        HeartTip.Add("大麻會導致集中力減弱");
        HeartTip.Add("大麻會導致記憶力及判斷力受損");
        HeartTip.Add("大麻會導致抑鬱");
        HeartTip.Add("使用大麻會導致對別人產生極度懷疑感");
        HeartTip.Add("大麻會導致情緒容易激動及脾氣暴躁");
        HeartTip.Add("大麻會導致呼吸系統疾病");
        HeartTip.Add("長期吸食大麻會有較大傾向嘗試其他毒品");
        HeartTip.Add("濫用咳水會導致腦部受損");
        HeartTip.Add("恰特草(阿拉伯茶)會刺激中樞神經、導致妄想及思緒混亂");
        HeartTip.Add("恰特草(阿拉伯茶)會導致心血管疾病及死亡");
        HeartTip.Add("毒郵票(LSD)會導致心跳加速、抽筋及死亡");
        HeartTip.Add("火狐狸(5-MeO-DIPT)會使視覺及聽覺扭曲、嘔吐腹瀉及導致死亡");
        HeartTip.Add("頭髮樣本可檢驗3個月內服食的毒品");
        //
        HeartTip.Add("甘地 : 力量不是來自肉體的力量，而是源自於不屈不撓的意志。");
        HeartTip.Add("愛因斯坦 : 人生就像騎腳踏車，為了保持平衡，你必須一直前進。");
        HeartTip.Add("卓別林 : 如果你總是低著頭，那麼你永遠無法看見彩虹。");
        HeartTip.Add("馬丁路德金恩 : 我們接受有限的失望，但絕不能失去無限的希望。");
        HeartTip.Add("亞里斯多德 : 我們每天做什麼，就會成為什麼樣的人。");
        HeartTip.Add("愛默生 : 你每生氣一分鐘，就失去六十秒的快樂。");
        HeartTip.Add("柏拉圖 : 成功的唯一秘訣，堅持最後一分鐘");
        HeartTip.Add("莎士比亞 : 黑夜無論怎樣悠長，白晝總會到來。");
        //
        Debug.Log("Added " + HeartTip.Count + " HeartTip");
        HeartTipUI.text = "";
    }

    void Update()
    {
        if (!playerDead)
        {
            if(GameController.instance.isGamePlaying)
            {
                InputControl();
                AddMoveSpeed();
                PlayerMovement();

                gameTimer += Time.deltaTime;
                gameTimer_Second = (int)gameTimer;
                if (gameTimer_Second > 59.0f)
                {
                    gameTimer_Second = (int)(gameTimer - gameTimer_Minute * 60);
                }
                gameTimer_Minute = (int)(gameTimer / 60);

                //CharAnim.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
        }
        else
        {
            Gameover();
        }

        //How to Over

        if (runCount >= WinTarget)
        {
            Gameover();
        }
    }

#region Input
    void InputControl()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        MobileInput ();
#else
        DeskopInput();
#endif
    }

    void DeskopInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            gDefine.Direction mDirection = HandDirection(m_screenPos, pos);
            Debug.Log("mDirection: " + mDirection.ToString());
        }
    }

    void MobileInput()
    {
        if (Input.touchCount <= 0)
            return;

        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Debug.Log("Began");
                m_screenPos = Input.touches[0].position;

            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                Debug.Log("Moved");
                //MoveCamera
                //Camera.main.transform.Translate (new Vector3 (-Input.touches [0].deltaPosition.x * Time.deltaTime, -Input.touches [0].deltaPosition.y * Time.deltaTime, 0));
            }

            if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Debug.Log("Ended");
                Vector2 pos = Input.touches[0].position;

                gDefine.Direction mDirection = HandDirection(m_screenPos, pos);
                Debug.Log("mDirection: " + mDirection.ToString());
            }
        }
    }
    #endregion

#region Movement
    gDefine.Direction HandDirection(Vector2 StartPos, Vector2 EndPos)
    {
        gDefine.Direction mDirection;

        if (Mathf.Abs(StartPos.x - EndPos.x) > Mathf.Abs(StartPos.y - EndPos.y))
        {
            targetX = transform.position.x;

            if (StartPos.x > EndPos.x)
            {
                movesound.Play();
                mDirection = gDefine.Direction.Left;
                targetX -= 3;
                if (isGrounded)
                {
                    //rb.AddForce(jump * 0.5f, ForceMode.Impulse);
                    //CharAnim.GetComponent<Animator>().Play("Base Layer.jump-up");
                }

            }
            else
            {
                movesound.Play();
                mDirection = gDefine.Direction.Right;
                targetX += 3;
                if (isGrounded)
                {
                    //rb.AddForce(jump * 0.5f, ForceMode.Impulse);
                    //CharAnim.GetComponent<Animator>().Play("Base Layer.jump-up");
                }
            }
        }
        else
        {
            if (m_screenPos.y > EndPos.y)
            {
                mDirection = gDefine.Direction.Down;
                
                movesound.Play();
                //Add Speed
                if (AddSpeedState == 0)
                {
                    AddSpeeding = true;
                    AddSpeedState = PlayerBuff.instance.PlayerCurStateCount;
                    PlayerBuff.instance.PlayerCurStateCount += 1;
                    PlayerBuff.instance.AddDisplayIcon(PlayerBuff.instance.PlayerCurStateCount, "加速", 0);
                    Invoke("ResetSpeed", 5);
                }
                else
                {
                    CancelInvoke("ResetSpeed");
                    Invoke("ResetSpeed", 5);
                }

                /*
                
                //CharAnim.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, new Vector3(targetX, 1, transform.position.z), 1);
                */
            }
            else
            {
                mDirection = gDefine.Direction.Up;
                if (isGrounded)
                {
                    jumpsound.Play();
                    isGrounded = false;
                    CharAnim.GetComponent<Animator>().Play("Base Layer.jump-up");
                    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                }
            }
        }
        return mDirection;
    }

    void ResetSpeed()
    {
        PlayerBuff.instance.EndDisplayIcon(AddSpeedState);Debug.Log("End Add Speed");
        AddSpeeding = false;
        AddSpeedState = 0;
    }

    void PlayerMovement()
    {

        if (AddSpeeding)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * curSpeed*2);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
        }

        transform.position = Vector3.Lerp(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), 0.5f);

        if (transform.position.x > 3)
        {
            transform.position = new Vector3(3, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -3)
        {
            transform.position = new Vector3(-3, transform.position.y, transform.position.z);
        }
        else if (transform.position.y > 1.5)
        {
            isGrounded = false;
        }
    }

    void AddMoveSpeed()
    {
        if (AddSpeeding)
        {
            UI_DisplaySpeed.text = "當前速度 : " + (curSpeed / moveSpeed).ToString("0.00") + "x2";
        }
        else
        {
            curSpeed = moveSpeed + (Time.time / 100);
            UI_DisplaySpeed.text = "當前速度 : " + (curSpeed / moveSpeed).ToString("0.00") + "x";
        }
        runCount = (curSpeed- moveSpeed) * 2;
        runCount_UI.text = runCount.ToString("0.0") + "公里";
        
    }
    #endregion
    void OnCollisionStay()
    {
        
            isGrounded = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Road")
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (other.tag == "SpawnRaodTrigger")
        {
            spawnManager.SpawnTrggerEntered();
            Debug.Log("Spawn Road");
        }

        if (other.tag == "Love")
        {
            Instantiate(heartFX, transform.position, Quaternion.identity);
            HeartTipUI.text = HeartTip[Random.Range(0, HeartTip.Count)];

            //playerHp += 1;

            ItemSpawner.instance.itemPool.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Drug")
        {
#if (!UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID))
                Handheld.Vibrate();
#endif
            HeartTipUI.text = "";
            badbadsound.Play();
            Walking.Stop();
            GameController.instance.isGamePlaying = false;
            CharAnim.GetComponent<Animator>().Play("Base Layer.idle");

            switch (other.GetComponent<DrugController>().drugtype)
            {
                case 1:
                    PlayerBuff.instance.Durg01();
                    return;
                case 2:
                    PlayerBuff.instance.Durg02();
                    return;
                case 3:
                    PlayerBuff.instance.Durg03();
                    return;
                case 4:
                    PlayerBuff.instance.Durg04();
                    return;
                case 5:
                    PlayerBuff.instance.Durg05();
                    return;
            }

            ItemSpawner.instance.itemPool.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }

    public void Gameover()
    {
        Debug.Log("GameEnd");
        Walking.Stop();

        CharAnim.SetActive(false);
        PlayerBuff.instance.RoadFX.SetActive(false);
        PlayerBuff.instance.FlakeoutFX.SetActive(false);
        PlayerBuff.instance.DissociateFX.SetActive(false);
        PlayerBuff.instance.Seye.SetActive(false);//1
        PlayerBuff.instance.Redeye.SetActive(false);//5
        PlayerBuff.instance.Deading.SetActive(false);

        GameController.instance.isGamePlaying = false;
        runResult_UI.text = (int)runCount + "";
        ResultTimer.text = "遊戲時間: " + gameTimer_Minute + "分" + gameTimer_Second + "秒";

        if(runCount>= WinTarget)
        {
            WinPanel.SetActive(true);
        }
        else
        { 
            LosePanel.SetActive(true);
        }
        ResultPanel.SetActive(true);
    }
}