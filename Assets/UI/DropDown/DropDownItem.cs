using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropDownItem : MonoBehaviour
{
    public DropDownScript DropDown;
    public TMP_Text Label;
    public Image Image;
    public int OwnID;

    public void Choice() => DropDown.Select(OwnID);

    public void SetColor(Color color) => Image.color = color;

    public void SetItem(int id, DropDownScript parent, string label)
    {
        OwnID = id;
        DropDown = parent;
        Label.text = label;
    }
}