using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAppearance : MonoBehaviour
{
    [Serializable]
    public class BodyPartSettings
    {
        public Transform bodyPart;
        public string bodyPartName;
        public BodyPartType partType;
        public Vector3 minScale; // Minimum scale values
        public Vector3 maxScale; // Maximum scale values
        public Color minColor; // Minimum color values
        public Color maxColor; // Maximum color values
        public int materialIndex;
    }

    public List<string> colorOptions = new List<string>() {};
    public List<Color> colorList = new List<Color>() {};





    public enum BodyPartType
    {
        Scale,
        Color,
        // Add more types as needed
    }

    public BodyPartSettings[] bodyPartsToAdjust;
    public GameObject sliderPrefab, dropdownPrefab;
    public Transform sliderParent;

    private void Start()
    {
        CreateSliders();


    }

    public void AdjustBodyPart(int bodyPartIndex, float value)
    {
        if (bodyPartIndex >= 0 && bodyPartIndex < bodyPartsToAdjust.Length)
        {
            BodyPartSettings settings = bodyPartsToAdjust[bodyPartIndex];

            switch (settings.partType)
            {
                case BodyPartType.Scale:
                    Vector3 newScale = Vector3.Lerp(settings.minScale, settings.maxScale, value);
                    settings.bodyPart.localScale = newScale;
                    break;

                case BodyPartType.Color:
                    Renderer renderer = settings.bodyPart.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        Color newColor = Color.Lerp(settings.minColor, settings.maxColor, value);
                        renderer.materials[2].color = newColor;
                    }
                    else
                    {
                        Debug.LogError("Body part does not have a Renderer component.");
                    }
                    break;

                // Add more cases for other parameter adjustments as needed

                default:
                    Debug.LogError("Unsupported body part type: " + settings.partType);
                    break;
            }
        }
        else
        {
            Debug.LogError("Invalid body part index: " + bodyPartIndex);
        }
    }

    public void AdjustBodyPart(int bodyPartIndex, Color color)
    {
        if (bodyPartIndex >= 0 && bodyPartIndex < bodyPartsToAdjust.Length)
        {
            BodyPartSettings settings = bodyPartsToAdjust[bodyPartIndex];

            switch (settings.partType)
            {
                case BodyPartType.Color:
                    Renderer renderer = settings.bodyPart.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.materials[settings.materialIndex].color = color;
                    }
                    else
                    {
                        Debug.LogError("Body part does not have a Renderer component.");
                    }
                    break;

                // Add more cases for other parameter adjustments as needed

                default:
                    Debug.LogError("Unsupported body part type: " + settings.partType);
                    break;
            }
        }
        else
        {
            Debug.LogError("Invalid body part index: " + bodyPartIndex);
        }
    }

    private void CreateSliders()
    {
        foreach (BodyPartSettings settings in bodyPartsToAdjust)
        {
            if (settings.partType == BodyPartType.Scale)
            {
                GameObject sliderObject = Instantiate(sliderPrefab, sliderParent);
                Slider slider = sliderObject.GetComponent<Slider>();
                PlayerAppearanceControl appearanceControl = sliderObject.GetComponent<PlayerAppearanceControl>();

                if (slider != null && appearanceControl != null)
                {
                    appearanceControl.bodyPartIndex = Array.IndexOf(bodyPartsToAdjust, settings);

                    slider.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = settings.bodyPartName;

                    slider.onValueChanged.AddListener((value) => AdjustBodyPart(appearanceControl.bodyPartIndex, value));
                }
                else
                {
                    Debug.LogError("Slider prefab is missing Slider or PlayerAppearanceControl component.");
                }
            }
            else if (settings.partType == BodyPartType.Color)
            {
                GameObject dropdownObject = Instantiate(dropdownPrefab, sliderParent);
                TMP_Dropdown dropdown = dropdownObject.GetComponent<TMP_Dropdown>();
                PlayerAppearanceControl appearanceControl = dropdownObject.GetComponent<PlayerAppearanceControl>();



                if (dropdown != null && appearanceControl != null)
                {
                    appearanceControl.bodyPartIndex = Array.IndexOf(bodyPartsToAdjust, settings);

                    dropdown.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = settings.bodyPartName;

                    dropdown.options.Clear();
                    
                    TMP_Text text = dropdown.GetComponent<TMP_Text>();

                    appearanceControl.myColorList = colorList;

                    foreach (string t in colorOptions)
                    {
                        dropdown.options.Add(new TMP_Dropdown.OptionData() { text = t });
                    }
                    //dropdown.onValueChanged.AddListener((value) => AdjustBodyPart(appearanceControl.bodyPartIndex, value));
                }
                else
                {
                    Debug.LogError("Dropdown prefab is missing dropdown or PlayerAppearanceControl component.");
                }
            }
        }
    }
}
