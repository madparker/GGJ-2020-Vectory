using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGod : MonoBehaviour {



    public Transform ball;

    public Rule[] ruleArr;
    public PlayerRule[] playerRuleArr;

    public int ruleIndex = 0;

    public static GameGod me;

    public static int stepNum = 10;

    public bool hasLost;
    public GameObject lostText, winText;
    public Vector2[] correctPositions;

    int stepIndex = 0;

    bool testing;
    float testTimer; 
    public float testRate = 0.1f;

    public void Awake() {
        if (me != null) {
            Debug.LogError("TOO MANY GODS THEY MUST FIGHT");
            Destroy(gameObject);
            return;
        }
        me = this;
        ruleArr = new Rule[3] 
            { new MoveRightRule(), new MoveDiagonalUpLeft(), new MoveDiagonalUpLeftOnUnit()};
        playerRuleArr = new PlayerRule[3] 
            { new Lvl1(), new Lv2(), new Lv3() };
        InitLevel();

    }

    void Start() {
    }

    void Update() {
        if (testing) {
            testTimer += Time.deltaTime;
            if (testTimer > testRate) {
                testTimer = 0;
                Step();
            }
        }
    }

    public void StartTest() {
        testing = true;

    }

    public void Step() {
        var pos = playerRuleArr[ruleIndex].Step(ball.transform.position);
        ball.transform.position = pos;
        stepIndex++;
        if (stepIndex >= stepNum) {
            if (!hasLost)
            {
                winText.SetActive(true);
                testing = false;

                Invoke("NextLevel", 0.1f);

                //NextLevel();

            }
        } else if (pos != correctPositions[stepIndex]) {
            hasLost = true;
            lostText.SetActive(true);
        }

    }
    public void InitLevel(){
        ball.transform.position = Vector2.zero;
        correctPositions = ruleArr[ruleIndex].GetPositions(ball.transform.position);
        winText.SetActive(false);
        lostText.SetActive(false);
        VisualizeRule.me.InitLevel();
        stepIndex = 0;
    }

    public void NextLevel(){
        ruleIndex++;
        if (ruleIndex < ruleArr.Length)
        {
            InitLevel();
            testing = true;
        }
    }
    
    public Rule GetCurrentRule(){
        return ruleArr[ruleIndex];
    }

}


