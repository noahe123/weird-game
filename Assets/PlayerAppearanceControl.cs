using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAppearanceControl : MonoBehaviour
{
    public PlayerAppearance bodyPartScaler; // Reference to the BodyPartScaler script
    public int bodyPartIndex; // Index of the body part you want to control

    private Slider slider;
    private Dropdown dropdown;

    private void Start()
    {
        if (GetComponent<Slider>() != null)
        {
            slider = GetComponent<Slider>();
        }
        else if (GetComponent<Dropdown>() != null)
        {
            dropdown = GetComponent<Dropdown>();
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
            switch(dropdown.value)
            {
                case 0:
                    bodyPartScaler.AdjustBodyPart(bodyPartIndex, new UnityEngine.Color(80, 180, 255));
                    break;
                case 1:
                    bodyPartScaler.AdjustBodyPart(bodyPartIndex, new UnityEngine.Color(90, 50, 20));
                    break;
                case 2:
                    bodyPartScaler.AdjustBodyPart(bodyPartIndex, new UnityEngine.Color(255, 80, 80));
                    break;
            }
        }
    }
}
