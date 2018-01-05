using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TooltipInventoryScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject player;
    public GameObject textTooltip;
    public GameObject panelToolTip;
    void Start() {
        //player= GameObject.FindGameObjectsWithTag("player")[0];
        //textTooltip = GameObject.FindGameObjectsWithTag("textToolTip")[0];

        panelToolTip = textTooltip.transform.parent.gameObject;
    }
    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        

        int instance = int.Parse(gameObject.name.Substring(4));
        if(player.GetComponent<InventoryController>().inventory[instance]!=null) {
            panelToolTip.SetActive(true);
            //panelToolTip.transform.position=Input.mousePosition;
            //panelToolTip.transform.position = gameObject.transform.position + new Vector3(0, +5, 0);
            if (player.GetComponent<InventoryController>().inventory[instance].GetType() == typeof(Equipment))
            {
               
				textTooltip.GetComponent<TMPro.TextMeshProUGUI>().SetText(((Equipment)player.GetComponent<InventoryController>().inventory[instance]).toString());
                
                
            }
        }
           
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panelToolTip.SetActive(false);




    }
}

