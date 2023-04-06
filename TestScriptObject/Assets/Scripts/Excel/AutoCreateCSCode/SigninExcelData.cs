/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class SigninExcelItem : ExcelItemBase
{
	/// <summary>
	/// 编号
	/// </summary>>
	public int id;
	/// <summary>
	/// 奖励类型
	/// </summary>>
	public int rewardtype;
	/// <summary>
	/// 奖励数值
	/// </summary>>
	public int rewardpara;
	/// <summary>
	/// 是否是高级奖励
	/// </summary>>
	public bool bigreward;
}


public class SigninExcelData : ExcelDataBase<SigninExcelItem>
{
	public SigninExcelItem[] items;

	public Dictionary<int,SigninExcelItem> itemDic = new Dictionary<int,SigninExcelItem>();

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

	public SigninExcelItem GetSigninExcelItem(int id)
	{
		if(itemDic.ContainsKey(id))
			return itemDic[id];
		else
			return null;
	}
	#region --- Get Method ---

	public int GetRewardtype(int id)
	{
		var item = GetSigninExcelItem(id);
		if(item == null)
			return default;
		return item.rewardtype;
	}

	public int GetRewardpara(int id)
	{
		var item = GetSigninExcelItem(id);
		if(item == null)
			return default;
		return item.rewardpara;
	}

	public bool GetBigreward(int id)
	{
		var item = GetSigninExcelItem(id);
		if(item == null)
			return default;
		return item.bigreward;
	}

	#endregion
}


#if UNITY_EDITOR
public class SigninAssetAssignment
{
	public static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)
	{
		var allRowItemDicList = excelMediumData.GetAllRowItemDicList();
		if(allRowItemDicList == null || allRowItemDicList.Count == 0)
			return false;

		int rowCount = allRowItemDicList.Count;
		SigninExcelData excelDataAsset = ScriptableObject.CreateInstance<SigninExcelData>();
		excelDataAsset.items = new SigninExcelItem[rowCount];

		for(int i = 0; i < rowCount; i++)
		{
			var itemRowDic = allRowItemDicList[i];
			excelDataAsset.items[i] = new SigninExcelItem();
			excelDataAsset.items[i].id = StringUtility.StringToInt(itemRowDic["id"]);
			excelDataAsset.items[i].rewardtype = StringUtility.StringToInt(itemRowDic["rewardtype"]);
			excelDataAsset.items[i].rewardpara = StringUtility.StringToInt(itemRowDic["rewardpara"]);
			excelDataAsset.items[i].bigreward = StringUtility.StringToBool(itemRowDic["bigreward"]);
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



