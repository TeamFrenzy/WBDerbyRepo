using System;
using System.Numerics;
using Fusion;
using UnityEngine;

namespace Networking
{
    public class TestSpawner : NetworkBehaviour
    {
        public CarEntity carPrefab;

        private void Start()
        {
            throw new NotImplementedException();
        }

        public void SpawnPlayer(NetworkRunner runner, RoomPlayer player, Transform SpawnPoint)
        {
            var index = RoomPlayer.Players.IndexOf(player);
            var point = SpawnPoint;

            var prefabId = player.KartId;
            var prefab = carPrefab;

            // Spawn player
            var entity = runner.Spawn(
                prefab,
                point.position,
                point.rotation,
                player.Object.InputAuthority
            );

            entity.Controller.RoomUser = player;
            player.GameState = RoomPlayer.EGameState.GameCutscene;
            player.Kart = entity.Controller;

            Debug.Log($"Spawning kart for {player.Username} as {entity.name}");
            entity.transform.name = $"Kart ({player.Username})";
        }
        
    }
}