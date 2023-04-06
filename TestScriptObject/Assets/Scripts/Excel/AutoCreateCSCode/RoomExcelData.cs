/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class RoomExcelItem : ExcelItemBase
{
	/// <summary>
	/// 编号
	/// </summary>>
	public int id;
	/// <summary>
	/// 名称
	/// </summary>>
	public int name;
	/// <summary>
	/// icon
	/// </summary>>
	public string icon;
	/// <summary>
	/// prefab
	/// </summary>>
	public string prefab;
}


public class RoomExcelData : ExcelDataBase<RoomExcelItem>
{
	public RoomExcelItem[] items;

	public Dictionary<int,RoomExcelItem> itemDic = new Dictionary<int,RoomExcelItem>();

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

	public RoomExcelItem GetRoomExcelItem(int id)
	{
		if(itemDic.ContainsKey(id))
			return itemDic[id];
		else
			return null;
	}
	#region --- Get Method ---

	public int GetName(int id)
	{
		var item = GetRoomExcelItem(id);
		if(item == null)
			return default;
		return item.name;
	}

	public string GetIcon(int id)
	{
		var item = GetRoomExcelItem(id);
		if(item == null)
			return default;
		return item.icon;
	}

	public string GetPrefab(int id)
	{
		var item = GetRoomExcelItem(id);
		if(item == null)
			return default;
		return item.prefab;
	}

	#endregion
}


#if UNITY_EDITOR
public class RoomAssetAssignment
{
	public static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)
	{
		var allRowItemDicList = excelMediumData.GetAllRowItemDicList();
		if(allRowItemDicList == null || allRowItemDicList.Count == 0)
			return false;

		int rowCount = allRowItemDicList.Count;
		RoomExcelData excelDataAsset = ScriptableObject.CreateInstance<RoomExcelData>();
		excelDataAsset.items = new RoomExcelItem[rowCount];

		for(int i = 0; i < rowCount; i++)
		{
			var itemRowDic = allRowItemDicList[i];
			excelDataAsset.items[i] = new RoomExcelItem();
			excelDataAsset.items[i].id = StringUtility.StringToInt(itemRowDic["id"]);
			excelDataAsset.items[i].name = StringUtility.StringToInt(itemRowDic["name"]);
			excelDataAsset.items[i].icon = itemRowDic["icon"];
			excelDataAsset.items[i].prefab = itemRowDic["prefab"];
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



