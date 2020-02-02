using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameGod : MonoBehaviour {
    [NamedArray(typeof(ShapeData.shapeType))]
    public Transform[] shapes;


    public Level[] levelArray;
    public Level curLevel;

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
        levelArray = new Level[4] { new Level1(), new Level2(), new Level3(), new Level4()};
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

        if (hasWon)
            return;

        stepIndex++;
        var success = curLevel.Step(stepIndex, true);
        if (stepIndex == stepNum && success) {
            if (!hasLost)
            {
                winText.SetActive(true);
                hasWon = true;
                testing = false;

                Invoke("NextLevel", 1f);

            }
        } else if (!success) {
            Lose();
        }

    }
    public void InitLevel(){
        curLevel = levelArray[levelIndex];
        curLevel.Init();

        print(levelArray[levelIndex]);

        correctPositions = curLevel.shapes[0].posArr;
        winText.SetActive(false);
        lostText.SetActive(false);
        VisualizeRule.me.InitLevel();
        stepIndex = 0;
        hasWon = false;
        hasLost = false;
        stepNum = levelArray[levelIndex].numSteps;
    }

    public void NextLevel(){
        curLevel.CleanUp();
        levelIndex++;
        if (levelIndex < levelArray.Length)
        {
            InitLevel();
            testing = true;
        }
    }

    public void Lose() {
        Vector2 pos = curLevel.shapes[0].transform.position;
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
        CamControl.me.DeadCam();

    }
}


