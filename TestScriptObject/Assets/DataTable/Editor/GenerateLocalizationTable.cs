using System.Data;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using I2.Loc;
using static I2.Loc.LocalizationEditor;

enum eLocalSpreadsheeet { CSV, XLS, XLSX, NONE };


public class GenerateLocalizationTable:Editor
{
    /// <summary>
    /// 将Excel数据转为多语言插件可读取的格式
    /// </summary>
    /// <param name="origin_dt"></param>
    /// <returns></returns>
    public static void CreateLanguageAsset(ExcelMediumData excelMediumData, string assetSavePath)
    {
        List<Dictionary<string, string>> excel_data = excelMediumData.GetAllRowItemDicList();
        DataTable dataTable = new DataTable();
        List<string> tableHead = new List<string>();
        tableHead.Add("Key");
        tableHead.Add("Type");
        foreach (var item in excel_data[0])
        {
            if(item.Key.ToUpper() == "ID")
            {
                continue;
            }
            tableHead.Add(item.Key);
        }
        //创建列
        for (int i = 0; i < tableHead.Count; i++)
        {
            DataColumn dc = new DataColumn(tableHead[i]);
            dataTable.Columns.Add(dc);
        }
        Debug.Log(tableHead.Count);
        //创建行
        for (int i = 0; i < excel_data.Count; i++)
        {
            Dictionary<string, string> excel_row_data = excel_data[i];
            string[] aryLine = new string[tableHead.Count];
            DataRow new_row = dataTable.NewRow();
            for (int j = 0; j < aryLine.Length; j++)
            {
                switch (j)
                {
                    case 0:
                        aryLine[0] = excel_row_data.ElementAt(0).Value;
                        break;
                    case 1:
                        aryLine[1] = "Text";
                        break;
                    default:
                        //因为excel_row_data没有Type这一列，所以不是j是j-1
                        aryLine[j] = excel_row_data.ElementAt(j-1).Value;
                        break;
                }
                new_row[j] = aryLine[j];
            }
            dataTable.Rows.Add(new_row);
        }
        //计算新生成的文件路径
        string new_filePath = assetSavePath +"/"+ excelMediumData.excelName+".txt";
        //生成新文件
        GenerateTable(new_filePath, dataTable);
        //资源刷新
        AssetDatabase.Refresh();
        //多语言插件读取txt
        ToI2Localization(new_filePath);
    }

    /// <summary>
    /// 生成新的数据表
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="dt"></param>
    private static void GenerateTable(string filePath, DataTable dt)
    {
        FileInfo fi = new FileInfo(filePath);
        if (!fi.Directory.Exists)
        {
            fi.Directory.Create();
        }
        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
            {
                string data = "";
                //写入表头
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    data += dt.Columns[i].ColumnName.ToString();
                    if (i < dt.Columns.Count - 1)
                    {
                        data += '\t';
                    }
                }
                sw.WriteLine(data);
                //写入每一行每一列的数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string str = dt.Rows[i][j].ToString();
                        data += str;
                        if (j < dt.Columns.Count - 1)
                        {
                            data += '\t';
                        }
                    }
                    sw.WriteLine(data);
                }
                sw.Close();
                fs.Close();
            }
        }
    }

    private static void ToI2Localization(string filePath)
    {
        EditorUtility.DisplayProgressBar("多语言程序", "开干!!! (ง •_•)ง", 0f);
        var CurrentExtension = eLocalSpreadsheeet.CSV;
        var UpdateMode = eSpreadsheetUpdateMode.Replace;
        filePath = Application.dataPath.Replace("Assets", filePath);
        try
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                switch (CurrentExtension)
                {
                    case eLocalSpreadsheeet.CSV:
                        Import_CSV(filePath, UpdateMode);
                        break;
                }
            }
            else
            {
                Debug.LogError($"路径错误：{filePath}");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();
    }

    static void Import_CSV(string FileName, eSpreadsheetUpdateMode mode)
    {
        EditorUtility.DisplayProgressBar("多语言程序", "正在转换多语言文件", 0.3f);
        LanguageSourceAsset sourceAsset = Resources.Load<LanguageSourceAsset>(LocalizationManager.GlobalSources[0]);
        EditorUtility.SetDirty(sourceAsset);
        var source = sourceAsset.mSource;
        //UpgradeManager.CreateLanguageSources( source =>
        //{
        var encoding = System.Text.Encoding.UTF8;
        string CSVstring = LocalizationReader.ReadCSVfile(FileName, encoding);
        EditorUtility.DisplayProgressBar("多语言程序", "CSV读表完成", 0.6f);
        char Separator = '\t';
        string sError = source.Import_CSV(string.Empty, CSVstring, mode, Separator);

        if (!string.IsNullOrEmpty(sError))
        {
            Debug.LogError(sError);
        }
        else
        {
            //自定义需求
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = sourceAsset;
            AssetDatabase.Refresh();
        }
        EditorUtility.DisplayProgressBar("多语言程序", "转换多语言文件完成", 0.9f);
    }
}
