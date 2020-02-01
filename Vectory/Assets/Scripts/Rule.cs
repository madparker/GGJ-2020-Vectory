using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule
{
    public string name;
    public int stepNum = 10;

    public virtual Vector2[] GetPositions(Vector2 startPos)
    {
        return null;
    }
}

public class PlayerRule {
    public virtual Vector2 Step(Vector2 currentPos, Vector2 input) {
        return Vector2.zero;
    }
    public virtual Vector2 Step(Vector2 currentPos) {
        return Step(currentPos, Vector2.zero);
    }
}

public class MoveRight : Rule {
    public override Vector2[] GetPositions(Vector2 startPos)
    {
        var posArr = new Vector2[stepNum];
        for (int i = 0; i < stepNum; i++)
        {
            posArr[i] = startPos;
            startPos += Vector2.right;
        }
        return posArr;
    }
}

public class MoveDiagonalUpLeft : Rule
{
    public override Vector2[] GetPositions(Vector2 startPos)
    {
        var posArr = new Vector2[stepNum];
        for (int i = 0; i < stepNum; i++)
        {
            posArr[i] = startPos;
            startPos += Vector2.left;
            startPos += Vector2.up;
        }
        return posArr;
    }
}


public class MoveDiagonalUpLeftOnUnit : Rule
{
    public override Vector2[] GetPositions(Vector2 startPos)
    {
        var posArr = new Vector2[stepNum];
        for (int i = 0; i < stepNum; i++)
        {
            posArr[i] = startPos;

            Vector2 newDir = new Vector2(-1, 1).normalized;

            startPos += newDir;
        }
        return posArr;
    }
}
public class AccelerateRight : Rule {
    public AccelerateRight() {
        stepNum = 20;
    }

    public override Vector2[] GetPositions(Vector2 startPos) {
        var posArr = new Vector2[stepNum];
        var vel = Vector2.zero;
        for (int i = 0; i < stepNum; i++) {
            posArr[i] = startPos;
            vel += Vector2.right * .1f;
            startPos += vel;
        }
        return posArr;
    }
}