<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_KQTOTAL.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KQTOTAL" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
考勤汇总
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        window.onload=function(){
           var tab = document.getElementById("tab");
           for (i = 2; i < (tab.rows.length-1); i++)
            {
                var cols = tab.rows[i].cells.length-2;
                for(var m = 5; m < cols; m++)
                {
                   if(tab.rows[i].getElementsByTagName("td")[m].getElementsByTagName("input")[0].value=="0")
                   {
                       tab.rows[i].getElementsByTagName("td")[m].getElementsByTagName("input")[0].value="";
                   }
                }
            }
            var footnum=tab.rows.length-1;
            for(var n = 2; n < cols; n++)
            {
                   if(tab.rows[footnum].getElementsByTagName("td")[n].getElementsByTagName("span")[0].innerHTML=="0")
                   {
                       tab.rows[footnum].getElementsByTagName("td")[n].getElementsByTagName("span")[0].innerHTML="";
                   }
           } 
        }
    </script>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
        <div class="box-outer">
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;&nbsp;
                          <strong>开始年月：</strong><asp:TextBox ID="tb_CXstarttime" Width="80px" runat="server"></asp:TextBox>
                        <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                            ID="CalendarExtender2" runat="server" Format="yyyy-MM" TargetControlID="tb_CXstarttime">
                        </asp:CalendarExtender>
                        &nbsp;&nbsp;
                        <strong>结束年月：</strong><asp:TextBox ID="tb_CXendtime" Width="80px" runat="server"></asp:TextBox>
                        <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                            ID="CalendarExtender3" runat="server" Format="yyyy-MM" TargetControlID="tb_CXendtime">
                        </asp:CalendarExtender> 
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="DropDownListtype" runat="server" AutoPostBack="false">
                            <asp:ListItem Text="—请选择—" Value=""></asp:ListItem>
                            <asp:ListItem Text="出勤" Value="KQ_CHUQIN"></asp:ListItem>
                            <asp:ListItem Text="国内出差" Value="KQ_GNCC"></asp:ListItem>
                            <asp:ListItem Text="国外出差" Value="KQ_GWCC"></asp:ListItem>
                            <asp:ListItem Text="病假" Value="KQ_BINGJ"></asp:ListItem>
                            <asp:ListItem Text="事假" Value="KQ_SHIJ"></asp:ListItem>
                            <asp:ListItem Text="旷工" Value="KQ_KUANGG"></asp:ListItem>
                            <asp:ListItem Text="倒休" Value="KQ_DAOXIU"></asp:ListItem>
                            <asp:ListItem Text="产假" Value="KQ_CHANJIA"></asp:ListItem>
                            <asp:ListItem Text="陪产假" Value="KQ_PEICHAN"></asp:ListItem>
                            <asp:ListItem Text="婚假" Value="KQ_HUNJIA"></asp:ListItem>
                            <asp:ListItem Text="丧假" Value="KQ_SANGJIA"></asp:ListItem>
                            <asp:ListItem Text="工伤" Value="KQ_GONGS"></asp:ListItem>
                            <asp:ListItem Text="年休假" Value="KQ_NIANX"></asp:ListItem>
                            <asp:ListItem Text="借调" Value="KQ_JIEDIAO"></asp:ListItem>
                            <asp:ListItem Text="周末工作" Value="KQ_ZMJBAN"></asp:ListItem>
                            <asp:ListItem Text="节日工作" Value="KQ_JRJIAB"></asp:ListItem>
                            <asp:ListItem Text="值班" Value="KQ_ZHIBAN"></asp:ListItem>
                            <asp:ListItem Text="夜班" Value="KQ_YEBAN"></asp:ListItem>
                            <asp:ListItem Text="中班" Value="KQ_ZHONGB"></asp:ListItem>
                            <asp:ListItem Text="餐补" Value="KQ_CBTS"></asp:ListItem>
                            <asp:ListItem Text="延时工作" Value="KQ_YSGZ"></asp:ListItem>
                            <asp:ListItem Text="病假1" Value="KQ_BEIYONG2"></asp:ListItem>
                            <asp:ListItem Text="事假1" Value="KQ_BEIYONG3"></asp:ListItem>
                            <asp:ListItem Text="倒休1" Value="KQ_BEIYONG1"></asp:ListItem>
                            <asp:ListItem Text="离职" Value="KQ_BEIYONG4"></asp:ListItem>
                            <asp:ListItem Text="未入职" Value="KQ_BEIYONG5"></asp:ListItem>
                            <asp:ListItem Text="待岗" Value="KQ_BEIYONG6"></asp:ListItem>
                            <asp:ListItem Text="在培" Value="KQ_QTJIA"></asp:ListItem>
                        </asp:DropDownList> 
                        <strong>大于/等于：</strong><asp:TextBox ID="tb_tsorxs" Width="50px" runat="server"></asp:TextBox>天/小时
                        <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <strong>部门：</strong>&nbsp;
                        <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedIndexChanged="dplbm_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <strong>班组：</strong>
                        <asp:DropDownList ID="ddlbz" runat="server" Width="90px" AutoPostBack="true" OnSelectedIndexChanged="ddlbz_OnSelectedIndexChanged">
                        </asp:DropDownList>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <strong>姓名：</strong><asp:TextBox ID="txtName" Width="50px" ForeColor="Gray" runat="server"></asp:TextBox>
                        &nbsp;&nbsp;
                        
                        <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click"></asp:Button>
                        &nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="../Assets/icon-fuction/388.gif" border="0" hspace="2" 
                                        align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
                         <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-500"  OffsetY="8"  TargetControlID="HyperLink1" PopupControlID="palORG">
                         </asp:PopupControlExtender>
                         
                         <asp:Panel ID="palORG" Width="600px" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server"> 
                         <table width="100%" >
                             <tr>       
                                 <td>
                                      <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                                          <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                                      </div>
                                      <br /><br />
                                 </td>
                             </tr>
                             <tr>
                                 <td>部门&nbsp;&nbsp;&nbsp;</td>
                                 <td colspan="3">
                                     <asp:CheckBoxList ID="listdepartment" runat="server" RepeatColumns="5">
                                     </asp:CheckBoxList>
                                 </td>
                             </tr>
                             <tr>
                                 <td>需合计项</td>
                                 <td colspan="3">
                                     <asp:CheckBoxList ID="CheckBoxListhj" runat="server" RepeatColumns="8">
                                          <asp:ListItem Text="国内出差" Value="KQ_GNCC"></asp:ListItem>
                                          <asp:ListItem Text="国外出差" Value="KQ_GWCC"></asp:ListItem>
                                          <asp:ListItem Text="病假" Value="KQ_BINGJ"></asp:ListItem>
                                          <asp:ListItem Text="事假" Value="KQ_SHIJ"></asp:ListItem>
                                          <asp:ListItem Text="旷工" Value="KQ_KUANGG"></asp:ListItem>
                                          <asp:ListItem Text="倒休" Value="KQ_DAOXIU"></asp:ListItem>
                                          <asp:ListItem Text="产假" Value="KQ_CHANJIA"></asp:ListItem>
                                          <asp:ListItem Text="陪产假" Value="KQ_PEICHAN"></asp:ListItem>
                                          <asp:ListItem Text="婚假" Value="KQ_HUNJIA"></asp:ListItem>
                                          <asp:ListItem Text="丧假" Value="KQ_SANGJIA"></asp:ListItem>
                                          <asp:ListItem Text="工伤" Value="KQ_GONGS"></asp:ListItem>
                                          <asp:ListItem Text="年休假" Value="KQ_NIANX"></asp:ListItem>
                                          <asp:ListItem Text="病假1" Value="KQ_BEIYONG2"></asp:ListItem>
                                          <asp:ListItem Text="事假1" Value="KQ_BEIYONG3"></asp:ListItem>
                                          <asp:ListItem Text="倒休1" Value="KQ_BEIYONG1"></asp:ListItem>
                                          <asp:ListItem Text="离职" Value="KQ_BEIYONG4"></asp:ListItem>
                                          <asp:ListItem Text="未入职" Value="KQ_BEIYONG5"></asp:ListItem>
                                          <asp:ListItem Text="待岗" Value="KQ_BEIYONG6"></asp:ListItem>
                                          <asp:ListItem Text="在培" Value="KQ_QTJIA"></asp:ListItem>
                                          <asp:ListItem Text="借调" Value="KQ_JIEDIAO"></asp:ListItem>
                                          <asp:ListItem Text="周末工作" Value="KQ_ZMJBAN"></asp:ListItem>
                                          <asp:ListItem Text="节日工作" Value="KQ_JRJIAB"></asp:ListItem>
                                          <asp:ListItem Text="值班" Value="KQ_ZHIBAN"></asp:ListItem>
                                          <asp:ListItem Text="夜班" Value="KQ_YEBAN"></asp:ListItem>
                                          <asp:ListItem Text="中班" Value="KQ_ZHONGB"></asp:ListItem>
                                     </asp:CheckBoxList>
                                 </td>
                             </tr>
                             <tr>
                                 <td>筛选合计:&nbsp;&nbsp;</td>
                                 <td>
                                     大于等于<asp:TextBox ID="txt_hjmin" runat="server"></asp:TextBox>
                                 </td>
                                 <td>小于等于</td>
                                 <td>
                                     <asp:TextBox ID="txt_hjmax" runat="server"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="4" align="center">
                                     <asp:Button ID="btn_confirm" runat="server" Text="确定" UseSubmitBehavior="false"  OnClick="btn_confirm_Click"/>&nbsp;&nbsp; 
                                     <asp:Button ID="btn_clear" runat="server" Text="清空" UseSubmitBehavior="false"   OnClick="btn_clear_Click"/>
                                 </td>
                             </tr>
                         </table>
                    </asp:Panel>
                   </td>
                </tr>
                <tr>
                   <td align="right"><asp:Button ID="btnexport" runat="server" Text="导出" OnClick="btnexport_Click"></asp:Button>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   </td>
                </tr>
            </table>
        </div>
    </div>
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="box-wrapper">
                <div class="box-outer">
                    <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                        <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                            border="1" width="100%">
                            <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" style="background-color: #B9D3EE;">
                                        <td rowspan="2">
                                            序号
                                        </td>
                                        <td rowspan="2">
                                            工号
                                        </td>
                                        <td rowspan="2">
                                            姓名
                                        </td>
                                        <td rowspan="2">
                                            部门
                                        </td>
                                        <td rowspan="2">
                                            班组
                                        </td>
                                        <td rowspan="2">
                                            出勤
                                        </td>
                                        <td colspan="2" runat="server" id="tdCCTG">
                                            出差统计
                                        </td>
                                        <td colspan="17" runat="server" id="tdGLQJTJ">
                                            各类请假统计
                                        </td>
                                        <td rowspan="2">
                                            借调
                                        </td>
                                        <td rowspan="2">
                                            周末工作（天）
                                        </td>
                                        <td rowspan="2">
                                            节日工作（天）
                                        </td>
                                        <td rowspan="2">
                                            值班（天）
                                        </td>
                                        <td rowspan="2">
                                            夜班
                                        </td>
                                        <td rowspan="2">
                                            中班
                                        </td>
                                        <td rowspan="2">
                                            餐补天数
                                        </td>
                                        <td rowspan="2">
                                            延时工作（小时）
                                        </td>
                                        <td rowspan="2">
                                            筛选合计
                                        </td>
                                        <td rowspan="2">
                                            查看具体考勤信息 
                                        </td>
                                    </tr>
                                    <tr style="background-color: #B9D3EE;">
                                        <td runat="server" id="tdGuonei">
                                            国内
                                        </td>
                                        <td runat="server" id="tdGuowai">
                                            国外
                                        </td>
                                        <td runat="server" id="tdBingjia">
                                            病假
                                        </td>
                                        <td runat="server" id="tdShijia">
                                            事假
                                        </td>
                                        <td runat="server" id="tdKuanggong">
                                            旷工
                                        </td>
                                        <td runat="server" id="tdDaoxiu">
                                            倒休
                                        </td>
                                        <td runat="server" id="tdChanjia">
                                            产假
                                        </td>
                                        <td runat="server" id="tdPeichanjia">
                                            陪产假
                                        </td>
                                        <td runat="server" id="tdHunjia">
                                            婚假
                                        </td>
                                        <td runat="server" id="tdSangjia">
                                            丧假
                                        </td>
                                        <td runat="server" id="tdGongshang">
                                            工伤
                                        </td>
                                        <td runat="server" id="tdNianxiu">
                                            年休假
                                        </td>
                                        <td runat="server" id="tdBeiyong1">
                                            倒休1
                                        </td>
                                        <td runat="server" id="tdBeiyong2">
                                            病假1
                                        </td>
                                        <td runat="server" id="tdBeiyong3">
                                            事假1
                                        </td>
                                        <td runat="server" id="tdBeiyong4">
                                            离职
                                        </td>
                                        <td runat="server" id="tdBeiyong5">
                                            未入职
                                        </td>
                                        <td runat="server" id="tdBeiyong6">
                                            待岗
                                        </td>
                                        <td runat="server" id="tdQita">
                                            在培
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);">
                                        <td>
                                            <asp:Label runat="server" ID="lbKQ_ST_ID" Visible="false" Text='<%#Eval("KQ_ST_ID")%>'></asp:Label>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false" Onclick="checkme(this)" />
                                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*(Convert.ToDouble(UCPaging1.PageSize))%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_gh" runat="server" Width="90px" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_name" runat="server" Width="50px" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_bm" runat="server" Width="90px" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_bz" runat="server" Text='<%#Eval("ST_DEPID1")%>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:TextBox ID="tb_chuqin" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_CHUQIN")%>'> </asp:TextBox>
                                        </td>
                                        <td id="tdGuonei1" runat="server" align="right">
                                            <asp:TextBox ID="tb_gncc" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_GNCC")%>'></asp:TextBox>
                                        </td>
                                        <td id="tdGuowai1" runat="server" align="right">
                                            <asp:TextBox ID="tb_gwcc" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_GWCC")%>'></asp:TextBox>
                                        </td>
                                        <td id="tdBingjia1" runat="server" align="right">
                                            <asp:TextBox ID="tb_bingj" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_BINGJ")%>'></asp:TextBox>
                                        </td>
                                        <td id="tdShijia1" runat="server" align="right">
                                            <asp:TextBox ID="tb_shij" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_SHIJ")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdKuanggong1" runat="server" align="right">
                                            <asp:TextBox ID="tb_kuangg" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_KUANGG")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdDaoxiu1" runat="server" align="right">
                                            <asp:TextBox ID="tb_daoxiu" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_DAOXIU")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdChanjia1" runat="server" align="right">
                                            <asp:TextBox ID="tb_chanjia" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_CHANJIA")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdPeichanjia1" runat="server" align="right">
                                            <asp:TextBox ID="tb_peichan" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_PEICHAN")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdHunjia1" runat="server" align="right">
                                            <asp:TextBox ID="tb_hunjia" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_HUNJIA")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdSangjia1" runat="server" align="right">
                                            <asp:TextBox ID="tb_sangjia" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_SANGJIA")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdGongshang1" runat="server" align="right">
                                            <asp:TextBox ID="tb_gongs" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_GONGS")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdNianxiu1" runat="server" align="right">
                                            <asp:TextBox ID="tb_nianx" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_NIANX")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdBeiyong11" runat="server" align="right">
                                            <asp:TextBox ID="tb_beiyong1" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_BEIYONG1")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdBeiyong21" runat="server" align="right">
                                            <asp:TextBox ID="tb_beiyong2" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_BEIYONG2")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdBeiyong31" runat="server" align="right">
                                            <asp:TextBox ID="tb_beiyong3" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_BEIYONG3")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdBeiyong41" runat="server" align="right">
                                            <asp:TextBox ID="tb_beiyong4" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_BEIYONG4")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdBeiyong51" runat="server" align="right">
                                            <asp:TextBox ID="tb_beiyong5" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_BEIYONG5")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdBeiyong61" runat="server" align="right">
                                            <asp:TextBox ID="tb_beiyong6" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_BEIYONG6")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td id="tdQita1" runat="server" align="right">
                                            <asp:TextBox ID="tb_qtjia" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="30px" Text='<%#Eval("KQ_QTJIA")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:TextBox ID="tb_jiediao" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("KQ_JIEDIAO")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:TextBox ID="tb_zmjban" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("KQ_ZMJBAN")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:TextBox ID="tb_jrjiab" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("KQ_JRJIAB")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:TextBox ID="tb_zhiban" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("KQ_ZHIBAN")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:TextBox ID="tb_yeban" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("KQ_YEBAN")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:TextBox ID="tb_zhongb" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("KQ_ZHONGB")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        
                                        <td align="right">
                                            <asp:TextBox ID="tb_KQ_CBTS" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("KQ_CBTS")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        
                                        <td align="right">
                                            <asp:TextBox ID="tb_ysgz" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("KQ_YSGZ")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbsearchhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:HyperLink ID="hlContract3" Target="_blank" ToolTip="查看具体考勤信息" NavigateUrl='<%#"~/OM_Data/OM_KQlistsearch.aspx?flag=kqtotal&stid="+Eval("KQ_ST_ID") %>'
                                                CssClass="link" runat="server">
                                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                                查看具体考勤信息
                                           </asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                
                                
                                <FooterTemplate>
                                    <tr>
                                        <td colspan="5" align="right">
                                        合计:
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_CHUQINhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_GNCChj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_GWCChj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_BINGJhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_SHIJhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_KUANGGhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_DAOXIUhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_CHANJIAhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_PEICHANhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_HUNJIAhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_SANGJIAhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_GONGShj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_NIANXhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_BEIYONG1hj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_BEIYONG2hj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_BEIYONG3hj" runat="server"></asp:Label>
                                        </td>
                                        
                                        <td align="center">
                                            <asp:Label ID="lbKQ_BEIYONG4hj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_BEIYONG5hj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_BEIYONG6hj" runat="server"></asp:Label>
                                        </td>
                                        
                                        <td align="center">
                                            <asp:Label ID="lbKQ_QTJIAhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_JIEDIAOhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_ZMJBANhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_JRJIABhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_ZHIBANhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_YEBANhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_ZHONGBhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_CBTShj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_YSGZhj" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                            没有记录!<br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        <%--</ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
