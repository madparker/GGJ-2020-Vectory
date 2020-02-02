using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualizeRule : MonoBehaviour
{

    public GameObject ResultUI;

    public static VisualizeRule me;

    GameObject prevHolder;

    public Transform previewBallPrefab;
    Transform[] previewBallArr;

    public TextMeshPro infoText;

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

        print(GameGod.me);
        print(GameGod.me.correctPositions);

        ShapeData[] shapes = GameGod.me.curLevel.shapes;

        foreach (ShapeData shape in shapes)
        {
            previewBallArr = new Transform[shape.posArr.Length];
            for (int i = 0; i < shape.posArr.Length; i++)
            {
                var vec = shape.posArr[i];

                Transform previewBall =
                    Instantiate(previewBallPrefab);
                previewBall.position = vec;
                previewBall.GetComponentInChildren<TextMeshPro>().text = "" + i;
              
                previewBall.SetParent(prevHolder.transform);
                previewBallArr[i] = previewBall;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var minDist = .5f;
        int tarBallInd = -1;
        for (int i = 0; i < previewBallArr.Length; i++) {
            var dist = (mousePos - (Vector2)previewBallArr[i].position).magnitude;
            if (dist < minDist) {
                tarBallInd = i;
                minDist = dist;
            }
        }
        if (tarBallInd != -1) {
            infoText.gameObject.SetActive(true);
            infoText.text = $"Position {tarBallInd} \nX:{Round(previewBallArr[tarBallInd].position.x)} Y:{Round(previewBallArr[tarBallInd].position.y)}";
            infoText.transform.position = previewBallArr[tarBallInd].position + Vector3.up;
        } else {
            infoText.gameObject.SetActive(false);
        }
    }

    public float Round(float a) {
        a *= 10f;
        a = Mathf.Round(a);
        a /= 10f;
        return a;
    }

    
}
