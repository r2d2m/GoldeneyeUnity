using UnityEngine;
using UnityEngine.UI;

public class AmmoSetter : MonoBehaviour
{
    public Text ammo;

    void Start()
    {
        ammo = GetComponent<Text>();
    }

    public void RefreshData(int newData)
    {
        string newText = newData.ToString();
        ammo.text = newText;
    }
}
