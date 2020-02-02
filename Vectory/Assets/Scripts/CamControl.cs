using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform ball;
    public Camera cam;
    public static CamControl me;
    private void Awake() {
        me = this;
    }
    void Start() {
        cam = Camera.main;
        
    }
    void Update() {
        if (GameGod.me.hasLost) {
            return;
        }
        var bl = new Vector2(-5f, -5f);
        var tr = new Vector2(5f, 5f);
        
        for (int i = 0; i < GameGod.me.curLevel.shapes.Length; i++) {
            var shapPos = GameGod.me.curLevel.shapes[i].transform.position;
            bl.x = Mathf.Min(shapPos.x - 2f, bl.x);
            bl.y = Mathf.Min(shapPos.y - 2f, bl.x);
            tr.x = Mathf.Max(shapPos.x + 2f, tr.x);
            tr.y = Mathf.Max(shapPos.y + 2f, tr.y);
        }
        cam.orthographicSize = Mathf.Max((tr.y - bl.y) / 2f, ((tr.x - bl.x) / 2f) * (9f / 16f));

        cam.transform.position = (bl + tr) / 2f;
        cam.transform.position -= Vector3.forward * 10f;

        /*var bl = new Vector2(Mathf.Min(-5, ball.position.x - 2f), Mathf.Min(-5, ball.position.y - 2f));
        var tr = new Vector2(Mathf.Max(5, ball.position.x + 2f), Mathf.Max(5f, ball.position.y + 2f));
        cam.orthographicSize = Mathf.Max((tr.y - bl.y) / 2f, ((tr.x-bl.x)/2f) * (9f/16f));

        cam.transform.position = (bl + tr) / 2f;
        cam.transform.position -= Vector3.forward * 10f;*/
    }

    public void DeadCam() {
        var bl = new Vector2(100, 100);
        var tr = new Vector2(-100, -100);

        for (int i = 0; i < GameGod.me.curLevel.shapes.Length; i++) {
            if (!GameGod.me.curLevel.shapes[i].failed) continue;

            print("FAILED");

            var shapPos = GameGod.me.curLevel.shapes[i].transform.position;

            bl.x = Mathf.Min(shapPos.x - 2f, bl.x);
            bl.y = Mathf.Min(shapPos.y - 2f, bl.x);
            tr.x = Mathf.Max(shapPos.x + 2f, tr.x);
            tr.y = Mathf.Max(shapPos.y + 2f, tr.y);
        }
        cam.orthographicSize = Mathf.Max((tr.y - bl.y) / 2f, ((tr.x - bl.x) / 2f) * (9f / 16f));

        cam.transform.position = (bl + tr) / 2f;
        cam.transform.position -= Vector3.forward * 10f;
    }

}
