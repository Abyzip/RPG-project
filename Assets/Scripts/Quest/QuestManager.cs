using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {

    public static QuestManager questManager;

    public GameObject player;
    public GameObject questAccomplished;

    public List<Quest> questListe = new List<Quest>();
    public List<Quest> currentQuestList = new List<Quest>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    void Awake()
    {
        if(questManager == null)
        {
            questManager = this;
        }
        else if (questManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void QuestRequest(QuestObject AquestObject)
    {
        //available quests
        if(AquestObject.availableQuestIDs.Count > 0)
        {
            for(int i = 0; i < questListe.Count; i++)
            {
                for(int j = 0; j < AquestObject.availableQuestIDs.Count; j++)
                {
                    if(questListe[i].id == AquestObject.availableQuestIDs[j] && questListe[i].progress == Quest.QuestProgress.AVAILABLE)
                    {
                        //AccepteQuest(AquestObject.availableQuestIDs[j]);
                        //quest ui manager
                        QuestUIManager.uiManager.questAvailable = true;
                        QuestUIManager.uiManager.availableQuests.Add(questListe[i]);

                    }
                }
            }
        }

        //active quests
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            for (int j = 0; j < AquestObject.receivableQuestIDs.Count; j++)
            {
                if(currentQuestList[i].id == AquestObject.receivableQuestIDs[j] && questListe[i].progress == Quest.QuestProgress.ACCEPTED || currentQuestList[i].progress == Quest.QuestProgress.COMPLETED)
                {
                    //CompleteQuest(AquestObject.receivableQuestIDs[j]);
                    QuestUIManager.uiManager.questRunning = true;
                    QuestUIManager.uiManager.activeQuests.Add(currentQuestList[i]);
                }
            }
        }
    }



    public void AccepteQuest(int questID)
    {
        for(int i=0; i<questListe.Count; i++)
        {
            if(questListe[i].id == questID && questListe[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                currentQuestList.Add(questListe[i]);
                QuestUIManager.uiManager.activeQuests.Add(questListe[i]);
                QuestUIManager.uiManager.imgQuestAvailable.SetActive(false);
                questListe[i].progress = Quest.QuestProgress.ACCEPTED;
            }
        }
    }

    public void CompleteQuest(int questID)
    {
        for (int i = 0; i < questListe.Count; i++)
        {
            if (questListe[i].id == questID && questListe[i].progress == Quest.QuestProgress.COMPLETED)
            {
                for (int j = 0; j < currentQuestList.Count; j++)
                {
                    if (currentQuestList[j].id == questID)
                    {

                        if (player.GetComponent<InventoryController>().AddEquipment(player.GetComponent<EquipmentConstructor>().getEquipment(currentQuestList[j].item)))
                        { 
                            currentQuestList[j].progress = Quest.QuestProgress.DONE;
                            currentQuestList.Remove(currentQuestList[j]);
                        }

                    }
                }
                //REWARD
            }
        }
        //check chain quests
        CheckChainQuest(questID);
    }

    //check chain quests
    void CheckChainQuest(int QuestID)
    {
        int tmpID = 0;

        for(int i = 0; i < questListe.Count; i++)
        {
            if(questListe[i].id == QuestID && questListe[i].nextQuest > 0)
            {
                tmpID = questListe[i].nextQuest;
            }
        }

        if(tmpID > 0)
        {
            for (int i = 0; i < questListe.Count; i++)
            {
                if (questListe[i].id == tmpID && questListe[i].progress == Quest.QuestProgress.NOT_AVAILABLE)
                {
                    questListe[i].progress = Quest.QuestProgress.AVAILABLE;
                    QuestUIManager.uiManager.imgQuestAvailable.SetActive(true);
                }
            }
        }
    }

    public void AddQuestItem(string questObjective, int itemAmount)
    {

        for (int i=0; i<currentQuestList.Count; i++)
        {
            if(currentQuestList[i].questObjective == questObjective && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].questObjectiveCount += itemAmount;
            }


            if((currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequirement) && (currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED))
            {
                currentQuestList[i].progress = Quest.QuestProgress.COMPLETED;
                StartCoroutine("ShowQuestAccomplished");
            }
        }
    }




    public bool RequestAvailableQuest(int questID)
    {
        for(int i=0; i<questListe.Count; i++)
        {
            if(questListe[i].id == questID && questListe[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questListe.Count; i++)
        {
            if (questListe[i].id == questID && questListe[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questListe.Count; i++)
        {
            if (questListe[i].id == questID && questListe[i].progress == Quest.QuestProgress.COMPLETED)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestDoneQuest(int questID)
    {
        for (int i = 0; i < questListe.Count; i++)
        {
            if (questListe[i].id == questID && questListe[i].progress == Quest.QuestProgress.DONE)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckAvailableQuests(QuestObject AquestObject)
    {
        for (int i = 0; i < questListe.Count; i++)
        {
            for (int j = 0; j < AquestObject.availableQuestIDs.Count; j++)
            {
                if (questListe[i].id == AquestObject.availableQuestIDs[j] && questListe[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckAcceptedQuests(QuestObject AquestObject)
    {
        for (int i = 0; i < questListe.Count; i++)
        {
            for (int j = 0; j < AquestObject.receivableQuestIDs.Count; j++)
            {
                if (questListe[i].id == AquestObject.receivableQuestIDs[j] && questListe[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckCompletedQuests(QuestObject AquestObject)
    {
        for (int i = 0; i < questListe.Count; i++)
        {
            for (int j = 0; j < AquestObject.receivableQuestIDs.Count; j++)
            {
                if (questListe[i].id == AquestObject.receivableQuestIDs[j] && questListe[i].progress == Quest.QuestProgress.COMPLETED)
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator ShowQuestAccomplished()
    {
        questAccomplished.SetActive(true);
        yield return new WaitForSeconds(2);
        questAccomplished.SetActive(false);
    }

}
