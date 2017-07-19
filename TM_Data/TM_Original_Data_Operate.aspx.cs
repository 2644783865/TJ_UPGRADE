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
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Original_Data_Operate : System.Web.UI.Page
    {
        #region
        string register;
        string sqlText;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitInfo();
                this.GetOriginalData();
                ViewState["state"] = null;//用来记录是何种变更
            }
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitInfo()
        {
            register = Request.QueryString["register"]; //e.Row.Cells[4].Text.Trim() + ' ' + tsaid.Text+ ' ' + tablename + ' ' + mptable+' '+mstable
            string[] fields = register.Split(' ');
            ViewState["struid"] = fields[0].ToString();//序号
            if (ViewState["struid"].ToString().Length < 3)
            {
                btnChangeZX.Visible = false;
                div_2.Visible = false;
            }
            else
            {
                btnChangeZX.Visible = true;
                div_2.Visible = true;
            }
            ViewState["engId"] = fields[1].ToString();//任务号
            ViewState["tablename"] = fields[2].ToString();//原始数据表
            ViewState["mptable"] = fields[3].ToString();//材料计划表
            ViewState["mstable"] = fields[4].ToString();//制作明细表
            ViewState["viewtable"] = fields[5].ToString();//原始数据视图
            ViewState["mspmtable"] = "TBMP_TASKDQJ";//生产部制作明细表
        

            //读取台数
            string sql = "select TSA_NUMBER from View_TM_TaskAssign where TSA_ID='" + ViewState["engId"].ToString() + "'";
            SqlDataReader dr_number = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr_number.HasRows)
            {
                dr_number.Read();
                taishu.Value = dr_number["TSA_NUMBER"].ToString();
                dr_number.Close();
            }
            this.Form.DefaultButton = btnCancel.UniqueID;
        }
        /// <summary>
        /// 获取原始数据
        /// </summary>
        private void GetOriginalData()
        {
            sqlText = "select * from " + ViewState["viewtable"] + "  where BM_ZONGXU='" + ViewState["struid"] + "' and BM_ENGID='" + ViewState["engId"] + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                #region //赋值两次，新旧(用于变更)
                //物料编码
                marid.Text = dr["BM_MARID"].ToString();
                hdmarid.Value = dr["BM_MARID"].ToString();

                if (marid.Text.Trim() == "")//如果物料编码为空，说明是部件，则修改时不能由部件修改为物料
                {
                    marid.Enabled = false;
                    marid.ToolTip = "物料编码为空时,表明该记录为部件,无法修改物料编码！！！";
                }
              
                //关联序号，用于变更
                ViewState["oldIndex"] = dr["BM_OLDINDEX"];
                //中文名称
                cnname.Text = dr["BM_CHANAME"].ToString();
                hdcnname.Value = dr["BM_CHANAME"].ToString();
              
                //图号
                tuhao.Text = dr["BM_TUHAO"].ToString();
                hdtuhao.Value = dr["BM_TUHAO"].ToString();
                //总序
                zongxu.Text = dr["BM_ZONGXU"].ToString();
                hdzongxu.Value = dr["BM_ZONGXU"].ToString();
                //数量
                //总数量
                shuliang.Text = dr["BM_NUMBER"].ToString();
                hdshuliang.Value = dr["BM_NUMBER"].ToString();
                //单台数量
                sing_shuliang.Value = dr["BM_SINGNUMBER"].ToString();
                //计划数量
                p_shuliang.Text = dr["BM_PNUMBER"].ToString();
                hd_p_shuliang.Value = dr["BM_PNUMBER"].ToString();
                //材料长度
                cailiaocd.Text = dr["BM_MALENGTH"].ToString();
                hdcailiaocd.Value = dr["BM_MALENGTH"].ToString();
                //材料宽度
                cailiaokd.Text = dr["BM_MAWIDTH"].ToString();
                hdcailiaokd.Value = dr["BM_MAWIDTH"].ToString();
                //下料备注
                xlNote.Text = dr["BM_NOTE"].ToString();
                hdxlNote.Value = dr["BM_NOTE"].ToString();

                //材料总长-采购
                cailiaozongchang.Text = dr["BM_MATOTALLGTH"].ToString();
                hdcailiaozongchang.Value = dr["BM_MATOTALLGTH"].ToString();

                
                //材料规格
                cailiaoguige.Text = dr["BM_MAGUIGE"].ToString();
                hdcailiaoguige.Value = dr["BM_MAGUIGE"].ToString();
               
                //理论重量
                lilunzhl.Text = dr["BM_THRYWGHT"].ToString();
                hdlilunzhl.Value = dr["BM_THRYWGHT"].ToString();
              
                //材料种类
                cailiaoType.SelectedValue = dr["BM_MASHAPE"].ToString();
                hdcailiaoType.Value = dr["BM_MASHAPE"].ToString();
                //面域
                bgzmy.Text = dr["BM_MPMY"].ToString();
                hdbgzmy.Value = dr["BM_MPMY"].ToString();
                my.Text=dr["BM_MABGZMY"].ToString();
                //图纸上单重
                txtTuDz.Text = dr["BM_TUUNITWGHT"].ToString();
                hidTudz.Value = dr["BM_TUUNITWGHT"].ToString();
                //图纸上总重
                txtTuZz.Text = dr["BM_TUTOTALWGHT"].ToString();
                //单位
                techUnit.Text = dr["BM_TECHUNIT"].ToString();
                hidtechunit.Value = dr["BM_TECHUNIT"].ToString();
                //材料用量
                txtYongliang.Text = dr["BM_YONGLIANG"].ToString();
                hidYongliang.Value = dr["BM_YONGLIANG"].ToString();
                //材料单重
                cailiaodzh.Text = Math.Round(Convert.ToDouble(dr["BM_MAUNITWGHT"].ToString()), 2).ToString();
                hdcailiaodzh.Value = Math.Round(Convert.ToDouble(dr["BM_MAUNITWGHT"].ToString()), 2).ToString();
                //材料总重-采购
                cailiaozongzhong.Text = Math.Round(Convert.ToDouble(dr["BM_MATOTALWGHT"].ToString()), 2).ToString();
                hdcailiaozongzhong.Value = Math.Round(Convert.ToDouble(dr["BM_MATOTALWGHT"].ToString()), 2).ToString();

                //材质
                caizhi.Text = dr["BM_MAQUALITY"].ToString();
                hdcaizhi.Value = dr["BM_MAQUALITY"].ToString();
     
                //标准
                biaozhun.Text = dr["BM_STANDARD"].ToString();
                hdbiaozhun.Value = dr["BM_STANDARD"].ToString();
                //下料方式
                xialiao.Text = dr["BM_XIALIAO"].ToString();
                hdxialiao.Value = dr["BM_XIALIAO"].ToString();
                //工艺流程
                txtProcess.Text = dr["BM_PROCESS"].ToString();
                hdtxtProcess.Value = dr["BM_PROCESS"].ToString();
             
                //是否定尺
                rblSFDC.SelectedValue = dr["BM_FIXEDSIZE"].ToString();
                hdtxtSFDC.Value = dr["BM_FIXEDSIZE"].ToString();
                //图纸上材料
                txtTZCZ.Text = dr["BM_TUMAQLTY"].ToString();

                //图纸上问题
                txtTZWT.Text = dr["BM_TUPROBLEM"].ToString();


                //制作明细
                //是否制作明细
                rblMSSTA.SelectedValue = dr["BM_MSSTATE"].ToString();
                //是否体现
                rblInMS.SelectedValue = dr["BM_ISMANU"].ToString();
                txthdinms.Value = dr["BM_ISMANU"].ToString();
                //审核状态
                rblMSRew.SelectedValue = dr["BM_MSREVIEW"].ToString();
                //变更状态
                rblMSChangeSta.SelectedValue = dr["BM_MSSTATUS"].ToString();


                //材料计划
                //是否材料计划
                rblMPSTA.SelectedValue = dr["BM_MPSTATE"].ToString();
                //是否能提交材料计划
                ddlWMarPlan.SelectedValue = dr["BM_WMARPLAN"].ToString();
                //审核状态
                rblMPRew.SelectedValue = dr["BM_MPREVIEW"].ToString();
                //变更状态
                rblMPChangSta.SelectedValue = dr["BM_MPSTATUS"].ToString();

                //备注
                beizhu.Text = dr["BM_ALLBEIZHU"].ToString();
                hdbeizhu.Value = dr["BM_ALLBEIZHU"].ToString();
   
                //代用标识
                ViewState["dy_flag"] = dr["BM_CALUNITWGHT"].ToString();
                if (Convert.ToInt16(dr["BM_CALUNITWGHT"].ToString()) > 0)
                {
                    lblAlt.Visible = true;
                    lblAlt.Text = "代用(" + Convert.ToInt16(dr["BM_CALUNITWGHT"].ToString()).ToString() + ")次";
                }
                else
                {
                    lblAlt.Visible = false;
                }

                #endregion
            }
            dr.Close();
          

       
            this.ControlEnable();
            this.SetMarginCoefficient();
            this.CheckMarids();
        }
        /// <summary>
        /// 检查物料编码为空时，控件的可编辑性
        /// </summary>
        private void CheckMarids()
        {
            if (hdmarid.Value=="")
            {
                cailiaocd.Enabled = false;
                cailiaokd.Enabled = false;
                xlNote.Enabled = false;
                cailiaozongchang.Enabled = false;
                cailiaoType.Enabled = false;
                my.Enabled = false;
                bgzmy.Enabled = false;
                techUnit.Enabled = false;
                txtYongliang.Enabled = false;
                cailiaodzh.Enabled = false;
                cailiaozongzhong.Enabled = false;
                xialiao.Enabled = false;
                txtProcess.Enabled = false;
                rblSFDC.Enabled = false;
                txtTZCZ.Enabled = false;
                txtTZWT.Enabled = false;
             
            }
        }
        /// <summary>
        /// 获取余量系数
        /// </summary>
        protected void SetMarginCoefficient()
        {
            string _MARID=marid.Text.Trim();
            string UNIT =techUnit.Text.Trim();//--单位
            string MarUWght = cailiaodzh.Text.Trim();  //--材料单重
            string MarTWght=cailiaozongzhong.Text.Trim(); //--材料总重
            string MarLen = cailiaocd.Text.Trim(); //--材料长度
            string MarTLen = cailiaozongchang.Text.Trim(); //--材料总长
            string MarNum = shuliang.Text.Trim(); //--材料数量
            string MarPNum = p_shuliang.Text.Trim(); //--计划数量
            string MarMy = my.Text.Trim(); //--面域
            string MarPMy = bgzmy.Text.Trim(); //--计划面域

            if(_MARID!="")
            {
                string sql = "select [dbo].[GetMarginCoefficient]('" + UNIT + "'," + MarUWght + "," + MarTWght + "," + MarLen + "," + MarTLen + "," + MarNum + "," + MarPNum + "," + MarMy + "," + MarPMy + ")";
                DataTable dt_sql=DBCallCommon.GetDTUsingSqlText(sql);
                lblXishu.Text="当前余量:"+dt_sql.Rows[0][0].ToString();
            }
            else
            {
                lblXishu.Text="";
            }

        }
        /// <summary>
        /// 根据材料计划、制作明细判断控件的可编辑性(待完善)
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="mp"></param>
        /// <param name="op"></param>
        private void ControlEnable()
        {
            #region 变量定义
            string ms_state = rblMSSTA.SelectedValue.ToString();//制作明细状态
            string ms_chgstate = rblMSChangeSta.SelectedValue.ToString();//制作明细变更状态
            string ms_rew = rblMSRew.SelectedValue.ToString();//制作明细审核状态

            string mp_state = rblMPSTA.SelectedValue.ToString();//材料计划状态
            string mp_chgstate = rblMPChangSta.SelectedValue.ToString();//材料计划变更状态
            string mp_rew = rblMPRew.SelectedValue.ToString();//材料计划审核状态

            this.TipInfoRew(ms_rew, lblMSRew);
            this.TipInfoRew(mp_rew, lblMPRew);
         
            this.TipInfoChagne(ms_chgstate,lblMS);
            this.TipInfoChagne(mp_chgstate,lblMP);
           
            #endregion

            #region 如果提交了材料计划，则【能否提交材料计划控件不可用】
            if (mp_state == "1" || mp_chgstate != "0")
            {
                ddlWMarPlan.Enabled = false;
                cailiaocd.Enabled = false;
                cailiaokd.Enabled = false;
                cailiaozongchang.Enabled = false;
               
                cailiaodzh.Enabled = false;
                cailiaozongzhong.Enabled = false;
                my.Enabled = false;
                bgzmy.Enabled = false;
                shuliang.Enabled = false;
                sing_shuliang.Disabled = false;
                rblSFDC.Enabled = false;
            }
            #endregion
            #region 如果提交了制作明细，则能否提交制作明细控件不可用
            if (ms_state == "1" || ms_chgstate != "0")
            {
                rblInMS.Enabled = false;
                
                cailiaocd.Enabled = false;
                cailiaokd.Enabled = false;
               
        
                tuhao.Enabled = false;
                xlNote.Enabled = false;
             
                
                txtTuDz.Enabled = false;
                txtTuZz.Enabled = false;
                xialiao.Enabled = false;
                txtProcess.Enabled = false;
               
               
            }
            #endregion

         
            //对于顶级部件的控制
          
            string a = "1";//最顶级部件
        
            if (zongxu.Text.Trim() == a)
            {
                
                confirmChange.Visible = false;
               
            
            }
            else//除开虚拟部件及最顶级部件外，其他部件及物料控件状态控制
            {
             

                //按钮可用性判断
                if (mp_state == "0" && ms_state == "0") //未生成计划/明细/外协
                {
                    btnConfirmReal.Visible = true;//确定
                    confirmChange.Visible = false;//变更
                   
                }
                else //已提交材料计划/明细/外协
                {
                    //1审核中，2驳回，3通过
                    //审核或驳回时中无法修改和变更
                    if (ms_rew == "1" || mp_rew == "1" || ms_rew == "2" || mp_rew == "2" )
                    {
                        btnConfirmReal.Visible = false;
                        confirmChange.Visible = false;
                       
                        lblTip.Visible = true;
                    }
                    else
                    {
                        //是否存在通过状态-可以进行变更
                        //存在审核通过的状态、并且材料计划和外协状态不为：(提交，审核)▷(1,0)
                        bool mp=!(mp_state=="1"&&mp_rew=="0");
                      
                        if ((ms_rew == "3" || mp_rew == "3" )&&mp)
                        {
                            btnConfirmReal.Visible = true;
                            confirmChange.Visible = true;
                         
                            if (hdmarid.Value != "")//不存在物料编码
                            {
                                btnattDelete.Visible = true;//结构删除
                                btnDelete.Visible = false;//单条删除
                            }
                            else
                            {
                                btnattDelete.Visible = false;//结构删除
                                btnDelete.Visible = true;//单条删除
                            }
                        }
                        else //所有未提交 (与mp_state == "0" && ms_state == "0" && out_state == "0"重复判断)
                        {
                            btnConfirmReal.Visible = true;
                            confirmChange.Visible = false;
                         
                        }
                    }

                    #region 提交了材料计划,并在审核中以下控件不能编辑
                    if (mp_rew=="1")//
                    {
                        //物料编码
                        marid.Enabled = false;
                      
                        //材料长度
                        cailiaocd.Enabled = false;
                        //材料宽度
                        cailiaokd.Enabled = false;
                        //材料总长
                        cailiaozongchang.Enabled = false;
                        //数量
                        sing_shuliang.Disabled = true;
                        p_shuliang.Enabled = false;
                        //材料单重
                        cailiaodzh.Enabled = false;
                        //材料总重
                        cailiaozongzhong.Enabled = false;
                        //计划面域
                        bgzmy.Enabled = false;
                        //是否定尺
                        rblSFDC.Enabled = false;
                        txtYongliang.Enabled = false;
                        techUnit.Enabled = false;
                        //毛坯形状
                        cailiaoType.Enabled = false;
                      
                    }
                    #endregion

                    #region 提交了制作明细,并在审核中以下控件不能编辑
                    if (ms_rew == "1")//
                    {
                      
                        //物料编码
                        marid.Enabled = false;
                        //图号
                        tuhao.Enabled = false;
                        //材料长度
                        cailiaocd.Enabled = false;
                        //材料宽度
                        cailiaokd.Enabled = false;
                        //材料总长
                        cailiaozongchang.Enabled = false;
                        ////////////材料单重
                        //////////cailiaodzh.Enabled = false;
                        ////////////材料总重
                        //////////cailiaozongzhong.Enabled = false;
                        //数量
                        sing_shuliang.Disabled = true;
                        p_shuliang.Enabled = false;
                        //////////计划面域(m2)
                        ////////bgzmy.Enabled = false;
                        //面域
                        my.Enabled = false;
                        txtTuDz.Enabled = false;
                        txtTuZz.Enabled = false;
                        txtYongliang.Enabled = false;
             
                        //体现
                        rblInMS.Enabled = false;
                       
                    }
                    #endregion

                   
                }
            }


            #region 虚拟部件总序不可修改
            if (zongxu.Text.Trim() == "1" || zongxu.Text.Trim() == "2")
            {
                btnChangeZX.Visible = false;
                div_2.Visible = false;
            }
            #endregion

            #region 能否添加物料编码
            if (hdmarid.Value != "")
            {
                btnAddMarID.Visible = false;
            }
            else
            {
                //当前记录或下级未提交
                string taskid = ViewState["engId"].ToString();
                string sql = "select count(*) as Nums from " + ViewState["tablename"] + " where BM_ENGID LIKE '" + taskid + "%' and (BM_XUHAO='" + zongxu.Text.Trim() + "' or BM_XUHAO like '" + zongxu.Text.Trim() + ".%' or BM_ZONGXU='" + zongxu.Text.Trim() + "' or BM_ZONGXU LIKE  '" + zongxu.Text.Trim() + ".%') and (BM_MPSTATE!='0' or BM_MPSTATUS!='0' or BM_MSSTATE!='0' or BM_MSSTATUS!='0' or BM_OSSTATE!='0' or BM_OSSTATUS!='0')";
                SqlDataReader dr_sql = DBCallCommon.GetDRUsingSqlText(sql);
                dr_sql.Read();
                if (Convert.ToInt16(dr_sql["Nums"].ToString()) > 0)
                {
                    btnAddMarID.Visible = false;
                }
                else
                {
                    btnAddMarID.Visible = true;
                }
                dr_sql.Close();
            }
            #endregion

        }
        /// <summary>
        /// 添加物料编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddMarID_OnClick(object sender, EventArgs e)
        {
            string ret = this.CanAddMarid();
            if ( ret== "OK")
            {
                hdmarid.Value = "notnull";
                marid.Enabled = true;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('控件已激活，请填写【物料编码】并修改相关数据！！！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('"+ret+"');", true);
            }
        }
        /// <summary>
        /// 能否添加物料编码
        /// </summary>
        /// <returns></returns>
        protected string CanAddMarid()
        {
            string retValue = "OK";
            string taskid = ViewState["engId"].ToString();
            //总序子集
            string sql_zongxu = "select count(*) as Nums from " + ViewState["tablename"] + " where BM_ENGID ='" + taskid + "' AND BM_ZONGXU LIKE '" + hdzongxu.Value + ".%' AND BM_MARID!=''";
            SqlDataReader dr_zongxu = DBCallCommon.GetDRUsingSqlText(sql_zongxu);
            dr_zongxu.Read();
            if (Convert.ToInt16(dr_zongxu["Nums"].ToString())>0)
            {
                dr_zongxu.Close();
                retValue = "该条记录【总序】下级存在物料编码，无法添加物料编码！！！";
                return retValue;
            }
            return retValue;
        }
        /// <summary>
        /// 审核状态提示（Label）
        /// </summary>
        /// <param name="rew"></param>
        /// <param name="lbltip"></param>
        private void TipInfoRew(string rew, Label lbltip)
        {
            lbltip.Visible = true;
            if (rew == "1")
            {
                lbltip.Text = "审核中...";
            }
            else if (rew == "2")
            {
                lbltip.Text = "驳回";
            }
            else if (rew == "3")
            {
                lbltip.Text = "通过";
            }
            else
            {
                lbltip.Text = "";
                lbltip.Visible = false;
            }
        }
        /// <summary>
        /// 变更状态提示(变更记录以红色显示)
        /// </summary>
        /// <param name="state"></param>
        /// <param name="lblChange"></param>
        private void TipInfoChagne(string state, Label lblChange)
        {
            if (state != "0")
            {
                lblChange.ForeColor = System.Drawing.Color.Red;
                lblChange.Font.Bold = true;
            }
        }
        /// <summary>
        /// 确认提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender,EventArgs e)
        {
            string retconfirmcheck = this.SubmitConfirm();
            if (retconfirmcheck == "0")
            {
                #region 更新变量定义
           
                string bm_xuhao = zongxu.Text.Trim();//序号
                string bm_tuhao = tuhao.Text.Trim();//图号
                string bm_marid = marid.Text.Trim();//物料编码              
                string bm_chaname = cnname.Text.Trim();//中文名称
               
                double bm_malength = Convert.ToDouble(cailiaocd.Text.Trim());//材料长度
                double bm_mawidth = Convert.ToDouble(cailiaokd.Text.Trim());//材料宽度
                string xialiaoNote = xlNote.Text.Trim();//下料备注
                double bm_matotallgth = Convert.ToDouble(cailiaozongchang.Text.Trim());//材料总长
                string bm_mashape = cailiaoType.SelectedValue;//材料类别
                double bm_mabgzmy = Convert.ToDouble(my.Text.Trim());//不规则面域
                double bm_mpmy = Convert.ToDouble(bgzmy.Text.Trim());//计划面域
              
                double bm_tuunitwght = Convert.ToDouble(txtTuDz.Text.Trim());//图纸上单重
                double bm_tutotalwght = Convert.ToDouble(txtTuZz.Text.Trim());//图纸上总重             
                double bm_matotalwght = Convert.ToDouble(cailiaozongzhong.Text.Trim());//材料总重
                double bm_maunitwght = Convert.ToDouble(cailiaodzh.Text.Trim());//材料单重
                string bm_xialiao = xialiao.Text.Trim().ToString();
                string bm_process = txtProcess.Text.Trim();//工艺流程
               
                string bm_fixedsize = rblSFDC.SelectedValue.ToString();//是否定尺
                string bm_tumaqlty = txtTZCZ.Text.Trim();//图纸上材质              
                string bm_tuproblem = txtTZWT.Text.Trim();//图纸问题
                double bm_number = Convert.ToDouble(shuliang.Text.Trim());//总数量
                double bm_singnumber = Convert.ToDouble(sing_shuliang.Value.Trim());//单台数量
                double bm_pnumber = Convert.ToDouble(p_shuliang.Text.Trim());//计划数量
                string bm_ismanu = rblInMS.SelectedValue.ToString();//是否体现在制作明细中            
                string bm_wmarplan=ddlWMarPlan.SelectedValue.ToString();//是否能提交材料计划
                string bm_techunit = techUnit.Text.Trim();
                string bm_yongliang = txtYongliang.Text.Trim();
              string bm_allbeizhu = beizhu.Text.Trim();//备注
               #endregion
              #region 原始数据更新及原始数据临时表更新
                List<string> list_sql = new List<string>();
                //调用存储过程的相关参数
                ParamsAlterChange paraAC = new ParamsAlterChange();
                paraAC.TableName = ViewState["viewtable"].ToString();
                paraAC.TableNameOrg = ViewState["tablename"].ToString();
                paraAC.MsTableName = ViewState["mstable"].ToString();
                paraAC.TaskID = ViewState["engId"].ToString();
                paraAC.XuHao = ViewState["struid"].ToString();
              
                //更新原始数据
                string sql_update_org = "update " + ViewState["tablename"] + " set BM_TUHAO='" + bm_tuhao + "',BM_MARID='" + bm_marid + "',BM_CHANAME='" + bm_chaname + "',BM_MALENGTH=" + bm_malength + ",BM_MAWIDTH=" + bm_mawidth + ",BM_MAUNITWGHT=" + bm_maunitwght + ",BM_MATOTALWGHT=" + bm_matotalwght + ",BM_MATOTALLGTH=" + bm_matotallgth + ",BM_MABGZMY=" + bm_mabgzmy + ",BM_TUMAQLTY='" + bm_tumaqlty + "',BM_TUPROBLEM='" + bm_tuproblem + "',BM_NUMBER=" + bm_number + ",BM_SINGNUMBER=" + bm_singnumber + ",BM_PNUMBER=" + bm_pnumber + ",BM_MASHAPE='" + bm_mashape + "',BM_ISMANU='" + bm_ismanu + "',BM_PROCESS='" + bm_process + "',BM_FIXEDSIZE='" + bm_fixedsize + "',BM_WMARPLAN='" + bm_wmarplan + "',BM_NOTE='" + xialiaoNote + "',BM_TUUNITWGHT=" + bm_tuunitwght + ",BM_TUTOTALWGHT=" + bm_tutotalwght + ",BM_MPMY=" + bm_mpmy + ",BM_XIALIAO='" + bm_xialiao + "',BM_TECHUNIT='"+bm_techunit+"',BM_YONGLIANG='"+bm_yongliang+"' where BM_XUHAO='" + ViewState["struid"] + "' and BM_ENGID='" + ViewState["engId"] + "'";
                list_sql.Add(sql_update_org);
                
                //是否提计划修改时关联到下级
                if (ckbWMarplanToLower.Checked)
                {
                    list_sql.Add("update " + ViewState["tablename"] + " set BM_WMARPLAN='" + bm_wmarplan + "' where BM_ENGID='" + ViewState["engId"] + "' and  (BM_ZONGXU='" + ViewState["struid"] + "' or BM_ZONGXU  like '" + ViewState["struid"] + ".%') and BM_MPSTATE='0' AND BM_MPSTATUS='0'");
                }



                #endregion

                if (ViewState["state"] == null)//所有未生成材料计划/制作明细/外协，或生成未提交的修改
                {
                    #region 正常修改
                    DBCallCommon.ExecuteTrans(list_sql);
                
    
                  
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已更新！！！');window.returnValue=1;window.close();", true);
                    
                    #endregion
                }
                else
                {
                    #region 变更修改
                    string ret = this.CheckChangeOccur();
                    if (ret != "")
                    {
                        if (ViewState["state"].ToString() == "wszChg")//物料/数量/重量变更
                        {
                            #region 重量数量等变更
                            //引起制作明细变更
                            if (ret.Contains("z"))
                            {
                                if (rblMSSTA.SelectedValue == "1")
                                {
                                    paraAC.MsWetherChg = 1;//只有在数据改变引起制作明细变更且该记录已提交了制作明细才会引起制作明细变更
                                    //制作明细变更(明细状态0，明细变更状态3，明细审核状态0)
                                    string sql_update_ms_org = "update " + ViewState["tablename"] + " set BM_MSSTATE='0',BM_MSSTATUS='3',BM_MSREVIEW='0' where BM_XUHAO='" + ViewState["struid"] + "' and BM_ENGID='" + ViewState["engId"] + "' ";
                                    list_sql.Add(sql_update_ms_org);
                                }
                                else
                                {
                                    paraAC.MsWetherChg = 0;
                                }
                            }
                            //引起材料计划变更
                            if (ret.Contains("c"))
                            {
                                //已生成材料计划
                                if (rblMPSTA.SelectedValue == "1")
                                {
                                    paraAC.MpWetherChg = 1;//材料计划对应一条，此状态没有多大用处
                                    string sql_update_mp_org = "update " + ViewState["tablename"] + " set BM_MPSTATE='0',BM_MPSTATUS='3',BM_MPREVIEW='0' where BM_XUHAO='" + ViewState["struid"] + "' and BM_ENGID='" + ViewState["engId"] + "' ";
                                    list_sql.Add(sql_update_mp_org);
                                }
                                else
                                {
                                    paraAC.MpWetherChg = 0;
                                }
                            }
                           

                            //更改所有父部件变更状态为修改
                            list_sql.Add("exec PRO_TM_MarNumWetChange '" + paraAC.TableNameOrg + "','" + paraAC.TaskID + "','" + paraAC.XuHao + "'," + paraAC.MpWetherChg + "," + paraAC.MsWetherChg + "," + paraAC.OutWetherChg + "");
                            

                            //如果勾选了关联下级变更，数量发生变化时，下级也发生变更
                            if(ckbMarChange.Checked)
                            {
                                if(hdshuliang.Value!=shuliang.Text.Trim())
                                {
                                    list_sql.Add(" exec PRO_TM_MarNumWgtChangeToLower '"+paraAC.TaskID+"','"+paraAC.XuHao+"','"+hdshuliang.Value.Trim()+"','"+shuliang.Text.Trim()+"'");
                                }
                            }
                        

                            //执行
                            DBCallCommon.ExecuteTrans(list_sql);
                            //提示
                           
                            
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('变更已生成,请进入相应页面执行变更！！！');window.returnValue=1;window.close();", true);
                           
                            #endregion
                        }
                        else if (ViewState["state"].ToString() == "alterChg")//代用变更(不用下推变更,所有数据自动修改)
                        {
                            #region 代用变更
                            list_sql.Add(" exec [PRO_TM_MSCalWeight] '" + paraAC.TableNameOrg + "','" + paraAC.TaskID + "'");
                            string returnSubCheck = this.CheckSubstitute();
                            if (returnSubCheck.Contains("SubstituteFalse"))
                            {
                                if (returnSubCheck != "SubstituteFalse")
                                {
                                    string beforemar = returnSubCheck.Split('-')[1];
                                    string aftermar = returnSubCheck.Split('-')[2];
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法代用！！！\\r\\r提示:代用前物料类别【" + beforemar + "】，代用后物料类别【" + aftermar + "】');window.returnValue=1;window.close();", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法代用！！！\\r\\r提示:物料编码未改变');window.returnValue=1;window.close();", true);
                                }
                            }
                            else if (returnSubCheck == "SubstituteCurrent")
                            {
                                list_sql.Add("Exec PRO_TM_SubstituteChange '" + paraAC.TableName + "','" + paraAC.TableNameOrg + "','" + paraAC.MsTableName + "','" + paraAC.TaskID + "','" + paraAC.XuHao + "'");
                                //执行
                                DBCallCommon.ExecuteTrans(list_sql);
                                if (ViewState["split_flag"].ToString() == "1")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('代用成功，数据已更新！！！\\r\\r提示:代用总序为已代用的拆分记录，物料编码未修改，当前操作只影响本条记录');window.returnValue=1;window.close();", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('代用成功，数据已更新！！！');window.returnValue=1;window.close();", true);
                                }
                            }
                            else if (returnSubCheck == "SubstituteAll")
                            {
                                if (ViewState["split_flag"].ToString() == "1")//拆分记录代用时，所有拆分记录都代用
                                {
                                    string sql_select_xuhao = "select BM_XUHAO from " + paraAC.TableName + " where BM_ENGID='" + paraAC.TaskID + "' and BM_ZONGXU='" + zongxu.Text.Trim() + "'";
                                    DataTable dt_select_xuhao = DBCallCommon.GetDTUsingSqlText(sql_select_xuhao);
                                    
                                    for (int i = 0; i < dt_select_xuhao.Rows.Count; i++)
                                    {
                                        string xh = dt_select_xuhao.Rows[i]["BM_XUHAO"].ToString();
                                        paraAC.XuHao = xh;
                                        list_sql.Add("update " + paraAC.TableNameOrg + " set BM_CALUNITWGHT=BM_CALUNITWGHT+1 where BM_ENGID='" + paraAC.TaskID + "' and BM_XUHAO='" + paraAC.XuHao + "'");
                                        list_sql.Add("Exec PRO_TM_SubstituteChange '" + paraAC.TableName + "','" + paraAC.TableNameOrg + "','" + paraAC.MsTableName + "','" + paraAC.TaskID + "','" + paraAC.XuHao + "'");
                                    }

                                    //执行
                                    DBCallCommon.ExecuteTrans(list_sql);
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('代用成功，数据已更新！！！\\r\\r提示:\\r\\r该总序为拆分记录，所有记录都已完成代用，需要您对其它数据进行核对！！！');window.returnValue=1;window.close();", true);
                                }
                                else
                                {
                                    list_sql.Add("update " + paraAC.TableNameOrg + " set BM_CALUNITWGHT=BM_CALUNITWGHT+1 where BM_ENGID='" + paraAC.TaskID + "' and BM_XUHAO='" + paraAC.XuHao + "'");
                                    list_sql.Add("Exec PRO_TM_SubstituteChange '" + paraAC.TableName + "','" + paraAC.TableNameOrg + "','" + paraAC.MsTableName + "','" + paraAC.TaskID + "','" + paraAC.XuHao + "'");
                                    //执行
                                    DBCallCommon.ExecuteTrans(list_sql);
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('代用成功，数据已更新！！！');window.returnValue=1;window.close();", true);
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据未改变,操作无效！！！');window.close();", true);
                    }
                    #endregion
                }
            }
            #region 错误提示
            else if (retconfirmcheck.Contains("MaridEmpty"))
            {
                marid.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('物料编码不能为空！！！');", true);
            }
            else if (retconfirmcheck.Contains("MaridNoMatch"))
            {
                marid.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('物料编码【" + marid.Text.Trim() + "】格式不正确！！！');", true);
            }
            else if (retconfirmcheck.Contains("MaridNoExistOrStopUsing"))
            {
                marid.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('物料【" + marid.Text.Trim() + "】不存在或已停用！！！');", true);
            }
            else if (retconfirmcheck.Contains("NumberFormatError"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数值格式不正确，请检查以下数据！！！\\r\\r提示:材料长度、材料宽度、材料总长、材料单重、材料总重、部件自身重量、面域、\\r\\r单重、总重、图纸上单重、图纸上总重');", true);
            }
            #endregion
        }
        /// <summary>
        /// 代用类别判断
        /// </summary>
        /// <returns>（1）不能代用（2）第一次代用:只代用当前条、其它也代用（3）已代用过:只代用当前条、其它也代用</returns>
        private string CheckSubstitute()
        {
            string old_mar_type=hdmarid.Value.Substring(0,5).ToString();
            string new_mar_type=marid.Text.Substring(0,5).ToString();
            if (old_mar_type == new_mar_type)
            {

                if (Convert.ToInt16(ViewState["dy_flag"].ToString()) > 0)//说明已代用过
                {
                    //在代用的情况下，如果物料编码未改变，只更新当前一条数据
                    if (hdmarid.Value.Trim() == marid.Text.Trim())
                    {
                        return "SubstituteCurrent";
                    }
                    else
                    {
                        return "SubstituteAll";
                    }
                }
                else//未代用过
                {
                    if (hdmarid.Value.Trim() == marid.Text.Trim())
                    {
                        return "SubstituteFalse";
                    }
                    else
                    {
                        return "SubstituteAll";
                    }
                }
            }
            else
            {
                return "SubstituteFalse-"+old_mar_type+"-"+new_mar_type;
            }
        }
        /// <summary>
        /// 能否确认提交判断
        /// </summary>
        /// <returns></returns>
        private string SubmitConfirm()
        {
            //物料编码不为空
            if (hdmarid.Value.Trim() != "")//如果原来有物料编码
            {
                if (marid.Text.Trim() != "")
                {
                    string pattern = @"^\d{2}\.\d{2}\.\d{6}$";
                    Regex rgx = new Regex(pattern);
                    string mid = marid.Text.Trim();
                    if (!rgx.IsMatch(mid, 0))
                    {
                        return "MaridNoMatch";
                    }
                    else
                    {
                        string sql_select_mar = "select MNAME,GUIGE,CAIZHI,TECHUNIT,MWEIGHT,GB from TBMA_MATERIAL where ID='" + marid.Text + "' and STATE='1' ";
                        SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_select_mar);
                        if (!dr.HasRows)
                        {
                            return "MaridNoExistOrStopUsing";
                        }
                    }
                }
                else
                {
                    return "MaridEmpty";
                }
            }
            //数值检查
            string patternnumber = @"^\d+(\.\d+)?$";
            Regex rgx_num=new Regex(patternnumber);
            //材料长度、材料宽度、材料总长、材料单重、材料总重、部件自身重量、单重、总重、图纸上单重（自身重量）、图纸上单重、图纸上总重、面域
            bool num = rgx_num.IsMatch(cailiaocd.Text) && rgx_num.IsMatch(cailiaokd.Text) && rgx_num.IsMatch(cailiaozongchang.Text) && rgx_num.IsMatch(cailiaodzh.Text) && rgx_num.IsMatch(cailiaozongzhong.Text)  &&  rgx_num.IsMatch(txtTuDz.Text) && rgx_num.IsMatch(txtTuZz.Text) && rgx_num.IsMatch(bgzmy.Text);
            if (!num)
            {
                return "NumberFormatError";
            }
            return "0";
        }
        /// <summary>
        /// 判断变更是否发生
        /// </summary>
        /// <returns>返回值为可能引起的变更类别</returns>
        /// <returns>c:材料计划；z:制作明细；</returns>
        private string CheckChangeOccur()
        {
            string ret = "";
            //物料编码
            if ((marid.Text.Trim() != hdmarid.Value))
            {
                ret+="cz";
            }

            if (marid.Text.Trim() != "")
            {
                if (rblSFDC.SelectedValue != hdtxtSFDC.Value)
                {
                    ret += "c";
                }
            }

            //未修改物料编码
            if (marid.Text.Trim() == hdmarid.Value)
            {
                if (techUnit.Text.Trim().Contains("-(kg-") || techUnit.Text.Trim().Contains("-(T-") || techUnit.Text.Trim().Contains("-(吨-") || techUnit.Text.Trim().Contains("-(t-") || techUnit.Text.Trim().Contains("-(KG-") || techUnit.Text.Trim().Contains("-(kG-") || techUnit.Text.Trim().Contains("-(Kg-"))//以重量作为采购主单位
                {
                    string sumold =Convert.ToDouble(hdcailiaozongzhong.Value.Trim()).ToString("0.01");
                    string sumnew = Convert.ToDouble(cailiaozongzhong.Text.Trim()).ToString("0.01");
                    if (sumold != sumnew)
                    {
                        ret += "c";
                    }
                }
                else if (techUnit.Text.Trim().Contains("-(平米-") || techUnit.Text.Trim().Contains("-(平方米-") || techUnit.Text.Trim().Contains("-(㎡-") || techUnit.Text.Trim().Contains("-(M2-") || techUnit.Text.Trim().Contains("-(m2-"))//以平米作为采购主单位
                {
                    string sumold = Convert.ToDouble(Convert.ToDouble(hdbgzmy.Value.Trim()) * Convert.ToDouble(hd_p_shuliang.Value.Trim())).ToString("0.001");
                    string sumnew = Convert.ToDouble(Convert.ToDouble(bgzmy.Text.Trim()) * Convert.ToDouble(p_shuliang.Text.Trim())).ToString("0.001");
                    if (sumold != sumnew)
                    {
                        ret += "c";
                    }
                }
                else if (techUnit.Text.Trim().Contains("-(m-") || techUnit.Text.Trim().Contains("-(米-") || techUnit.Text.Trim().Contains("-(M-"))//已长度为采购主单位
                {
                    string sumold = Convert.ToDouble(hdcailiaozongchang.Value.Trim()).ToString("0");
                    string sumnew = Convert.ToDouble(cailiaozongchang.Text.Trim()).ToString("0");
                    if (sumold != sumnew)
                    {
                        ret += "c";
                    }
                }
                else//以数量作为采购主单位
                {
                    if (p_shuliang.Text.Trim() != hd_p_shuliang.Value.Trim())
                    {
                        ret += "c";
                    }
                }
            }


            //总数量//关键部件//是否体现在制作明细中
            if ((shuliang.Text.Trim() != hdshuliang.Value) ||  (rblInMS.SelectedValue != txthdinms.Value) || (tuhao.Text.Trim() != hdtuhao.Value.ToString()))
            {
                ret += "z";
            }

            //单重/自重/总重
            if ((txtTuDz.Text.Trim() != hidTudz.Value)||(txtTuZz.Text.Trim()!=txtTuZz.Text))
            {
                ret += "z";
            }
            return ret;
        }
        /// <summary>
        /// 返回材料总长或材料长度(待修改)
        /// </summary>
        /// <param name="wlbm">物料编码</param>
        /// <param name="clmc">材料名称</param>
        /// <param name="clcd">材料长度</param>
        /// <returns></returns>
        private double InsertChangduOrZongChang(string wlbm, string clmc, double clcd)
        {
            string dx="";
            string erj = "";
            if (wlbm != "")
            {
                dx = wlbm.Substring(0, 2);//eg：01 or 02
                erj = wlbm.Substring(0, 5);//eg：01.01
            }
            else
            {
                return 0;
            }
            if (dx != "02" && erj != "01.01")//非低值易耗品02和标准件
            {
                if (clmc.Contains("钢板") || clmc.Contains("钢格板"))//用此方法判断的可靠性有待商榷，目前为找到更好方法进行判断
                {
                    return clcd;
                }
                else
                {
                    return clcd*1.05;
                }
            }
            else
            {
                return clcd;
            }
        }
        /// <summary>
        /// 取消，关闭窗口，不刷新父页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.close();", true);
        }
        /// <summary>
        /// 变更Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void confirmChange_Click(object sender, EventArgs e)
        {
            if (ViewState["count"] == null)
            {
                ViewState["count"] = "0";
            }
            if (Convert.ToInt32(ViewState["count"]) % 2 == 0)
            {
                btnMarChange.Enabled = true;
             //   btnAttChange.Enabled = true;
                btnDelete.Enabled = true;
                btnattDelete.Enabled =true;
                confirmChange.Text = "锁定变更";
            }
            else
            {
                btnMarChange.Enabled = false;
             //   btnAttChange.Enabled = false;
                btnDelete.Enabled = false;
                btnattDelete.Enabled = false;
                confirmChange.Text = "激活变更";
            }
            ViewState["count"] = (Convert.ToInt32(ViewState["count"]) + 1).ToString();

        }
        /// <summary>
        /// 选择相应变更类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClick_Change(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            ViewState["state"] = ((Button)sender).CommandName;
            confirmChange.Enabled = false;
            //调用存储过程的相关参数
            ParamsAlterChange paraAC = new ParamsAlterChange();
            paraAC.TableName = ViewState["viewtable"].ToString();
            paraAC.TableNameOrg = ViewState["tablename"].ToString();
            paraAC.MsTableName = ViewState["mstable"].ToString();
            paraAC.TaskID = ViewState["engId"].ToString();
            paraAC.XuHao = ViewState["struid"].ToString();

            //if (ckbAttChange.Checked)
            //{
            //    paraAC.AttChangeToLower = "1";
            //}
            //else
            //{
            //    paraAC.AttChangeToLower = "0";
            //}

            if (ckbMarChange.Checked)
            {
                paraAC.NumberChangeToLower = "1";
            }
            else
            {
                paraAC.NumberChangeToLower = "0";
            }

            #region 物料/数量/重量(btnConfirm_Click)
            if (ViewState["state"].ToString() == "wszChg")
            {
                btnConfirmReal.Visible = true;
                //其他变更不可再用
              //  btnAttChange.Enabled = false;
                btnDelete.Enabled = false;
                btnattDelete.Enabled = false;
               
                //相关控件激活
                //物料编码
                if (marid.Text.Trim() != "")
                {
                    marid.Enabled = true;          
                }
                else
                {
                    marid.ReadOnly = true;
                }
                //材料长度
                cailiaocd.Enabled = true;
                //材料宽度
                cailiaokd.Enabled = true;
                //数量
                sing_shuliang.Disabled = false;
                p_shuliang.Enabled = true;
                //材料单重
                cailiaodzh.Enabled = true;
                //材料总重
                cailiaozongzhong.Enabled = true;
                cailiaozongzhong.ReadOnly = false;
                cailiaozongchang.Enabled = true;
                xlNote.Enabled = true;
                txtTuDz.Enabled = true;
                txtTuZz.Enabled = true;
                xialiao.Enabled = true;
                txtProcess.Enabled = true;
                //面域
                bgzmy.Enabled = true;
                my.Enabled = true;
                //是否定尺
                rblSFDC.Enabled = true;
                //是否体现在制作明细中
                rblInMS.Enabled = true;
                //毛坯形状
                cailiaoType.Enabled = true;
                tuhao.Enabled = true;
                techUnit.Enabled = true;
                txtYongliang.Enabled = true;
            }
            #endregion
            
            //#region 结构（只修改状态）
            //else if (ViewState["state"].ToString() == "attChg")
            //{
            //    //更改其父部件及子部件状态后关闭
            //    list_sql.Add("exec PRO_TM_AttChange '" + paraAC.TableName + "','" + paraAC.TaskID + "','" + paraAC.XuHao + "','"+paraAC.AttChangeToLower+"'");
            //    DBCallCommon.ExecuteTrans(list_sql);
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('变更已生成,请进入相应页面执行变更！！！');window.close();", true);
            //}
            //#endregion

            #region 单条删除(与结构删除类似，可调用同一存储过程)
            else if (ViewState["state"].ToString() == "singleDelete")
            {
            //更改该条记录变更状态审核通过后删除（该删除变更可能引起材料计划、制作明细及外协变更，因此要所有变更审核通过后才能删除）
                list_sql.Add(" exec [PRO_TM_AttChangeDeleteJudge] '" + paraAC.TableName + "','" + paraAC.TaskID + "','" + paraAC.XuHao + "'");
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('变更已生成,请进入相应页面执行变更！！！');window.close();", true);
            }
            #endregion



            #region 代用变更(btnConfirm_Click)
            else if (ViewState["state"].ToString() == "alterChg")
            {
                string check = this.AlterChangeExist();
                if (check == "OK")
                {
                    btnConfirmReal.Visible = true;
                    //其他变更不可再用
                    confirmChange.Enabled = false;
                    btnMarChange.Enabled = false;
                   // btnAttChange.Enabled = false;
                    btnDelete.Enabled = false;
                    btnattDelete.Enabled = false;
                    //可编辑控件

                    //物料编码
                    if (marid.Text.Trim() != "")
                    {
                        marid.Enabled = true;
                    }
                    else
                    {
                        marid.ReadOnly = true;
                    }
                    //材料长度
                    cailiaocd.Enabled = true;
                    //材料宽度
                    cailiaokd.Enabled = true;
                    //材料总长
                    cailiaozongchang.Enabled = true;
                    //数量
                    sing_shuliang.Disabled = false;
                    //材料单重
                    cailiaodzh.Enabled = true;
                    //材料总重
                    cailiaozongzhong.Enabled = true;
                    cailiaozongzhong.ReadOnly = false;
                    //自重//单重//总重
                    if (marid.Text.Trim() == "")
                    {
                       
                       //对于部件而言，单重和总重不可修改
                       
                    }
                    else
                    {
                        
                      //对于物料而言，单重和总重可修改，但自重（除开材料外的重量）不可修改
                        
                    }
                    //面域
                    bgzmy.Enabled = true;
                }
                else if (check.Contains("BuJian"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('部件无法代用！！！');window.close();", true);
                }
                else if (check.Contains("ChangeHandling"))
                {
                    string[] index = check.Split('-');
                    string unhandle = index[1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('总序下变更记录"+unhandle+"未处理,无法代用！！！');window.close();", true);
                }
            }
           #endregion
        }
        /// <summary>
        /// 对于代用记录而言，是否有未处理的变更记录;对于部件，无法进行代用
        /// </summary>
        /// <returns></returns>
        private string AlterChangeExist()
        {
            if (marid.Text.Trim() == "")
            {
                return "BuJian";
            }
            else
            {
                string sql_txt = "select BM_XUHAO from " + ViewState["tablename"] + " where BM_ENGID='" + ViewState["engId"] + "' and BM_ZONGXU='" + zongxu.Text.Trim() + "' and (BM_MPSTATUS!='0' or BM_MSSTATUS!='0' or BM_OSSTATUS!='0')";
                SqlDataReader dr_sql = DBCallCommon.GetDRUsingSqlText(sql_txt);
                if (dr_sql.HasRows)
                {
                    string ret = "ChangeHandling-";
                    while (dr_sql.Read())
                    {
                        ret += "【" + dr_sql["BM_XUHAO"].ToString() + "】";
                    }
                    dr_sql.Close();
                    return ret;
                }
                else
                {
                    return "OK";
                }
            }
        }
        /// <summary>
        /// 修改总序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChangeZX_OnClick(object sender, EventArgs e)
        {
            string cmd=((Button)sender).CommandName;
            if (cmd == "ActivateIndex")
            {
                zongxu.Enabled = true;
                btnChangeZX.Text = "提交";
                btnChangeZX.CommandName = "Submit";
            }
            else if (cmd == "Submit")
            {
                btnChangeZX.CommandName = "ActivateIndex";
                zongxu.Enabled = false;
                btnChangeZX.Text = "修改";

                if (zongxu.Text.Trim() == hdzongxu.Value.Trim())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('总序未改变,操作无效！！！');", true);
                }
                else
                {
                    string retVaule = this.CheckEditZongXu();
                    if (retVaule == "0")
                    {
                        List<string> list_sql = new List<string>();
                        ParamsAlterChange editzongxu = new ParamsAlterChange();
                        editzongxu.TableNameOrg = ViewState["tablename"].ToString();//原始数据表
                        editzongxu.TaskID = ViewState["engId"].ToString();//生产制号
                        editzongxu.MsTableName = ViewState["mstable"].ToString();//制作明细表(技术)
                        editzongxu.MspmTableName = ViewState["mspmtable"].ToString();//制作明细表（生产）
                        editzongxu.XuHao = ViewState["struid"].ToString();
                        editzongxu.OldZongXu = hdzongxu.Value.Trim();
                        editzongxu.NewZongXu = zongxu.Text.Trim();
                        if (ckbWchgZX.Checked)
                        {
                            editzongxu.EditType = "1";
                        }
                        else
                        {
                            editzongxu.EditType = "0";
                        }
                        //其他表是确定的，在存储过程中指定
                        string sql = "exec PRO_TM_EditZongXu '" + editzongxu.TableNameOrg + "','" + editzongxu.MsTableName + "','" + editzongxu.MspmTableName + "','" + editzongxu.TaskID + "','" + editzongxu.XuHao + "','" + editzongxu.OldZongXu + "','" + editzongxu.NewZongXu + "','" + editzongxu.EditType + "'";
                        list_sql.Add(sql);
                        DBCallCommon.ExecuteTrans(list_sql);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('总序已修改！！！');window.close();", true);
                    }
                    else if (retVaule.Contains("FormatError"))
                    {
                        zongxu.Text = hdzongxu.Value;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法修改！！！\\r\\r总序格式错误！！！');", true);
                    }
                    else if (retVaule.Contains("Existed"))
                    {
                        zongxu.Text = hdzongxu.Value;
                        if (ckbWchgZX.Checked)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法修改！！！\\r\\r总序或下级总序已存在！！！');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法修改！！！\\r\\r总序已存在！！！');", true);
                        }
                    }
                    else if (retVaule.Contains("BelongNotExist"))
                    {
                        zongxu.Text = hdzongxu.Value;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法修改！！！\\r\\r修改后总序不存在父部件！！！');", true);
                    }
                    else if (retVaule.Contains("BelongToMar"))
                    {
                        zongxu.Text = hdzongxu.Value;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改后总序归属到底层物料！！！');", true);
                    }
                }
            }
        }
      
        /// <summary>
        /// 检查修改的总序是否符合要求(1、不能存在；2、不能归属到物料；3、格式)
        /// </summary>
        /// <returns></returns>
        private string CheckEditZongXu()
        {
            string[] a = ViewState["engId"].ToString().Split('-');
            /////////////////////////////////////string firstCharofZX = a[a.Length - 1];
            ////////////////////////////////////string pattern = @"^(" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}(\\.[1-9]{1}[0-9]*)*)$";
            string firstCharofZX = "1";
            string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "(\\.[1-9]{1}[0-9]*)*)$";
            string pattern2 = @"^" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}$";
            Regex rgx = new Regex(pattern);
            Regex rgx2 = new Regex(pattern2);
            string ret = "0";
            //格式
            if (!rgx.IsMatch(zongxu.Text.Trim()))
            {
                ret = "FormatError";
                return ret;
            }
            //存在性
            string sql_exist = "";
            if (ckbWchgZX.Checked)
            {
                sql_exist = "select count(BM_ZONGXU) as Nums from " + ViewState["tablename"] + " where BM_ENGID LIKE '" + a[0] + "-%' and BM_ZONGXU IN(select STUFF(BM_ZONGXU,1,len('" + hdzongxu.Value + "'),'" + zongxu.Text + "') from " + ViewState["tablename"] + " where BM_ENGID LIKE '" + a[0] + "-%' and (BM_ZONGXU='" + hdzongxu.Value.Trim() + "' or   BM_ZONGXU like '" + hdzongxu.Value.Trim() + ".%')) AND BM_ZONGXU NOT IN(select BM_ZONGXU from " + ViewState["tablename"] + " where BM_ENGID LIKE '" + a[0] + "-%' and (BM_ZONGXU='" + hdzongxu.Value.Trim() + "' or   BM_ZONGXU like '" + hdzongxu.Value.Trim() + ".%') ) ";
            }
            else
            {
                sql_exist = "select count(BM_ZONGXU) as Nums from " + ViewState["tablename"] + " where BM_ENGID LIKE '" + a[0] + "-%' and BM_ZONGXU='" + zongxu.Text.Trim() + "'";
            }
            DataTable dt_exist = DBCallCommon.GetDTUsingSqlText(sql_exist);
            if (Convert.ToInt16(dt_exist.Rows[0]["Nums"].ToString()) > 0)
            {
                ret = "Existed";
                return ret;
            }
            //归属性
            string fx = zongxu.Text.Trim().Substring(0, zongxu.Text.Trim().LastIndexOf('.'));
             //归属部件不存在
            string sql_belongexist = "select count(BM_ZONGXU) as Nums from " + ViewState["tablename"] + " where BM_ENGID LIKE '" + a[0] + "-%' and BM_ZONGXU='" + fx + "' and (BM_MARID='' or BM_MARID is null)";
            DataTable dt_belongexist = DBCallCommon.GetDTUsingSqlText(sql_belongexist);
            if (dt_belongexist.Rows[0]["Nums"].ToString() == "0")
            {
                ret = "BelongNotExist";
                return ret;
            }
             //归属到物料
            string sql_belong = "select count(BM_ZONGXU) as Nums from " + ViewState["tablename"] + " where BM_ENGID like '" + a[0] + "-%' and BM_ZONGXU='" + fx + "' and BM_MARID!='' and BM_MARID not like '01.08.%' and BM_MARID not like '01.11.%'";
            DataTable dt_belong = DBCallCommon.GetDTUsingSqlText(sql_belong);
            if (Convert.ToInt16(dt_belong.Rows[0]["Nums"].ToString()) > 0)
            {
                ret = "BelongToMar";
                return ret;
            }
            return ret;
        }

        /// <summary>
        /// 取消明细变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelMSChange_OnClick(object sender, EventArgs e)
        {
            if (rblMSChangeSta.SelectedValue=="0") //正常记录
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('非变更明细，无法取消！！！');window.close();", true);
                return;
            }

            if(rblMSRew.SelectedValue=="1") //审核中记录
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核中记录，无法取消！！！');window.close();", true);
                return;
            }

            //存在删除记录，无法直接取消变更
            string sqldelete = "select * from " + ViewState["tablename"].ToString() + " where BM_ENGID='" + ViewState["engId"].ToString() + "' AND (BM_XUHAO='" + hdzongxu.Value.Trim() + "' OR BM_XUHAO LIKE '" + hdzongxu.Value.Trim() + ".%') AND BM_MSSTATUS='1'";
            DataTable dt_delete = DBCallCommon.GetDTUsingSqlText(sqldelete);
            if (dt_delete.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('存在变更删除记录，无法直接取消变更！！！');", true);
                return;
            }

            List<string> list_sql = new List<string>();

            list_sql.Add(" exec PRO_TM_CancelMSChange  '" + ViewState["engId"].ToString()+ "','" + ViewState["struid"].ToString() + "'");
            
            try
            {
                this.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');window.close();", true);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert(程序出错，请与管理员联系'！！！');", true);
            }
        }
        /// <summary>
        /// 取消计划变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelMpChange_OnClick(object sender, EventArgs e)
        {
            if (hdmarid.Value.Trim()=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('不含物料编码，无法进行【取消计划变更】操作！！！');", true);
                return;
            }

            if (rblMPChangSta.SelectedValue == "1")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除记录，无法进行【取消计划变更】操作！！！');", true);
                return;
            }

            //未提交计划的变更记录可以取消
            if (rblMPSTA.SelectedValue == "0" && rblMPChangSta.SelectedValue != "0"&&rblMPRew.SelectedValue=="0")
            {
                string sql_findold = "SELECT TOP 1 [MP_MARID],[MP_WEIGHT],[MP_NUMBER],[MP_LENGTH],[MP_WIDTH],[MP_FIXEDSIZE],[MP_PURCUNIT],[MP_UNIT]  from [View_TM_MPHZY] WHERE [MP_ENGID]='" + ViewState["engId"].ToString() + "' AND [MP_NEWXUHAO]='" + zongxu.Text.Trim() + "' AND [MP_ZONGXU]='" + zongxu.Text.Trim() + "' AND [MP_STATERV]='8' AND [MP_STATUS]='0'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_findold);
                if (dt.Rows.Count > 0)
                {
                    string error_before = "\\r\\r变更前信息》物料编码【" + dt.Rows[0]["MP_MARID"].ToString() + "】,重量【" +Convert.ToDouble(dt.Rows[0]["MP_WEIGHT"].ToString()).ToString("0.00") + "】,数量【" + Convert.ToDouble(dt.Rows[0]["MP_NUMBER"].ToString()).ToString("0.00") + "】,长【" + Convert.ToDouble(dt.Rows[0]["MP_LENGTH"].ToString()).ToString("0.00") + "】，宽【" + Convert.ToDouble(dt.Rows[0]["MP_WIDTH"].ToString()).ToString("0.00") + "】，是否定尺【" + dt.Rows[0]["MP_FIXEDSIZE"].ToString() + "】，单位【" + dt.Rows[0]["MP_UNIT"].ToString() + "】";

                    string _marid = hdmarid.Value.Trim();
                    string _wght = Convert.ToDouble(hdcailiaozongzhong.Value.Trim()).ToString("0.00");
                    string _num;

                    if (dt.Rows[0]["MP_PURCUNIT"].ToString() == "米" || dt.Rows[0]["MP_PURCUNIT"].ToString() == "M" || dt.Rows[0]["MP_PURCUNIT"].ToString() == "m")
                    {
                        _num = (Convert.ToDouble(hdcailiaozongchang.Value.Trim()) / 1000).ToString("0.00");
                    }
                    else if (dt.Rows[0]["MP_PURCUNIT"].ToString() == "平米" || dt.Rows[0]["MP_PURCUNIT"].ToString() == "平方米" || dt.Rows[0]["MP_PURCUNIT"].ToString() == "m2" || dt.Rows[0]["MP_PURCUNIT"].ToString() == "M2" || dt.Rows[0]["MP_PURCUNIT"].ToString() == "㎡")
                    {
                        _num = (Convert.ToDouble(hdbgzmy.Value.Trim()) * Convert.ToDouble(hd_p_shuliang.Value.Trim())).ToString("0.00");
                    }
                    else
                    {
                        _num = Convert.ToDouble(hd_p_shuliang.Value.Trim()).ToString("0.00");
                    }

                    string _len = Convert.ToDouble(hdcailiaocd.Value.Trim()).ToString("0.00");

                    if (techUnit.Text.Trim().Contains("-(米-") || techUnit.Text.Trim().Contains("-(M-") || techUnit.Text.Trim().Contains("-(m-") || hdcailiaoType.Value == "型" || hdcailiaoType.Value=="圆钢")
                    {
                        _len = Convert.ToDouble(hdcailiaozongchang.Value.Trim()).ToString("0.00");
                    }
                    string _width = Convert.ToDouble(hdcailiaokd.Value.Trim()).ToString("0.00");
                    string _fix = hdtxtSFDC.Value.Trim();
                    string _unit = hidtechunit.Value.Trim();

                    string error_after = "\\r\\r当前信息》物料编码【" + _marid + "】,重量【" + _wght + "】,数量【" + _num + "】,长【" + _len + "】，宽【" + _width + "】，是否定尺【" + _fix+ "】，单位【" + _unit + "】";


                    if (_marid != dt.Rows[0]["MP_MARID"].ToString() || _num !=Convert.ToDouble(dt.Rows[0]["MP_NUMBER"].ToString()).ToString("0.00") || _wght != Convert.ToDouble(dt.Rows[0]["MP_WEIGHT"].ToString()).ToString("0.00") || _len != Convert.ToDouble(dt.Rows[0]["MP_LENGTH"].ToString()).ToString("0.00") || _width != Convert.ToDouble(dt.Rows[0]["MP_WIDTH"].ToString()).ToString("0.00") || _fix != dt.Rows[0]["MP_FIXEDSIZE"].ToString())
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('变更前后信息不一致，无法进行【取消计划变更】操作！！！\\r\\r提示:\\r\\r变更前记录信息:" + error_before + "" + error_after + "');", true);
                        return;
                    }
                    else
                    {
                        string sqlupdate = "update " + ViewState["tablename"].ToString() + " set BM_MPSTATE='1',BM_MPSTATUS='0',BM_MPREVIEW='3' WHERE [BM_ENGID]='" + ViewState["engId"].ToString() + "' AND [BM_XUHAO]='" + zongxu.Text.Trim() + "' AND [BM_ZONGXU]='" + zongxu.Text.Trim() + "' AND BM_MPSTATE='0'  AND BM_MPSTATUS!='0'";
                        DBCallCommon.ExeSqlText(sqlupdate);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');window.close();", true);
                        return;
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法进行【取消计划变更】操作！！！\\r\\r提示:未找到变更前记录');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法进行【取消计划变更】操作！！！\\r\\r提示:非变更记录或计划已生成');", true);
                return;
            }
        }
      


        protected  void ExecuteTrans(List<string> sqlTexts)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlConn.Open();

            //启用事务
            SqlTransaction sqlTran = sqlConn.BeginTransaction();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandTimeout = 1000;
            sqlCmd.Transaction = sqlTran;
            try
            {
                foreach (string sqlText in sqlTexts)
                {
                    sqlCmd.CommandText = sqlText;
                    sqlCmd.ExecuteNonQuery();
                }
                sqlTran.Commit();
            }
            catch (Exception)
            {
                sqlTran.Rollback();
                throw;
            }
            finally
            {
               DBCallCommon.closeConn(sqlConn);
            }
        }

        //存储过程参数
        private class ParamsAlterChange
        {
            private string _TableName;//原始数据视图
            private string _TableNameOrg;//原始数据
            private string _MsTableName;//制作明细表
            private string _MspmTableName;//生产部制作明细表
            private string _TaskID;//生产制号
            private string _XuHao;//序号
            private string _ZongXu;//总序
            private int _MpWetherChg;//材料计划是否变更
            private int _MsWetherChg;//制作明细是否变更
            private int _OutWetherChg;//外协是否变更
            private string _OldZongXu;
            private string _NewZongXu;
            private string _EditType;//是否修改下级（0：否，1：是）
            private string _SqlForCal;
            private string _NumberChangeToLower;//数量变更关联到下级
            private string _AttChangeToLower;//结构变更关联到下级
            public string TableName
            {
                get { return _TableName; }
                set { _TableName = value; }
            }

            public string TableNameOrg
            {
                get { return _TableNameOrg; }
                set { _TableNameOrg = value; }
            }

            public string MspmTableName
            {
                get { return _MspmTableName; }
                set { _MspmTableName = value; }
            }
            public string MsTableName
            {
                get { return _MsTableName; }
                set { _MsTableName = value; }
            }

            public string TaskID
            {
                get { return _TaskID; }
                set { _TaskID = value; }
            }

            public string XuHao
            {
                get { return _XuHao; }
                set { _XuHao = value; }
            }

            public string ZongXu
            {
                get { return _ZongXu; }
                set { _ZongXu = value; }
            }

            public int MpWetherChg
            {
                get { return _MpWetherChg; }
                set { _MpWetherChg = value; }
            }
            public int MsWetherChg
            {
                get { return _MsWetherChg; }
                set { _MsWetherChg = value; }
            }
            public int OutWetherChg
            {
                get { return _OutWetherChg; }
                set { _OutWetherChg = value; }
            }
            public string OldZongXu
            {
                get { return _OldZongXu; }
                set { _OldZongXu = value; }
            }
            public string NewZongXu
            {
                get { return _NewZongXu; }
                set { _NewZongXu = value; }
            }
            public string EditType
            {
                get { return _EditType; }
                set { _EditType = value; }
            }
            public string SqlForCal
            {
                get { return _SqlForCal; }
                set { _SqlForCal = value; }
            }

            public string NumberChangeToLower
            {
                get { return _NumberChangeToLower; }
                set { _NumberChangeToLower = value; }
            }
            public string AttChangeToLower
            {
                get { return _AttChangeToLower; }
                set { _AttChangeToLower = value; }
            }
        }
    }
}
