/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class PlotExcelItem : ExcelItemBase
{
	/// <summary>
	/// 编号
	/// </summary>>
	public int id;
	/// <summary>
	/// 剧情类型
	/// </summary>>
	public int plot_type;
	/// <summary>
	/// NPC
	/// </summary>>
	public int plot_npc;
	/// <summary>
	/// 下一步
	/// </summary>>
	public int next_plot;
	/// <summary>
	/// 剧情背景
	/// </summary>>
	public string bg_image;
	/// <summary>
	/// 是否主角对话
	/// </summary>>
	public bool main_character;
	/// <summary>
	/// 文本内容
	/// </summary>>
	public int plot_text;
}


public class PlotExcelData : ExcelDataBase<PlotExcelItem>
{
	public PlotExcelItem[] items;

	public Dictionary<int,PlotExcelItem> itemDic = new Dictionary<int,PlotExcelItem>();

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

	public PlotExcelItem GetPlotExcelItem(int id)
	{
		if(itemDic.ContainsKey(id))
			return itemDic[id];
		else
			return null;
	}
	#region --- Get Method ---

	public int GetPlot_type(int id)
	{
		var item = GetPlotExcelItem(id);
		if(item == null)
			return default;
		return item.plot_type;
	}

	public int GetPlot_npc(int id)
	{
		var item = GetPlotExcelItem(id);
		if(item == null)
			return default;
		return item.plot_npc;
	}

	public int GetNext_plot(int id)
	{
		var item = GetPlotExcelItem(id);
		if(item == null)
			return default;
		return item.next_plot;
	}

	public string GetBg_image(int id)
	{
		var item = GetPlotExcelItem(id);
		if(item == null)
			return default;
		return item.bg_image;
	}

	public bool GetMain_character(int id)
	{
		var item = GetPlotExcelItem(id);
		if(item == null)
			return default;
		return item.main_character;
	}

	public int GetPlot_text(int id)
	{
		var item = GetPlotExcelItem(id);
		if(item == null)
			return default;
		return item.plot_text;
	}

	#endregion
}


#if UNITY_EDITOR
public class PlotAssetAssignment
{
	public static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)
	{
		var allRowItemDicList = excelMediumData.GetAllRowItemDicList();
		if(allRowItemDicList == null || allRowItemDicList.Count == 0)
			return false;

		int rowCount = allRowItemDicList.Count;
		PlotExcelData excelDataAsset = ScriptableObject.CreateInstance<PlotExcelData>();
		excelDataAsset.items = new PlotExcelItem[rowCount];

		for(int i = 0; i < rowCount; i++)
		{
			var itemRowDic = allRowItemDicList[i];
			excelDataAsset.items[i] = new PlotExcelItem();
			excelDataAsset.items[i].id = StringUtility.StringToInt(itemRowDic["id"]);
			excelDataAsset.items[i].plot_type = StringUtility.StringToInt(itemRowDic["plot_type"]);
			excelDataAsset.items[i].plot_npc = StringUtility.StringToInt(itemRowDic["plot_npc"]);
			excelDataAsset.items[i].next_plot = StringUtility.StringToInt(itemRowDic["next_plot"]);
			excelDataAsset.items[i].bg_image = itemRowDic["bg_image"];
			excelDataAsset.items[i].main_character = StringUtility.StringToBool(itemRowDic["main_character"]);
			excelDataAsset.items[i].plot_text = StringUtility.StringToInt(itemRowDic["plot_text"]);
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



