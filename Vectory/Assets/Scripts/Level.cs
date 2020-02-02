using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level {
    public List<string> floatLabels = new List<string>();
    public List<string> vectorLabels = new List<string>();

    public List<float> floatInputs = new List<float>();
    public List<Vector2> vectorInputs = new List<Vector2>();

    public ShapeData[] shapes;

    public Rule playerRule;
    public Rule designerRule;

    public int numSteps = 5;

    public abstract void Init();



    public void Simulate() {
        for (int i = 0; i < numSteps; i++) {
            Step(i);
        }
        Reset();
    }

    void Reset() {
        for (int i = 0; i < shapes.Length; i++) {
            shapes[i].transform.position = shapes[i].startPos;
        }
    }
    public void CleanUp() {
        for (int i = 0; i < shapes.Length; i++) {
            GameObject.Destroy(shapes[i].transform.gameObject);
        }
    }

    //TODO: Check for other shapes
    //TODO: pass inputs correctly
    //TODO: return more info

    public bool Step(int stepNum, bool playerTime = false) {
        var success = true;
        for (int i = 0; i < shapes.Length; i++) {
            var shape = shapes[i];
            shape.transform.position = playerTime && shape.playerOwned ? PlayerBallMove(shape) : DesignerBallMove(shape);
            if (!playerTime) {
                shape.posArr[stepNum] = shape.transform.position;
            } else {
                if (shape.playerOwned && (Vector2)shape.transform.position != shape.posArr[stepNum - 1]) {
                    shape.failed = true;
                    success = false;
                }
                
            }
        }
        return success;
    }

    public abstract Vector2 PlayerBallMove(ShapeData shape);
    public abstract Vector2 DesignerBallMove(ShapeData shape);
}

public struct ShapeData{

    public enum shapeType
    {
        ball, rec, tri, length
    }
    
    public shapeType type;
    public Vector2 startPos;
    public Transform transform;
    public Vector2[] posArr;
    public bool playerOwned, failed;

    public ShapeData(shapeType t, Vector2 sPos, Transform trans, int numSteps, bool pOwned=true) {
        type = t;
        posArr = new Vector2[numSteps];
        startPos = sPos;
        transform = trans;
        playerOwned = pOwned;
        failed = false;
    }

}

public class Level1 : Level {

    Transform[] shapePrefabs;
    public override void Init()
    {
        shapePrefabs = GameGod.me.shapes;
        designerRule = new MoveRightOne();
        playerRule = new Lv1();

        shapes = new ShapeData[1];

        var bal = ShapeData.shapeType.ball;
        shapes[0] = new ShapeData(bal, Vector2.zero, GameObject.Instantiate(shapePrefabs[(int)bal]), numSteps);

        Simulate();
    }

    public override Vector2 PlayerBallMove(ShapeData shape){
        return playerRule.BallMove(shape.transform.position);
    }

    public override Vector2 DesignerBallMove(ShapeData shape){
        return designerRule.BallMove(shape.transform.position);
    }
}

public class Level2 : Level
{

    public override void Init()
    {
        designerRule = new MoveDiagonalUpLeft();
        playerRule = new Lv2();

        shapes = new ShapeData[1];

        var bal = ShapeData.shapeType.ball;
        shapes[0] = new ShapeData(bal, Vector2.zero, GameObject.Instantiate(GameGod.me.shapes[(int)bal]), numSteps);

        Simulate();
    }

    public override Vector2 PlayerBallMove(ShapeData shape)
    {
        return playerRule.BallMove(shape.transform.position);
    }

    public override Vector2 DesignerBallMove(ShapeData shape)
    {
        return designerRule.BallMove(shape.transform.position);
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

    public override Vector2 PlayerBallMove(ShapeData shape)
    {
        return playerRule.BallMove(vectorInputs[0]);
    }

    public override Vector2 DesignerBallMove(ShapeData shape)
    {
        return designerRule.BallMove(vectorInputs[0]);
    }
}