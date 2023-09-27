using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameUI : MonoBehaviour
{
	public interface IGameUIComponent
	{
		void Init(CarEntity entity);
	}

	public CanvasGroup fader;
	public Animator introAnimator;
	public Animator countdownAnimator;
	public Animator itemAnimator;
	public GameObject timesContainer;
	public GameObject coinCountContainer;
	public GameObject lapCountContainer;
	public GameObject pickupContainer;
	public EndRaceUI endRaceScreen;
	public Image pickupDisplay;
	public Image boostBar;
	public Text coinCount;
	public Text lapCount;
	public Text raceTimeText;
	public Text[] lapTimeTexts;
	public Text introGameModeText;
	public Text introTrackNameText;
	public Button continueEndButton;
	private bool _startedCountdown;

	public CarEntity Car { get; private set; }
	private CarController CarController => Car.Controller;

	public void Init(CarEntity car)
	{
		Car = car;

		var uis = GetComponentsInChildren<IGameUIComponent>(true);
		foreach (var ui in uis) ui.Init(car);

		var track = Track.Current;

		if (track == null)
			Debug.LogWarning($"You need to initialize the GameUI on a track for track-specific values to be updated!");
		else
		{
			introGameModeText.text = GameManager.Instance.GameType.modeName;
			introTrackNameText.text = track.definition.trackName;
		}

		GameType gameType = GameManager.Instance.GameType;

		if (gameType.IsPracticeMode())
		{
			timesContainer.SetActive(false);
			lapCountContainer.SetActive(false);
		}

		continueEndButton.gameObject.SetActive(car.Object.HasStateAuthority);
	}

	private void OnDestroy()
	{
		
	}
	
	public void FinishCountdown()
	{
		// Kart.OnRaceStart();
	}

	public void HideIntro()
	{
		introAnimator.SetTrigger("Exit");
	}

	private void FadeIn()
	{
		StartCoroutine(FadeInRoutine());
	}

	private IEnumerator FadeInRoutine()
	{
		float t = 1;
		while (t > 0)
		{
			fader.alpha = 1 - t;
			t -= Time.deltaTime;
			yield return null;
		}
	}

	private void Update()
	{
		if (!_startedCountdown && Track.Current != null && Track.Current.StartRaceTimer.IsRunning)
		{
			var remainingTime = Track.Current.StartRaceTimer.RemainingTime(Car.Runner);
			if (remainingTime != null && remainingTime <= 3.0f)
			{
				_startedCountdown = true;
				HideIntro();
				FadeIn();
				countdownAnimator.SetTrigger("StartCountdown");
			}
		}

		UpdateBoostBar();

		var controller = Car.Controller;
		if (controller.BoostTime > 0f)
		{
			if (controller.BoostTierIndex == -1) return;

			Color color = controller.driftTiers[controller.BoostTierIndex].color;
			SetBoostBarColor(color);
		}
		else
		{
			if (!controller.IsDrifting) return;

			SetBoostBarColor(controller.DriftTierIndex < controller.driftTiers.Length - 1
				? controller.driftTiers[controller.DriftTierIndex + 1].color
				: controller.driftTiers[controller.DriftTierIndex].color);
		}
	}

	private void UpdateBoostBar()
	{
		if (!CarController.Object || !CarController.Object.IsValid)
			return;
		
		var driftIndex = CarController.DriftTierIndex;
		var boostIndex = CarController.BoostTierIndex;

		if (CarController.IsDrifting)
		{
			if (driftIndex < CarController.driftTiers.Length - 1)
				SetBoostBar((CarController.DriftTime - CarController.driftTiers[driftIndex].startTime) /
				            (CarController.driftTiers[driftIndex + 1].startTime - CarController.driftTiers[driftIndex].startTime));
			else
				SetBoostBar(1);
		}
		else
		{
			SetBoostBar(boostIndex == -1
				? 0f
				: CarController.BoostTime / CarController.driftTiers[boostIndex].boostDuration);
		}
	}

	public void SetBoostBar(float amount)
	{
		boostBar.fillAmount = amount;
	}

	public void SetBoostBarColor(Color color)
	{
		boostBar.color = color;
	}
	
	public void ShowEndRaceScreen()
	{
		endRaceScreen.gameObject.SetActive(true);
	}

	// UI Hook
	public void OpenPauseMenu()
	{
		InterfaceManager.Instance.OpenPauseMenu();
	}
}