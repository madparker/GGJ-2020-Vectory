using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualizeRule : MonoBehaviour
{
    public Transform ballTrans;

    //public Vector2[] ResultingPositions;

    public GameObject ResultUI;

    // Start is called before the first frame update
    void Start()
    {
        ballTrans = GameObject.Find("Ball").transform;

        Rule currentRule = GameGod.me.GetCurrentRule();

        GameObject prevHolder = new GameObject();
        prevHolder.name = "Preview Position Holder";

        int i = 1;
        foreach(Vector2 vec in GameGod.me.correctPositions){
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
