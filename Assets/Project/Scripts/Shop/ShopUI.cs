using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
	// Main Panels
	public GameObject mainShop;
	public GameObject backButton;
	public GameObject shipShop;
	public GameObject abilitiesShop;
	public GameObject refineriesShop;

	// SHIP SHOP
	public Text healthCostText;
	public Text healthCurrentText;
	public Text visionRadiusCostText;
	public Text visionRadiusCurrentText;
	public Text movesPerTurnCostText;
	public Text movesPerTurnCurrentText;
	public Text moveAcrossCostText;

	public Button moveAcrossButton;
	public Button HealthButton;
	public Button VisionRadiusButton;
	public Button MovesPerTurnButton;

	private int _moveAcrossCost;
	internal int moveAcrossCost
	{
		set
		{
			_moveAcrossCost = value;
			moveAcrossCostText.text = "$" + _moveAcrossCost;
		}
		get
		{
			return _moveAcrossCost;
		}
	}

	private int _healthCost;
	internal int healthCost
	{
		set
		{
			_healthCost = value;
			healthCostText.text = "$" + _healthCost;
		}
		get
		{
			return _healthCost;
		}
	}

	private int _healthCurrent;
	internal int healthCurrent
	{
		set
		{
			_healthCurrent = value;
			healthCurrentText.text = _healthCurrent.ToString();
		}
		get
		{
			return _healthCurrent;
		}
	}

	private int _visionRadiusCost;
	internal int visionRadiusCost
	{
		set
		{
			_visionRadiusCost = value;
			visionRadiusCostText.text = "$" + _visionRadiusCost;
		}
		get
		{
			return _visionRadiusCost;
		}
	}

	private int _visionRadiusCurrent;
	internal int visionRadiusCurrent
	{
		set
		{
			_visionRadiusCurrent = value;
			visionRadiusCurrentText.text = _visionRadiusCurrent.ToString();
		}
		get
		{
			return _visionRadiusCurrent;
		}
	}

	private int _movesPerTurnCost;
	internal int movesPerTurnCost
	{
		set
		{
			_movesPerTurnCost = value;
			movesPerTurnCostText.text = "$" + _movesPerTurnCost;
		}
		get
		{
			return _movesPerTurnCost;
		}
	}

	private int _movesPerTurnCurrent;
	internal int movesPerTurnCurrent
	{
		set
		{
			_movesPerTurnCurrent = value;
			movesPerTurnCurrentText.text = _movesPerTurnCurrent.ToString();
		}
		get
		{
			return _movesPerTurnCurrent;
		}
	}

	// ABILITIES SHOP
	public Text unlockedAttacksCostText;
	public Text attackPowerCostText;
	public Text attackPowerCurrentText;
	public Text attackRangeCostText;
	public Text attackRangeCurrentText;
	public Text sideAttackPowerCostText;
	public Text sideAttackPowerCurrentText;
	public Text sideAttackRangeCostText;
	public Text sideAttackRangeCurrentText;
	public Text attacksPerTurnCostText;
	public Text attacksPerTurnCurrentText;
	public Text rotationsAreFreeCostText;

	// Hide until player unlocks attack
	public GameObject attackUpgrade;
	public GameObject attackRangeUpgrade;
	public GameObject sideAttackUpgrade;
	public GameObject sideAttackRangeUpgrade;
	public GameObject attacksPerTurnUpgrade;
	public GameObject rotationsFreeUpgrade;

	public Button unlockAttacksButton;
	public Button rotationsAreFreeButton;
	public Button AttackButton;
	public Button AttackRangeButton;
	public Button SideAttackButton;
	public Button SideAttackRangeButton;
	public Button AttacksPerTurnButton;

	private bool _unlockedAttacks;
	internal bool unlockedAttacks
	{
		set
		{
			_unlockedAttacks = value;
			if (!_unlockedAttacks)
			{
				unlockAttacksButton.gameObject.SetActive(true);
				attackUpgrade.SetActive(false);
				attackRangeUpgrade.SetActive(false);
				sideAttackUpgrade.SetActive(false);
				sideAttackRangeUpgrade.SetActive(false);
				attacksPerTurnUpgrade.SetActive(false);
				rotationsFreeUpgrade.SetActive(false);
			}
			else
			{
				unlockAttacksButton.gameObject.SetActive(false);
				attackUpgrade.SetActive(true);
				attackRangeUpgrade.SetActive(true);
				sideAttackUpgrade.SetActive(true);
				sideAttackRangeUpgrade.SetActive(true);
				attacksPerTurnUpgrade.SetActive(true);
				rotationsFreeUpgrade.SetActive(true);
			}
		}
		get
		{
			return _unlockedAttacks;
		}
	}

	private int _unlockedAttacksCost;
	internal int unlockedAttacksCost
	{
		set
		{
			_unlockedAttacksCost = value;
			unlockedAttacksCostText.text = "$" + _unlockedAttacksCost;
		}
		get
		{
			return _unlockedAttacksCost;
		}
	}

	private int _rotationsAreFreeCost;
	internal int rotationsAreFreeCost
	{
		set
		{
			_rotationsAreFreeCost = value;
			rotationsAreFreeCostText.text = "$" + _rotationsAreFreeCost;
		}
		get
		{
			return _rotationsAreFreeCost;
		}
	}

	private int _attacksPerTurnCost;
	internal int attacksPerTurnCost
	{
		set
		{
			_attacksPerTurnCost = value;
			attacksPerTurnCostText.text = "$" + _attacksPerTurnCost;
		}
		get
		{
			return _attacksPerTurnCost;
		}
	}

	private int _attacksPerTurnCurrent;
	internal int attacksPerTurnCurrent
	{
		set
		{
			_attacksPerTurnCurrent = value;
			attacksPerTurnCurrentText.text = _attacksPerTurnCurrent.ToString();
		}
		get
		{
			return _attacksPerTurnCurrent;
		}
	}

	private int _sideAttackRangeCost;
	internal int sideAttackRangeCost
	{
		set
		{
			_sideAttackRangeCost = value;
			sideAttackRangeCostText.text = "$" + _sideAttackRangeCost;
		}
		get
		{
			return _sideAttackRangeCost;
		}
	}

	private int _sideAttackRangeCurrent;
	internal int sideAttackRangeCurrent
	{
		set
		{
			_sideAttackRangeCurrent = value;
			sideAttackRangeCurrentText.text = _sideAttackRangeCurrent.ToString();
		}
		get
		{
			return _sideAttackRangeCurrent;
		}
	}

	private int _sideAttackPowerCost;
	internal int sideAttackPowerCost
	{
		set
		{
			_sideAttackPowerCost = value;
			sideAttackPowerCostText.text = "$" + _sideAttackPowerCost;
		}
		get
		{
			return _sideAttackPowerCost;
		}
	}

	private int _sideAttackPowerCurrent;
	internal int sideAttackPowerCurrent
	{
		set
		{
			_sideAttackPowerCurrent = value;
			sideAttackPowerCurrentText.text = _sideAttackPowerCurrent.ToString();
		}
		get
		{
			return _sideAttackPowerCurrent;
		}
	}

	private int _attackRangeCost;
	internal int attackRangeCost
	{
		set
		{
			_attackRangeCost = value;
			attackRangeCostText.text = "$" + _attackRangeCost;
		}
		get
		{
			return _attackRangeCost;
		}
	}

	private int _attackRangeCurrent;
	internal int attackRangeCurrent
	{
		set
		{
			_attackRangeCurrent = value;
			attackRangeCurrentText.text = _attackRangeCurrent.ToString();
		}
		get
		{
			return _attackRangeCurrent;
		}
	}

	private int _attackPowerCost;
	internal int attackPowerCost
	{
		set
		{
			_attackPowerCost = value;
			attackPowerCostText.text = "$" + _attackPowerCost;
		}
		get
		{
			return _attackPowerCost;
		}
	}

	private int _attackPowerCurrent;
	internal int attackPowerCurrent
	{
		set
		{
			_attackPowerCurrent = value;
			attackPowerCurrentText.text = _attackPowerCurrent.ToString();
		}
		get
		{
			return _attackPowerCurrent;
		}
	}

	// REFINERIES SHOP
	public Text buildRangeCostText;
	public Text buildRangeCurrentText;
	public Text level1RefineryCostText;
	public Text level1RefineryHealthText;
	public Text level1RefineryVisionRadiusText;
	public Text level2RefineryCostText;
	public Text level2RefineryHealthText;
	public Text level2RefineryVisionRadiusText;
	public Text level3RefineryCostText;
	public Text level3RefineryHealthText;
	public Text level3RefineryVisionRadiusText;
	public Text level4RefineryCostText;
	public Text level4RefineryHealthText;
	public Text level4RefineryVisionRadiusText;

	public Button BuildRangeButton;
	public Button Level1RefineryButton;
	public Button Level2RefineryButton;
	public Button Level3RefineryButton;
	public Button Level4RefineryButton;

	private int _level4RefineryVisionRadius;
	internal int level4RefineryVisionRadius
	{
		set
		{
			_level4RefineryVisionRadius = value;
			level4RefineryVisionRadiusText.text = _level4RefineryVisionRadius.ToString();
		}
		get
		{
			return _level4RefineryVisionRadius;
		}
	}

	private int _level4RefineryHealth;
	internal int level4RefineryHealth
	{
		set
		{
			_level4RefineryHealth = value;
			level4RefineryHealthText.text = _level4RefineryHealth.ToString();
		}
		get
		{
			return _level4RefineryHealth;
		}
	}

	private int _level3RefineryVisionRadius;
	internal int level3RefineryVisionRadius
	{
		set
		{
			_level3RefineryVisionRadius = value;
			level3RefineryVisionRadiusText.text = _level3RefineryVisionRadius.ToString();
		}
		get
		{
			return _level3RefineryVisionRadius;
		}
	}

	private int _level3RefineryHealth;
	internal int level3RefineryHealth
	{
		set
		{
			_level3RefineryHealth = value;
			level3RefineryHealthText.text = _level3RefineryHealth.ToString();
		}
		get
		{
			return _level3RefineryHealth;
		}
	}

	private int _level2RefineryVisionRadius;
	internal int level2RefineryVisionRadius
	{
		set
		{
			_level2RefineryVisionRadius = value;
			level2RefineryVisionRadiusText.text = _level2RefineryVisionRadius.ToString();
		}
		get
		{
			return _level2RefineryVisionRadius;
		}
	}

	private int _level2RefineryHealth;
	internal int level2RefineryHealth
	{
		set
		{
			_level2RefineryHealth = value;
			level2RefineryHealthText.text = _level2RefineryHealth.ToString();
		}
		get
		{
			return _level2RefineryHealth;
		}
	}

	private int _level1RefineryVisionRadius;
	internal int level1RefineryVisionRadius
	{
		set
		{
			_level1RefineryVisionRadius = value;
			level1RefineryVisionRadiusText.text = _level1RefineryVisionRadius.ToString();
		}
		get
		{
			return _level1RefineryVisionRadius;
		}
	}

	private int _level1RefineryHealth;
	internal int level1RefineryHealth
	{
		set
		{
			_level1RefineryHealth = value;
			level1RefineryHealthText.text = _level1RefineryHealth.ToString();
		}
		get
		{
			return _level1RefineryHealth;
		}
	}

	private int _level4RefineryCost;
	internal int level4RefineryCost
	{
		set
		{
			_level4RefineryCost = value;
			level4RefineryCostText.text = "$" + _level4RefineryCost;
		}
		get
		{
			return _level4RefineryCost;
		}
	}

	private int _level3RefineryCost;
	internal int level3RefineryCost
	{
		set
		{
			_level3RefineryCost = value;
			level3RefineryCostText.text = "$" + _level3RefineryCost;
		}
		get
		{
			return _level3RefineryCost;
		}
	}

	private int _level2RefineryCost;
	internal int level2RefineryCost
	{
		set
		{
			_level2RefineryCost = value;
			level2RefineryCostText.text = "$" + _level2RefineryCost;
		}
		get
		{
			return _level2RefineryCost;
		}
	}

	private int _level1RefineryCost;
	internal int level1RefineryCost
	{
		set
		{
			_level1RefineryCost = value;
			level1RefineryCostText.text = "$" + _level1RefineryCost;
		}
		get
		{
			return _level1RefineryCost;
		}
	}

	private int _buildRangeCost;
	internal int buildRangeCost
	{
		set
		{
			_buildRangeCost = value;
			buildRangeCostText.text = "$" + _buildRangeCost;
		}
		get
		{
			return _buildRangeCost;
		}
	}

	private int _buildRangeCurrent;
	internal int buildRangeCurrent
	{
		set
		{
			_buildRangeCurrent = value;
			buildRangeCurrentText.text = _buildRangeCurrent.ToString();
		}
		get
		{
			return _buildRangeCurrent;
		}
	}

	void Test() { }
}