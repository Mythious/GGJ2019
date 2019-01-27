using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour {

    public static int WoodAmount;
    public static int StoneAmount;
    public static int FoodAmount;
    public static int PopulationAmount;
    public static bool Failed = true;
    
    // Use this for initialization

    public void UpdateFood(int pFood)
    {
        FoodAmount = pFood;
    }
    public void UpdateStone(int pStone)
    {
        StoneAmount = pStone;
    }
    public void UpdateWood(int pWood)
    {
        WoodAmount = pWood;
    }
    public void UpdatePopulation(int pPopulationAmount)
    {
        PopulationAmount = pPopulationAmount;
    }

    public void DeathByFood(int pFood)
    {
        if(pFood == 0)
        {
            Failed = false;
            LoadDeathScene();
        }
    }

    public void LoadDeathScene()
    {
       SceneManager.LoadScene("Scenes/GameOver");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
