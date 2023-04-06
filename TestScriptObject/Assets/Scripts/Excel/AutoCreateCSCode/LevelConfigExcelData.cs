/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class LevelConfigExcelItem : ExcelItemBase
{
	/// <summary>
	/// 关卡id
	/// </summary>>
	public int id;
	/// <summary>
	/// 卸眼镜
	/// </summary>>
	public bool removeglasses;
	/// <summary>
	/// 自动变化
	/// </summary>>
	public bool state_change;
	/// <summary>
	/// 洗脸
	/// </summary>>
	public bool washface;
	/// <summary>
	/// 伤口处理
	/// </summary>>
	public int wound;
	/// <summary>
	/// 挑痘
	/// </summary>>
	public string acne;
	/// <summary>
	/// 胎记
	/// </summary>>
	public int birthmark;
	/// <summary>
	/// 黑头
	/// </summary>>
	public bool blackhead;
	/// <summary>
	/// 面膜
	/// </summary>>
	public bool facial_mask;
	/// <summary>
	/// 遮瑕
	/// </summary>>
	public bool conceal;
	/// <summary>
	/// 选眼影
	/// </summary>>
	public string choose_eye_shadow;
	/// <summary>
	/// 睫毛
	/// </summary>>
	public string eyelash;
	/// <summary>
	/// 美瞳
	/// </summary>>
	public string pupil;
	/// <summary>
	/// 选面妆
	/// </summary>>
	public string choose_makeup;
	/// <summary>
	/// 选口红
	/// </summary>>
	public string choose_lipstick;
	/// <summary>
	/// 衣服
	/// </summary>>
	public string clothing;
	/// <summary>
	/// 头发
	/// </summary>>
	public string hair;
	/// <summary>
	/// 眼镜
	/// </summary>>
	public string glasses;
	/// <summary>
	/// 耳环
	/// </summary>>
	public string earrings;
	/// <summary>
	/// 项链
	/// </summary>>
	public string necklace;
}


public class LevelConfigExcelData : ExcelDataBase<LevelConfigExcelItem>
{
	public LevelConfigExcelItem[] items;

	public Dictionary<int,LevelConfigExcelItem> itemDic = new Dictionary<int,LevelConfigExcelItem>();

	public void Init()
	{
		itemDic.Clear();
		if(items != null && items.Length > 0)
		{
			for(int i = 0; i < items.Length; i++)
			{
				itemDic.Add(items[i].id, items[i]);
			}
		}
	}

	public LevelConfigExcelItem GetLevelConfigExcelItem(int id)
	{
		if(itemDic.ContainsKey(id))
			return itemDic[id];
		else
			return null;
	}
	#region --- Get Method ---

	public bool GetRemoveglasses(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.removeglasses;
	}

	public bool GetState_change(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.state_change;
	}

	public bool GetWashface(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.washface;
	}

	public int GetWound(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.wound;
	}

	public string GetAcne(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.acne;
	}

	public int GetBirthmark(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.birthmark;
	}

	public bool GetBlackhead(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.blackhead;
	}

	public bool GetFacial_mask(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.facial_mask;
	}

	public bool GetConceal(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.conceal;
	}

	public string GetChoose_eye_shadow(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.choose_eye_shadow;
	}

	public string GetEyelash(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.eyelash;
	}

	public string GetPupil(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.pupil;
	}

	public string GetChoose_makeup(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.choose_makeup;
	}

	public string GetChoose_lipstick(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.choose_lipstick;
	}

	public string GetClothing(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.clothing;
	}

	public string GetHair(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.hair;
	}

	public string GetGlasses(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.glasses;
	}

	public string GetEarrings(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.earrings;
	}

	public string GetNecklace(int id)
	{
		var item = GetLevelConfigExcelItem(id);
		if(item == null)
			return default;
		return item.necklace;
	}

	#endregion
}


#if UNITY_EDITOR
public class LevelConfigAssetAssignment
{
	public static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)
	{
		var allRowItemDicList = excelMediumData.GetAllRowItemDicList();
		if(allRowItemDicList == null || allRowItemDicList.Count == 0)
			return false;

		int rowCount = allRowItemDicList.Count;
		LevelConfigExcelData excelDataAsset = ScriptableObject.CreateInstance<LevelConfigExcelData>();
		excelDataAsset.items = new LevelConfigExcelItem[rowCount];

		for(int i = 0; i < rowCount; i++)
		{
			var itemRowDic = allRowItemDicList[i];
			excelDataAsset.items[i] = new LevelConfigExcelItem();
			excelDataAsset.items[i].id = StringUtility.StringToInt(itemRowDic["id"]);
			excelDataAsset.items[i].removeglasses = StringUtility.StringToBool(itemRowDic["removeglasses"]);
			excelDataAsset.items[i].state_change = StringUtility.StringToBool(itemRowDic["state_change"]);
			excelDataAsset.items[i].washface = StringUtility.StringToBool(itemRowDic["washface"]);
			excelDataAsset.items[i].wound = StringUtility.StringToInt(itemRowDic["wound"]);
			excelDataAsset.items[i].acne = itemRowDic["acne"];
			excelDataAsset.items[i].birthmark = StringUtility.StringToInt(itemRowDic["birthmark"]);
			excelDataAsset.items[i].blackhead = StringUtility.StringToBool(itemRowDic["blackhead"]);
			excelDataAsset.items[i].facial_mask = StringUtility.StringToBool(itemRowDic["facial_mask"]);
			excelDataAsset.items[i].conceal = StringUtility.StringToBool(itemRowDic["conceal"]);
			excelDataAsset.items[i].choose_eye_shadow = itemRowDic["choose_eye_shadow"];
			excelDataAsset.items[i].eyelash = itemRowDic["eyelash"];
			excelDataAsset.items[i].pupil = itemRowDic["pupil"];
			excelDataAsset.items[i].choose_makeup = itemRowDic["choose_makeup"];
			excelDataAsset.items[i].choose_lipstick = itemRowDic["choose_lipstick"];
			excelDataAsset.items[i].clothing = itemRowDic["clothing"];
			excelDataAsset.items[i].hair = itemRowDic["hair"];
			excelDataAsset.items[i].glasses = itemRowDic["glasses"];
			excelDataAsset.items[i].earrings = itemRowDic["earrings"];
			excelDataAsset.items[i].necklace = itemRowDic["necklace"];
		}
		if(!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string fullPath = Path.Combine(excelAssetPath,excelMediumData.excelName) + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(fullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset,fullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif



