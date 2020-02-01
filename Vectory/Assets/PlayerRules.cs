using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lvl1 : PlayerRule {

    public override Vector2 Step(Vector2 currentPos) {
        return currentPos + Vector2.right;
    }

}

public class Lv2 : PlayerRule{

    public override Vector2 Step(Vector2 currentPos)
    {
        return currentPos + Vector2.up + Vector2.left;
    }

}

public class Lv3 : PlayerRule
{

    public override Vector2 Step(Vector2 currentPos)
    {
        return currentPos + (Vector2.up + Vector2.left);
    }

}

