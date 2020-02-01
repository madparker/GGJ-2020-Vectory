﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeRule : MonoBehaviour
{
    public Vector2[] ResultingPositions;

    public GameObject ResultUI;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Vector2 vec in ResultingPositions){
            GameObject position =
                Instantiate<GameObject>(Resources.Load<GameObject>("UI/PreviewPosition"));
            position.transform.position = vec;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
