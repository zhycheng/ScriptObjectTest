/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class FurnishExcelItem : ExcelItemBase
{
	/// <summary>
	/// 编号
	/// </summary>>
	public int id;
	/// <summary>
	/// 升级名称
	/// </summary>>
	public int name;
	/// <summary>
	/// 所属房间
	/// </summary>>
	public int room;
	/// <summary>
	/// icon
	/// </summary>>
	public int icon;
	/// <summary>
	/// 升级价格
	/// </summary>>
	public int cost_money;
	/// <summary>
	/// 结算钞票加成（%）
	/// </summary>>
	public int extra_money;
}


public class FurnishExcelData : ExcelDataBase<FurnishExcelItem>
{
	public FurnishExcelItem[] items;

	public Dictionary<int,FurnishExcelItem> itemDic = new Dictionary<int,FurnishExcelItem>();

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

	public FurnishExcelItem GetFurnishExcelItem(int id)
	{
		if(itemDic.ContainsKey(id))
			return itemDic[id];
		else
			return null;
	}
	#region --- Get Method ---

	public int GetName(int id)
	{
		var item = GetFurnishExcelItem(id);
		if(item == null)
			return default;
		return item.name;
	}

	public int GetRoom(int id)
	{
		var item = GetFurnishExcelItem(id);
		if(item == null)
			return default;
		return item.room;
	}

	public int GetIcon(int id)
	{
		var item = GetFurnishExcelItem(id);
		if(item == null)
			return default;
		return item.icon;
	}

	public int GetCost_money(int id)
	{
		var item = GetFurnishExcelItem(id);
		if(item == null)
			return default;
		return item.cost_money;
	}

	public int GetExtra_money(int id)
	{
		var item = GetFurnishExcelItem(id);
		if(item == null)
			return default;
		return item.extra_money;
	}

	#endregion
}


#if UNITY_EDITOR
public class FurnishAssetAssignment
{
	public static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)
	{
		var allRowItemDicList = excelMediumData.GetAllRowItemDicList();
		if(allRowItemDicList == null || allRowItemDicList.Count == 0)
			return false;

		int rowCount = allRowItemDicList.Count;
		FurnishExcelData excelDataAsset = ScriptableObject.CreateInstance<FurnishExcelData>();
		excelDataAsset.items = new FurnishExcelItem[rowCount];

		for(int i = 0; i < rowCount; i++)
		{
			var itemRowDic = allRowItemDicList[i];
			excelDataAsset.items[i] = new FurnishExcelItem();
			excelDataAsset.items[i].id = StringUtility.StringToInt(itemRowDic["id"]);
			excelDataAsset.items[i].name = StringUtility.StringToInt(itemRowDic["name"]);
			excelDataAsset.items[i].room = StringUtility.StringToInt(itemRowDic["room"]);
			excelDataAsset.items[i].icon = StringUtility.StringToInt(itemRowDic["icon"]);
			excelDataAsset.items[i].cost_money = StringUtility.StringToInt(itemRowDic["cost_money"]);
			excelDataAsset.items[i].extra_money = StringUtility.StringToInt(itemRowDic["extra_money"]);
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



