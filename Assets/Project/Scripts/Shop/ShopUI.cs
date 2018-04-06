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
	public Button moveAcrossButtonText;

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
			healthCurrentText.text = _healthCost.ToString();
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
	public Button rotationsAreFree;

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
			_attackPowerCurrent = value;
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