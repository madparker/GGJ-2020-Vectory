using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level
{
    public enum inputKeys
    {
        startPos = 0
    }

    public const string DICT_START_POS = "StartPos";

    public List<float> floatInputs;
    public List<Vector2> vectorInputs;

    public ShapeData[] shapes;

    public Rule playerRule;
    public Rule designerRule;

    public int numSteps = 5;

    public abstract void Init();

    public Vector2[] GetPositions(Vector2 position){
        Vector2[] result = new Vector2[numSteps];

        result[0] = position;

        for (int i = 1; i < result.Length; i++){
            result[i] = Step(result[i - 1], false);
        }

        return result;
    }

    public Vector2 Step(Vector3 pos, bool playerTime)
    {
        Vector2 ballPosition = playerRule.BallMove(pos);//vectorInputs[(int)inputKeys.startPos]);

        if (playerTime && ballPosition == Vector2.negativeInfinity)
        {
            ballPosition = designerRule.BallMove(pos);
        }

        return ballPosition;
    }
}

public struct ShapeData{

    public enum shapeType
    {
        ball, rec, tri, length
    }

    public shapeType type;
    public Vector2 startPos;
    public Transform transform;
}

public class Level1 : Level {

    public override void Init(){
        designerRule = new MoveRightOne();
        playerRule = new Lv1();

        ShapeData sd = new ShapeData();
        sd.type = ShapeData.shapeType.ball;
        sd.startPos = Vector2.zero;

        shapes = new ShapeData[1] { sd};
    }
}

public class Level2 : Level
{

    public override void Init()
    {
        designerRule = new MoveDiagonalUpLeft();
        playerRule = new Lv2();

        ShapeData sd = new ShapeData();
        sd.type = ShapeData.shapeType.ball;
        sd.startPos = Vector2.zero;

        shapes = new ShapeData[1] { sd };
    }
}

public class Level3 : Level
{

    public override void Init()
    {
        designerRule = new MoveDiagonalUpLeftOneUnit();
        playerRule = new Lv3();

        ShapeData sd = new ShapeData();
        sd.type = ShapeData.shapeType.ball;
        sd.startPos = Vector2.zero;

        shapes = new ShapeData[1] { sd };
    }
}