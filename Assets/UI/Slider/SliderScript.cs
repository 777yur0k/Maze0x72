using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SliderScript : MonoBehaviour
{
    public Slider Slider;
    public TMP_Text SliderText;

    void Update() => SliderText.text = Slider.value.ToString();
}