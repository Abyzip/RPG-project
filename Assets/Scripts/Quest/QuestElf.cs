using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestElf : MonoBehaviour {
    public int questID;

    void Update()
    {
        if (QuestManager.questManager.RequestAcceptedQuest(questID))
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        if (QuestManager.questManager.RequestCompleteQuest(questID))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        if(gameObject.GetComponent<IAScript>().currentHealth < 0)
        {
            QuestManager.questManager.AddQuestItem("Chef de tribu tue", 1);
        }

        
    }


}
