/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class ParameterExcelItem : ExcelItemBase
{
	/// <summary>
	/// 编号
	/// </summary>>
	public int id;
	/// <summary>
	/// 值
	/// </summary>>
	public string value;
}


public class ParameterExcelData : ExcelDataBase<ParameterExcelItem>
{
	public ParameterExcelItem[] items;

	public Dictionary<int,ParameterExcelItem> itemDic = new Dictionary<int,ParameterExcelItem>();

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

	public ParameterExcelItem GetParameterExcelItem(int id)
	{
		if(itemDic.ContainsKey(id))
			return itemDic[id];
		else
			return null;
	}
	#region --- Get Method ---

	public string GetValue(int id)
	{
		var item = GetParameterExcelItem(id);
		if(item == null)
			return default;
		return item.value;
	}

	#endregion
}


#if UNITY_EDITOR
public class ParameterAssetAssignment
{
	public static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)
	{
		var allRowItemDicList = excelMediumData.GetAllRowItemDicList();
		if(allRowItemDicList == null || allRowItemDicList.Count == 0)
			return false;

		int rowCount = allRowItemDicList.Count;
		ParameterExcelData excelDataAsset = ScriptableObject.CreateInstance<ParameterExcelData>();
		excelDataAsset.items = new ParameterExcelItem[rowCount];

		for(int i = 0; i < rowCount; i++)
		{
			var itemRowDic = allRowItemDicList[i];
			excelDataAsset.items[i] = new ParameterExcelItem();
			excelDataAsset.items[i].id = StringUtility.StringToInt(itemRowDic["id"]);
			excelDataAsset.items[i].value = itemRowDic["value"];
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



