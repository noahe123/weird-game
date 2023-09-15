using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAppearanceControl : MonoBehaviour
{
    public PlayerAppearance bodyPartScaler; // Reference to the BodyPartScaler script
    public int bodyPartIndex; // Index of the body part you want to control

    private Slider slider;
    private TMP_Dropdown dropdown;

    public List<UnityEngine.Color> myColorList = new List<UnityEngine.Color>() { };


    private void Start()
    {
        if (GetComponent<Slider>() != null)
        {
            slider = GetComponent<Slider>();
        }
        else if (GetComponent<TMP_Dropdown>() != null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }

        bodyPartScaler = FindObjectOfType<PlayerAppearance>();
    }

    public void OnSliderValueChanged()
    {
        // Ensure you have a reference to the BodyPartScaler script and a valid body part index
        if (bodyPartScaler != null && bodyPartIndex >= 0 && bodyPartIndex < bodyPartScaler.bodyPartsToAdjust.Length)
        {
            bodyPartScaler.AdjustBodyPart(bodyPartIndex, slider.value);
        }
    }

    public void OnDropdownValueChanged()
    {
        // Ensure you have a reference to the BodyPartScaler script and a valid body part index
        if (bodyPartScaler != null && bodyPartIndex >= 0 && bodyPartIndex < bodyPartScaler.bodyPartsToAdjust.Length)
        {

            //blue
            bodyPartScaler.AdjustBodyPart(bodyPartIndex, myColorList[dropdown.value]);

        }
    }
}
