using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule
{
    public string name;
    public int stepNum = 10;
    public Vector2 v1;
    public float f1;

    public virtual void Init(){}

    public virtual void Randomize(){}

    public virtual Vector2 BallMove(Vector2 currentPos, Vector2 inputV, float inputF){
        return Vector2.negativeInfinity;
    }

    public virtual Vector2 BallMove(Vector2 currentPos, Vector2 inputV)
    {
        return Vector2.negativeInfinity;
    }

    public virtual Vector2 BallMove(Vector2 currentPos, float inputV)
    {
        return Vector2.negativeInfinity;
    }

    public virtual Vector2 BallMove(Vector2 currentPos)
    {
        return Vector2.negativeInfinity;
    }

    public virtual Vector2 BallMove()
    {
        return Vector2.negativeInfinity;
    }
}

public class MoveRightOne : Rule {

    public override Vector2 BallMove(Vector2 currentPos)
    {
        return currentPos += Vector2.right;
    }
}

public class MoveRightSome : Rule
{

    public override void Init() { }
    public override void Randomize() { }

    public override Vector2 BallMove(Vector2 currentPos, float input)
    {
        return currentPos += Vector2.right * input;
    }
}

public class MoveDiagonalUpLeft : Rule
{
    public override void Init() { }
    public override void Randomize() { }
   
    public override Vector2 BallMove(Vector2 currentPos)
    {
        currentPos += Vector2.left;
        currentPos += Vector2.up;

        return currentPos;
    }
}


public class MoveDiagonalUpLeftOneUnit : Rule
{

    public override void Init() { }
    public override void Randomize() { }

    public override Vector2 BallMove(Vector2 currentPos)
    {
        Vector2 newDir = new Vector2(-1, 1).normalized;

        return currentPos += newDir;
    }
}

public class AccelerateRight : Rule {

    public Vector2 velocity;
    public override void Init() { }
    public override void Randomize() { }


    public override Vector2 BallMove(Vector2 currentPos){
        velocity += Vector2.right * .1f;
        currentPos += velocity;
        return currentPos;
    }
}

public class LerpAway : Rule {

    public Vector2 vel;
    public override void Init() { }
    public override void Randomize() { }


    public override Vector2 BallMove(Vector2 currentPos) {

        return currentPos + currentPos * .1f;
    }
}