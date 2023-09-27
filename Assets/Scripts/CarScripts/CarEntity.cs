using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class CarEntity : CarComponent
{
    public static event Action<CarEntity> OnCarSpawned;
    public static event Action<CarEntity> OnCarDespawned;
    public CarCamera Camera { get; private set; }
    public CarController Controller { get; private set; }
    public CarInput Input { get; private set; }
    public GameUI Hud { get; private set; }
    public NetworkRigidbody Rigidbody { get; private set; }
    
    private bool _despawned;

    private void Awake()
    {
        // Set references before initializing all components
        Camera = GetComponent<CarCamera>();
        Controller = GetComponent<CarController>();
        Input = GetComponent<CarInput>();
        Rigidbody = GetComponent<NetworkRigidbody>();

        // Initializes all KartComponents on or under the Kart prefab
        var components = GetComponentsInChildren<CarComponent>();
        foreach (var component in components) component.Init(this);
    }

    public static readonly List<CarEntity> Cars = new List<CarEntity>();

    public override void Spawned()
    {
        base.Spawned();

        if (Object.HasInputAuthority)
        {
            // Create HUD
            Hud = Instantiate(ResourceManager.Instance.hudPrefab);
            Hud.Init(this);

            Instantiate(ResourceManager.Instance.nicknameCanvasPrefab);
        }

        Cars.Add(this);
        OnCarSpawned?.Invoke(this);
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        base.Despawned(runner, hasState);
        Cars.Remove(this);
        _despawned = true;
        OnCarDespawned?.Invoke(this);
    }

    private void OnDestroy()
    {
        Cars.Remove(this);
        if (!_despawned)
        {
            OnCarDespawned?.Invoke(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out ICollidable collidable))
        {
            collidable.Collide(this);
        }
    }

    public void SpinOut()
    {
        Controller.IsSpinout = true;
    }

    private IEnumerable OnSpinOut()
    {
        yield return new WaitForSeconds(2f);

        Controller.IsSpinout = false;
    }
}
