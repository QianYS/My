using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MyProject.Common.Files
{
    public class CommonExportExcel
    {
        /// <summary>
        /// 导出Excel返回FileStream类型
        /// </summary>
        /// <param name="modal">数据对象</param>
        /// <param name="fileName">文件名</param>
        /// <param name="startRowNum">数据主体关键词</param>
        /// <returns>FileStream</returns>
        public static void ExportExcel<TContent, TOther>(ExportExcelModal<TContent, TOther> modal, List<int> lstIndex, string startRowNum = "{List.RowNum}") where TContent : class where TOther : class
        {
            if (File.Exists(modal.TemplateFileName))
            {
                string extension = Path.GetExtension(modal.TemplateFileName).ToLower();
                FileStream readFileStream = new FileStream(modal.TemplateFileName, FileMode.Open, FileAccess.Read);

                IWorkbook workbook;
                if (extension == ".xlsx")
                {
                    workbook = new XSSFWorkbook(readFileStream);
                }
                else
                {
                    workbook = new HSSFWorkbook(readFileStream);
                }
                readFileStream.Close();

                foreach (var index in lstIndex)
                {
                    ISheet sheet = workbook.GetSheetAt(index);
                    sheet.ForceFormulaRecalculation = true;//计算表格公式

                    //标题 合计 统计
                    if (modal.Other != null)
                    {
                        Type t = modal.Other.GetType();
                        for (var i = 0; i <= sheet.LastRowNum; i++)
                        {
                            foreach (PropertyInfo pi in t.GetProperties())
                            {
                                var name = pi.Name;
                                SetCellValueByTemplateStr(sheet, i, "{" + name + "}", pi.GetValue(modal.Other, null));
                            }
                        }
                    }

                    //主体 数据内容
                    if (modal.Content != null)
                    {
                        //主体内容所在行
                        var rowNum = GetContentRowNum(sheet, startRowNum);
                        if (rowNum >= 0)
                        {
                            var columnsCount = sheet.GetRow(rowNum).LastCellNum;

                            //将底部内容下移数据内容行
                            if (modal.Content.Count - 1 > 0 && (rowNum + 1) < sheet.LastRowNum)
                            {
                                InsertRow(sheet, rowNum + 1, modal.Content.Count - 1, sheet.GetRow(rowNum), modal.Other != null);

                                //isShift && startRow <= sheet.LastRowNum
                            }
                            //获取主体内容行个单元格参数

                            //写入内容数据
                            WriteContent(sheet, rowNum, modal.Content);

                            //自动列宽
                            if (!modal.Content.GetType().AssemblyQualifiedName.Contains("ExportSalaryListDto") &&
                                !modal.Content.GetType().AssemblyQualifiedName.Contains("ExportBonusListDto") &&
                                !modal.Content.GetType().AssemblyQualifiedName.Contains("ExportLaborRewardListDto"))
                            {
                                for (int j = 0; j <= columnsCount; j++)
                                {
                                    sheet.AutoSizeColumn(j, true);
                                }
                            }
                        }
                    }
                }

                var filePath = Path.Combine(modal.FilePath, modal.FileToken);
                var writeFileStream = File.Create(filePath);
                workbook.Write(writeFileStream);
                workbook.Close();
                writeFileStream.Close();
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        /// <summary>
        /// 替换单元格模板值
        /// </summary>
        /// <param name="sheet">表</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="cellTemplateValue">单元格模板名称</param>
        /// <param name="cellFillValue">单元格值</param>
        /// <param name="conNextRow"></param>
        public static void SetCellValueByTemplateStr(ISheet sheet, int rowIndex, string cellTemplateValue, object cellFillValue, bool conNextRow = true)
        {
            int cellStartIndex = sheet.GetRow(rowIndex).FirstCellNum;
            int cellEndIndex = sheet.GetRow(rowIndex).LastCellNum;
            bool find = false;
            for (int i = cellStartIndex; i < cellEndIndex; i++)
            {
                if (find)
                    break;
                else
                {
                    ICell cell = sheet.GetRow(rowIndex).GetCell(i);
                    if (cell != null)
                    {
                        if (cell.CellType == CellType.String)
                        {
                            string strCellValue = sheet.GetRow(rowIndex).GetCell(i).StringCellValue;
                            if (string.Compare(strCellValue, cellTemplateValue, true) == 0)
                            {
                                find = true;
                                sheet.GetRow(rowIndex).GetCell(i).SetCellValue(GetValueType(cellFillValue));
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>值已经类型</returns>
        private static dynamic GetValueType(object value)
        {
            object obj = null;
            if (value != null)
            {
                Type type = value.GetType();
                switch (type.ToString())
                {
                    case "System.String":
                        obj = value.ToString();
                        break;
                    case "System.Int32":
                        obj = Convert.ToInt32(value.ToString());
                        break;
                    case "System.Decimal":
                        obj = Convert.ToDouble(value.ToString());
                        break;
                    default:
                        obj = value.ToString();
                        break;
                }
            }
            else
            {
                obj = string.Empty;
            }
            return obj;
        }
        /// <summary>
        /// 获取主体内容行的位置
        /// </summary>
        /// <param name="sheet">表</param>
        /// <param name="startRowNum">数据主体关键词</param>
        /// <returns></returns>
        public static int GetContentRowNum(ISheet sheet, string startRowNum)
        {
            for (var i = 0; i <= sheet.LastRowNum; i++)
            {
                int cellStartIndex = sheet.GetRow(i).FirstCellNum;
                int cellEndIndex = sheet.GetRow(i).LastCellNum;
                for (int j = cellStartIndex; j < cellEndIndex; j++)
                {
                    ICell cell = sheet.GetRow(i).GetCell(j);
                    if (cell != null)
                    {
                        if (cell.CellType == CellType.String)
                        {
                            if (cell.StringCellValue == startRowNum)
                            {
                                return i;
                            }
                        }
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="startRow">插入行</param>
        /// <param name="n">插入行总数</param>
        /// <param name="formatLine">源格式行</param>
        /// <param name="isShift">是否需要移动行</param>
        private static void InsertRow(ISheet sheet, int startRow, int n, IRow formatLine, bool isShift)
        {
            //批量移动行
            if (isShift)
            {
                sheet.ShiftRows(startRow, sheet.LastRowNum, n);
            }
            //对批量移动后空出的空行插，创建相应的行，并以插入行的上一行为格式源(即：插入行-1的那一行)
            for (int i = startRow; i < startRow + n; i++)
            {
                IRow targetRow = null;
                ICell sourceCell = null;
                ICell targetCell = null;

                targetRow = sheet.CreateRow(i);

                for (int j = formatLine.FirstCellNum; j < formatLine.LastCellNum; j++)
                {
                    sourceCell = formatLine.GetCell(j);
                    if (sourceCell == null)
                        continue;
                    targetCell = targetRow.CreateCell(j);

                    targetCell.CellStyle = sourceCell.CellStyle;
                    targetCell.SetCellType(sourceCell.CellType);
                }
            }
        }
        ///// <summary>
        ///// 获取数据主体参数
        ///// </summary>
        ///// <param name="sheet"></param>
        ///// <param name="startRow"></param>
        ///// <returns></returns>
        //private static List<string> GetCellStr(ISheet sheet, int startRow)
        //{
        //    var listTemplateStr = new List<string>();
        //    IRow row = sheet.GetRow(startRow);
        //    int cellStartIndex = row.FirstCellNum;
        //    int cellEndIndex = row.LastCellNum;
        //    for (int i = cellStartIndex; i < cellEndIndex; i++)
        //    {
        //        listTemplateStr.Add(row.GetCell(i).StringCellValue);
        //    }
        //    return listTemplateStr;
        //}
        /// <summary>
        /// 写入主体内容
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="startRow"></param>
        /// <param name="listDic"></param>
        private static void WriteContent<TContent>(ISheet sheet, int startRow, List<TContent> listDic)
        {
            List<ICell> listTemplateCells = new List<ICell>();
            IRow templateRow = sheet.GetRow(startRow);
            int cellEndIndex = templateRow.LastCellNum;
            for (int i = 0; i < cellEndIndex; i++)
            {
                listTemplateCells.Add(templateRow.GetCell(i));
            }

            for (var i = 0; i < listDic.Count; i++)
            {
                int rowindex = startRow + i;
                IRow row = sheet.CreateRow(rowindex);
                for (var j = 0; j < listTemplateCells.Count; j++)
                {
                    ICell templateCell = listTemplateCells[j];
                    if (templateCell != null && CellType.String == templateCell.CellType)
                    {
                        string strTemplate = templateCell.StringCellValue;
                        if (!string.IsNullOrEmpty(strTemplate))
                        {
                            ICell cell = row.CreateCell(j);
                            string patternFormulaTemplate = @"(?<=\{\=).*(?=})";//匹配 {=sum(A@Row:B@Row)}
                            string patternDataTemplate = @"(?<=\{List\.).*(?=})";//匹配 {List.Name}
                            if (Regex.IsMatch(strTemplate, patternFormulaTemplate, RegexOptions.IgnoreCase))
                            {
                                //设置公式
                                cell.SetCellType(CellType.Formula);
                                MatchCollection matches = Regex.Matches(strTemplate, patternFormulaTemplate, RegexOptions.IgnoreCase);
                                foreach (Match match in matches)
                                {
                                    string formula = match.Value;
                                    formula = Regex.Replace(formula, "@Row", (rowindex + 1).ToString(), RegexOptions.IgnoreCase);
                                    cell.SetCellFormula(formula);
                                    IFormulaEvaluator evaluator = WorkbookFactory.CreateFormulaEvaluator(sheet.Workbook);
                                    evaluator.EvaluateFormulaCell(cell);
                                }

                            }
                            else if (Regex.IsMatch(strTemplate, patternDataTemplate, RegexOptions.IgnoreCase))
                            {
                                //设置数据值
                                cell.SetCellValue(GetPropertiesValue(strTemplate, listDic[i]));

                            }
                            else
                            {
                                //原样
                                cell.SetCellValue(strTemplate);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取特性值
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="tObject">对象</param>
        /// <returns></returns>
        private static dynamic GetPropertiesValue<T>(string name, T tObject)
        {
            Type t = tObject.GetType();
            foreach (PropertyInfo pi in t.GetProperties())
            {
                if (string.Compare("{List." + pi.Name + "}", name) == 0)
                {
                    return GetValueType(pi.GetValue(tObject, null));
                }
            }
            return string.Empty;
        }

        private static dynamic GetCellCalue(ICell cell)
        {
            if (cell != null)
            {
                CellType cellType = cell.CellType;
                switch (cellType)
                {
                    case CellType.Boolean:
                        return cell.BooleanCellValue;
                    case CellType.Error:
                        return cell.ErrorCellValue;
                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(cell))
                        {
                            return cell.DateCellValue;
                        }
                        else
                        {
                            return cell.NumericCellValue;
                        }
                    case CellType.Formula:
                        IFormulaEvaluator evaluator = WorkbookFactory.CreateFormulaEvaluator(cell.Sheet.Workbook);
                        CellValue formulaValue = evaluator.Evaluate(cell);
                        switch (formulaValue.CellType)
                        {
                            case CellType.Boolean:
                                return formulaValue.BooleanValue;
                            case CellType.Error:
                                return formulaValue.ErrorValue;
                            case CellType.Numeric:
                                return formulaValue.NumberValue;
                            case CellType.String:
                                return formulaValue.StringValue;
                            case CellType.Blank:
                            default:
                                return "";
                        }
                    case CellType.String:
                        return cell.StringCellValue;
                    case CellType.Blank:
                    default:
                        return "";
                }
            }
            return null;
        }
    }
    /// <summary>
    /// 导出对象
    /// </summary>
    /// <typeparam name="TContent">数据主体</typeparam>
    /// <typeparam name="TOther">其他数据</typeparam>
    public class ExportExcelModal<TContent, TOther> where TContent : class where TOther : class
    {
        /// <summary>
        /// 模板文件名称
        /// </summary>
        public string TemplateFileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 保存文件名
        /// </summary>
        public string FileToken { get; set; }
        /// <summary>
        /// 数据主体
        /// </summary>
        public List<TContent> Content { get; set; }
        /// <summary>
        /// 其他数据
        /// </summary>
        public TOther Other { get; set; }
    }

    public class ExportExcelModal<TContent> : ExportExcelModal<TContent, object> where TContent : class
    {

    }

    /// <summary>
    /// 模板路径
    /// </summary>
    public class ExportTemplateFile
    {
        
    }
}
