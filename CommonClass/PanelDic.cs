using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.CommonClass
{
    public class PanelDic
    {
        /// <summary>
        /// 将panel内的控件的值与数据库表的字段绑定成键值对
        /// </summary>
        /// <param name="panel">目标panel，可递归</param>
        /// <param name="TableName">数据库表名</param>
        /// <param name="dic">new键值对</param>
        /// <returns>键值对</returns>
        public static Dictionary<string, string> DicPan(Panel panel, string TableName, Dictionary<string, string> dic)
        {
            string sql = string.Format("select top 1 * from {0} ", TableName);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            List<string> col = new List<string>();
            foreach (DataColumn item in dt.Columns)
            {
                col.Add(item.ColumnName);
            }
            foreach (Control ctr in panel.Controls)
            {
                if (ctr is Panel)
                {
                    Panel pan = (Panel)ctr;
                    DicPan(pan, TableName, dic);
                }
                else if (ctr is TextBox)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        dic.Add(field, ((TextBox)ctr).Text.Trim());
                    }
                }
                else if (ctr is HtmlInputHidden)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        dic.Add(field, ((HtmlInputHidden)ctr).Value.Trim());
                    }
                }
                else if (ctr is HtmlInputText)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        dic.Add(field, ((HtmlInputText)ctr).Value.Trim());
                    }
                }
                else if (ctr is Label)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        dic.Add(field, ((Label)ctr).Text.Trim());
                    }
                }
                else if (ctr is HiddenField)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        dic.Add(field, ((HiddenField)ctr).Value.Trim());
                    }
                }
                else if (ctr is RadioButton)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        if (((RadioButton)ctr).Checked)
                        {
                            dic.Add(field, "y");
                        }
                        else
                        {
                            dic.Add(field, "n");
                        }
                    }
                }
                else if (ctr is CheckBox)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        if (((CheckBox)ctr).Checked)
                        {
                            dic.Add(field, "y");
                        }
                        else
                        {
                            dic.Add(field, "n");
                        }
                    }
                }
                else if (ctr is RadioButtonList)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        dic.Add(field, ((RadioButtonList)ctr).SelectedValue);
                    }
                }
                else if (ctr is CheckBoxList)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    string value = "";
                    if (col.Contains(field))
                    {
                        foreach (ListItem item in ((CheckBoxList)ctr).Items)
                        {
                            if (item.Selected)
                            {
                                value += item.Value + "|";
                            }
                        }
                        value = value.Trim('|');
                        dic.Add(field, value);
                    }
                }
                else if (ctr is DropDownList)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        dic.Add(field, ((DropDownList)ctr).SelectedValue);
                    }
                }
            }
            return dic;
        }

        /// <summary>
        /// 将DataTable中的数据绑定到Panel中的控件
        /// </summary>
        /// <param name="panel">需要绑定的panel</param>
        /// <param name="dt">数据源DataTable</param>
        public static void BindPanel(Panel panel, DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            List<string> col = new List<string>();
            foreach (DataColumn item in dt.Columns)
            {
                col.Add(item.ColumnName);
            }
            foreach (Control ctr in panel.Controls)
            {
                if (ctr is Panel)
                {
                    Panel pan = (Panel)ctr;
                    BindPanel(pan, dt);
                }
                else if (ctr is TextBox)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        ((TextBox)ctr).Text = dr[field].ToString();
                    }
                }
                else if (ctr is HtmlInputHidden)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        ((HtmlInputHidden)ctr).Value = dr[field].ToString();
                    }
                }
                else if (ctr is HtmlInputText)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        ((HtmlInputText)ctr).Value = dr[field].ToString();
                    }
                }
                else if (ctr is Label)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        ((Label)ctr).Text = dr[field].ToString();
                    }
                }
                else if (ctr is HiddenField)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        ((HiddenField)ctr).Value = dr[field].ToString();
                    }
                }
                else if (ctr is RadioButton)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        if (dr[field].ToString() == "y")
                        {
                            ((RadioButton)ctr).Checked = true;
                        }
                        else
                        {
                            ((RadioButton)ctr).Checked = false;
                        }
                    }
                }
                else if (ctr is CheckBox)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        if (dr[field].ToString() == "y")
                        {
                            ((CheckBox)ctr).Checked = true;
                        }
                        else
                        {
                            ((CheckBox)ctr).Checked = false;
                        }
                    }
                }
                else if (ctr is RadioButtonList)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        ((RadioButtonList)ctr).SelectedValue = dr[field].ToString();
                    }
                }
                else if (ctr is CheckBoxList)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    string[] value = (dr[field].ToString()+"|").Split('|');
                    if (col.Contains(field))
                    {
                        foreach (ListItem item in ((CheckBoxList)ctr).Items)
                        {
                            if (value.Contains(item.Value))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                else if (ctr is DropDownList)
                {
                    string field = ctr.ID.Substring(ctr.ID.IndexOf('_') + 1);
                    if (col.Contains(field))
                    {
                        ((DropDownList)ctr).SelectedValue = dr[field].ToString();
                    }
                }
            }
        }
    }
}
