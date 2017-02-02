using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CYF_Game
{
    public enum Color
    {
        BLUE,
        YELLOW,
        GREEN
    }

    public class Spawn : MonoBehaviour
    {
        public float endTime;
        public float timer;
        public GameObject circlePrefab;
        public Sprite[] colors;

        ////////////////////////////////////
        ///// GUI 
        public Button btnYellow, btnBlue, btnGreen, btnRestart, btnNextLevel;
        public Text infoText;
        public Text textLevel;
        /// //////////////////////////////////////////
        private int NumberOfCircles;
        private int Level = 1;
        private bool isQustionAsked = false;
        private float speed = 0.1f;
        private int QuestionIndex;
        public List<CYF_Circle> circles;

        void Start()
        {
            NumberOfCircles = 5;
            circles = new List<CYF_Circle>();
            if (colors.Length > 0)
            {
                generateRandomCircles(NumberOfCircles);
            }
            textLevel.text = "Level " + Level;
            btnYellow.onClick.AddListener(btn_chooseColor_onClick);
            btnGreen.onClick.AddListener(btn_chooseColor_onClick);
            btnBlue.onClick.AddListener(btn_chooseColor_onClick);
            btnRestart.onClick.AddListener(btnRestart_onClick);
            btnNextLevel.onClick.AddListener(btnNextLevel_onClick);
        }

        private void btnNextLevel_onClick()
        {
            NumberOfCircles = NumberOfCircles + 1;
            infoText.text = "";
            generateRandomCircles(NumberOfCircles);
            btnNextLevel.gameObject.SetActive(false);
            isQustionAsked = false;
            textLevel.text = "Level " + Level;
            timer = 0;
        }

        private void btnRestart_onClick()
        {
            NumberOfCircles = 5;
            Level = 1;
            textLevel.text = "Level " + Level;
            generateRandomCircles(NumberOfCircles);
            infoText.text = "";
            btnRestart.gameObject.SetActive(false);
            isQustionAsked = false;
            timer = 0;
        }

        private void btn_chooseColor_onClick()
        {
            string _clickedButtonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
            if((_clickedButtonName == "btnBlue" && circles[QuestionIndex].color == Color.BLUE) ||
                (_clickedButtonName == "btnYellow" && circles[QuestionIndex].color == Color.YELLOW) ||
                (_clickedButtonName == "btnGreen" && circles[QuestionIndex].color == Color.GREEN))
            {
                Level++;
                infoText.text = "Next level: " + Level;
                btnNextLevel.gameObject.SetActive(true);
            }
            else
            {
                infoText.text = "You Lost." + System.Environment.NewLine + " Would you like to restart?";
                btnRestart.gameObject.SetActive(true);
            }

            DestroyCircles();
            SetButtonsActive(false, false, false);
            timer = 0;
        }

        void generateRandomCircles(int num)
        {
            for (int i = 0; i < num; ++i)
            {
                int arrayIdx;
                if(Level >= 10)
                {
                    arrayIdx = Random.Range(0, 3);
                }
                else
                {
                    arrayIdx = Random.Range(0, 2);
                }
                Sprite color = colors[arrayIdx];

                CYF_Circle circle = new CYF_Circle();
                circle.gameObj = Instantiate(circlePrefab);
                circle.gameObj.name = color.name;
                circle.color = (Color)arrayIdx;
                circle.gameObj.GetComponent<SpriteRenderer>().sprite = color;
                circle.gameObj.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-4.0f, 4.0f));
                circles.Add(circle);
            }
        }

        private void MoveCircles()
        {
            if (circles.Count > 0)
            {
                foreach(var circle in circles)
                {
                    moveCircle(circle);
                }
            }
        }

        private void moveCircle(CYF_Circle circle)
        {
            circle.gameObj.transform.position = Vector3.MoveTowards(circle.gameObj.transform.position, circle.Target, speed);

            if (Vector3.Distance(circle.gameObj.transform.position, circle.Target) < 0.2)
            {
                circle.Target = newTarget();
            }
        }

        void Update()
        {
            if (timer < endTime)
            {
                updateTimer();
                MoveCircles();
            }
            else
            {
                if (!isQustionAsked)
                {
                    QuestionIndex = Random.Range(0, circles.Count - 1);
                    doQustionMarkedCircle(circles[QuestionIndex]);
                    isQustionAsked = true;
                    if(Level >= 10)
                    {
                        SetButtonsActive(true, true, true);
                    }
                    else
                    {
                        SetButtonsActive(true, true, false);
                    }
                }
            }
        }

        private void DestroyCircles()
        {
            foreach (var circle in circles)
            {
                Destroy(circle.gameObj);
            }
            circles.Clear();
        }

        private void doQustionMarkedCircle(CYF_Circle circle)
        {
            circle.gameObj.GetComponent<SpriteRenderer>().sprite = colors[3];
        }

        private void updateTimer()
        {
            timer += Time.deltaTime;
        }

        private void SetButtonsActive(bool activateBlue, bool activateYellow, bool activateGreen)
        {
            btnBlue.gameObject.SetActive(activateBlue);
            btnYellow.gameObject.SetActive(activateYellow);
            btnGreen.gameObject.SetActive(activateGreen);
        }

        private Vector3 newTarget()
        {
            float Xpos = Random.Range(-5.0f, 5.0f);
            float Ypos = Random.Range(-4.0f, 4.0f);
            return new Vector3(Xpos, Ypos);
        }
    }

}
