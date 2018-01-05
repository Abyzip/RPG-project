using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour {

    public static QuestUIManager uiManager;
    public GameObject player;

    public bool questAvailable = false;
    public bool questRunning = false;
    private bool questPanelActive = false;
    private bool questLogPanelActive = false;

    public GameObject questPanel;
    public GameObject questLogPanel;

    private QuestObject currentQuestObject;

    public List<Quest> availableQuests = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();

    public GameObject qButton;
    public GameObject qLogButton;
    private List<GameObject> qButtons = new List<GameObject>();

    private GameObject acceptButton;
    private GameObject completeButton;

    public Transform qButtonSpacer1;
    public Transform qButtonSpacer2;
    public Transform qLogButtonSpacer;

    public GameObject questTitle;               //Text
    public GameObject questDescription;         //Text
    public GameObject questSummary;             //Text
    public GameObject questReward;

    public GameObject questLogTitle;            //Text
    public GameObject questLogDescription;      //Text
    public GameObject questLogSummary;          //Text
    public GameObject questLogReward;

    public GameObject imgQuestAvailable;

    public AudioSource PlayeraudioSource;
    public AudioClip PageOn;
    public AudioClip PageOff;

    void Awake ()
    {
        if(uiManager == null)
        {
            uiManager = this;
        }
        else if (uiManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        HideQuestPanel();
    }

    // Use this for initialization
    void Start () {

        foreach (Quest availableQuest in QuestManager.questManager.questListe)
        {
            if (QuestManager.questManager.RequestAvailableQuest(availableQuest.id))
            {
                imgQuestAvailable.SetActive(true);
                break;
            }
            else
            {
                imgQuestAvailable.SetActive(false);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K))
        {
            questLogPanelActive = !questLogPanelActive;
            if (questLogPanelActive)
            {
                PlayeraudioSource.PlayOneShot(PageOn);
                ShowQuestLogPanel();
            }

            else
            {
                HideQuestLogPanel();
                PlayeraudioSource.PlayOneShot(PageOff);
            }
        }

        
    }

    public void CheckQuests(QuestObject questObject)
    {
        currentQuestObject = questObject;
        QuestManager.questManager.QuestRequest(questObject);
        if((questRunning || questAvailable) && !questPanelActive)
        {
            ShowQuestPanel();
        }
        else
        {
            HideQuestPanel();
        }
    }

    public void ShowQuestPanel()
    {
        questPanelActive = true;
        Cursor.visible = true;
        player.GetComponent<PlayerController>().enabled = false;
       // player.GetComponent<DashScript>().enabled = false;
        player.GetComponent<AttackPlayerScript>().enabled = false;
        questPanel.SetActive(questPanelActive);
        FillQuestButton();
    }

    public void ShowQuestLogPanel()
    {
        questLogPanelActive = true;
        Cursor.visible = true;
        player.GetComponent<PlayerController>().enabled = false;
       // player.GetComponent<DashScript>().enabled = false;
        player.GetComponent<AttackPlayerScript>().enabled = false;
        questLogPanel.SetActive(questLogPanelActive);
        FillQuestLogButton();
    }

    public void HideQuestPanel()
    {
        questPanelActive = false;
        questAvailable = false;
        questRunning = false;

        Cursor.visible = false;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<AttackPlayerScript>().enabled = true;

        questTitle.GetComponent<TMPro.TextMeshProUGUI>().SetText("");
        questDescription.GetComponent<TMPro.TextMeshProUGUI>().SetText("");
        questSummary.GetComponent<TMPro.TextMeshProUGUI>().SetText("");

        availableQuests.Clear();
        activeQuests.Clear();
        for(int i = 0; i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();
        questPanel.SetActive(questPanelActive);
    }

    public void HideQuestLogPanel()
    {
        questLogPanelActive = false;

        Cursor.visible = false;
        player.GetComponent<PlayerController>().enabled = true;
        //player.GetComponent<DashScript>().enabled = true;
        player.GetComponent<AttackPlayerScript>().enabled = true;

        questLogTitle.GetComponent<TMPro.TextMeshProUGUI>().SetText("");
        questLogDescription.GetComponent<TMPro.TextMeshProUGUI>().SetText("");
        questLogSummary.GetComponent<TMPro.TextMeshProUGUI>().SetText("");

        availableQuests.Clear();
        activeQuests.Clear();
        for (int i = 0; i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();
        questLogPanel.SetActive(questLogPanelActive);
    }


    void FillQuestButton()
    {
        foreach(Quest availableQuest in availableQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QButton qBut = questButton.GetComponent<QButton>();
            qBut.questID = availableQuest.id;
            qBut.questTitle.GetComponent<TMPro.TextMeshProUGUI>().SetText(availableQuest.title);
            qBut.transform.SetParent(qButtonSpacer1, false);
            qButtons.Add(questButton);
        }

        foreach (Quest activeQuest in activeQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QButton qBut = questButton.GetComponent<QButton>();
            qBut.questID = activeQuest.id;
            qBut.questTitle.GetComponent<TMPro.TextMeshProUGUI>().SetText(activeQuest.title);
            qBut.transform.SetParent(qButtonSpacer2, false);
            qButtons.Add(questButton);
        }
    }

    void FillQuestLogButton()
    {
        foreach (Quest availableQuest in QuestManager.questManager.questListe)
        {
            if(QuestManager.questManager.RequestAcceptedQuest(availableQuest.id) || QuestManager.questManager.RequestCompleteQuest(availableQuest.id))
                {
                    ShowSelectedLogQuest(availableQuest.id);
                }
            else if(QuestManager.questManager.RequestAvailableQuest(availableQuest.id))
            {
                questLogTitle.GetComponent<TMPro.TextMeshProUGUI>().SetText("Quetes disponibles");
                questLogDescription.GetComponent<TMPro.TextMeshProUGUI>().SetText(availableQuest.summary);
                questLogSummary.GetComponent<TMPro.TextMeshProUGUI>().SetText(availableQuest.title);
                questLogReward.GetComponent<TMPro.TextMeshProUGUI>().SetText("Recompense:<br>" + availableQuest.item);
            }
        }
    }


    public void ShowSelectedQuest(int questID)
    {
        for(int i = 0; i < availableQuests.Count; i++)
        {
            if(availableQuests[i].id == questID)
            {
                questTitle.GetComponent<TMPro.TextMeshProUGUI>().SetText(availableQuests[i].title); 
                if(availableQuests[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    questDescription.GetComponent<TMPro.TextMeshProUGUI>().SetText(availableQuests[i].description);
                    questSummary.GetComponent<TMPro.TextMeshProUGUI>().SetText(availableQuests[i].questObjective + " : " + availableQuests[i].questObjectiveCount + " / " + availableQuests[i].questObjectiveRequirement);
                    questReward.GetComponent<TMPro.TextMeshProUGUI>().SetText("Récompense:<br>" + availableQuests[i].item);
                }
            }
        }

        for(int i = 0; i < activeQuests.Count; i++)
        {
            if (activeQuests[i].id == questID)
            {
                questTitle.GetComponent<TMPro.TextMeshProUGUI>().SetText(activeQuests[i].title);
                if (activeQuests[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    questDescription.GetComponent<TMPro.TextMeshProUGUI>().SetText(activeQuests[i].hint);
                    questSummary.GetComponent<TMPro.TextMeshProUGUI>().SetText(activeQuests[i].questObjective + " : " + activeQuests[i].questObjectiveCount + " / " + activeQuests[i].questObjectiveRequirement);
                }
                else if (activeQuests[i].progress == Quest.QuestProgress.COMPLETED)
                {
                    questDescription.GetComponent<TMPro.TextMeshProUGUI>().SetText(activeQuests[i].congratulation);
                    questSummary.GetComponent<TMPro.TextMeshProUGUI>().SetText(activeQuests[i].questObjective + " : " + activeQuests[i].questObjectiveCount + " / " + activeQuests[i].questObjectiveRequirement);
                }
            }
        }
    }

    public void ShowSelectedLogQuest(int questID)
    {
        for (int i = 0; i < QuestManager.questManager.questListe.Count; i++)
        {
            if (QuestManager.questManager.questListe[i].id == questID)
            {
                questLogTitle.GetComponent<TMPro.TextMeshProUGUI>().SetText(QuestManager.questManager.questListe[i].title);
                if (QuestManager.questManager.questListe[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    questLogDescription.GetComponent<TMPro.TextMeshProUGUI>().SetText(QuestManager.questManager.questListe[i].description);
                    questLogSummary.GetComponent<TMPro.TextMeshProUGUI>().SetText(QuestManager.questManager.questListe[i].questObjective + " : " + QuestManager.questManager.questListe[i].questObjectiveCount + " / " + QuestManager.questManager.questListe[i].questObjectiveRequirement);
                }

                if (QuestManager.questManager.questListe[i].progress == Quest.QuestProgress.COMPLETED)
                {
                    questLogDescription.GetComponent<TMPro.TextMeshProUGUI>().SetText(QuestManager.questManager.questListe[i].congratulation);
                    questLogSummary.GetComponent<TMPro.TextMeshProUGUI>().SetText(QuestManager.questManager.questListe[i].questObjective + " : " + QuestManager.questManager.questListe[i].questObjectiveCount + " / " + QuestManager.questManager.questListe[i].questObjectiveRequirement);
                    
                }
            }
        }
    }

}
