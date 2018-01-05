using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryController : MonoBehaviour {
	public int nbSlot = 20;
	public ObjectGame[] inventory;
	public GameObject[] slotList;
	public GameObject inventoryGO;
    public GameObject full;
    // Use this for initialization
    void Start () {
		inventory = new Equipment[nbSlot];
		//slotList = new GameObject[nbSlot];
		for (int i = 0; i < nbSlot-1; i++) {
				
				Button btn=slotList [i].GetComponent<Button> ();
			int i2 = i;
		
			btn.onClick.AddListener (delegate{TaskOnClick(i2);});
        }

		}





	public void UpdateInventoryUI () {
		for (int i = 0; i < nbSlot; i++) {
			if (inventory [i]!= null) {
				slotList [i].transform.GetChild (0).GetComponent<RawImage>().enabled = true;
				slotList [i].transform.GetChild (0).GetComponent<RawImage>().texture = inventory [i].image;
			} else {
				slotList [i].transform.GetChild (0).GetComponent<RawImage>().enabled = false;
			}
		}
	}

	public bool AddEquipment(Equipment eq) {
		for (int i = 0; i < nbSlot; i++) {

			if (inventory [i]== null) {
				
				inventory [i] = eq;
				GameObject go=Instantiate(eq.modele);
				go.transform.parent = inventoryGO.transform;
				go.transform.localPosition = new Vector3 (0, 0, 0);
				go.transform.localRotation = Quaternion.Euler (-90, 0, 0);
				eq.modele = go;
                UpdateInventoryUI();

                return true;
			} 
		}
        UpdateInventoryUI();
        StartCoroutine("Showfull");
        return false;
	}

	public void Replace(int i, string leftOrRight) {
		Equipment eq;
		if (leftOrRight == "right") {
			eq = GetComponent<EquipmentsPlayer> ().RArm;
		}
		else {
			eq = GetComponent<EquipmentsPlayer> ().LArm;
		}

		inventory [i] = eq;
		UpdateInventoryUI ();
	}

    public void DestroyObject(int i)
    {
        Destroy(inventory[i].modele);
        inventory[i] = null;
        UpdateInventoryUI();
    }


    public void TaskOnClick(int i) {
        if (inventory[i] != null)
        {

            if (inventory[i].GetType() == typeof(Equipment))
            {
                Equipment eq = ((Equipment)inventory[i]);
                Replace(i, ((Equipment)inventory[i]).leftOrRight);
                GetComponent<EquipmentsPlayer>().Equiper(eq, eq.leftOrRight);

            }
        }
			
	}

    IEnumerator Showfull()
    {
        full.SetActive(true);
        yield return new WaitForSeconds(2);
        full.SetActive(false);
    }

}
