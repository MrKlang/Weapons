using System;
using UnityEngine;

public class MainController : GenericSingleton<MainController>
{
    [SerializeField] private WeaponsDataLibrary _weaponsLibrary;

    public static Action OnUpdate;

    public WeaponsDataLibrary WeaponsLibrary => _weaponsLibrary;

    private void Awake()
    {
        OnAwake();
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }
}
