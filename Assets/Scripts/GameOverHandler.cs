using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour {

    public int WoodAmount;
    public int StoneAmount;
    public int FoodAmount;
    public bool Failed;
    
    // Use this for initialization
    void Start () {
        
	}

    public void DeathByFood(int pFood)
    {
        if(pFood == 0)
        {
            Failed = true;
            LoadDeathScene();
        }
    }

    public void LoadDeathScene()
    {
       SceneManager.LoadScene("Scenes/GameOver");
    }

}
