//-------------------------------------------------------------
// CodeSmith DBDocumenter Templates v3.0
// Author:  Jason Alexander (jalexander@telligent.com), Eric J. Smith
//-------------------------------------------------------------

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.ComponentModel.Design;
using CodeSmith.Engine;
using SchemaExplorer;

public class TemplateHelper : CodeTemplate
{
    // Number of columns that should be displayed on the summary lists.
    //public const int NUM_OF_COLUMNS = 3;


    public TemplateHelper() : base()
    {
    }

    private string outputDirectory = String.Empty;
    [Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [Optional]
    [Category("输出")]
    [Description("输出文件夹")]
    public string OutputDirectory
    {
        get
        {
            // default to the directory that the template is located in
            if (outputDirectory.Length == 0)
                return Path.Combine(CodeTemplateInfo.DirectoryName, "output\\");

            return outputDirectory;
        }
        set
        {
            if (!value.EndsWith("\\"))
                value += "\\";
            outputDirectory = value;
        }
    }

    public void OutputTemplate(CodeTemplate template)
    {
        this.CopyPropertiesTo(template);
        template.Render(this.Response);
    }

    public void DeleteFiles(string directory, string searchPattern)
    {
        string[] files = Directory.GetFiles(directory, searchPattern);

        for (int i = 0; i < files.Length; i++)
        {
            try
            {
                File.Delete(files[i]);
            }
            catch (Exception ex)
            {
                Response.WriteLine("Error while attempting to delete file (" + files[i] + ").\r\n" + ex.Message);
            }
        }
    }
    /// <summary>
    /// 获取系统类型
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    public string GetSystemType(ColumnSchema col)
    {
        string name = col.Name;
        string dataType = col.DataType.ToString();
        if (dataType == "Decimal")
        {
            return "decimal?";
        }
        else if (dataType == "String")
        {
            return "string";
        }
        else if (dataType == "DateTime")
        {
            return "DateTime?";
        }
        else if (dataType == "Int32")
        {
            return "int?";
        }
        else if (dataType == "Int64")
        {
            return "int?";
        }
        else
        {
            return col.SystemType.ToString();
        }
    }
    /// <summary>
    /// 获取属性名
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    public string GetPropName(ColumnSchema col)
    {
        string name = col.Name.ToLower();
        string[] names = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        string newName = "";
        for (var i = 1; i < names.Length; i++)
        {
            newName += names[i][0].ToString().ToUpper() + names[i].Substring(1);
        }
        return newName;
    }
    /// <summary>
    /// 获取属性名小写开头
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    public string GetPropNameLower(ColumnSchema col)
    {
        string name = col.Name.ToLower();
        string[] names = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        string newName = names[1][0].ToString().ToLower() + names[1].Substring(1);
        for (var i = 2; i < names.Length; i++)
        {
            newName += names[i][0].ToString().ToUpper() + names[i].Substring(1);
        }
        return newName;
    }
    /// <summary>
    /// 获取类名
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    public string GetClassName(TableSchema table)
    {
        string name = table.Name.ToLower();
        string[] names = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        string newName = "";
        for (var i = 1; i < names.Length; i++)
        {
            newName += names[i][0].ToString().ToUpper() + names[i].Substring(1);
        }
        return newName;
    }
}