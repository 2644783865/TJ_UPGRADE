using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Cost_Budget_Add_Detail : BasicPage
    {
        int shengChan, caiGou, caiWu, jinDu, shenHe, yiJi, erJi;
        string tsaId, userName, uid, depId, position;
        protected void Page_Load(object sender, EventArgs e)
        {
            userName = Session["UserName"].ToString();
            uid = Session["UserID"].ToString();
            depId = Session["UserDeptID"].ToString();
            position = Session["POSITION"].ToString(); ;
            tsaId = Request.QueryString["tsaId"].ToString();
            GetState(tsaId);

            if (!IsPostBack)
            {
                SetTSAInfo(tsaId);
                BindRepeaterSource(rpt_YS_FERROUS_METAL, pal_No_YS_FERROUS_METAL, tsaId, "01.07");//黑色金属
                BindRepeaterSource(rpt_YS_PURCHASE_PART, pal_No_YS_PURCHASE_PART, tsaId, "01.11");//外购件                
                BindRepeaterSource(rpt_YS_PAINT_COATING, pal_No_YS_PAINT_COATING, tsaId, "01.15");//油漆涂料
                BindRepeaterSource(rpt_YS_ELECTRICAL, pal_No_YS_ELECTRICAL, tsaId, "01.03");//电气电料  
                BindCastingForgingRepeaterSource(rpt_YS_CASTING_FORGING_COST, pal_No_YS_CASTING_FORGING_COST, tsaId);//铸锻件
                BindRepeaterSource(rpt_YS_OTHERMAT_COST, pal_No_YS_OTHERMAT_COST, tsaId);//其他材料
                ControlerVisile();

            }
        }

        /// <summary>
        /// 权限、控件的可见性、可用性
        /// </summary>
        protected void ControlerVisile()
        {
            if (depId == "06")    //06财务部s
            {
                if (jinDu <= 1)    //预算编制进度为新增或财务填写中
                {
                    //输入
                    txt_YS_MATERIAL_COST.ReadOnly = false;
                    txt_YS_LABOUR_COST.ReadOnly = false;
                    txt_YS_NOTE.ReadOnly = false;
                    //按钮
                    btn_Save.Visible = true;
                    btn_PushDown.Visible = true;

                }
                else if (caiWu == 1 && position == "0601")    //财务调整与审核为待审批时，且登陆角色为0501
                {
                    //输入
                    txt_YS_MATERIAL_COST.ReadOnly = false;
                    txt_YS_LABOUR_COST.ReadOnly = false;
                    txt_YS_NOTE.ReadOnly = false;
                    //按钮
                    btn_Save.Visible = true;
                    rdl_CaiWuCheck.Enabled = true;
                    txt_YS_CAIWU_YJ.ReadOnly = false;
                    txt_YS_CAIWU_YJ.BackColor = System.Drawing.Color.Orange;
                }

            }
            else if (depId == "05" && caiGou == 1)    //05采购部，采购状态为待反馈
            {
                btn_Save.Visible = true;
                #region 使repeater中的反馈项可用
                foreach (RepeaterItem Item in rpt_YS_FERROUS_METAL.Items)
                {
                    ((TextBox)Item.FindControl("txt_YS_FERROUS_METAL_Average_Price_FB")).ReadOnly = false;
                    ((TextBox)Item.FindControl("txt_YS_NOTE")).ReadOnly = false;
                };
                foreach (RepeaterItem Item in rpt_YS_PURCHASE_PART.Items)
                {
                    ((TextBox)Item.FindControl("txt_YS_PURCHASE_PART_Average_Price_FB")).ReadOnly = false;
                    ((TextBox)Item.FindControl("txt_YS_NOTE")).ReadOnly = false;
                };
                foreach (RepeaterItem Item in rpt_YS_PAINT_COATING.Items)
                {
                    ((TextBox)Item.FindControl("txt_YS_PAINT_COATING_Average_Price_FB")).ReadOnly = false;
                    ((TextBox)Item.FindControl("txt_YS_NOTE")).ReadOnly = false;
                };
                foreach (RepeaterItem Item in rpt_YS_ELECTRICAL.Items)
                {
                    ((TextBox)Item.FindControl("txt_YS_ELECTRICAL_Average_Price_FB")).ReadOnly = false;
                    ((TextBox)Item.FindControl("txt_YS_NOTE")).ReadOnly = false;
                };
                foreach (RepeaterItem Item in rpt_YS_CASTING_FORGING_COST.Items)
                {
                    ((TextBox)Item.FindControl("txt_YS_CASTING_FORGING_COST_Average_Price_FB")).ReadOnly = false;
                    ((TextBox)Item.FindControl("txt_YS_NOTE")).ReadOnly = false;
                };
                foreach (RepeaterItem Item in rpt_YS_OTHERMAT_COST.Items)
                {
                    ((TextBox)Item.FindControl("txt_YS_OTHERMAT_COST_Average_Price_FB")).ReadOnly = false;
                    ((TextBox)Item.FindControl("txt_YS_NOTE")).ReadOnly = false;
                };
                #endregion
                rdl_CaiGouCheck.Enabled = true;
                txt_YS_CAIGOU_YJ.ReadOnly = false;
                txt_YS_CAIGOU_YJ.BackColor = System.Drawing.Color.Orange;
            }
            else if (depId == "04" && shengChan == 1)    //04生产部，生产状态为待反馈
            {
                btn_Save.Visible = true;
                txt_YS_UNIT_LABOUR_COST_FB.ReadOnly = false;
                rdl_ShengChanCheck.Enabled = true;
                txt_YS_SHENGCHAN_YJ.ReadOnly = false;
                txt_YS_SHENGCHAN_YJ.BackColor = System.Drawing.Color.Orange;
            }
            else if (yiJi == 1 && position == "0102")
            {
                rdl_YiJiCheck.Enabled = true;
                txt_YS_FIRST_REV_YJ.ReadOnly = false;
                txt_YS_FIRST_REV_YJ.BackColor = System.Drawing.Color.Orange;
            }
            else if (erJi == 1 && position == "0101")
            {
                rdl_ErJiCheck.Enabled = true;
                txt_YS_SECOND_REV_YJ.ReadOnly = false;
                txt_YS_SECOND_REV_YJ.BackColor = System.Drawing.Color.Orange;
            }



        }

        #region 获取任务号信息并填充到页面上

        /// <summary>
        /// 获取任务号的各个状态
        /// </summary>
        /// <param name="id"></param>
        protected void GetState(string id)
        {
            string sql = "select YS_SHENGCHAN,YS_CAIGOU,YS_CAIWU,YS_STATE,YS_REVSTATE,YS_FIRST_REVSTATE,YS_SECOND_REVSTATE FROM YS_COST_BUDGET where YS_TSA_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            //获取生产、采购、财务、编制进度、审核、一级审核、二级审核的状态
            shengChan = int.Parse(dt.Rows[0]["YS_SHENGCHAN"].ToString());
            caiGou = int.Parse(dt.Rows[0]["YS_CAIGOU"].ToString());
            caiWu = int.Parse(dt.Rows[0]["YS_CAIWU"].ToString());
            jinDu = int.Parse(dt.Rows[0]["YS_STATE"].ToString());
            shenHe = int.Parse(dt.Rows[0]["YS_REVSTATE"].ToString());
            yiJi = int.Parse(dt.Rows[0]["YS_FIRST_REVSTATE"].ToString());
            erJi = int.Parse(dt.Rows[0]["YS_SECOND_REVSTATE"].ToString());
        }

        /// <summary>
        /// 填充任务号的基本信息
        /// </summary>
        /// <param name="id">任务号</param>
        public void SetTSAInfo(string id)
        {
            string sql = @"select YS_BUDGET_INCOME,YS_WEIGHT,YS_CONTRACT_NO,YS_PROJECTNAME,YS_ENGINEERNAME,YS_ADDNAME,YS_ADDTIME,YS_NOTE,
YS_MATERIAL_COST,YS_LABOUR_COST,YS_TRANS_COST,
(YS_MATERIAL_COST+YS_LABOUR_COST+YS_TRANS_COST) AS YS_TOTALCOST_ALL,(YS_BUDGET_INCOME-YS_TOTALCOST_ALL) AS YS_PROFIT,YS_PROFIT/YS_TOTALCOST_ALL AS YS_PROFIT_RATE,
YS_FERROUS_METAL,YS_PURCHASE_PART,YS_PAINT_COATING,YS_ELECTRICAL,YS_CASTING_FORGING_COST,YS_OTHERMAT_COST,
(YS_FERROUS_METAL+YS_PURCHASE_PART+YS_PAINT_COATING+YS_ELECTRICAL+YS_CASTING_FORGING_COST+YS_OTHERMAT_COST) AS materil_history_reference,
YS_FERROUS_METAL_FB,YS_PURCHASE_PART_FB,YS_PAINT_COATING_FB,YS_ELECTRICAL_FB,YS_CASTING_FORGING_COST_FB,YS_OTHERMAT_COST_FB,
(YS_FERROUS_METAL_FB+YS_PURCHASE_PART_FB+YS_PAINT_COATING_FB+YS_ELECTRICAL_FB+YS_CASTING_FORGING_COST_FB+YS_OTHERMAT_COST_FB) AS materil_dispart_reference,
YS_UNIT_LABOUR_COST_FB,YS_WEIGHT*YS_UNIT_LABOUR_COST_FB AS labour_dispart_reference, 
YS_SHENGCHAN_NAME,YS_SHENGCHAN_SJ,YS_SHENGCHAN_YJ,
YS_CAIGOU_NAME,YS_CAIGOU_SJ,YS_CAIGOU_YJ,
YS_CAIWU_NAME,YS_CAIWU_SJ,YS_CAIWU_YJ,
YS_FIRST_REV_NAME,YS_FIRST_REV_SJ,YS_FIRST_REV_YJ,
YS_SECOND_REV_NAME,YS_SECOND_REV_SJ,YS_SECOND_REV_YJ  
from YS_COST_BUDGET where YS_TSA_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            //预算基本信息            
            txt_YS_TSA_ID.Text = id;
            txt_YS_BUDGET_INCOME.Text = dt.Rows[0]["YS_BUDGET_INCOME"].ToString();//任务号预算收入
            txt_YS_WEIGHT.Text = dt.Rows[0]["YS_WEIGHT"].ToString();//任务号重量
            txt_YS_CONTRACT_NO.Text = dt.Rows[0]["YS_CONTRACT_NO"].ToString();//合同号
            txt_YS_PROJECTNAME.Text = dt.Rows[0]["YS_PROJECTNAME"].ToString();//项目名称
            txt_YS_ENGINEERNAME.Text = dt.Rows[0]["YS_ENGINEERNAME"].ToString();
            txt_YS_ADDNAME.Text = dt.Rows[0]["YS_ADDNAME"].ToString();//财务制单人
            txt_YS_ADDTIME.Text = dt.Rows[0]["YS_ADDTIME"].ToString();//提交时间
            txt_YS_NOTE.Text = dt.Rows[0]["YS_NOTE"].ToString();//备注
            //txt_YS_NOTE.Text = uid + userName + depId;

            //预算费用分配
            txt_YS_MATERIAL_COST.Text = dt.Rows[0]["YS_MATERIAL_COST"].ToString();//材料费
            txt_YS_LABOUR_COST.Text = dt.Rows[0]["YS_LABOUR_COST"].ToString();//人工费
            txt_YS_TRANS_COST.Text = dt.Rows[0]["YS_TRANS_COST"].ToString();//运费
            //txt_YS_TOTALCOST_ALL.Text = dt.Rows[0]["YS_TOTALCOST_ALL"].ToString();//预算总额
            //txt_YS_PROFIT.Text = dt.Rows[0]["YS_PROFIT"].ToString();//毛利润
            //txt_YS_PROFIT_RATE.Text = dt.Rows[0]["YS_PROFIT_RATE"].ToString();//毛利率

            //材料费参考
            //txt_YS_FERROUS_METAL.Text = dt.Rows[0]["YS_FERROUS_METAL"].ToString();
            //txt_YS_PURCHASE_PART.Text = dt.Rows[0]["YS_PURCHASE_PART"].ToString();
            //txt_YS_PAINT_COATING.Text = dt.Rows[0]["YS_PAINT_COATING"].ToString();
            //txt_YS_ELECTRICAL.Text = dt.Rows[0]["YS_ELECTRICAL"].ToString();
            //txt_YS_CASTING_FORGING_COST.Text = dt.Rows[0]["YS_CASTING_FORGING_COST"].ToString();
            //txt_YS_OTHERMAT_COST.Text = dt.Rows[0]["YS_OTHERMAT_COST"].ToString();
            //txt_materil_history_reference.Text = dt.Rows[0]["materil_history_reference"].ToString();

            //部门反馈
            //txt_YS_FERROUS_METAL_FB.Text = dt.Rows[0]["YS_FERROUS_METAL_FB"].ToString();
            //txt_YS_PURCHASE_PART_FB.Text = dt.Rows[0]["YS_PURCHASE_PART_FB"].ToString();
            //txt_YS_PAINT_COATING_FB.Text = dt.Rows[0]["YS_PAINT_COATING_FB"].ToString();
            //txt_YS_ELECTRICAL_FB.Text = dt.Rows[0]["YS_ELECTRICAL_FB"].ToString();
            //txt_YS_CASTING_FORGING_COST_FB.Text = dt.Rows[0]["YS_CASTING_FORGING_COST_FB"].ToString();
            //txt_YS_OTHERMAT_COST_FB.Text = dt.Rows[0]["YS_OTHERMAT_COST_FB"].ToString();
            //txt_materil_dispart_reference.Text = dt.Rows[0]["materil_dispart_reference"].ToString();

            txt_YS_UNIT_LABOUR_COST_FB.Text = dt.Rows[0]["YS_UNIT_LABOUR_COST_FB"].ToString();
            txt_labour_dispart_reference.Text = dt.Rows[0]["labour_dispart_reference"].ToString();






            #region 绑定审批结果、审批人、审批时间
            switch (caiGou)
            {
                case 2: rdl_CaiGouCheck.SelectedIndex = 0; break;
                case 3: rdl_CaiGouCheck.SelectedIndex = 1; break;
                default: break;
            }
            lb_YS_CAIGOU_NAME.Text = dt.Rows[0]["YS_CAIGOU_NAME"].ToString();
            lb_YS_CAIGOU_SJ.Text = dt.Rows[0]["YS_CAIGOU_SJ"].ToString();
            txt_YS_CAIGOU_YJ.Text = dt.Rows[0]["YS_CAIGOU_YJ"].ToString();

            switch (shengChan)
            {
                case 2: rdl_ShengChanCheck.SelectedIndex = 0; break;
                case 3: rdl_ShengChanCheck.SelectedIndex = 1; break;
                default: break;
            }
            lb_YS_SHENGCHAN_NAME.Text = dt.Rows[0]["YS_SHENGCHAN_NAME"].ToString();
            lb_YS_SHENGCHAN_SJ.Text = dt.Rows[0]["YS_SHENGCHAN_SJ"].ToString();
            txt_YS_SHENGCHAN_YJ.Text = dt.Rows[0]["YS_SHENGCHAN_YJ"].ToString();

            switch (caiWu)
            {
                case 2: rdl_CaiWuCheck.SelectedIndex = 0; break;
                case 3: rdl_CaiWuCheck.SelectedIndex = 1; break;
                default: break;
            }
            lb_YS_CAIWU_NAME.Text = dt.Rows[0]["YS_CAIWU_NAME"].ToString();
            lb_YS_CAIWU_SJ.Text = dt.Rows[0]["YS_CAIWU_SJ"].ToString();
            txt_YS_CAIWU_YJ.Text = dt.Rows[0]["YS_CAIWU_YJ"].ToString();

            switch (yiJi)
            {
                case 2: rdl_YiJiCheck.SelectedIndex = 0; break;
                case 3: rdl_YiJiCheck.SelectedIndex = 1; break;
                default: break;
            }
            lb_YS_FIRST_REV_NAME.Text = dt.Rows[0]["YS_FIRST_REV_NAME"].ToString();
            lb_YS_FIRST_REV_SJ.Text = dt.Rows[0]["YS_FIRST_REV_SJ"].ToString();
            txt_YS_FIRST_REV_YJ.Text = dt.Rows[0]["YS_FIRST_REV_YJ"].ToString();

            switch (erJi)
            {
                case 2: rdl_ErJiCheck.SelectedIndex = 0; break;
                case 3: rdl_ErJiCheck.SelectedIndex = 1; break;
                default: break;
            }
            lb_YS_SECOND_REV_NAME.Text = dt.Rows[0]["YS_SECOND_REV_NAME"].ToString();
            lb_YS_SECOND_REV_SJ.Text = dt.Rows[0]["YS_SECOND_REV_SJ"].ToString();
            txt_YS_SECOND_REV_YJ.Text = dt.Rows[0]["YS_SECOND_REV_YJ"].ToString();
            #endregion



        }

        /// <summary>
        /// 绑定Repeater数据
        /// </summary>
        /// <param name="rpt">Repeater</param>
        /// <param name="id">任务号</param>       
        /// <param name="code">物料编码前5位，(带.)</param>
        protected void BindRepeaterSource(Repeater rpt, Panel panel, string id, string code)
        {
            string sql1 = String.Format(@"select YS_CODE ,YS_NAME ,YS_Union_Amount ,YS_Average_Price,YS_Average_Price_FB,YS_NOTE from  YS_COST_BUDGET_DETAIL 
where YS_TSA_ID='{0}' AND YS_CODE LIKE '{1}%' ORDER BY YS_CODE", id, code);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
            if (dt.Rows.Count != 0)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
            }
            else
            {
                rpt.Visible = false;
                panel.Visible = true;
            }

        }
        /// <summary>
        /// 绑定铸锻件repeater
        /// </summary>
        /// <param name="rpt"></param>
        /// <param name="panel"></param>
        /// <param name="id"></param>
        protected void BindCastingForgingRepeaterSource(Repeater rpt, Panel panel, string id)
        {
            string sql1 = String.Format(@"select YS_CODE ,YS_NAME ,YS_Union_Amount,YS_WEIGHT ,YS_Average_Price,YS_Average_Price_FB,YS_NOTE from  YS_COST_BUDGET_DETAIL 
where YS_TSA_ID='{0}' AND ( YS_CODE LIKE '01.08%' OR YS_CODE LIKE '01.09%') ORDER BY YS_CODE", id);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
            if (dt.Rows.Count != 0)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
            }
            else
            {
                rpt.Visible = false;
                panel.Visible = true;
            }

        }
        /// <summary>
        /// 绑定其他材料Repeater数据
        /// </summary>
        /// <param name="rpt">Repeater</param>
        /// <param name="id">任务号</param>
        /// <param name="condition">like或not like</param>
        /// <param name="code">物料编码前5位，(带.)</param>
        protected void BindRepeaterSource(Repeater rpt, Panel panel, string id)
        {
            string sql = String.Format(@"select YS_CODE ,YS_NAME ,YS_Union_Amount ,YS_Average_Price,YS_Average_Price_FB,YS_NOTE from  YS_COST_BUDGET_DETAIL 
where YS_TSA_ID='{0}' AND YS_CODE NOT LIKE '01.07%' AND YS_CODE NOT LIKE '01.11%' AND YS_CODE NOT LIKE '01.08%'AND YS_CODE NOT LIKE '01.09%' AND YS_CODE NOT LIKE '01.15%' AND YS_CODE NOT LIKE '01.03%' ORDER BY YS_CODE", id);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count != 0)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
            }
            else
            {
                rpt.Visible = false;
                panel.Visible = true;
            }
        }

        #endregion




        #region 审核与反馈结果单选按钮触发的事件

        //采购
        protected void rdl_CaiGouCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdl_CaiGouCheck.SelectedIndex == 0)    //同意
            {
                btn_RebutToCaiWu.Visible = false;
                btn_Submit.Visible = true;
            }
            else    //不同意
            {
                btn_RebutToCaiWu.Visible = true;
                btn_Submit.Visible = false;
            }
        }
        //生产
        protected void rdl_ShengChanCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdl_ShengChanCheck.SelectedIndex == 0)    //同意
            {
                btn_RebutToCaiWu.Visible = false;
                btn_Submit.Visible = true;
            }
            else    //不同意
            {
                btn_RebutToCaiWu.Visible = true;
                btn_Submit.Visible = false;
            }
        }
        //财务
        protected void rdl_CaiWuCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdl_CaiWuCheck.SelectedIndex == 0)    //同意
            {
                btn_RebutToCaiWu.Visible = false;
                btn_RebutToCaiGou.Visible = false;
                btn_RebutToShengChan.Visible = false;
                btn_Submit.Visible = true;
            }
            else
            {
                btn_RebutToCaiWu.Visible = true;
                btn_RebutToCaiGou.Visible = true;
                btn_RebutToShengChan.Visible = true;
                btn_Submit.Visible = false;
            }
        }
        //一级审核
        protected void rdl_YiJiCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdl_YiJiCheck.SelectedIndex == 0)    //同意
            {
                btn_RebutToCaiWu.Visible = false;
                btn_RebutToCaiGou.Visible = false;
                btn_RebutToShengChan.Visible = false;
                btn_Submit.Visible = true;
            }
            else
            {
                btn_RebutToCaiWu.Visible = true;
                btn_RebutToCaiGou.Visible = true;
                btn_RebutToShengChan.Visible = true;
                btn_Submit.Visible = false;
            }
        }
        //二级审核
        protected void rdl_ErJiCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdl_ErJiCheck.SelectedIndex == 0)    //同意
            {
                btn_RebutToCaiWu.Visible = false;
                btn_RebutToCaiGou.Visible = false;
                btn_RebutToShengChan.Visible = false;
                btn_Submit.Visible = true;
            }
            else
            {
                btn_RebutToCaiWu.Visible = true;
                btn_RebutToCaiGou.Visible = true;
                btn_RebutToShengChan.Visible = true;
                btn_Submit.Visible = false;
            }
        }
        #endregion







        /// <summary>
        /// 保存按钮:财务填写、采购部反馈、生产部反馈、财务调整使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();//保存更新语句



            if (depId == "06")
            {
                #region 财务填写保存
                if (jinDu <= 1)
                {
                    listsql.Clear();
                    listsql.Add(string.Format(@"UPDATE  dbo.YS_COST_BUDGET
SET     YS_NOTE = '{0}' ,
        YS_MATERIAL_COST = '{1}' ,
        YS_LABOUR_COST = '{2}' ,        
        YS_FERROUS_METAL = '{3}' ,
        YS_FERROUS_METAL_FB = '{4}' ,
        YS_PURCHASE_PART = '{5}' ,
        YS_PURCHASE_PART_FB = '{6}' ,
        YS_PAINT_COATING = '{7}' ,
        YS_PAINT_COATING_FB = '{8}' ,
        YS_ELECTRICAL = '{9}' ,
        YS_ELECTRICAL_FB = '{10}' ,
        YS_CASTING_FORGING_COST = '{11}' ,
        YS_CASTING_FORGING_COST_FB = '{12}' ,
        YS_OTHERMAT_COST = '{13}' ,
        YS_OTHERMAT_COST_FB = '{14}' ,
        YS_UNIT_LABOUR_COST_FB = '{15}'
WHERE   YS_TSA_ID = '{16}';",
                    Request.Form[txt_YS_NOTE.UniqueID].Trim(),
                    Request.Form[txt_YS_MATERIAL_COST.UniqueID].Trim(),
                    Request.Form[txt_YS_LABOUR_COST.UniqueID].Trim(),
                    Request.Form[txt_YS_FERROUS_METAL.UniqueID].Trim(),
                    Request.Form[txt_YS_FERROUS_METAL_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_PURCHASE_PART.UniqueID].Trim(),
                    Request.Form[txt_YS_PURCHASE_PART_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_PAINT_COATING.UniqueID].Trim(),
                    Request.Form[txt_YS_PAINT_COATING_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_ELECTRICAL.UniqueID].Trim(),
                    Request.Form[txt_YS_ELECTRICAL_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_CASTING_FORGING_COST.UniqueID].Trim(),
                    Request.Form[txt_YS_CASTING_FORGING_COST_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_OTHERMAT_COST.UniqueID].Trim(),
                    Request.Form[txt_YS_OTHERMAT_COST_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_UNIT_LABOUR_COST_FB.UniqueID].Trim(),
                    tsaId));
                    //保存财务制单人
                    listsql.Add(string.Format("UPDATE  dbo.YS_COST_BUDGET SET YS_ADDNAME = '{0}' WHERE YS_TSA_ID = '{1}' AND YS_ADDNAME IS NULL", userName, tsaId));
                    listsql.Add(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_STATE=1,YS_REVSTATE=0 WHERE YS_TSA_ID='{0}'", tsaId));
                    DBCallCommon.ExecuteTrans(listsql);
                    listsql.Clear();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.close();window.opener.location.reload();", true);
                }
                #endregion

                #region 财务调整保存
                else if (caiWu == 1 && position == "0601")
                {
                    listsql.Clear();
                    listsql.Add(string.Format(@"UPDATE  dbo.YS_COST_BUDGET
SET     YS_NOTE = '{0}' ,
        YS_MATERIAL_COST = '{1}' ,
        YS_LABOUR_COST = '{2}' ,        
        YS_FERROUS_METAL = '{3}' ,
        YS_PURCHASE_PART = '{4}' ,
        YS_PAINT_COATING = '{5}' ,
        YS_ELECTRICAL = '{6}' ,
        YS_CASTING_FORGING_COST = '{7}' ,
        YS_OTHERMAT_COST = '{8}' 
WHERE   YS_TSA_ID = '{9}';",
                    Request.Form[txt_YS_NOTE.UniqueID].Trim(),
                    Request.Form[txt_YS_MATERIAL_COST.UniqueID].Trim(),
                    Request.Form[txt_YS_LABOUR_COST.UniqueID].Trim(),
                    Request.Form[txt_YS_FERROUS_METAL.UniqueID].Trim(),
                    Request.Form[txt_YS_PURCHASE_PART.UniqueID].Trim(),
                    Request.Form[txt_YS_PAINT_COATING.UniqueID].Trim(),
                    Request.Form[txt_YS_ELECTRICAL.UniqueID].Trim(),
                    Request.Form[txt_YS_CASTING_FORGING_COST.UniqueID].Trim(),
                    Request.Form[txt_YS_OTHERMAT_COST.UniqueID].Trim(),
                    tsaId));
                    DBCallCommon.ExecuteTrans(listsql);
                    listsql.Clear();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.close();window.opener.location.reload();", true);


                }
                #endregion
            }


            #region 采购部反馈保存
            else if (caiGou == 1 && depId == "05")
            {
                listsql.Clear();
                //获取各个repeater中的数据
                GetDeatils(rpt_YS_FERROUS_METAL, "txt_YS_FERROUS_METAL_Average_Price_FB", listsql);
                GetDeatils(rpt_YS_PURCHASE_PART, "txt_YS_PURCHASE_PART_Average_Price_FB", listsql);
                GetDeatils(rpt_YS_PAINT_COATING, "txt_YS_PAINT_COATING_Average_Price_FB", listsql);
                GetDeatils(rpt_YS_ELECTRICAL, "txt_YS_ELECTRICAL_Average_Price_FB", listsql);
                GetDeatils(rpt_YS_CASTING_FORGING_COST, "txt_YS_CASTING_FORGING_COST_Average_Price_FB", listsql);
                GetDeatils(rpt_YS_OTHERMAT_COST, "txt_YS_OTHERMAT_COST_Average_Price_FB", listsql);
                listsql.Add(string.Format(@"UPDATE  dbo.YS_COST_BUDGET
SET     
        
        YS_FERROUS_METAL_FB = '{0}' ,
        YS_PURCHASE_PART_FB = '{1}' ,        
        YS_PAINT_COATING_FB = '{2}' ,        
        YS_ELECTRICAL_FB = '{3}' ,        
        YS_CASTING_FORGING_COST_FB = '{4}' ,        
        YS_OTHERMAT_COST_FB = '{5}' ,
        YS_UNIT_LABOUR_COST_FB = '{6}'
WHERE   YS_TSA_ID = '{7}';",

                    Request.Form[txt_YS_FERROUS_METAL_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_PURCHASE_PART_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_PAINT_COATING_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_ELECTRICAL_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_CASTING_FORGING_COST_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_OTHERMAT_COST_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_UNIT_LABOUR_COST_FB.UniqueID].Trim(),
                    tsaId));
                listsql.Add(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_REVSTATE=0 WHERE YS_TSA_ID='{0}'", tsaId));
                DBCallCommon.ExecuteTrans(listsql);
                listsql.Clear();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功!');window.close();window.opener.location.reload();", true);
            }
            #endregion

            #region 生产部反馈保存
            else if (depId == "04" && shengChan == 1)    //04生产部，生产状态为待反馈
            {
                listsql.Clear();
                listsql.Add(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_UNIT_LABOUR_COST_FB='{0}',YS_REVSTATE=0 WHERE YS_TSA_ID='{1}'", Request.Form[txt_YS_UNIT_LABOUR_COST_FB.UniqueID], tsaId));
                DBCallCommon.ExecuteTrans(listsql);
                listsql.Clear();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功!');window.close();window.opener.location.reload();", true);
            }
            #endregion


        }






        /// <summary>
        /// 下推至部门反馈按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_PushDown_Click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();//保存更新语句        

            listsql.Add(string.Format(@"UPDATE  dbo.YS_COST_BUDGET
SET     YS_NOTE = '{0}' ,
        YS_MATERIAL_COST = '{1}' ,
        YS_LABOUR_COST = '{2}' ,        
        YS_FERROUS_METAL = '{3}' ,
        YS_FERROUS_METAL_FB = '{4}' ,
        YS_PURCHASE_PART = '{5}' ,
        YS_PURCHASE_PART_FB = '{6}' ,
        YS_PAINT_COATING = '{7}' ,
        YS_PAINT_COATING_FB = '{8}' ,
        YS_ELECTRICAL = '{9}' ,
        YS_ELECTRICAL_FB = '{10}' ,
        YS_CASTING_FORGING_COST = '{11}' ,
        YS_CASTING_FORGING_COST_FB = '{12}' ,
        YS_OTHERMAT_COST = '{13}' ,
        YS_OTHERMAT_COST_FB = '{14}' ,
        YS_UNIT_LABOUR_COST_FB = '{15}'
WHERE   YS_TSA_ID = '{16}';",
            Request.Form[txt_YS_NOTE.UniqueID].Trim(),
            Request.Form[txt_YS_MATERIAL_COST.UniqueID].Trim(),
            Request.Form[txt_YS_LABOUR_COST.UniqueID].Trim(),
            Request.Form[txt_YS_FERROUS_METAL.UniqueID].Trim(),
            Request.Form[txt_YS_FERROUS_METAL_FB.UniqueID].Trim(),
            Request.Form[txt_YS_PURCHASE_PART.UniqueID].Trim(),
            Request.Form[txt_YS_PURCHASE_PART_FB.UniqueID].Trim(),
            Request.Form[txt_YS_PAINT_COATING.UniqueID].Trim(),
            Request.Form[txt_YS_PAINT_COATING_FB.UniqueID].Trim(),
            Request.Form[txt_YS_ELECTRICAL.UniqueID].Trim(),
            Request.Form[txt_YS_ELECTRICAL_FB.UniqueID].Trim(),
            Request.Form[txt_YS_CASTING_FORGING_COST.UniqueID].Trim(),
            Request.Form[txt_YS_CASTING_FORGING_COST_FB.UniqueID].Trim(),
            Request.Form[txt_YS_OTHERMAT_COST.UniqueID].Trim(),
            Request.Form[txt_YS_OTHERMAT_COST_FB.UniqueID].Trim(),
            Request.Form[txt_YS_UNIT_LABOUR_COST_FB.UniqueID].Trim(),
            tsaId));
            //保存财务制单人
            listsql.Add(string.Format("UPDATE  dbo.YS_COST_BUDGET SET YS_ADDNAME = '{0}' WHERE YS_TSA_ID = '{1}' AND YS_ADDNAME IS NULL", userName, tsaId));
            //更新状态
            listsql.Add(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_STATE=2, YS_SHENGCHAN=1,YS_SHENGCHAN_SJ=NULL, YS_SHENGCHAN_NAME=NULL,YS_SHENGCHAN_YJ=NULL,YS_CAIGOU=1,YS_CAIGOU_SJ=NULL ,YS_CAIGOU_NAME=NULL,YS_CAIGOU_YJ=NULL,YS_REVSTATE=0,YS_REBUT=0 WHERE YS_TSA_ID='{0}'", tsaId));
            DBCallCommon.ExecuteTrans(listsql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('下推成功!');window.close();window.opener.location.reload();", true);
        }

        /// <summary>
        /// 提交按钮：采购部反馈、生产部反馈、财务调整、一级审批、二级审批用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();//保存更新语句

            #region 采购部提交
            if (caiGou == 1 && depId == "05")    //05采购部，采购状态为待反馈
            {
                //获取各个repeater中的数据
                listsql.Clear();
                GetDeatils(rpt_YS_FERROUS_METAL, "txt_YS_FERROUS_METAL_Average_Price_FB", listsql);
                GetDeatils(rpt_YS_PURCHASE_PART, "txt_YS_PURCHASE_PART_Average_Price_FB", listsql);
                GetDeatils(rpt_YS_PAINT_COATING, "txt_YS_PAINT_COATING_Average_Price_FB", listsql);
                GetDeatils(rpt_YS_ELECTRICAL, "txt_YS_ELECTRICAL_Average_Price_FB", listsql);
                GetDeatils(rpt_YS_CASTING_FORGING_COST, "txt_YS_CASTING_FORGING_COST_Average_Price_FB", listsql);
                GetDeatils(rpt_YS_OTHERMAT_COST, "txt_YS_OTHERMAT_COST_Average_Price_FB", listsql);
                listsql.Add(string.Format(@"UPDATE  dbo.YS_COST_BUDGET
SET     
        
        YS_FERROUS_METAL_FB = '{0}' ,
        YS_PURCHASE_PART_FB = '{1}' ,        
        YS_PAINT_COATING_FB = '{2}' ,        
        YS_ELECTRICAL_FB = '{3}' ,        
        YS_CASTING_FORGING_COST_FB = '{4}' ,        
        YS_OTHERMAT_COST_FB = '{5}' ,
        YS_UNIT_LABOUR_COST_FB = '{6}'
WHERE   YS_TSA_ID = '{7}';",

                    Request.Form[txt_YS_FERROUS_METAL_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_PURCHASE_PART_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_PAINT_COATING_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_ELECTRICAL_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_CASTING_FORGING_COST_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_OTHERMAT_COST_FB.UniqueID].Trim(),
                    Request.Form[txt_YS_UNIT_LABOUR_COST_FB.UniqueID].Trim(),
                    tsaId));

                listsql.Add(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_CAIGOU=2,YS_CAIGOU_NAME='{0}',YS_CAIGOU_YJ='{1}',YS_CAIGOU_SJ=GETDATE(),YS_REVSTATE=0 WHERE YS_TSA_ID='{2}'", userName, Request.Form[txt_YS_CAIGOU_YJ.UniqueID], tsaId));
                DBCallCommon.ExecuteTrans(listsql);

                string sql = "SELECT YS_SHENGCHAN,YS_CAIGOU FROM dbo.YS_COST_BUDGET WHERE YS_TSA_ID='" + tsaId + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                listsql.Clear();

                if (dt.Rows[0]["YS_SHENGCHAN"].ToString() == "2" && dt.Rows[0]["YS_CAIGOU"].ToString() == "2")
                {
                    DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_STATE=3,YS_CAIWU=1,YS_CAIWU_NAME=NULL,YS_CAIWU_SJ=NULL,YS_CAIWU_YJ=NULL,YS_REVSTATE=0,YS_REBUT=0 WHERE YS_TSA_ID='{0}'", tsaId));
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功!');window.close();window.opener.location.reload();", true);
            }
            #endregion

            #region 生产部提交
            else if (depId == "04" && shengChan == 1)    //04生产部，生产状态为待反馈
            {
                listsql.Clear();
                listsql.Add(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_UNIT_LABOUR_COST_FB='{0}',YS_SHENGCHAN=2,YS_SHENGCHAN_NAME='{1}',YS_SHENGCHAN_YJ='{2}',YS_SHENGCHAN_SJ=GETDATE(),YS_REVSTATE=0 WHERE YS_TSA_ID='{3}'", Request.Form[txt_YS_UNIT_LABOUR_COST_FB.UniqueID], userName, Request.Form[txt_YS_SHENGCHAN_YJ.UniqueID], tsaId));
                DBCallCommon.ExecuteTrans(listsql);
                listsql.Clear();

                string sql = "SELECT YS_SHENGCHAN,YS_CAIGOU FROM dbo.YS_COST_BUDGET WHERE YS_TSA_ID='" + tsaId + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                if (dt.Rows[0]["YS_SHENGCHAN"].ToString() == "2" && dt.Rows[0]["YS_CAIGOU"].ToString() == "2")
                {
                    DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_STATE=3,YS_CAIWU=1,YS_CAIWU_NAME=NULL,YS_CAIWU_SJ=NULL,YS_CAIWU_YJ=NULL,YS_REVSTATE=0,YS_REBUT=0 WHERE YS_TSA_ID='{0}'", tsaId));
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功!');window.close();window.opener.location.reload();", true);
            }
            #endregion

            #region 财务调整提交
            else if (caiWu == 1 && position == "0601")    //财务部长进行财务调整
            {
                listsql.Clear();
                listsql.Add(string.Format(@"UPDATE  dbo.YS_COST_BUDGET
SET     YS_NOTE = '{0}' ,
        YS_MATERIAL_COST = '{1}' ,
        YS_LABOUR_COST = '{2}' ,        
        YS_FERROUS_METAL = '{3}' ,
        YS_PURCHASE_PART = '{4}' ,
        YS_PAINT_COATING = '{5}' ,
        YS_ELECTRICAL = '{6}' ,
        YS_CASTING_FORGING_COST = '{7}' ,
        YS_OTHERMAT_COST = '{8}' 
WHERE   YS_TSA_ID = '{9}';",
                    Request.Form[txt_YS_NOTE.UniqueID].Trim(),
                    Request.Form[txt_YS_MATERIAL_COST.UniqueID].Trim(),
                    Request.Form[txt_YS_LABOUR_COST.UniqueID].Trim(),
                    Request.Form[txt_YS_FERROUS_METAL.UniqueID].Trim(),
                    Request.Form[txt_YS_PURCHASE_PART.UniqueID].Trim(),
                    Request.Form[txt_YS_PAINT_COATING.UniqueID].Trim(),
                    Request.Form[txt_YS_ELECTRICAL.UniqueID].Trim(),
                    Request.Form[txt_YS_CASTING_FORGING_COST.UniqueID].Trim(),
                    Request.Form[txt_YS_OTHERMAT_COST.UniqueID].Trim(),
                    tsaId));
                listsql.Add(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_CAIWU='2',YS_CAIWU_NAME='{0}',YS_CAIWU_SJ=GETDATE(),YS_CAIWU_YJ='{1}',YS_STATE='4',YS_REVSTATE='1',YS_FIRST_REVSTATE='1',YS_REBUT='0' WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_CAIWU_YJ.UniqueID], tsaId));
                DBCallCommon.ExecuteTrans(listsql);
                listsql.Clear();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功！');window.close();window.opener.location.reload();", true);

            }
            #endregion

            #region 一级提交
            else if (yiJi == 1 && position == "0102")
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_FIRST_REVSTATE=2,YS_FIRST_REV_NAME='{0}',YS_FIRST_REV_YJ='{1}',YS_FIRST_REV_SJ=GETDATE(),YS_SECOND_REVSTATE=1 WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_FIRST_REV_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已提交审批!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('错误！');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion

            #region 二级提交
            else if (erJi == 1 && position == "0101")
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_SECOND_REVSTATE=2,YS_SECOND_REV_NAME='{0}',YS_SECOND_REV_YJ='{1}',YS_SECOND_REV_SJ=GETDATE(),YS_STATE=5,YS_REVSTATE=2 WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_SECOND_REV_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已提交审批!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('错误！');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion
        }

        /// <summary>
        /// 驳回至财务填写：采购部反馈、生产部反馈、财务调整、一级审批、二级审批使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_RebutToCaiWu_Click(object sender, EventArgs e)
        {

            #region 采购部反馈驳回到财务填写
            if (depId == "05" && caiGou == 1)
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_CAIGOU=3,YS_CAIGOU_NAME='{0}',YS_CAIGOU_YJ='{1}',YS_CAIGOU_SJ=GETDATE(),YS_STATE=1,YS_REBUT='06' WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_CAIGOU_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已驳回到财务部重新填写!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('错误！');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion

            #region 生产部反馈驳回到财务填写
            else if (depId == "04" && shengChan == 1)
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_SHENGCHAN=3,YS_SHENGCHAN_NAME='{0}',YS_SHENGCHAN_YJ='{1}',YS_SHENGCHAN_SJ=GETDATE(),YS_STATE=1,YS_REBUT='06' WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_SHENGCHAN_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已驳回到财务部重新填写!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('驳回失败!');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion

            #region 财务调整驳回到财务填写
            else if (depId == "06" && caiWu == 1 && position == "0601")
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_CAIWU=3,YS_CAIWU_NAME='{0}',YS_CAIWU_YJ='{1}',YS_CAIWU_SJ=GETDATE(),YS_STATE=1,YS_REBUT='06' WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_CAIWU_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已驳回到财务部重新填写!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('错误！');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion

            #region 一级驳回
            else if (yiJi == 1 && position == "0102")
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_FIRST_REVSTATE=3,YS_FIRST_REV_NAME='{0}',YS_FIRST_REV_YJ='{1}',YS_FIRST_REV_SJ=GETDATE(),YS_REVSTATE=3,YS_STATE=1,YS_REBUT='06' WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_FIRST_REV_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已驳回到财务部重新填写!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('错误！');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion

            #region 二级驳回
            else if (erJi == 1 && position == "0101")
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_SECOND_REVSTATE=3,YS_SECOND_REV_NAME='{0}',YS_SECOND_REV_YJ='{1}',YS_SECOND_REV_SJ=GETDATE(),YS_REVSTATE=3,YS_STATE=1,YS_REBUT='06' WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_SECOND_REV_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已驳回到财务部重新填写!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('错误！');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion

        }


        /// <summary>
        /// 驳回至采购反馈：财务调整、一级审批、二级审批使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_RebutToCaiGou_Click(object sender, EventArgs e)
        {
            #region 财务调整
            if (depId == "06" && caiWu == 1 && position == "0601")
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_CAIGOU=1,YS_CAIWU=3,YS_CAIWU_NAME='{0}',YS_CAIWU_YJ='{1}',YS_CAIWU_SJ=GETDATE(),YS_STATE=2,YS_REBUT='05' WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_CAIWU_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已驳回到采购部重新反馈!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('错误！');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion

            #region 一级驳回
            else if (yiJi == 1 && position == "0102")
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_FIRST_REVSTATE=3,YS_FIRST_REV_NAME='{0}',YS_FIRST_REV_YJ='{1}',YS_FIRST_REV_SJ=GETDATE(),YS_REVSTATE=3,YS_STATE=2,YS_CAIGOU=1,YS_REBUT='05' WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_FIRST_REV_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已驳回到采购部重新填写!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('错误！');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion

            #region 二级驳回
            else if (erJi == 1 && position == "0101")
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_SECOND_REVSTATE=3,YS_SECOND_REV_NAME='{0}',YS_SECOND_REV_YJ='{1}',YS_SECOND_REV_SJ=GETDATE(),YS_REVSTATE=3,YS_STATE=2,YS_CAIGOU=1,YS_REBUT='05' WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_SECOND_REV_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已驳回到采购部重新填写!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('错误！');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion
        }
        /// <summary>
        /// 驳回至生产反馈：财务调整、一级审批、二级审批使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_RebutToShengChan_Click(object sender, EventArgs e)
        {
            #region 财务调整
            if (depId == "06" && caiWu == 1 && position == "0601")
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_SHENGCHAN=1,YS_CAIWU=3,YS_CAIWU_NAME='{0}',YS_CAIWU_YJ='{1}',YS_CAIWU_SJ=GETDATE(),YS_STATE=2,YS_REBUT='04' WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_CAIWU_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已驳回到生产部重新反馈!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('错误！');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion

            #region 一级驳回
            else if (yiJi == 1 && position == "0102")
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_FIRST_REVSTATE=3,YS_FIRST_REV_NAME='{0}',YS_FIRST_REV_YJ='{1}',YS_FIRST_REV_SJ=GETDATE(),YS_REVSTATE=3,YS_STATE=2,YS_SHENGCHAN=1,YS_REBUT='04' WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_FIRST_REV_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已驳回到生产部重新填写!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('错误！');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion

            #region 二级驳回
            else if (erJi == 1 && position == "0101")
            {
                if (DBCallCommon.ExeSqlTextGetInt(string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_SECOND_REVSTATE=3,YS_SECOND_REV_NAME='{0}',YS_SECOND_REV_YJ='{1}',YS_SECOND_REV_SJ=GETDATE(),YS_REVSTATE=3,YS_STATE=2,YS_SHENGCHAN=1,YS_REBUT='04' WHERE YS_TSA_ID='{2}';", userName, Request.Form[txt_YS_SECOND_REV_YJ.UniqueID], tsaId)) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已驳回到生产部重新填写!');window.close();window.opener.location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('错误！');window.close();window.opener.location.reload();", true);
                }
            }
            #endregion
        }




        #region 计算预算总价、反馈总价
        public string GetProduct(string n1, string n2)
        {
            return (Convert.ToDouble(n1) * Convert.ToDouble(n2)).ToString("0.0000");
        }

        public string GetProduct(string n1, string n2, string n3)
        {
            return (Convert.ToDouble(n1) * Convert.ToDouble(n2) * Convert.ToDouble(n3)).ToString("0.0000");
        }
        #endregion


        /// <summary>
        /// 获取前端各个repeater中反馈的数据
        /// </summary>
        /// <param name="rpt">repeater的id</param>
        /// <param name="c">反馈单价的控件id</param>
        /// <param name="liststr">添加sql语句的集合</param>
        public void GetDeatils(Repeater rpt, string c, List<string> liststr)
        {
            foreach (RepeaterItem Item in rpt.Items)
            {
                string priceFb = ((TextBox)Item.FindControl(c)).Text.Trim();
                string note = ((TextBox)Item.FindControl("txt_YS_NOTE")).Text.Trim();
                string code = ((Label)Item.FindControl("lb_YS_CODE")).Text.Trim();
                liststr.Add(string.Format("UPDATE dbo.YS_COST_BUDGET_DETAIL SET YS_Average_Price_FB='{0}',YS_NOTE='{1}',YS_ADDPER='{2}',YS_ADDTIME=GETDATE() WHERE YS_TSA_ID='{3}' AND YS_CODE='{4}';", priceFb, note, userName, tsaId, code));
                liststr.Add(string.Format("UPDATE dbo.YS_COST_BUDGET_DETAIL SET YS_Average_Price='{0}' WHERE YS_TSA_ID='{1}' AND YS_CODE='{2}' AND YS_Average_Price IS NULL;", priceFb, tsaId, code));
            };
        }









    }
}
