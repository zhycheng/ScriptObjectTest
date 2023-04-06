/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class DressupExcelItem : ExcelItemBase
{
	/// <summary>
	/// 编号
	/// </summary>>
	public int id;
	/// <summary>
	/// 部位
	/// </summary>>
	public int parts;
	/// <summary>
	/// 解锁类型
	/// </summary>>
	public int unlock_type;
	/// <summary>
	/// 解锁参数
	/// </summary>>
	public int unlock_para;
	/// <summary>
	/// 是否可以跳过左眼
	/// </summary>>
	public bool skip;
	/// <summary>
	/// 眼饰选项
	/// </summary>>
	public string eyewear;
}


public class DressupExcelData : ExcelDataBase<DressupExcelItem>
{
	public DressupExcelItem[] items;

	public Dictionary<int,DressupExcelItem> itemDic = new Dictionary<int,DressupExcelItem>();

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

	public DressupExcelItem GetDressupExcelItem(int id)
	{
		if(itemDic.ContainsKey(id))
			return itemDic[id];
		else
			return null;
	}
	#region --- Get Method ---

	public int GetParts(int id)
	{
		var item = GetDressupExcelItem(id);
		if(item == null)
			return default;
		return item.parts;
	}

	public int GetUnlock_type(int id)
	{
		var item = GetDressupExcelItem(id);
		if(item == null)
			return default;
		return item.unlock_type;
	}

	public int GetUnlock_para(int id)
	{
		var item = GetDressupExcelItem(id);
		if(item == null)
			return default;
		return item.unlock_para;
	}

	public bool GetSkip(int id)
	{
		var item = GetDressupExcelItem(id);
		if(item == null)
			return default;
		return item.skip;
	}

	public string GetEyewear(int id)
	{
		var item = GetDressupExcelItem(id);
		if(item == null)
			return default;
		return item.eyewear;
	}

	#endregion
}


#if UNITY_EDITOR
public class DressupAssetAssignment
{
	public static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)
	{
		var allRowItemDicList = excelMediumData.GetAllRowItemDicList();
		if(allRowItemDicList == null || allRowItemDicList.Count == 0)
			return false;

		int rowCount = allRowItemDicList.Count;
		DressupExcelData excelDataAsset = ScriptableObject.CreateInstance<DressupExcelData>();
		excelDataAsset.items = new DressupExcelItem[rowCount];

		for(int i = 0; i < rowCount; i++)
		{
			var itemRowDic = allRowItemDicList[i];
			excelDataAsset.items[i] = new DressupExcelItem();
			excelDataAsset.items[i].id = StringUtility.StringToInt(itemRowDic["id"]);
			excelDataAsset.items[i].parts = StringUtility.StringToInt(itemRowDic["parts"]);
			excelDataAsset.items[i].unlock_type = StringUtility.StringToInt(itemRowDic["unlock_type"]);
			excelDataAsset.items[i].unlock_para = StringUtility.StringToInt(itemRowDic["unlock_para"]);
			excelDataAsset.items[i].skip = StringUtility.StringToBool(itemRowDic["skip"]);
			excelDataAsset.items[i].eyewear = itemRowDic["eyewear"];
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



