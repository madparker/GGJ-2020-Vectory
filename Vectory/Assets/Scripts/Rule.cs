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
    public virtual Vector2 Step(Vector2 currentPos)
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

public class MoveDiagonalUpLeft : Rule
{
    public override Vector2[] GetPositions(Vector2 startPos)
    {
        var stepNum = GameGod.stepNum;
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
        var stepNum = GameGod.stepNum;
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