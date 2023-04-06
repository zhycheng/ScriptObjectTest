/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class ToolSkinExcelItem : ExcelItemBase
{
	/// <summary>
	/// 编号
	/// </summary>>
	public int id;
	/// <summary>
	/// 工具类型
	/// </summary>>
	public int tooltype;
	/// <summary>
	/// 解锁方式
	/// </summary>>
	public int unlocktype;
	/// <summary>
	/// 解锁参数
	/// </summary>>
	public int unlockpara;
}


public class ToolSkinExcelData : ExcelDataBase<ToolSkinExcelItem>
{
	public ToolSkinExcelItem[] items;

	public Dictionary<int,ToolSkinExcelItem> itemDic = new Dictionary<int,ToolSkinExcelItem>();

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

	public ToolSkinExcelItem GetToolSkinExcelItem(int id)
	{
		if(itemDic.ContainsKey(id))
			return itemDic[id];
		else
			return null;
	}
	#region --- Get Method ---

	public int GetTooltype(int id)
	{
		var item = GetToolSkinExcelItem(id);
		if(item == null)
			return default;
		return item.tooltype;
	}

	public int GetUnlocktype(int id)
	{
		var item = GetToolSkinExcelItem(id);
		if(item == null)
			return default;
		return item.unlocktype;
	}

	public int GetUnlockpara(int id)
	{
		var item = GetToolSkinExcelItem(id);
		if(item == null)
			return default;
		return item.unlockpara;
	}

	#endregion
}


#if UNITY_EDITOR
public class ToolSkinAssetAssignment
{
	public static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)
	{
		var allRowItemDicList = excelMediumData.GetAllRowItemDicList();
		if(allRowItemDicList == null || allRowItemDicList.Count == 0)
			return false;

		int rowCount = allRowItemDicList.Count;
		ToolSkinExcelData excelDataAsset = ScriptableObject.CreateInstance<ToolSkinExcelData>();
		excelDataAsset.items = new ToolSkinExcelItem[rowCount];

		for(int i = 0; i < rowCount; i++)
		{
			var itemRowDic = allRowItemDicList[i];
			excelDataAsset.items[i] = new ToolSkinExcelItem();
			excelDataAsset.items[i].id = StringUtility.StringToInt(itemRowDic["id"]);
			excelDataAsset.items[i].tooltype = StringUtility.StringToInt(itemRowDic["tooltype"]);
			excelDataAsset.items[i].unlocktype = StringUtility.StringToInt(itemRowDic["unlocktype"]);
			excelDataAsset.items[i].unlockpara = StringUtility.StringToInt(itemRowDic["unlockpara"]);
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



