using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }

    public Slider explvlSlider;
    public TMP_Text expLvLText;

    public LevelUpSelectionButton[] levelUpButtons;

    public GameObject levelUpPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateExperience(int currentExp, int levelExp, int currentLvL)
    {
        explvlSlider.maxValue = levelExp;
        explvlSlider.value = currentExp;

        expLvLText.text = "Level: " + currentLvL;
    }
}
