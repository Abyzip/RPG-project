using System.Collections;
using UnityEngine;

public class col2 : MonoBehaviour {
    public int questID;

    void OnTriggerEnter(Collider other)
    {
        QuestManager.questManager.AddQuestItem("Champignons", 1);
        if (QuestManager.questManager.RequestCompleteQuest(questID))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
