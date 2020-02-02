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

    public int numSteps = 10;

    public abstract void Init();



    public void Simulate() {
        SetRules(false);
        for (int i = 0; i < numSteps; i++) {
            Step(i);
        }
        Reset();
        SetRules(true);
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

    public abstract void SetRules(bool playerTime);

    //TODO: Check for other shapes
    //TODO: pass inputs correctly
    //TODO: return more info

    public bool Step(int stepNum, bool playerTime = false) {
        var success = true;
        for (int i = 0; i < shapes.Length; i++) {
            var shape = shapes[i];
            shape.transform.position = BallMove(shape);
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

    public abstract Vector2 BallMove(ShapeData shape);
}

public class ShapeData{

    public enum shapeType
    {
        ball, rec, tri, length
    }
    
    public shapeType type;
    public Vector2 startPos;
    public Transform transform;
    public Vector2[] posArr;
    public bool playerOwned, failed;
    public Rule rule;

    public ShapeData(shapeType t, Vector2 sPos, Transform trans, int numSteps, bool pOwned=true) {
        type = t;
        posArr = new Vector2[numSteps];
        startPos = sPos;
        transform = trans;
        playerOwned = pOwned;
        failed = false;
        transform.position = startPos;
    }

    /*public ShapeData Clone() {
        return new ShapeData(type, startPos, transform, posArr.Length, playerOwned);
    }*/

}

public class Level1 : Level {

    Transform[] shapePrefabs;
    public override void Init()
    {
        shapePrefabs = GameGod.me.shapes;


        shapes = new ShapeData[1];

        var bal = ShapeData.shapeType.ball;
        shapes[0] = new ShapeData(bal, Vector2.zero, GameObject.Instantiate(shapePrefabs[(int)bal]), numSteps);

        Simulate();
    }

    public override void SetRules(bool playerTime) {
        for (int i = 0; i < shapes.Length; i++) {
            if (playerTime && shapes[i].playerOwned)
                shapes[i].rule = new Lv1();
            else
                shapes[i].rule = new MoveRightOne();
        }
    }

    public override Vector2 BallMove(ShapeData shape) {
        return shape.rule.BallMove(shape.transform.position);
    }
}

public class Level2 : Level
{

    public override void Init()
    {

        shapes = new ShapeData[1];

        var bal = ShapeData.shapeType.ball;
        shapes[0] = new ShapeData(bal, Vector2.zero, GameObject.Instantiate(GameGod.me.shapes[(int)bal]), numSteps);

        Simulate();
    }

    public override void SetRules(bool playerTime) {
        for (int i = 0; i < shapes.Length; i++) {
            if (playerTime && shapes[i].playerOwned)
                shapes[i].rule = new Lv2();
            else
                shapes[i].rule = new MoveDiagonalUpLeft();
        }
    }

    public override Vector2 BallMove(ShapeData shape) {
        return shape.rule.BallMove(shape.transform.position);
    }



}

public class Level3 : Level
{

    public override void Init()
    {

        shapes = new ShapeData[1];

        var bal = ShapeData.shapeType.ball;
        shapes[0] = new ShapeData(bal, Vector2.zero, GameObject.Instantiate(GameGod.me.shapes[(int)bal]), numSteps);

        Simulate();
    }

    public override void SetRules(bool playerTime) {
        for (int i = 0; i < shapes.Length; i++) {
            if (playerTime && shapes[i].playerOwned)
                shapes[i].rule = new Lv3();
            else
                shapes[i].rule = new MoveDiagonalUpLeftOneUnit();
        }
    }

    public override Vector2 BallMove(ShapeData shape) {
        return shape.rule.BallMove(shape.transform.position);
    }

}
public class Level4 : Level
{

    public override void Init() {
        numSteps = 20;
        shapes = new ShapeData[1];

        var bal = ShapeData.shapeType.ball;
        shapes[0] = new ShapeData(bal, Vector2.zero, GameObject.Instantiate(GameGod.me.shapes[(int)bal]), numSteps);

        Simulate();
    }

    public override void SetRules(bool playerTime) {
        for (int i = 0; i < shapes.Length; i++) {
            if (playerTime && shapes[i].playerOwned)
                shapes[i].rule = new Lv4();
            else
                shapes[i].rule = new AccelerateRight();
        }
    }

    public override Vector2 BallMove(ShapeData shape) {
        return shape.rule.BallMove(shape.transform.position);
    }

}