using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
	#region serialiazable variables
	[SerializeField] GameObject _layoutGroup;
	#endregion

	#region local variables
	SceneReferences _sceneReferences;
	#endregion

	#region getters and setters
	public SceneReferences SceneReferences { get { if (_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; } return _sceneReferences; } }
	#endregion

	#region unity methods
	#endregion

	#region local methods
	#endregion

	#region public methods
	public void BuyFish()
    {
        if (SceneReferences.Resources.SpendMoney(100)) { SceneReferences.Resources.GainFood(100); }
    }

	public void BuyMaterials()
	{
		if (SceneReferences.Resources.SpendMoney(100)) { SceneReferences.Resources.GainMaterials(100); }
	}

	public void BuyMedicine()
	{
		if (SceneReferences.Resources.SpendMoney(100)) { SceneReferences.Resources.GainMedicine(100); }
	}

	public void OnClickShop()
    {
		_layoutGroup.SetActive(!_layoutGroup.activeSelf);
    }
	#endregion

	#region coroutines
	#endregion
}
