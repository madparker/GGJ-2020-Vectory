using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform ball;
    public Camera cam;
    void Start() {
        cam = Camera.main;
    }
    void Update() {
        var bl = new Vector2(Mathf.Min(-5, ball.position.x - 2f), Mathf.Min(-5, ball.position.y - 2f));
        var tr = new Vector2(Mathf.Max(5, ball.position.x + 2f), Mathf.Max(5f, ball.position.y + 2f));
        cam.orthographicSize = (tr.y - bl.y) / 2f;
        cam.transform.position = (bl + tr) / 2f;
        cam.transform.position -= Vector3.forward * 10f;

    }
}
