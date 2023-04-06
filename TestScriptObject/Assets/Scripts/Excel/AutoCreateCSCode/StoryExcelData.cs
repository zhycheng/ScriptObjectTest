/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class StoryExcelItem : ExcelItemBase
{
	/// <summary>
	/// 编号
	/// </summary>>
	public int id;
	/// <summary>
	/// 剧情标题
	/// </summary>>
	public int title;
	/// <summary>
	/// 背景小图
	/// </summary>>
	public string bg_image;
	/// <summary>
	/// 剧情内容
	/// </summary>>
	public int plot;
	/// <summary>
	/// 解锁剧情
	/// </summary>>
	public int next_story;
	/// <summary>
	/// 是否默认解锁
	/// </summary>>
	public bool unlock;
	/// <summary>
	/// 解锁NPC
	/// </summary>>
	public int unlock_npc;
	/// <summary>
	/// 是否增加好感度
	/// </summary>>
	public bool favorability;
}


public class StoryExcelData : ExcelDataBase<StoryExcelItem>
{
	public StoryExcelItem[] items;

	public Dictionary<int,StoryExcelItem> itemDic = new Dictionary<int,StoryExcelItem>();

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

	public StoryExcelItem GetStoryExcelItem(int id)
	{
		if(itemDic.ContainsKey(id))
			return itemDic[id];
		else
			return null;
	}
	#region --- Get Method ---

	public int GetTitle(int id)
	{
		var item = GetStoryExcelItem(id);
		if(item == null)
			return default;
		return item.title;
	}

	public string GetBg_image(int id)
	{
		var item = GetStoryExcelItem(id);
		if(item == null)
			return default;
		return item.bg_image;
	}

	public int GetPlot(int id)
	{
		var item = GetStoryExcelItem(id);
		if(item == null)
			return default;
		return item.plot;
	}

	public int GetNext_story(int id)
	{
		var item = GetStoryExcelItem(id);
		if(item == null)
			return default;
		return item.next_story;
	}

	public bool GetUnlock(int id)
	{
		var item = GetStoryExcelItem(id);
		if(item == null)
			return default;
		return item.unlock;
	}

	public int GetUnlock_npc(int id)
	{
		var item = GetStoryExcelItem(id);
		if(item == null)
			return default;
		return item.unlock_npc;
	}

	public bool GetFavorability(int id)
	{
		var item = GetStoryExcelItem(id);
		if(item == null)
			return default;
		return item.favorability;
	}

	#endregion
}


#if UNITY_EDITOR
public class StoryAssetAssignment
{
	public static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)
	{
		var allRowItemDicList = excelMediumData.GetAllRowItemDicList();
		if(allRowItemDicList == null || allRowItemDicList.Count == 0)
			return false;

		int rowCount = allRowItemDicList.Count;
		StoryExcelData excelDataAsset = ScriptableObject.CreateInstance<StoryExcelData>();
		excelDataAsset.items = new StoryExcelItem[rowCount];

		for(int i = 0; i < rowCount; i++)
		{
			var itemRowDic = allRowItemDicList[i];
			excelDataAsset.items[i] = new StoryExcelItem();
			excelDataAsset.items[i].id = StringUtility.StringToInt(itemRowDic["id"]);
			excelDataAsset.items[i].title = StringUtility.StringToInt(itemRowDic["title"]);
			excelDataAsset.items[i].bg_image = itemRowDic["bg_image"];
			excelDataAsset.items[i].plot = StringUtility.StringToInt(itemRowDic["plot"]);
			excelDataAsset.items[i].next_story = StringUtility.StringToInt(itemRowDic["next_story"]);
			excelDataAsset.items[i].unlock = StringUtility.StringToBool(itemRowDic["unlock"]);
			excelDataAsset.items[i].unlock_npc = StringUtility.StringToInt(itemRowDic["unlock_npc"]);
			excelDataAsset.items[i].favorability = StringUtility.StringToBool(itemRowDic["favorability"]);
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



