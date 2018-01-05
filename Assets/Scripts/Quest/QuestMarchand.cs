using System.Collections;
using UnityEngine;

public class QuestMarchand : MonoBehaviour {
    public int questID;
    public GameObject magician;
    

    private bool entered = false;

    

    void Update()
    {
        if (QuestManager.questManager.RequestAcceptedQuest(questID))
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }

        if (QuestManager.questManager.RequestCompleteQuest(questID) || QuestManager.questManager.RequestAvailableQuest(questID))
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (QuestManager.questManager.RequestDoneQuest(questID))
        {
            magician.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player" && !entered)
        {
            QuestManager.questManager.AddQuestItem("Tour", 1);
            entered = !entered;
        }
    }
}
