using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class EndRaceUI : MonoBehaviour, GameUI.IGameUIComponent, IDisabledUI
{
  public PlayerResultItem resultItemPrefab;
	public Button continueEndButton;
	public GameObject resultsContainer;

	private CarEntity _car;

	private const float DELAY = 4;
	public void Init(CarEntity entity)
	{
		_car = entity;
		continueEndButton.onClick.AddListener(() => LevelManager.LoadMenu());
	}

	public void Setup()
	{
		CarEntity.OnCarSpawned += RedrawResultsList;
		CarEntity.OnCarDespawned += RedrawResultsList;
	}

	public void OnDestruction()
	{
		CarEntity.OnCarSpawned -= RedrawResultsList;
		CarEntity.OnCarDespawned -= RedrawResultsList;
	}

	public void RedrawResultsList(CarComponent updated)
	{
		var parent = resultsContainer.transform;
		ClearParent(parent);

		/*
		var karts = GetFinishedKarts();
		for (var i = 0; i < karts.Count; i++)
		{
			var kart = karts[i];

			Instantiate(resultItemPrefab, parent)
				.SetResult(kart.Controller.RoomUser.Username.Value, kart.LapController.GetTotalRaceTime(), i + 1);
		}

		EnsureContinueButton(karts);
		*/
	}

    private void EnsureContinueButton(List<CarEntity> karts)
	{
        var allFinished = karts.Count == CarEntity.Cars.Count;
		if (RoomPlayer.Local.IsLeader) {
            continueEndButton.gameObject.SetActive(allFinished);
        }
    }

	private static void ClearParent(Transform parent)
	{
		var len = parent.childCount;
		for (var i = 0; i < len; i++)
		{
			Destroy(parent.GetChild(i).gameObject);
		}
	}
}