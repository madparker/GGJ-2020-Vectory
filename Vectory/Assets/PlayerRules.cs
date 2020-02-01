using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lvl1 : PlayerRule {
    public override Vector2 Step(Vector2 currentPos) {
        return currentPos + Vector2.right;
    }
}

