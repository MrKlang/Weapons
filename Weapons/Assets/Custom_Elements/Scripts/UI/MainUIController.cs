using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private WeaponsPanel _weaponsPanel;

    private AssetReferenceSprite _currentlyReferencedSprite = new AssetReferenceSprite("");

    private void Start()
    {
        _weaponsPanel.DisableCanvas();
    }

    public void UpdateWeaponsPanel(string name, AssetReferenceSprite weaponIcon)
    {
        if (!_weaponsPanel.IsCanvasEnabled())
        {
            _weaponsPanel.EnableCanvas();
        }

        if (_currentlyReferencedSprite.RuntimeKeyIsValid())
        {
            _currentlyReferencedSprite.ReleaseAsset();
        }

        AsyncOperationHandle<Sprite> SpriteHandle = weaponIcon.LoadAssetAsync();
        SpriteHandle.Completed += SpriteLoadCompleted;

        _weaponsPanel.weaponNameLabel.text = name;
        _currentlyReferencedSprite = weaponIcon;
    }

    private void SpriteLoadCompleted(AsyncOperationHandle<Sprite> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _weaponsPanel.weaponIcon.sprite = handle.Result;
        }
    }
}
