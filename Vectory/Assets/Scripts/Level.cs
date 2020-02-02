using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level
{
    public const string DICT_START_POS = "StartPos";

    public List<string> floatLabels = new List<string>();
    public List<string> vectorLabels = new List<string>();

    public List<float> floatInputs = new List<float>();
    public List<Vector2> vectorInputs = new List<Vector2>();

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

    //TODO: Check for other shapes
    //TODO: pass inputs correctly
    //TODO: return more info

    public Vector2 Step(Vector3 pos, bool playerTime)
    {
        Vector2 ballPosition = PlayerBallMove();//vectorInputs[(int)inputKeys.startPos]);

        if (playerTime && ballPosition == Vector2.negativeInfinity)
        {
            ballPosition = DesignerBallMove();
        }

        return ballPosition;
    }

    public abstract Vector2 PlayerBallMove();
    public abstract Vector2 DesignerBallMove();
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

    public override void Init()
    {
        designerRule = new MoveRightOne();
        playerRule = new Lv1();

        ShapeData sd = new ShapeData();
        sd.type = ShapeData.shapeType.ball;
        sd.startPos = Vector2.zero;

        shapes = new ShapeData[1] { sd};
    }

    public override Vector2 PlayerBallMove(){
        return playerRule.BallMove(vectorInputs[0]);
    }

    public override Vector2 DesignerBallMove(){
        return designerRule.BallMove(vectorInputs[0]);
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

    public override Vector2 PlayerBallMove()
    {
        return playerRule.BallMove(vectorInputs[0]);
    }

    public override Vector2 DesignerBallMove()
    {
        return designerRule.BallMove(vectorInputs[0]);
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

    public override Vector2 PlayerBallMove()
    {
        return playerRule.BallMove(vectorInputs[0]);
    }

    public override Vector2 DesignerBallMove()
    {
        return designerRule.BallMove(vectorInputs[0]);
    }
}