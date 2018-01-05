using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour {

    private bool inTrigger = false;

    public List<int> availableQuestIDs = new List<int>();
    public List<int> receivableQuestIDs = new List<int>();
    

    public GameObject QuestMarker;
    public Image image;

    public Sprite questAvailable;
    public Sprite questAccepted;
    public Sprite questReceivable;

    public void setQuestMaker()
    {
        if(QuestManager.questManager.CheckCompletedQuests(this))
        {
            QuestMarker.SetActive(true);
            image.sprite = questReceivable;
        }
        else if(QuestManager.questManager.CheckAvailableQuests(this))
        {
            QuestMarker.SetActive(true);
            image.sprite = questAvailable;
        }
        else if (QuestManager.questManager.CheckAcceptedQuests(this))
        {
            QuestMarker.SetActive(true);
            image.sprite = questAccepted;
        }
        else
        {
            QuestMarker.SetActive(false);
            //gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        setQuestMaker();
        if (inTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            QuestUIManager.uiManager.CheckQuests(this);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            inTrigger = false;
        }

    }
}
