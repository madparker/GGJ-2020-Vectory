using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lv1 : Rule {
    //Move the ball to the right one unit per step.
    public override Vector2 BallMove(Vector2 currentPos) {
        return currentPos + Vector2.right;
    }

}

public class Lv2 : Rule{
    //Move the ball to the up one unit and left one unit per step.
    public override Vector2 BallMove(Vector2 currentPos)
    {
        return currentPos + Vector2.up + Vector2.left;
    }

}

public class Lv3 : Rule {
    //Move the ball diagonally up and to the left by one unit per step.
    public override Vector2 BallMove(Vector2 currentPos)
    {
        return currentPos + (Vector2.up + Vector2.left).normalized;
    }

}
public class Lv4 : Rule {
    //Accelerate the ball to the right at .1 units per step.
    Vector2 vel;
    public override Vector2 BallMove(Vector2 currentPos) {
        vel += Vector2.right * .1f;
        return currentPos + vel;
    }

}

//public class Lv5 : Rule {

//    public override Vector2 Step(Vector2 currentPos, Vector2 input)
//    {
//        return base.Step(currentPos, input);
//    }

//}

