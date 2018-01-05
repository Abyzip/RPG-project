using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStats : MonoBehaviour {

    public GameObject AttaqueText;
    public GameObject HealthText;
    public GameObject CriticsText;

    public GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update () {
        AttaqueText.GetComponent<TMPro.TextMeshProUGUI>().SetText("<b>Attaque</b> : " + (Player.GetComponent<EquipmentsPlayer>().dgtLeft + Player.GetComponent<EquipmentsPlayer>().dgtRight));
        HealthText.GetComponent<TMPro.TextMeshProUGUI>().SetText("<b>PV Max</b> : " + Player.GetComponent<HealthUIScript>().maxHealth);
        CriticsText.GetComponent<TMPro.TextMeshProUGUI>().SetText("<b>Critique</b> : " + Player.GetComponent<EquipmentsPlayer>().criticalStrike);
    }
}
