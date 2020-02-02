using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameGod : MonoBehaviour {



    public Transform ball;

    public Level[] levelArray;

    public int levelIndex = 0;

    public static GameGod me;

    public static int stepNum = 10;

    public bool hasLost, hasWon;
    public GameObject lostText, winText;
    public Vector2[] correctPositions;

    int stepIndex = 0;

    bool testing;
    float testTimer; 
    public float testRate = 0.1f;

    public GameObject arrowHead;

    public void Awake() {
        if (me != null) {
            Debug.LogError("TOO MANY GODS THEY MUST FIGHT");
            Destroy(gameObject);
            return;
        }
        me = this;
        levelArray = new Level[3] { new Level1(), new Level2(), new Level3() };
            //{ new MoveRightOne(), new MoveDiagonalUpLeft(), new MoveDiagonalUpLeftOnUnit(), new AccelerateRight()};
        //playerRuleArr = new PlayerRule[4] 
        //    { new Lv1(), new Lv2(), new Lv3(), new Lv4()};
        InitLevel();

        arrowHead.SetActive(false);
    }

    void Start() {
    }

    void Update() {
        if (testing) {
            testTimer += Time.deltaTime;
            if (testTimer > testRate) {
                testTimer = 0;
                Step();
            }
        }
    }

    public void StartTest() {
        testing = true;

    }

    public void Step() {

        levelArray[levelIndex].vectorInputs[0] = ball.transform.position;


        if (hasWon)
            return;
        var pos = levelArray[levelIndex].Step(ball.transform.position, true);
        ball.transform.position = pos;
        stepIndex++;
        if (stepIndex == stepNum-1 && pos == correctPositions[stepIndex]) {
            if (!hasLost)
            {
                winText.SetActive(true);
                hasWon = true;
                testing = false;

                Invoke("NextLevel", 0.1f);

                //NextLevel();

            }
        } else if (pos != correctPositions[stepIndex]) {
            Lose();

            //arrowHead.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x));

            //arrowHead.transform.Rotate(Vector3.forward, Vector2.Angle(pos, correctPositions[stepIndex]));
        }

    }
    public void InitLevel(){
        ball.transform.position = Vector2.zero;

        levelArray[levelIndex].vectorInputs.Add(ball.transform.position);

        levelArray[levelIndex].Init();

        print(levelArray[levelIndex]);

        correctPositions = levelArray[levelIndex].GetPositions(ball.transform.position);
        winText.SetActive(false);
        lostText.SetActive(false);
        VisualizeRule.me.InitLevel();
        stepIndex = 0;
        hasWon = false;
        hasLost = false;
        stepNum = levelArray[levelIndex].numSteps;
    }

    public void NextLevel(){
        levelIndex++;
        if (levelIndex < levelArray.Length)
        {
            InitLevel();
            testing = true;
        }
    }

    public void Lose() {
        Vector2 pos = ball.transform.position;
        hasLost = true;
        testing = false;
        lostText.SetActive(true);
        lostText.GetComponent<TextMeshProUGUI>().text = "Error in Step " + stepIndex + "\n" +
            "You were off by " + (pos - correctPositions[stepIndex]);
        arrowHead.SetActive(true);
        arrowHead.transform.position = correctPositions[stepIndex];

        Vector2 dir = (correctPositions[stepIndex] - pos).normalized;
        arrowHead.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        var lin = arrowHead.GetComponent<LineRenderer>();
        lin.positionCount = 2;
        lin.SetPosition(0, pos);
        lin.SetPosition(1, correctPositions[stepIndex] - dir * .1f);
        CamControl.me.DeadCam(correctPositions[stepIndex]);

    }
}


