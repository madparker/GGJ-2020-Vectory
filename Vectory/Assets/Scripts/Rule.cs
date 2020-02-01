using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule
{
    public string name;
    public virtual Vector2[] GetPositions(Vector2 startPos)
    {
        return null;
    }
}

public class PlayerRule
{
    public virtual Vector2 Step(Vector2 startPos)
    {
        return Vector2.zero;
    }
}

public class MoveRightRule : Rule
{
    public override Vector2[] GetPositions(Vector2 startPos)
    {
        var stepNum = GameGod.stepNum;
        var posArr = new Vector2[stepNum];
        for (int i = 0; i < stepNum; i++)
        {
            posArr[i] = startPos;
            startPos += Vector2.right;
        }
        return posArr;
    }
}