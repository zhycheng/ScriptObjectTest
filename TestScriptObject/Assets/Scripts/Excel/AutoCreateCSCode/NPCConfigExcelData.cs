/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class NPCConfigExcelItem : ExcelItemBase
{
	/// <summary>
	/// 编号
	/// </summary>>
	public int id;
	/// <summary>
	/// 角色名称
	/// </summary>>
	public int npc_name;
	/// <summary>
	/// 化妆前形象
	/// </summary>>
	public string premakeup_look;
	/// <summary>
	/// 故事线
	/// </summary>>
	public string stories;
	/// <summary>
	/// 是否默认解锁
	/// </summary>>
	public bool unlock;
	/// <summary>
	/// 默认衣服
	/// </summary>>
	public int default_cloth;
}


public class NPCConfigExcelData : ExcelDataBase<NPCConfigExcelItem>
{
	public NPCConfigExcelItem[] items;

	public Dictionary<int,NPCConfigExcelItem> itemDic = new Dictionary<int,NPCConfigExcelItem>();

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

	public NPCConfigExcelItem GetNPCConfigExcelItem(int id)
	{
		if(itemDic.ContainsKey(id))
			return itemDic[id];
		else
			return null;
	}
	#region --- Get Method ---

	public int GetNpc_name(int id)
	{
		var item = GetNPCConfigExcelItem(id);
		if(item == null)
			return default;
		return item.npc_name;
	}

	public string GetPremakeup_look(int id)
	{
		var item = GetNPCConfigExcelItem(id);
		if(item == null)
			return default;
		return item.premakeup_look;
	}

	public string GetStories(int id)
	{
		var item = GetNPCConfigExcelItem(id);
		if(item == null)
			return default;
		return item.stories;
	}

	public bool GetUnlock(int id)
	{
		var item = GetNPCConfigExcelItem(id);
		if(item == null)
			return default;
		return item.unlock;
	}

	public int GetDefault_cloth(int id)
	{
		var item = GetNPCConfigExcelItem(id);
		if(item == null)
			return default;
		return item.default_cloth;
	}

	#endregion
}


#if UNITY_EDITOR
public class NPCConfigAssetAssignment
{
	public static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)
	{
		var allRowItemDicList = excelMediumData.GetAllRowItemDicList();
		if(allRowItemDicList == null || allRowItemDicList.Count == 0)
			return false;

		int rowCount = allRowItemDicList.Count;
		NPCConfigExcelData excelDataAsset = ScriptableObject.CreateInstance<NPCConfigExcelData>();
		excelDataAsset.items = new NPCConfigExcelItem[rowCount];

		for(int i = 0; i < rowCount; i++)
		{
			var itemRowDic = allRowItemDicList[i];
			excelDataAsset.items[i] = new NPCConfigExcelItem();
			excelDataAsset.items[i].id = StringUtility.StringToInt(itemRowDic["id"]);
			excelDataAsset.items[i].npc_name = StringUtility.StringToInt(itemRowDic["npc_name"]);
			excelDataAsset.items[i].premakeup_look = itemRowDic["premakeup_look"];
			excelDataAsset.items[i].stories = itemRowDic["stories"];
			excelDataAsset.items[i].unlock = StringUtility.StringToBool(itemRowDic["unlock"]);
			excelDataAsset.items[i].default_cloth = StringUtility.StringToInt(itemRowDic["default_cloth"]);
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



