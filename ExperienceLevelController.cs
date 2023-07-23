using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
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
    public List<Weapon> weaponsToUpgrade;
 
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

        // UIController.instance.levelUpButtons[1].UpgradeButtonDisplay(PlayerController.instance.activeWeapon);
        //UIController.instance.levelUpButtons[0].UpgradeButtonDisplay(PlayerController.instance.assignedWeapons[0]);
        //UIController.instance.levelUpButtons[1].UpgradeButtonDisplay(PlayerController.instance.unassignedWeapons[0]);
        //UIController.instance.levelUpButtons[2].UpgradeButtonDisplay(PlayerController.instance.unassignedWeapons[1]);

        weaponsToUpgrade.Clear();

        List<Weapon> availableWeapons = new List<Weapon>();

        availableWeapons.AddRange(PlayerController.instance.assignedWeapons);

            if(availableWeapons.Count > 0)
        {
            int selected = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }
        if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLevelledWeapon.Count < PlayerController.instance.maxWeapon)
        {
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        }

        for(int i = weaponsToUpgrade.Count; i < 3; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int selected = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }
        for(int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            UIController.instance.levelUpButtons[i].UpgradeButtonDisplay(weaponsToUpgrade[i]);
        }

        for(int i = 0; i < UIController.instance.levelUpButtons.Length; i++)
        {
            if(i < weaponsToUpgrade.Count)
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
