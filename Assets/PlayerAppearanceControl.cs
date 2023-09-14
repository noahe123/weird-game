using UnityEngine;
using UnityEngine.UI;

public class PlayerAppearanceControl : MonoBehaviour
{
    public PlayerAppearance bodyPartScaler; // Reference to the BodyPartScaler script
    public int bodyPartIndex; // Index of the body part you want to control

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void OnSliderValueChanged()
    {
        // Ensure you have a reference to the BodyPartScaler script and a valid body part index
        if (bodyPartScaler != null && bodyPartIndex >= 0 && bodyPartIndex < bodyPartScaler.bodyPartsToScale.Length)
        {
            bodyPartScaler.ScaleBodyPart(bodyPartIndex, slider.value);
        }
    }
}
