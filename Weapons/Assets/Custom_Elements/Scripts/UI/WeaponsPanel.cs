using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsPanel : MonoBehaviour
{
    [SerializeField] private Canvas _weaponsPanelCanvas;

    public Image weaponIcon;
    public TextMeshProUGUI weaponNameLabel;

    public void EnableCanvas()
    {
        _weaponsPanelCanvas.enabled = true;
    }

    public void DisableCanvas()
    {
        _weaponsPanelCanvas.enabled = false;
    }

    public bool IsCanvasEnabled()
    {
        return _weaponsPanelCanvas.enabled;
    }
}
