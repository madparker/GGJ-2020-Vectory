using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level
{
    public Dictionary<string, float> floatInputs;
    public Dictionary<string, Vector2> vectorInputs;

    public ShapeData[] shapes;

    public Rule SimRule; // handles all NPCs

    public Rule playerBallRule, playerRecRule, playerTriRule;
    public Rule desBallRule, desRecRule, desTriRule;

    public abstract void Step();
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

    public Level1(){
        desginerRule = new MoveRightOne();
        playerRule = new Lv1();

        ShapeData sd = new ShapeData();
        sd.type = ShapeData.shapeType.ball;
        sd.startPos = Vector2.zero;

        shapes = new ShapeData[1] { sd};
    }

    public

    public override void Step(bool playerTime){
        for (int i = 0; i < (int)ShapeData.shapeType.length; i++)
        {
            if (playerTime && playerRules[i] != null){
                //run player version of rule
            } else {
                //run desginer version of rule if it exists
            }

        }
        //playerRule.Step(shapes[0].transform.position);
    }
}