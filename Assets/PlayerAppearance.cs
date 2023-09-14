using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAppearance : MonoBehaviour
{
    [Serializable]
    public class BodyPartScaleSettings
    {
        public Transform bodyPart;
        public Vector3 minScale;
        public Vector3 maxScale;
        public string bodyPartName;
    }

    public BodyPartScaleSettings[] bodyPartsToScale;
    public GameObject sliderPrefab;
    public Transform sliderParent; // Reference to the Vertical Layout Group where sliders should be created

    private void Start()
    {
        CreateSliders();
    }

    public void ScaleBodyPart(int bodyPartIndex, float scaleAmount)
    {
        if (bodyPartIndex >= 0 && bodyPartIndex < bodyPartsToScale.Length)
        {
            BodyPartScaleSettings settings = bodyPartsToScale[bodyPartIndex];
            Vector3 newScale = Vector3.Lerp(settings.minScale, settings.maxScale, scaleAmount);
            settings.bodyPart.localScale = newScale;
        }
        else
        {
            Debug.LogError("Invalid body part index: " + bodyPartIndex);
        }
    }

    private void CreateSliders()
    {
        foreach (BodyPartScaleSettings settings in bodyPartsToScale)
        {
            GameObject sliderObject = Instantiate(sliderPrefab, sliderParent);
            Slider slider = sliderObject.GetComponent<Slider>();
            PlayerAppearanceControl appearanceControl = slider.GetComponent<PlayerAppearanceControl>();

            if (slider != null && appearanceControl != null)
            {
                appearanceControl.bodyPartIndex = Array.IndexOf(bodyPartsToScale, settings);

                //set body part name
                slider.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = settings.bodyPartName;

                slider.onValueChanged.AddListener((value) => ScaleBodyPart(appearanceControl.bodyPartIndex, value));
            }
            else
            {
                Debug.LogError("Slider prefab is missing Slider or PlayerAppearanceControl component.");
            }
        }
    }
}
