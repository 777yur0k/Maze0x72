using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class DropDownScript : MonoBehaviour
{
    public Color DefaultColor = Color.white, SelectColor = Color.yellow;
    public int CountIntems, id;
    public GameObject PrefabItem, Template;
    public RectTransform Parent;
    public List<DropDownItem> ItemsScripts;
    public List<GameObject> Items;
    public TMP_Text MainLable;
    public UnityAction UnityAction;

    public void GenerateOptions(string[] options, string selected)
    {
        for (var k = 0; k < Items.Count; k++) Destroy(Items[k]);

        Items.Clear();
        ItemsScripts.Clear();

        CountIntems = options.Length;

        for (var i = 0; i < CountIntems; i++)
        {
            Items.Add(Instantiate(PrefabItem, Parent));
            ItemsScripts.Add(Items[i].GetComponent<DropDownItem>());
            ItemsScripts[i].SetItem(i, this, options[i]);
            ItemsScripts[i].SetColor(DefaultColor);
            if (options[i] == selected) ItemsScripts[i].SetColor(SelectColor);
        }

        ChangeSize(Template.GetComponent<RectTransform>());
    }

    void ChangeSize(RectTransform Objetc)
    {
        var rectPrefab = PrefabItem.GetComponent<RectTransform>().rect;
        /*if (Objetc.sizeDelta.y < rectPrefab.height * CountIntems)*/
        Objetc.sizeDelta = new(Objetc.sizeDelta.x, (rectPrefab.height * CountIntems) + Parent.GetComponent<VerticalLayoutGroup>().spacing * CountIntems / 2);
        Objetc.localPosition = new(0, -72.5f, 0);
    }

    public void Select(int variable)
    {
        id = variable;
        MainLable.text = ItemsScripts[id].Label.text;

        for (var i = 0; i < CountIntems; i++) ItemsScripts[i].SetColor(DefaultColor);

        ItemsScripts[id].SetColor(SelectColor);
        Interact();
        UnityAction.Invoke();
    }

    public void Interact()
    {
        MyObject.ChangeActive(Template);
        GetComponent<Button>().interactable = !GetComponent<Button>().interactable;
    }
}