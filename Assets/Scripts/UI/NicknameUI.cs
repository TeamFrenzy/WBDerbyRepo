using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NicknameUI : MonoBehaviour
{
    public WorldUINickname nicknamePrefab;

    private readonly Dictionary<CarEntity, WorldUINickname> _kartNicknames =
        new Dictionary<CarEntity, WorldUINickname>();

    private void Awake() {
        EnsureAllTexts();

        CarEntity.OnCarSpawned += SpawnNicknameText;
        CarEntity.OnCarDespawned += DespawnNicknameText;
    }

    private void OnDestroy() {
        CarEntity.OnCarSpawned -= SpawnNicknameText;
        CarEntity.OnCarDespawned -= DespawnNicknameText;
    }

    private void EnsureAllTexts() {
        // we need to make sure that any karts that spawned before the callback was subscribed, are registered
        var karts = CarEntity.Cars;
        foreach ( var kart in karts.Where(kart => !_kartNicknames.ContainsKey(kart)) ) {
            SpawnNicknameText(kart); 
        }
    }

    private void SpawnNicknameText(CarEntity car) {
        // we dont want to see our own name tag - dont spawn
        if ( car.Object.IsValid && car.Object.HasInputAuthority )
            return;

        var obj = Instantiate(nicknamePrefab, this.transform);
        obj.SetKart(car);

        _kartNicknames.Add(car, obj);
    }

    private void DespawnNicknameText(CarEntity car) {
        if ( !_kartNicknames.ContainsKey(car) )
            return;

        var text = _kartNicknames[car];
        Destroy(text.gameObject);

        _kartNicknames.Remove(car);
    }
}
