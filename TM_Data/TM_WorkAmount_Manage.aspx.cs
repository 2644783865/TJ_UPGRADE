using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_WorkAmount_Manage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            const int startColumn = 14;//第二行表头的起始列数
            //int start_Column;
            switch (e.Row.RowType)
            {
                //判断是否表头
                case DataControlRowType.Header:
                    //第一行表头
                    TableCellCollection tcHeader = e.Row.Cells;
                    tcHeader.Clear();

                    //toggle(a,b,c)函数的两个参数代表要隐藏的内容列数和表头列数，a为起始内容列，b为终止内容列，c为表头列数。
                    tcHeader.Add(new TableHeaderCell());
                    //tcHeader[0].Attributes.Add("rowspan", "2");
                    tcHeader[0].Attributes.Add("colspan", "15");
                    tcHeader[0].Attributes.Add("onclick", "toggle(1,14,0)");
                    tcHeader[0].Attributes["style"] += "cursor:pointer;cursor:hand;";
                    tcHeader[0].Text = "工程信息" + "<span style='color:red;'>(点击可伸缩)</span>";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[1].Attributes.Add("colspan", "6");
                    tcHeader[1].Attributes.Add("onclick", "toggle(16,20,1)");
                    tcHeader[1].Attributes["style"] += "cursor:pointer;cursor:hand;";
                    tcHeader[1].Text = "接收日期" + "<span style='color:red;'>(点击可伸缩)</span>";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[2].Attributes.Add("colspan", "12");
                    tcHeader[2].Attributes.Add("onclick", "toggle(22,32,2)"); tcHeader[2].Attributes["style"] += "cursor:pointer;cursor:hand;";
                    tcHeader[2].Text = "技术负责人" + "<span style='color:red;'>(点击可伸缩)</span>";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[3].Attributes.Add("colspan", "4");
                    tcHeader[3].Attributes.Add("onclick", "toggle(34,36,3)");
                    tcHeader[3].Attributes["style"] += "cursor:pointer;cursor:hand;";
                    tcHeader[3].Text = "船次" + "<span style='color:red;'>(点击可伸缩)</span>";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[4].Attributes.Add("colspan", "1");
                    tcHeader[4].Text = "工程量";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[5].Attributes.Add("colspan", "4");
                    tcHeader[5].Attributes.Add("onclick", "toggle(39,41,5)");
                    tcHeader[5].Attributes["style"] += "cursor:pointer;cursor:hand;";
                    tcHeader[5].Text = "分项重量" + "<span style='color:red;'>(点击可伸缩)</span>";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[6].Attributes.Add("colspan", "3");
                    tcHeader[6].Attributes.Add("onclick", "toggle(43,44,6)");
                    tcHeader[6].Attributes["style"] += "cursor:pointer;cursor:hand;";
                    tcHeader[6].Text = "批次发运百分比" + "<span style='color:red;'>(点击可伸缩)</span>";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[7].Attributes.Add("colspan", "8");
                    tcHeader[7].Attributes.Add("onclick", "toggle(46,52,7)");
                    tcHeader[7].Attributes["style"] += "cursor:pointer;cursor:hand;";
                    tcHeader[7].Text = "统计信息" + "<span style='color:red;'>(点击可伸缩)</span>";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[8].Attributes.Add("colspan", "4");
                    tcHeader[8].Attributes.Add("onclick", "toggle(54,56,8)");
                    tcHeader[8].Attributes["style"] += "cursor:pointer;cursor:hand;";
                    tcHeader[8].Text = "制作单位" + "<span style='color:red;'>(点击可伸缩)</span>";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[9].Attributes.Add("colspan", "5");
                    tcHeader[9].Attributes.Add("onclick", "toggle(58,61,9)");
                    tcHeader[9].Attributes["style"] += "cursor:pointer;cursor:hand;";
                    tcHeader[9].Text = "合同号" + "<span style='color:red;'>(点击可伸缩)</span>";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[10].Attributes.Add("colspan", "3");
                    tcHeader[10].Attributes.Add("onclick", "toggle(63,64,10)");
                    tcHeader[10].Attributes["style"] += "cursor:pointer;cursor:hand;";
                    tcHeader[10].Text = "备注" + "<span style='color:red;'>(点击可伸缩)</span>";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[11].Attributes.Add("colspan", "1");
                    tcHeader[11].Text = "成本用完成统计";

                    tcHeader.Add(new TableHeaderCell());
                    //tcHeader[12].Attributes.Add("colspan", "1");
                    tcHeader[12].Text = "说明";

                    tcHeader.Add(new TableHeaderCell());
                    //tcHeader[13].Attributes.Add("colspan", "1");
                    tcHeader[13].Text = "发出日期</th></tr><tr>";


                    #region
                    //第二行表头 共67个

                    //任务ID
                    //部件名称
                    //项目ID
                    //项目名称
                    //工程ID
                    //工程名称
                    //发运
                    //准备
                    //类型
                    //代号
                    //图号
                    //设备号
                    //设计单位
                    //型号
                    //接收日期
                    //合同完成日期
                    //类型
                    //图纸状态
                    //委托人
                    //备注
                    //技术负责人
                    //技术负责人姓名
                    //任务开始时间
                    //工期
                    //计划完成日期
                    //说明
                    //截止今天剩余时间
                    //实际完成工期
                    //油漆计划
                    //技术交底
                    //计划准备进度
                    //第三方
                    //船次
                    //工程等级船次调整
                    //目的港
                    //集港时间
                    //数量(工程量)
                    //分项总重(工程量)
                    //结算重量
                    //厂内提供材料重量
                    //单重
                    //批次发运百分比
                    //设备重量
                    //产品发运百分比
                    //统计信息体积（m3）
                    //总净重(kg)
                    //毛重(kg)
                    //体重比
                    //包装材料比
                    //包装材料
                    //最终体积（m3）
                    //包数
                    //制造单位
                    //供料方式
                    //开工日期
                    //完工日期
                    //合同号
                    //任务单号
                    //预付款%
                    //进度款%
                    //结算%
                    //备注
                    //总序号1
                    //总序号2
                    //成本用完成统计
                    //说明
                    //发出日期

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn].Text = "  ";
                    tcHeader[startColumn].Attributes.Add("height", "80px");//为了防止伸缩时高度变化
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+1].Text = "任务ID";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+2].Text = "部件名称";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+3].Text = "项目ID";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+4].Text = "项目名称";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+5].Text = "工程ID";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+6].Text = "工程名称";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+7].Text = "发送";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+8].Text = "准备";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+9].Text = "类型";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+10].Text = "代号";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+11].Text = "图号";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+12].Text = "设备号";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+13].Text = "设计单位";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+14].Text = "型号";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+15].Text = "接受日期";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+16].Text = "合同完成日期";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+17].Text = "类型";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+18].Text = "图纸状态";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+19].Text = "委托人";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+20].Text = "备注";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+21].Text = "技术负责人";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+22].Text = "技术负责人姓名";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+23].Text = "任务开始时间";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+24].Text = "工期";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+25].Text = "计划完成日期";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+26].Text = "说明";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+27].Text = "截至今天剩余时间";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+28].Text = "实际完成工期";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+29].Text = "油漆计划";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+30].Text = "技术交底";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+31].Text = "计划准备进度";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+32].Text = "第三方";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+33].Text = "船次";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+34].Text = "工程等级船次调整";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+35].Text = "目的港";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+36].Text = "集港时间";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+37].Text = "数量（工程量）";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+38].Text = "分项总量（工程量）";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+39].Text = "结算重量"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+40].Text = "厂内提供材料重量"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+41].Text = "单重"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+42].Text = "批次发运百分比"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+43].Text = "设备重量"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+44].Text = "产品发运百分比"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+45].Text = "统计信息体积（M3)"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+46].Text = "总净重（kg)"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+47].Text = "毛重"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+48].Text = "体重比"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+49].Text = "包装材料比"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+50].Text = "包装材料"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+51].Text = "最终体积（m3）"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+52].Text = "包数"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+53].Text = "制造单位"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+54].Text = "供料方式"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+55].Text = "开工日期"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+56].Text = "完工日期"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+57].Text = "合同号"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+58].Text = "任务单号"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+59].Text = "预付款%"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+60].Text = "进度款%"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+61].Text = "结算%"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+62].Text = "备注"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+63].Text = "总序号1"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+64].Text = "总序号2"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+65].Text = "成本用完成统计"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+66].Text = "说明"; tcHeader.Add(new TableHeaderCell());
                    tcHeader[startColumn+67].Text = "发出日期";

                    #endregion

                    break;
            }
        }

    }
}
