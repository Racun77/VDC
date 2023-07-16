using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    public int currentExperience;
    public ExpPickup pickup;
    public List<int> expLevels;
    public int currentLevel = 1, levelCount = 100;
 
    void Start()
    {
       // expLevels.Add(1);
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    void Update()
    {
      
    }
    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;

        if(currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }
        UIController.instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);
    }
    public void SpawnExp(Vector3 position, int expValue) { 
    Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
    }
    void LevelUp()
    {
        currentExperience -= expLevels[currentLevel];
        
        currentLevel++;
        if(currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }
       // PlayerController.instance.activeWeapon.LevelUp(); //Auskommentiert weil das lvl nicht mehr von alleine passieren soll

        UIController.instance.levelUpPanel.SetActive(true);

        Time.timeScale = 0f; //pausiert das Spiel

        UIController.instance.levelUpButtons[1].UpgradeButtonDisplay(PlayerController.instance.activeWeapon);
    }
}
