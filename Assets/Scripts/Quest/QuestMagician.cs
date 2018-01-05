using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMagician : MonoBehaviour {
    public int questID;

    // Update is called once per frame
    void Update () {
        if (QuestManager.questManager.RequestAcceptedQuest(questID))
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            QuestManager.questManager.AddQuestItem("Champignons", 1);
            Destroy(gameObject);
        }
    }
}
