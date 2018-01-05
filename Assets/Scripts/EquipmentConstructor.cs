using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentConstructor : MonoBehaviour {
	public GameObject Axe1;
	public GameObject Axe2;
	public GameObject Axe3;
	public GameObject Club1;
	public GameObject Hammer1;
	public GameObject Hammer2;
	public GameObject PeasantTool1;
	public GameObject PeasantTool2;
	public GameObject PeasantTool3;
	public GameObject Sword1;
	public GameObject Sword2;
	public GameObject Sword3;
	public GameObject Sword4;
	public GameObject Sword5;
	public GameObject Sword6;
	public GameObject Sword7;

	public Texture imgAxe1;
	public Texture imgAxe2;
	public Texture imgAxe3;
	public Texture imgClub1;
	public Texture imgHammer1;
	public Texture imgHammer2;
	public Texture imgPeasantTool1;
	public Texture imgPeasantTool2;
	public Texture imgPeasantTool3;
	public Texture imgSword1;
	public Texture imgSword2;
	public Texture imgSword3;
	public Texture imgSword4;
	public Texture imgSword5;
	public Texture imgSword6;
	public Texture imgSword7;


	public Equipment getEquipment(string name) {
		switch(name) {
		//Hand,modele, img, name,dgt, health, crit
		case "Hache de bucheron":
			return new Equipment ("left",Axe1, imgAxe1, "Hache de bucheron", 30, 50, 10);
			break;
		case "Hache de garde":
			return new Equipment ("left", Axe2, imgAxe2, "Hache de garde", 50, 150, 20);
			break;
		case "Hache de destruction":
			return new Equipment ("right", Axe3, imgAxe3, "Hache de destruction", 60, 120, 40);
			break;
		case "Masse de barbare":
			return new Equipment ("right", Club1, imgClub1, "Masse de barbare", 40, 80, 10);
			break;
		case "Marteau des enfers":
			return new Equipment ("left", Hammer1, imgHammer1, "Marteau des enfers", 60, 170, 10);
			break;
		case "Marteau de la justice":
			return new Equipment ("right", Hammer2, imgHammer2, "Marteau de la justice", 30, 80, 0);
			break;
		case "Masse de paysan":
			return new Equipment ("left", PeasantTool1, imgPeasantTool1, "Masse du paysan", 10, 70, 0);
			break;
		case "Hache de paysan":
			return new Equipment ("right", PeasantTool2, imgPeasantTool2, "Hache de paysan", 20, 50, 0);
			break;
		case "Faucille de paysan":
			return new Equipment ("left", PeasantTool3, imgPeasantTool3, "Faucille de paysan", 10, 50, 10);
			break;
		case "Epée d'apprenti":
			return new Equipment ("right", Sword1, imgSword1, "Epée d'apprenti", 30, 50, 10);
			break;
		case "Epée de démon":
			return new Equipment ("left", Sword2, imgSword2, "Epée de démon", 40, 60, 20);
			break;
		case "Epée de chevalier":
			return new Equipment ("right", Sword3, imgSword3, "Epée de chevalier", 30, 60, 10);
			break;
		case "Epée de titan":
			return new Equipment ("left", Sword4, imgSword4, "Epée de titan", 40, 70, 20);
			break;
		case "Epée d'emeraude":
			return new Equipment ("right", Sword5, imgSword5, "Epée d'emeraude", 30, 80, 20);
			break;
		case "Epée de rubis":
			return new Equipment ("left", Sword6, imgSword6, "Epée de rubis", 50, 60, 30);
			break;
		case "Epée éternelle":
			return new Equipment ("right", Sword7, imgSword7, "Epée éternelle", 70, 160, 40);
			break;





	}
		return null;
}
}
