/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class StepsExcelItem : ExcelItemBase
{
	/// <summary>
	/// 编号
	/// </summary>>
	public int id;
	/// <summary>
	/// 包含环节
	/// </summary>>
	public string special_level;
	/// <summary>
	/// 奖励金币
	/// </summary>>
	public int money;
}


public class StepsExcelData : ExcelDataBase<StepsExcelItem>
{
	public StepsExcelItem[] items;

	public Dictionary<int,StepsExcelItem> itemDic = new Dictionary<int,StepsExcelItem>();

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

	public StepsExcelItem GetStepsExcelItem(int id)
	{
		if(itemDic.ContainsKey(id))
			return itemDic[id];
		else
			return null;
	}
	#region --- Get Method ---

	public string GetSpecial_level(int id)
	{
		var item = GetStepsExcelItem(id);
		if(item == null)
			return default;
		return item.special_level;
	}

	public int GetMoney(int id)
	{
		var item = GetStepsExcelItem(id);
		if(item == null)
			return default;
		return item.money;
	}

	#endregion
}


#if UNITY_EDITOR
public class StepsAssetAssignment
{
	public static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)
	{
		var allRowItemDicList = excelMediumData.GetAllRowItemDicList();
		if(allRowItemDicList == null || allRowItemDicList.Count == 0)
			return false;

		int rowCount = allRowItemDicList.Count;
		StepsExcelData excelDataAsset = ScriptableObject.CreateInstance<StepsExcelData>();
		excelDataAsset.items = new StepsExcelItem[rowCount];

		for(int i = 0; i < rowCount; i++)
		{
			var itemRowDic = allRowItemDicList[i];
			excelDataAsset.items[i] = new StepsExcelItem();
			excelDataAsset.items[i].id = StringUtility.StringToInt(itemRowDic["id"]);
			excelDataAsset.items[i].special_level = itemRowDic["special_level"];
			excelDataAsset.items[i].money = StringUtility.StringToInt(itemRowDic["money"]);
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



