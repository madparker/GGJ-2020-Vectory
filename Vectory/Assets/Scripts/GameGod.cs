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
    }

    void Start() {
    }

    void Update() {
        
    }

    void Step() {

    }

    
    public Rule GetCurrentRule(){
        return ruleArr[ruleIndex];
    }

}


public class Rule {
    public string name;
    public virtual Vector2[] GetPositions (Vector2 startPos) {
        return null;
    }
}

public class PlayerRule {
    public virtual Vector2 Step(Vector2 startPos) {
        return Vector2.zero;
    }
}

public class MoveRightRule : Rule {
    public override Vector2[] GetPositions(Vector2 startPos) {
        var stepNum = 5;
        var posArr = new Vector2[stepNum];
        for (int i = 0; i < stepNum; i++) {
            posArr[i] = startPos;
            startPos += Vector2.right;
        }
        return posArr;
    }
}


