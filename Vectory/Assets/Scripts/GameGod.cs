using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGod : MonoBehaviour {

    public Transform ball;

    public Rule[] ruleArr;
    public PlayerRule[] playerRuleArr;

    public int ruleIndex;

    public static GameGod me;

    public static int stepNum = 10;

    public bool hasLost;
    public GameObject lostText, winText;
    public Vector2[] correctPositions;

    int stepIndex = 0;
    public void Awake() {
        if (me != null) {
            Debug.LogError("TOO MANY GODS THEY MUST FIGHT");
            Destroy(gameObject);
            return;
        }
        me = this;

        ruleArr = new Rule[1] { new MoveRightRule() };
        playerRuleArr = new PlayerRule[1] { new Lvl1() };
        ruleIndex = 0;
        correctPositions = ruleArr[ruleIndex].GetPositions(ball.transform.position);
    }

    void Start() {
    }

    void Update() {
        
    }

    public void Step() {
        var pos = playerRuleArr[ruleIndex].Step(ball.transform.position);
        ball.transform.position = pos;
        stepIndex++;
        if (stepIndex >= stepNum) {
            if (!hasLost) winText.SetActive(true);
        } else if (pos != correctPositions[stepIndex]) {
            hasLost = true;
            lostText.SetActive(true);
        }

    }

    
    public Rule GetCurrentRule(){
        return ruleArr[ruleIndex];
    }

}


