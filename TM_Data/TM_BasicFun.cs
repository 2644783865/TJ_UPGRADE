using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace ZCZJ_DPF.TM_Data
{
    public class TM_BasicFun
    {
        #region 列的显示与隐藏
        /// <summary>
        /// 绑定CheckBoxList（需要显示，隐藏的列）
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cklName"></param>
        public static void BindCklShowHiddenItems(string type, CheckBoxList cklName)
        {
            switch (type)
            {
                case "Copy":
                    #region
                    cklName.Items.Clear();

                    cklName.Items.Add(new ListItem("序号", "1"));
                    cklName.Items.Add(new ListItem("图号", "2"));
                    cklName.Items.Add(new ListItem("物料编码", "3"));
                    cklName.Items.Add(new ListItem("总序", "4"));
                    cklName.Items.Add(new ListItem("中文名称", "5"));
                    cklName.Items.Add(new ListItem("备注", "6"));
                    cklName.Items.Add(new ListItem("材料长度", "7"));
                    cklName.Items.Add(new ListItem("材料宽度", "8"));
                    cklName.Items.Add(new ListItem("数量", "9"));
                    cklName.Items.Add(new ListItem("实际单重", "10"));
                    cklName.Items.Add(new ListItem("材料单重", "11"));
                    cklName.Items.Add(new ListItem("材料总重", "12"));
                    cklName.Items.Add(new ListItem("面域", "13"));
                    cklName.Items.Add(new ListItem("图纸上单重", "14"));
                    cklName.Items.Add(new ListItem("图纸上材质", "15"));
                    cklName.Items.Add(new ListItem("图纸上标准", "16"));
                    cklName.Items.Add(new ListItem("图纸上问题", "17"));
                    cklName.Items.Add(new ListItem("材质", "18"));
                    cklName.Items.Add(new ListItem("规格", "19"));
                    cklName.Items.Add(new ListItem("材料名称", "20"));
                    cklName.Items.Add(new ListItem("材料规格", "21"));
                    cklName.Items.Add(new ListItem("理论重量", "22"));
                    cklName.Items.Add(new ListItem("总重", "23"));
                    cklName.Items.Add(new ListItem("材料总长", "24"));
                    cklName.Items.Add(new ListItem("单位", "25"));
                    cklName.Items.Add(new ListItem("国标", "26"));
                    cklName.Items.Add(new ListItem("毛坯", "27"));
                    cklName.Items.Add(new ListItem("状态", "28"));
                    cklName.Items.Add(new ListItem("工艺流程", "29"));
                    cklName.Items.Add(new ListItem("英文名称", "30"));
                    cklName.Items.Add(new ListItem("关键部件", "31"));
                    cklName.Items.Add(new ListItem("定尺", "32"));


                    #endregion
                    break;
                case "Input":
                    #region
                    cklName.Items.Clear();
                    cklName.Items.Add(new ListItem("图号", "2"));                  
                    cklName.Items.Add(new ListItem("总序", "3"));
                    cklName.Items.Add(new ListItem("物料编码", "4"));
                    cklName.Items.Add(new ListItem("中文名称", "5"));
                    cklName.Items.Add(new ListItem("规格", "6"));
                    cklName.Items.Add(new ListItem("材质", "7"));
                    cklName.Items.Add(new ListItem("材料长度", "8"));
                    cklName.Items.Add(new ListItem("材料宽度", "9"));
                    cklName.Items.Add(new ListItem("下料备注", "10"));
                    cklName.Items.Add(new ListItem("数量", "11"));                   
                    cklName.Items.Add(new ListItem("图纸上单重", "12"));                   
                    cklName.Items.Add(new ListItem("图纸总重", "13"));
                    cklName.Items.Add(new ListItem("材料单重", "14"));
                    cklName.Items.Add(new ListItem("材料总重", "15"));
                    cklName.Items.Add(new ListItem("技术单位", "16"));
                    cklName.Items.Add(new ListItem("材料总量", "17"));                 
                    cklName.Items.Add(new ListItem("面域", "18"));
                    cklName.Items.Add(new ListItem("材料总长", "19"));
                    cklName.Items.Add(new ListItem("材料种类", "20"));                
                    cklName.Items.Add(new ListItem("下料", "21"));
                    cklName.Items.Add(new ListItem("工艺流程", "22"));              
                    cklName.Items.Add(new ListItem("库", "23"));
                    cklName.Items.Add(new ListItem("理论重量", "24"));                                
                    cklName.Items.Add(new ListItem("国标", "25"));
                    cklName.Items.Add(new ListItem("采购单位", "26"));                  
                    cklName.Items.Add(new ListItem("制作明细", "27"));
                    cklName.Items.Add(new ListItem("是否定尺", "28"));
                          cklName.Items.Add(new ListItem("材料计划", "29"));
                          cklName.Items.Add(new ListItem("备注", "30"));
                    #endregion
                    break;
                case "OrgView":
                    #region
                    cklName.Items.Clear();

                   cklName.Items.Add(new ListItem("C", "2"));
                   cklName.Items.Add(new ListItem("Z", "3"));
                    cklName.Items.Add(new ListItem("图号", "4"));
                    cklName.Items.Add(new ListItem("物料编码", "5"));
                    cklName.Items.Add(new ListItem("总序", "6"));
                    cklName.Items.Add(new ListItem("中文名称", "7"));
                    cklName.Items.Add(new ListItem("材料规格", "8"));
                    cklName.Items.Add(new ListItem("材质", "9"));
                    cklName.Items.Add(new ListItem("数量", "10"));
                    cklName.Items.Add(new ListItem("图纸单重", "11"));
                    cklName.Items.Add(new ListItem("图纸总重", "12"));
                    cklName.Items.Add(new ListItem("材料种类", "13"));
                    cklName.Items.Add(new ListItem("技术单位", "14"));
                    cklName.Items.Add(new ListItem("材料用量", "15"));
                    cklName.Items.Add(new ListItem("面域", "16"));
                    cklName.Items.Add(new ListItem("计划面域", "17"));
                    cklName.Items.Add(new ListItem("材料总长", "18"));
                    cklName.Items.Add(new ListItem("材料单重", "19"));
                    cklName.Items.Add(new ListItem("材料总重", "20"));

                    cklName.Items.Add(new ListItem("长度", "21"));
                    cklName.Items.Add(new ListItem("宽度", "22"));
                    cklName.Items.Add(new ListItem("下料备注", "23"));
                    cklName.Items.Add(new ListItem("下料方式", "24"));
                    cklName.Items.Add(new ListItem("工艺流程", "25"));
                    cklName.Items.Add(new ListItem("库", "26"));
                  
                    cklName.Items.Add(new ListItem("备注", "27"));
                    cklName.Items.Add(new ListItem("理论重量", "28"));
                    cklName.Items.Add(new ListItem("国标", "29"));
                    cklName.Items.Add(new ListItem("制作明细", "30"));
                    cklName.Items.Add(new ListItem("是否定尺", "31"));
                    cklName.Items.Add(new ListItem("材料计划", "32"));
                   
                    #endregion
                    break;
                case "MsOrgInputMode":
                    #region
                    cklName.Items.Clear();

                    cklName.Items.Add(new ListItem("明细序号", "2"));
                    cklName.Items.Add(new ListItem("序号", "3"));
                    cklName.Items.Add(new ListItem("图号", "4"));
                    cklName.Items.Add(new ListItem("物料编码", "5"));
                    cklName.Items.Add(new ListItem("总序", "6"));
                    cklName.Items.Add(new ListItem("中文名称", "7"));
                    cklName.Items.Add(new ListItem("体现", "8"));
                    cklName.Items.Add(new ListItem("备注", "9"));
                    cklName.Items.Add(new ListItem("材料长度", "10"));
                    cklName.Items.Add(new ListItem("材料宽度", "11"));
                    cklName.Items.Add(new ListItem("数量", "12"));
                    cklName.Items.Add(new ListItem("实际单重", "13"));
                    cklName.Items.Add(new ListItem("材料单重", "14"));
                    cklName.Items.Add(new ListItem("材料总重", "15"));
                    cklName.Items.Add(new ListItem("面域", "16"));
                    //cklName.Items.Add(new ListItem("条件属性", "17"));
                    cklName.Items.Add(new ListItem("图纸上单重", "17"));
                    cklName.Items.Add(new ListItem("图纸上材质", "18"));
                    cklName.Items.Add(new ListItem("图纸上标准", "19"));
                    cklName.Items.Add(new ListItem("图纸上问题", "20"));
                    cklName.Items.Add(new ListItem("材质", "21"));
                    cklName.Items.Add(new ListItem("规格", "22"));
                    cklName.Items.Add(new ListItem("材料名称", "23"));
                    cklName.Items.Add(new ListItem("材料规格", "24"));
                    cklName.Items.Add(new ListItem("理论重量", "25"));
                    cklName.Items.Add(new ListItem("总重", "26"));
                    cklName.Items.Add(new ListItem("材料总长", "27"));
                    cklName.Items.Add(new ListItem("单位", "28"));
                    cklName.Items.Add(new ListItem("国标", "29"));
                    cklName.Items.Add(new ListItem("材料种类", "30"));
                    cklName.Items.Add(new ListItem("下料", "31"));
                    cklName.Items.Add(new ListItem("工艺流程", "32"));
                    cklName.Items.Add(new ListItem("英文名称", "33"));
                    cklName.Items.Add(new ListItem("关键部件", "34"));
                    cklName.Items.Add(new ListItem("定尺", "35"));
                    cklName.Items.Add(new ListItem("材料计划", "36"));
                    cklName.Items.Add(new ListItem("库", "37"));
                    #endregion
                    break;
                default:

                    break;
            }
            cklName.Items.Insert(0,new ListItem("勾选时隐藏列", "Hidden"));
            cklName.Items[0].Attributes.Add("style", "color:red");
            cklName.Items[0].Selected=true;
            cklName.Items[0].Enabled = false;
            cklName.Items.Insert(1,new ListItem("默认设置","default"));
        }
        /// <summary>
        /// 显示与隐藏列
        /// </summary>
        /// <param name="gridview"></param>
        /// <param name="item"></param>
        /// <param name="type"></param>
        public static void HiddenShowColumn(GridView gridview, CheckBoxList cklName,int index,string type)
        {
          
             if (index == 1)//默认设置项
            {
                #region
                if (cklName.Items[index].Selected)//显示默认项
                {
                    ShowDefault(gridview, cklName, index, type);
                }
                else
                {
                    for (int i = 2; i < cklName.Items.Count; i++)
                    {
                        cklName.Items[i].Selected = false;
                        gridview.Columns[i].HeaderStyle.CssClass = "show";
                        gridview.Columns[i].ItemStyle.CssClass = "show";
                        gridview.Columns[i].FooterStyle.CssClass = "show";
                    }
                }
                #endregion
            }
            else
            {
                #region
                int hiddenshowindex = Convert.ToInt16(cklName.Items[index].Value);

                if (cklName.Items[0].Selected)
                {
                    if (cklName.Items[index].Selected)
                    {
                        gridview.Columns[hiddenshowindex].HeaderStyle.CssClass = "hidden";
                        gridview.Columns[hiddenshowindex].ItemStyle.CssClass = "hidden";
                        gridview.Columns[hiddenshowindex].FooterStyle.CssClass = "hidden";
                    }
                    else
                    {
                        gridview.Columns[hiddenshowindex].HeaderStyle.CssClass = "show";
                        gridview.Columns[hiddenshowindex].ItemStyle.CssClass = "show";
                        gridview.Columns[hiddenshowindex].FooterStyle.CssClass = "show";
                    }
                }
               
                #endregion

            }
         //   gridview.FixRowColumn.FixRowType = "Header,Pager";

        }

        public static  void ShowDefault(GridView gridview,CheckBoxList ckbName,int index,string type)
        {
            string defautcolumns = ReturnDefaultColumnString(type);
            for (int i = 2; i < ckbName.Items.Count; i++)
            {
                string columsindex="$"+ckbName.Items[i].Value.ToString()+"$";
                int columns = Convert.ToInt16(ckbName.Items[i].Value.ToString());
                if (ckbName.Items[0].Selected)//勾选时隐藏列
                {
                    #region
                    if (ckbName.Items[1].Selected)//默认设置
                    {
                        if (defautcolumns.Contains(columsindex))
                        {
                            ckbName.Items[i].Selected = false;
                            gridview.Columns[columns].HeaderStyle.CssClass = "show";
                            gridview.Columns[columns].ItemStyle.CssClass = "show";
                            gridview.Columns[columns].FooterStyle.CssClass = "show";
                        }
                        else
                        {
                            ckbName.Items[i].Selected = true;
                            gridview.Columns[columns].HeaderStyle.CssClass = "hidden";
                            gridview.Columns[columns].ItemStyle.CssClass = "hidden";
                            gridview.Columns[columns].FooterStyle.CssClass = "hidden";

                        }
                    }
                    else
                    {
                        if (ckbName.Items[i].Selected)
                        {
                            gridview.Columns[columns].HeaderStyle.CssClass = "hidden";
                            gridview.Columns[columns].ItemStyle.CssClass = "hidden";
                            gridview.Columns[columns].FooterStyle.CssClass = "hidden";
                        }
                        else
                        {
                            gridview.Columns[columns].HeaderStyle.CssClass = "show";
                            gridview.Columns[columns].ItemStyle.CssClass = "show";
                            gridview.Columns[columns].FooterStyle.CssClass = "show";
                        }
                    }
                    #endregion
                }
                else //勾选显示列
                {
                    #region
                    if (ckbName.Items[1].Selected)
                    {
                        if (defautcolumns.Contains(columsindex))
                        {
                            ckbName.Items[i].Selected = true;
                            gridview.Columns[columns].HeaderStyle.CssClass = "show";
                            gridview.Columns[columns].ItemStyle.CssClass = "show";
                            gridview.Columns[columns].FooterStyle.CssClass = "show";
                        }
                        else
                        {
                            ckbName.Items[i].Selected = false;
                            gridview.Columns[columns].HeaderStyle.CssClass = "hidden";
                            gridview.Columns[columns].ItemStyle.CssClass = "hidden";
                            gridview.Columns[columns].FooterStyle.CssClass = "hidden";

                        }
                    }
                    else
                    {
                        if (ckbName.Items[i].Selected)
                        {
                            gridview.Columns[columns].HeaderStyle.CssClass = "show";
                            gridview.Columns[columns].ItemStyle.CssClass = "show";
                            gridview.Columns[columns].FooterStyle.CssClass = "show";
                        }
                        else
                        {
                            gridview.Columns[columns].HeaderStyle.CssClass = "hidden";
                            gridview.Columns[columns].ItemStyle.CssClass = "hidden";
                            gridview.Columns[columns].FooterStyle.CssClass = "hidden";
                        }
                    }
                    #endregion
                }
            }
        }

        public static string ReturnDefaultColumnString(string type)
        {
            string retvalue = "";
            switch (type)
            {
                /*==============================
                    图号	2
                    物料编码	3
                    总序	4
                    中文名称	5
                    材料长度	7
                    材料宽度	8
                    数量	9
                    实际单重	10
                    材料单重	11
                    图纸上单重	14
                    材质	18
                    规格	19
                    工艺流程	29
                 =============================*/
                case "Copy":
                    retvalue = "$2$3$4$5$7$8$9$10$11$14$18$19$21$22$29$33$34$35$";
                    break;
                /*==============================
                    图号	2
                    物料编码	3
                    总序	4
                    中文名称	5
                    备注	6
                    材料长度	7
                    材料宽度	8
                    数量	9
                    实际单重	10
                    材料单重	11
                    面域	13
                    条件属性	14
                    图纸上单重	15
                    材质	19
                    规格	20
                   =============================*/
                case "Input":
                    retvalue = "$2$3$4$5$6$7$8$9$11$13$15$16$17$20$21$22$27$28$29$";
                    break;
                /*==============================
                    明细序号	5
                    序号	6
                    图号(标识号)	7
                    物料编码	8
                    总序	9
                    中文名称	10
                    规格	11
                    库	12
                    材料规格	17
                    长度(mm)	18
                    宽度(mm)	19
                    材料单重	21
                    数量	26
                    单重	27
                    图纸上单重	29
                 =============================*/
                case "OrgView":
                    retvalue = "$2$3$4$5$6$7$8$9$10$11$12$13$14$15$16$17$18$19$22$23$24$27$30$31$32$";
                    break;
                /*==============================
                    明细序号 2
                    序号     3
                    图号	4
                    物料编码	5
                    总序	6
                    中文名称	7
                    体现        8
                    备注	9
                    材料长度	10
                    材料宽度	11
                    数量	12
                    实际单重	13
                    材料单重	14
                    面域	16
                    条件属性	17
                    图纸上单重	18
                    材质	22
                    规格	23
                    毛坯    31
                    状态    32
                    库  38
                    =============================*/
                case "MsOrgInputMode":
                    retvalue = "$2$3$4$5$6$7$8$9$10$11$12$13$14$16$17$18$22$23$31$32$38$";
                    break;
                default: break;
            }
            return retvalue;
        }
        #endregion

        #region 采购能否驳回计划的判断
        public static string PurRejectJudge(string pur_pcode)
        {
            string retValue = "0";
            if(pur_pcode.Contains("/BOM_"))
            {
                System.Data.DataTable dt;
                try
                {
                    SqlConnection sqlConn = new SqlConnection();
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                    DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_PurPlanRejectJudge]");
                    DBCallCommon.AddParameterToStoredProc(sqlCmd, "@PT_CODE", pur_pcode, SqlDbType.Text, 1000);
                    sqlConn.Open();
                    dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                    sqlConn.Close();
                }
                catch (Exception)
                {
                    throw;
                }
                retValue=dt.Rows[0][0].ToString();
            }
            return retValue;
        }

        #endregion
    }
}
