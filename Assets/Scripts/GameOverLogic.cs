using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverLogic : MonoBehaviour {

    public Text WoodText;
    public Text StoneText;
    public Text FailedText;
    public Text PopulationText;
    public Text FoodText;
    //private GameOverHandler gameOverHandler;
    // Use this for initialization
    void Start () {
        //gameOverHandler = GameObject.FindGameObjectWithTag("MapManager").GetComponent<GameOverHandler>();
        FailedText.text = "Success? : " + GameOverHandler.victory;
        WoodText.text = "Wood : " + GameOverHandler.WoodAmount;
        StoneText.text = "Stone : " + GameOverHandler.StoneAmount;
        FoodText.text = "Food : " + GameOverHandler.FoodAmount;
        PopulationText.text = "Population : " + GameOverHandler.PopulationAmount;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
