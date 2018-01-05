using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QButton : MonoBehaviour {

    public int questID;
    public GameObject questTitle;

    private GameObject acceptButton;
    private GameObject completeButton;

    private QButton acceptButScript;
    private QButton completeButScript;

    public void Start()
    {
        acceptButton = GameObject.Find("QuestCanvas").transform.Find("QuestPanel").transform.Find("DescriptionQuests").transform.Find("Buttons").transform.Find("QButtonAccepte").gameObject;
        acceptButScript = acceptButton.GetComponent<QButton>();

        completeButton = GameObject.Find("QuestCanvas").transform.Find("QuestPanel").transform.Find("DescriptionQuests").transform.Find("Buttons").transform.Find("QButtonComplete").gameObject;
        completeButScript = completeButton.GetComponent<QButton>();

        acceptButton.SetActive(false);
        completeButton.SetActive(false);
    }

    public void ShowAllInfos()
    {
        Debug.Log(questID);
        QuestUIManager.uiManager.ShowSelectedQuest(questID);

        if (QuestManager.questManager.RequestAvailableQuest(questID))
        {
            acceptButton.SetActive(true);
            acceptButScript.questID = questID;
        }
        else
        {
            acceptButton.SetActive(false);
        }

        if (QuestManager.questManager.RequestCompleteQuest(questID))
        {
            completeButton.SetActive(true);
            completeButScript.questID = questID;
        }
        else
        {
            completeButton.SetActive(false);
        }

    }

    public void AccepteQuest()
    {
        QuestManager.questManager.AccepteQuest(questID);
        QuestUIManager.uiManager.HideQuestPanel();

        QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];
        foreach (QuestObject obj in currentQuestGuys)
        {
            obj.setQuestMaker();
        }
    }

    public void CompleteQuest()
    {
        QuestManager.questManager.CompleteQuest(questID);
        QuestUIManager.uiManager.HideQuestPanel();

        QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];
        foreach (QuestObject obj in currentQuestGuys)
        {
            obj.setQuestMaker();
        }
    }

    public void ClosePanel()
    {
        QuestUIManager.uiManager.HideQuestPanel();
    }

}
