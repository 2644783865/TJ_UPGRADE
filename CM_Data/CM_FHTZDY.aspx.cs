using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_FHTZDY : System.Web.UI.Page
    {
        string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["CM_FID"];
            BindData();
        }

        private void BindData()
        {
            string sql = "select a.*,b.ST_NAME as name1,d.ST_NAME as name2,e.ST_NAME as name3 from TBCM_FHNOTICE as a left join TBDS_STAFFINFO as b on a.CM_MANCLERK=b.ST_ID left join TBDS_STAFFINFO as d on a.CM_BMZG=d.ST_ID left join TBDS_STAFFINFO as e on a.CM_GSLD=e.ST_ID where CM_FID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                CM_BIANHAO.Text = dr["CM_BIANHAO"].ToString();
                hid_BianHao.Value = dr["CM_BIANHAO"].ToString();
                CM_CUSNAME.Text = dr["CM_CUSNAME"].ToString();
                CM_SH.Text = dr["CM_SH"].ToString();
                CM_JH.Text = dr["CM_JH"].ToString();
                CM_LXR.Text = dr["CM_LXR"].ToString();
                CM_LXFS.Text = dr["CM_LXFS"].ToString();
                CM_JHTIME.Text = dr["CM_JHTIME"].ToString();
                CM_BEIZHU.Text = dr["CM_BEIZHU"].ToString();
                CM_MANCLERK.Text = dr["name1"].ToString();
                CM_ZDTIME.Text = dr["CM_ZDTIME"].ToString();
                //Hidden.Value = dr["CM_ATTACH"].ToString();
                //HidCSR.Value = dr["CM_CSR"].ToString();
                ZG.Text = "李立恒";

            }
            string sql1 = "select a.*,(case when b.CM_YFSM is null then '0' else CM_YFSM end) as TSA_YFSM from View_CM_FaHuo as a left join (select sum(CM_FHNUM) as CM_YFSM,CM_ID from VIEW_CM_FaHuo where CM_CONFIRM!=3 group by CM_ID) as b on a.CM_ID=b.CM_ID where CM_FID='" + id + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sql1);
            Det_Repeater.DataSource = dt;
            Det_Repeater.DataBind();
        }
    }
}
