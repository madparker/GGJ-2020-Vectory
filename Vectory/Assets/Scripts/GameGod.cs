using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameGod : MonoBehaviour {



    public Transform ball;

    public Rule[] ruleArr;
    public PlayerRule[] playerRuleArr;

    public int ruleIndex = 0;

    public static GameGod me;

    public static int stepNum = 10;

    public bool hasLost, hasWon;
    public GameObject lostText, winText;
    public Vector2[] correctPositions;

    int stepIndex = 0;

    bool testing;
    float testTimer; 
    public float testRate = 0.1f;

    public GameObject arrowHead;

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

        arrowHead.SetActive(false);
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
        if (hasWon || hasLost)
            return;
        var pos = playerRuleArr[ruleIndex].Step(ball.transform.position);
        ball.transform.position = pos;
        stepIndex++;
        if (stepIndex >= stepNum) {
            if (!hasLost)
            {
                winText.SetActive(true);
                hasWon = true;
                testing = false;

                Invoke("NextLevel", 0.1f);

                //NextLevel();

            }
        } else if (pos != correctPositions[stepIndex]) {
            hasLost = true;
            lostText.SetActive(true);
            lostText.GetComponent<TextMeshPro>().text = "Error in Step " + stepIndex + "\n" +
                "You were off by " + (pos - correctPositions[stepIndex]);
            arrowHead.SetActive(true);
            arrowHead.transform.position = correctPositions[stepIndex];

            Vector2 dir = (correctPositions[stepIndex]-pos).normalized;
            arrowHead.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            //arrowHead.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x));

            //arrowHead.transform.Rotate(Vector3.forward, Vector2.Angle(pos, correctPositions[stepIndex]));
        }

    }
    public void InitLevel(){
        ball.transform.position = Vector2.zero;
        correctPositions = ruleArr[ruleIndex].GetPositions(ball.transform.position);
        winText.SetActive(false);
        lostText.SetActive(false);
        VisualizeRule.me.InitLevel();
        stepIndex = 0;
        hasWon = false;
        hasLost = false;
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


