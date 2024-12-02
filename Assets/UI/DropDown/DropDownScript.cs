using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class DropDownScript : MonoBehaviour
{
    public Color DefaultColor = Color.white, SelectColor = Color.yellow;
    public GameObject PrefabItem, Template;
    public RectTransform Parent;
    public TMP_Text MainLable;
    int CountIntems, id;
    List<DropDownItem> ItemsScripts = new();
    List<GameObject> Items = new();
    UnityAction<string> Action;

    public void Initialize(List<string> options, string selected, UnityAction<string> action)
    {
        Action = action;
        MainLable.text = selected;
        GenerateOptions(options, selected);
        ChangeSize(Template.GetComponent<RectTransform>());
    }

    void GenerateOptions(List<string> options, string selected)
    {
        for (var k = 0; k < Items.Count; k++) Destroy(Items[k]);

        Items.Clear();
        ItemsScripts.Clear();

        CountIntems = options.Count;

        for (var i = 0; i < CountIntems; i++)
        {
            Items.Add(Instantiate(PrefabItem, Parent));
            ItemsScripts.Add(Items[i].GetComponent<DropDownItem>());
            ItemsScripts[i].SetItem(i, this, options[i]);
            ItemsScripts[i].SetColor(DefaultColor);
            if (options[i] == selected) ItemsScripts[i].SetColor(SelectColor);
        }
    }

    void ChangeSize(RectTransform Objetc)
    {
        var rectPrefab = PrefabItem.GetComponent<RectTransform>().rect;
        Objetc.sizeDelta = new(Objetc.sizeDelta.x, (rectPrefab.height * CountIntems) + Parent.GetComponent<VerticalLayoutGroup>().spacing * (CountIntems - 1));
        Template.transform.localPosition = new(Template.transform.localPosition.x, -GetComponent<RectTransform>().rect.height / 2);
    }

    public void Select(int variable)
    {
        id = variable;
        MainLable.text = ItemsScripts[id].Label.text;

        for (var i = 0; i < CountIntems; i++) ItemsScripts[i].SetColor(DefaultColor);

        ItemsScripts[id].SetColor(SelectColor);
        Interact();
        Action.Invoke(ItemsScripts[id].Label.text);
    }

    public void Interact()
    {
        MyObject.ChangeActive(Template);
        GetComponent<Button>().interactable = !GetComponent<Button>().interactable;
    }
}