﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualizeRule : MonoBehaviour
{

    public GameObject ResultUI;

    public static VisualizeRule me;

    GameObject prevHolder;

    private void Awake()
    {
        if (me != null)
        {
            Destroy(this);
        }

        me = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        InitLevel();
    }

    public void InitLevel(){

        Destroy(prevHolder);
        prevHolder = new GameObject();

        prevHolder.name = "Preview Position Holder";

        int i = 1;


        print(GameGod.me);
        print(GameGod.me.correctPositions);

        foreach (Vector2 vec in GameGod.me.correctPositions)
        {
            GameObject position =
                Instantiate<GameObject>(Resources.Load<GameObject>("UI/PreviewPosition"));
            position.transform.position = vec;
            position.GetComponentInChildren<TextMeshPro>().text = "" + i;
            i++;


            position.transform.SetParent(prevHolder.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
