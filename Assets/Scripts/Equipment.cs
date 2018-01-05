using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Equipment : ObjectGame{
	
	public string name;
	public int degats;
	public int health;
	public int criticalStrike;
	public string leftOrRight;

	public Equipment(string leftOrRight, GameObject modele, Texture image, string name, int degats, int health,  int criticalStrike) {

		this.leftOrRight = leftOrRight;
		this.modele = modele;
		this.image = image;
		this.name=name;
		this.degats=degats;
		this.health = health;
		this.criticalStrike=criticalStrike;
	}

    public string toString()
    {

        
        return "<color=yellow>"+name+"</color> ( <i>"+leftOrRight+"</i>)<br>"+
            "dgt : <color=green>"+degats+"</color><br>"+
            "pv : <color=green>" + health + "</color><br>" +
            "crit : <color=green>+" + criticalStrike + "%</color>";
    }
}
