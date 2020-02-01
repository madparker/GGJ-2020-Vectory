using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGod : MonoBehaviour {

    public Transform ball;

    public Rule[] ruleList;

    public Vector2[] posArr;

    public static GameGod me;
    public void Awake() {
        if (me != null) {
            Debug.LogError("TOO MANY GODS THEY MUST FIGHT");
            Destroy(gameObject);
            return;
        }
        me = this;
    }

    void Start() {
        ruleList = new Rule[1]{new MoveRightRule()};
    }

    void Update() {
        
    }
    


}


public class Rule {
    public string name;
    public virtual Vector2[] GetPositions (Vector2 startPos) {
        return null;
    }
}

public class MoveRightRule : Rule
{
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


