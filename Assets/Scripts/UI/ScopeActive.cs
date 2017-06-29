using UnityEngine;
using UnityEngine.UI;

public class ScopeActive : MonoBehaviour
{
    public Image scope;

    void Start()
    {
        scope = GetComponent<Image>();
    }

    public void Active(bool action)
    {
        scope.enabled = action;
    }
}