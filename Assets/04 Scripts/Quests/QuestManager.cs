using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    #region Singleton

    public static QuestManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject enemies;
    public Text questInfo;
    public Text giverText;
    public Text accepterText;

    public bool questOneAccepted;
    public bool questOneCompleted;
    public int enemiesKilled = 0;

    public bool questTwoAccepted;
    public bool questTwoCompleted;

    private void Start()
    {
        questOneAccepted = false;
        questOneCompleted = false;

        questTwoAccepted = false;
        questTwoCompleted = false;
    }

    public void AcceptQuestOne()
    {
        questOneAccepted = true;
        enemies.SetActive(true);

        questInfo.text = "Kill the lumberjacks. " + "(" + enemiesKilled + "/4)";

        giverText.text = "The tree behind me is sacred for my village, and there are some humans who want to chop it down. Deal with them for us, and we will reward you greatly. Their camp is located to the west!";
    }

    public void EnemyKilled()
    {
        enemiesKilled++;

        questInfo.text = "Kill the lumberjacks. " + "(" + enemiesKilled + "/4)";

        if (enemiesKilled >= 4)
        {
            questOneCompleted = true;
            questInfo.text = "Return to the Pigman.";

            giverText.text = "Have you dealt with them?";
        }
    }

    public void AcceptQuestTwo()
    {
        questTwoAccepted = true;

        questInfo.text = "Go to the pigman village and warn them.";

        giverText.text = "Oh thank you so much, this tree is everything to us. Please do me another small favor. Go to my village, located to the east, and warn our chief that there are more of them. He will reward you for everything.";
    }

    public void CompleteQuestTwo()
    {
        questTwoCompleted = true;

        questInfo.text = "";

        giverText.text = "Thank you again for everything!";

        accepterText.text = "Throgg was wise to enlist your help and send you to me. Come, we have much to talk about.";
    }
}
