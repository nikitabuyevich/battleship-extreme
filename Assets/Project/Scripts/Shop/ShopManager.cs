using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShopManager : MonoBehaviour
{
	private ITurn _turn;

	[Inject]
	public void Construct(ITurn turn)
	{
		_turn = turn;
	}

	[Header("Setup")]
	public ShopUI shopUI;
	public GameSceneManager gameSceneManager;

	public void OpenShop()
	{
		var player = _turn.CurrentPlayer();
		player.ChangeState(typeof(IPlayerShopingState));
		shopUI.mainShop.SetActive(true);
	}

	private void CloseShop()
	{
		shopUI.shipShop.SetActive(false);
		shopUI.abilitiesShop.SetActive(false);
		shopUI.refineriesShop.SetActive(false);
		shopUI.mainShop.SetActive(false);
	}

	private void UpdateHealthButton(Player player)
	{
		// health
		var healthCost = Mathf.RoundToInt(ShopValues.health * Mathf.Pow(2, player.boughtAmount.health));
		shopUI.healthCost = healthCost;
		shopUI.healthCurrent = player.maxHealth;
		shopUI.HealthButton.interactable = healthCost <= player.money;
	}

	private void UpdateVisionRadiusButton(Player player)
	{
		var visionRadiusCost = Mathf.RoundToInt(ShopValues.visionRadius * Mathf.Pow(2, player.boughtAmount.visionRadius));
		shopUI.visionRadiusCost = visionRadiusCost;
		shopUI.visionRadiusCurrent = player.visionRadius;
		shopUI.VisionRadiusButton.interactable = visionRadiusCost <= player.money;
	}

	private void UpdateMovesPerTurnButton(Player player)
	{
		var movesPerTurnCost = Mathf.RoundToInt(ShopValues.movesPerTurn * Mathf.Pow(2, player.boughtAmount.movesPerTurn));
		shopUI.movesPerTurnCost = movesPerTurnCost;
		shopUI.movesPerTurnCurrent = player.numberOfMovesPerTurn;
		shopUI.MovesPerTurnButton.interactable = movesPerTurnCost <= player.money;
	}

	private void UpdateMoveAcrossButton(Player player)
	{
		if (player.boughtAmount.moveAcross)
		{
			shopUI.moveAcrossButton.interactable = false;
			shopUI.moveAcrossCostText.text = "---";
		}
		else
		{
			var moveAcrossCost = ShopValues.moveAcross;
			shopUI.moveAcrossCost = moveAcrossCost;
			shopUI.moveAcrossButton.interactable = moveAcrossCost <= player.money;
		}
	}

	public void OpenShipShop()
	{
		var player = _turn.CurrentPlayer();

		UpdateHealthButton(player);
		UpdateVisionRadiusButton(player);
		UpdateMovesPerTurnButton(player);
		UpdateMoveAcrossButton(player);

		shopUI.shipShop.SetActive(true);
	}

	private void UpdateAttackButton(Player player)
	{
		var attackCost = Mathf.RoundToInt(ShopValues.attack * Mathf.Pow(2, player.boughtAmount.attack));
		shopUI.attackPowerCost = attackCost;
		shopUI.attackPowerCurrent = player.attackPower;
		shopUI.AttackButton.interactable = attackCost <= player.money;
	}

	private void UpdateAttackRangeButton(Player player)
	{
		var attackRangeCost = Mathf.RoundToInt(ShopValues.attackRange * Mathf.Pow(2, player.boughtAmount.attackRange));
		shopUI.attackRangeCost = attackRangeCost;
		shopUI.attackRangeCurrent = player.numberOfAttackSpacesPerTurn;
		shopUI.AttackRangeButton.interactable = attackRangeCost <= player.money;
	}

	private void UpdateSideAttackButton(Player player)
	{
		var sideAttackCost = Mathf.RoundToInt(ShopValues.sideAttack * Mathf.Pow(2, player.boughtAmount.sideAttack));
		shopUI.sideAttackPowerCost = sideAttackCost;
		shopUI.sideAttackPowerCurrent = player.sideHitAttackPower;
		shopUI.SideAttackButton.interactable = sideAttackCost <= player.money;
	}

	private void UpdateSideAttackRangeButton(Player player)
	{
		var sideAttackRangeCost = Mathf.RoundToInt(ShopValues.sideAttackRange * Mathf.Pow(2, player.boughtAmount.sideAttackRange));
		shopUI.sideAttackRangeCost = sideAttackRangeCost;
		shopUI.sideAttackRangeCurrent = player.sideHitRange;
		shopUI.SideAttackRangeButton.interactable = sideAttackRangeCost <= player.money;
	}

	private void UpdateAttacksPerTurnButton(Player player)
	{
		var attacksPerTurnCost = Mathf.RoundToInt(ShopValues.attacksPerTurn * Mathf.Pow(2, player.boughtAmount.attacksPerTurn));
		shopUI.attacksPerTurnCost = attacksPerTurnCost;
		shopUI.attacksPerTurnCurrent = player.numberOfAttacksPerTurn;
		shopUI.AttacksPerTurnButton.interactable = attacksPerTurnCost <= player.money;
	}

	private void UpdateRotationsAreFreeButton(Player player)
	{
		if (player.boughtAmount.RotationsAreFree)
		{
			shopUI.rotationsAreFreeButton.interactable = false;
			shopUI.rotationsAreFreeCostText.text = "---";
		}
		else
		{
			var rotationsAreFreeCost = ShopValues.rotationsAreFree;
			shopUI.rotationsAreFreeCost = rotationsAreFreeCost;
			shopUI.rotationsAreFreeButton.interactable = rotationsAreFreeCost <= player.money;
		}
	}

	private void UpdateUnlockedAttacksButton(Player player)
	{
		var unlockedAttackCost = ShopValues.unlockedAttacks;
		shopUI.unlockedAttacksCost = unlockedAttackCost;
		shopUI.unlockAttacksButton.interactable = unlockedAttackCost <= player.money;
	}

	public void OpenAbilitiesShop()
	{
		var player = _turn.CurrentPlayer();

		if (!player.boughtAmount.unlockedAttack)
		{
			shopUI.unlockedAttacks = false;
			UpdateUnlockedAttacksButton(player);
		}
		else
		{
			UpdateAttackButton(player);
			UpdateAttackRangeButton(player);
			UpdateSideAttackButton(player);
			UpdateSideAttackRangeButton(player);
			UpdateAttacksPerTurnButton(player);
			UpdateRotationsAreFreeButton(player);

			shopUI.unlockedAttacks = true;
		}

		shopUI.abilitiesShop.SetActive(true);
	}

	private void UpdateBuildRangeButton(Player player)
	{
		var buildRangeCost = Mathf.RoundToInt(ShopValues.buildRange * Mathf.Pow(2, player.boughtAmount.buildRange));
		shopUI.buildRangeCost = buildRangeCost;
		shopUI.buildRangeCurrent = player.buildRange;
		shopUI.BuildRangeButton.interactable = buildRangeCost <= player.money;
	}

	private void UpdateLevel1RefineryButton(Player player)
	{
		var level1RefineryCost = ShopValues.level1Refinery;
		shopUI.level1RefineryCost = level1RefineryCost;
		shopUI.Level1RefineryButton.interactable = level1RefineryCost <= player.money;
		shopUI.level1RefineryHealth = ShopValues.level1RefineryHealth;
		shopUI.level1RefineryVisionRadius = ShopValues.level1RefineryVisionRadius;
	}

	private void UpdateLevel2RefineryButton(Player player)
	{
		var level2RefineryCost = ShopValues.level2Refinery;
		shopUI.level2RefineryCost = level2RefineryCost;
		shopUI.Level2RefineryButton.interactable = level2RefineryCost <= player.money;
		shopUI.level2RefineryHealth = ShopValues.level2RefineryHealth;
		shopUI.level2RefineryVisionRadius = ShopValues.level2RefineryVisionRadius;
	}

	private void UpdateLevel3RefineryButton(Player player)
	{
		var level3RefineryCost = ShopValues.level3Refinery;
		shopUI.level3RefineryCost = level3RefineryCost;
		shopUI.Level3RefineryButton.interactable = level3RefineryCost <= player.money;
		shopUI.level3RefineryHealth = ShopValues.level3RefineryHealth;
		shopUI.level3RefineryVisionRadius = ShopValues.level3RefineryVisionRadius;
	}

	private void UpdateLevel4RefineryButton(Player player)
	{
		var level4RefineryCost = ShopValues.level4Refinery;
		shopUI.level4RefineryCost = level4RefineryCost;
		shopUI.Level4RefineryButton.interactable = level4RefineryCost <= player.money;
		shopUI.level4RefineryHealth = ShopValues.level4RefineryHealth;
		shopUI.level4RefineryVisionRadius = ShopValues.level4RefineryVisionRadius;
	}

	public void OpenRefineriesShop()
	{
		var player = _turn.CurrentPlayer();

		UpdateBuildRangeButton(player);

		UpdateLevel1RefineryButton(player);
		UpdateLevel2RefineryButton(player);
		UpdateLevel3RefineryButton(player);
		UpdateLevel4RefineryButton(player);

		shopUI.refineriesShop.SetActive(true);
	}

	public void BackButton()
	{
		if (shopUI.shipShop.activeSelf || shopUI.abilitiesShop.activeSelf || shopUI.refineriesShop.activeSelf)
		{
			shopUI.shipShop.SetActive(false);
			shopUI.abilitiesShop.SetActive(false);
			shopUI.refineriesShop.SetActive(false);
		}
		else
		{
			var player = _turn.CurrentPlayer();
			player.SetInitialState();
			shopUI.mainShop.SetActive(false);
		}
	}

	public void PurchaseHealthUpgrade()
	{
		var player = _turn.CurrentPlayer();

		player.boughtAmount.health += 1;
		player.money -= shopUI.healthCost;
		player.health += 1;
		player.maxHealth += 1;
		UpdateHealthButton(player);
		gameSceneManager.SetPlayerStats();
	}

	public void PurchaseVisionRadiusUpgrade()
	{
		var player = _turn.CurrentPlayer();

		player.boughtAmount.visionRadius += 1;
		player.money -= shopUI.visionRadiusCost;
		player.visionRadius += 1;
		player.numberOfMoveSpacesPerTurn += 1;
		UpdateVisionRadiusButton(player);
		gameSceneManager.SetPlayerStats();
	}

	public void PurchaseMovesPerTurnUpgrade()
	{
		var player = _turn.CurrentPlayer();

		player.boughtAmount.movesPerTurn += 1;
		player.money -= shopUI.movesPerTurnCost;
		player.numberOfMovesPerTurn += 1;
		UpdateMovesPerTurnButton(player);
		gameSceneManager.SetPlayerStats();
	}

	public void PurchaseMoveAcrossUpgrade()
	{
		var player = _turn.CurrentPlayer();

		player.boughtAmount.moveAcross = true;
		player.money -= shopUI.moveAcrossCost;
		player.canMoveAcross = true;
		UpdateMoveAcrossButton(player);
		gameSceneManager.SetPlayerStats();
	}

	public void PurchaseAttackUpgrade()
	{
		var player = _turn.CurrentPlayer();

		player.boughtAmount.attack += 1;
		player.money -= shopUI.attackPowerCost;
		player.attackPower += 1;
		UpdateAttackButton(player);
		gameSceneManager.SetPlayerStats();
	}

	public void PurchaseAttackRangeUpgrade()
	{
		var player = _turn.CurrentPlayer();

		player.boughtAmount.attackRange += 1;
		player.money -= shopUI.attackRangeCost;
		player.numberOfAttackSpacesPerTurn += 1;
		UpdateAttackRangeButton(player);
		gameSceneManager.SetPlayerStats();
	}

	public void PurchaseSideAttackUpgrade()
	{
		var player = _turn.CurrentPlayer();

		player.boughtAmount.sideAttack += 1;
		player.money -= shopUI.sideAttackPowerCost;
		player.sideHitAttackPower += 1;
		UpdateSideAttackButton(player);
		gameSceneManager.SetPlayerStats();
	}

	public void PurchaseSideAttackRangeUpgrade()
	{
		var player = _turn.CurrentPlayer();

		player.boughtAmount.sideAttackRange += 1;
		player.money -= shopUI.sideAttackRangeCost;
		player.sideHitRange += 1;
		UpdateSideAttackRangeButton(player);
		gameSceneManager.SetPlayerStats();
	}

	public void PurchaseAttacksPerTurnUpgrade()
	{
		var player = _turn.CurrentPlayer();

		player.boughtAmount.attacksPerTurn += 1;
		player.money -= shopUI.attacksPerTurnCost;
		player.numberOfAttacksPerTurn += 1;
		UpdateAttacksPerTurnButton(player);
		gameSceneManager.SetPlayerStats();
	}

	public void PurchaseRotationsAreFreeUpgrade()
	{
		var player = _turn.CurrentPlayer();

		player.boughtAmount.RotationsAreFree = true;
		player.money -= shopUI.rotationsAreFreeCost;
		player.rotationsAreFree = true;
		UpdateRotationsAreFreeButton(player);
		gameSceneManager.SetPlayerStats();
	}

	public void PurchaseUnlockAttacksUpgrade()
	{
		var player = _turn.CurrentPlayer();

		player.boughtAmount.unlockedAttack = true;
		player.boughtAmount.attacksPerTurn += 1;
		player.money -= shopUI.unlockedAttacksCost;
		player.numberOfAttacksPerTurn += 1;
		shopUI.unlockedAttacks = true;
		UpdateAttacksPerTurnButton(player);
		gameSceneManager.SetPlayerStats();
	}

	public void PurchaseBuildRangeUpgrade()
	{
		var player = _turn.CurrentPlayer();

		player.boughtAmount.buildRange += 1;
		player.money -= shopUI.buildRangeCost;
		player.buildRange += 1;
		UpdateBuildRangeButton(player);
		gameSceneManager.SetPlayerStats();
	}

	public void PurchaseLevel1Refinery()
	{
		var player = _turn.CurrentPlayer();

		player.money -= shopUI.level1RefineryCost;
		player.refineryHealth = ShopValues.level1RefineryHealth;
		player.refineryVisionRadius = ShopValues.level1RefineryVisionRadius;
		player.refineryIncome = ShopValues.level1RefineryIncome;
		gameSceneManager.SetPlayerStats();
	}

	public void InitiatePotentialPurchaseOfLevel1Refinery()
	{
		var player = _turn.CurrentPlayer();

		player.refineryHealth = ShopValues.level1RefineryHealth;
		player.refineryVisionRadius = ShopValues.level1RefineryVisionRadius;
		player.refineryIncome = ShopValues.level1RefineryIncome;
		player.SetInitialState();
		player.ChangeState(typeof(IPlayerBuildState));
		CloseShop();
	}

	public void PurchaseLevel2Refinery()
	{
		var player = _turn.CurrentPlayer();

		player.money -= shopUI.level2RefineryCost;
		player.refineryHealth = ShopValues.level2RefineryHealth;
		player.refineryVisionRadius = ShopValues.level2RefineryVisionRadius;
		player.refineryIncome = ShopValues.level2RefineryIncome;
		gameSceneManager.SetPlayerStats();
	}

	public void InitiatePotentialPurchaseOfLevel2Refinery()
	{
		var player = _turn.CurrentPlayer();

		player.refineryHealth = ShopValues.level2RefineryHealth;
		player.refineryVisionRadius = ShopValues.level2RefineryVisionRadius;
		player.refineryIncome = ShopValues.level2RefineryIncome;
		player.SetInitialState();
		player.ChangeState(typeof(IPlayerBuildState));
		CloseShop();
	}

	public void PurchaseLevel3Refinery()
	{
		var player = _turn.CurrentPlayer();

		player.money -= shopUI.level3RefineryCost;
		player.refineryHealth = ShopValues.level3RefineryHealth;
		player.refineryVisionRadius = ShopValues.level3RefineryVisionRadius;
		player.refineryIncome = ShopValues.level3RefineryIncome;
		gameSceneManager.SetPlayerStats();
	}

	public void InitiatePotentialPurchaseOfLevel3Refinery()
	{
		var player = _turn.CurrentPlayer();

		player.refineryHealth = ShopValues.level3RefineryHealth;
		player.refineryVisionRadius = ShopValues.level3RefineryVisionRadius;
		player.refineryIncome = ShopValues.level3RefineryIncome;
		player.SetInitialState();
		player.ChangeState(typeof(IPlayerBuildState));
		CloseShop();
	}

	public void PurchaseLevel4Refinery()
	{
		var player = _turn.CurrentPlayer();

		player.money -= shopUI.level4RefineryCost;
		player.refineryHealth = ShopValues.level4RefineryHealth;
		player.refineryVisionRadius = ShopValues.level4RefineryVisionRadius;
		player.refineryIncome = ShopValues.level4RefineryIncome;
		gameSceneManager.SetPlayerStats();
	}

	public void InitiatePotentialPurchaseOfLevel4Refinery()
	{
		var player = _turn.CurrentPlayer();

		player.refineryHealth = ShopValues.level4RefineryHealth;
		player.refineryVisionRadius = ShopValues.level4RefineryVisionRadius;
		player.refineryIncome = ShopValues.level4RefineryIncome;
		player.SetInitialState();
		player.ChangeState(typeof(IPlayerBuildState));
		CloseShop();
	}
}