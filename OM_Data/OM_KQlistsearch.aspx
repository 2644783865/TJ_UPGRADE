<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_KQlistsearch.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KQlistsearch" Title="考勤信息查询" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
   考勤信息查询
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
                for(var m = 7; m < cols; m++)
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
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="radio_all" runat="server" Text="全部数据" GroupName="shenhe" OnCheckedChanged="radio_CheckedChanged"
                                            AutoPostBack="True"/>
                        <asp:RadioButton ID="radio_dangnian" runat="server" Text="当前年份数据" GroupName="shenhe" OnCheckedChanged="radio_CheckedChanged"
                                            AutoPostBack="True" Checked="true" />
                       
                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <div id="div_statistcs" runat="server" style="width: 100%; height: auto; overflow: scroll; display: block;" visible="false">
                        <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                            border="1" width="100%">
                            <asp:Repeater ID="rptKQTJ1" runat="server" OnItemDataBound="rptKQTJ1_OnItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" style="background-color: #B9D3EE;">
                                        <td rowspan="2">
                                            序号
                                        </td>
                                        <td rowspan="2">
                                            年月
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
                                            查看明细
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
                                            备注
                                        </td>
                                        <td rowspan="2">
                                            修改时间
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
                                            <asp:Label ID="lb_yf" runat="server" Width="80px" Text='<%#Eval("KQ_DATE")%>'></asp:Label>
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
                                        <td>
                                        <asp:HyperLink ID="hlContract3" Target="_blank" ToolTip="查看" NavigateUrl='<%#"~/OM_Data/OM_KQTJdetail.aspx?stid="+Eval("KQ_ST_ID")+"&yearmonth="+Eval("KQ_DATE") %>'
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                    查看
                                </asp:HyperLink>
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
                                        <td align="right">
                                            <asp:TextBox ID="tb_beizhu" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("KQ_BEIZHU")%>' onfocus="javascript:setToolTipGet(this);"
                                                onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_xgtime" runat="server" Text='<%#Eval("KQ_XGTIME")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                
                                
                                <FooterTemplate>
                                    <tr>
                                        <td colspan="7" align="right">
                                        合计:
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbKQ_CHUQINhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_GNCChj" runat="server">
                                            <asp:Label ID="lbKQ_GNCChj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_GWCChj" runat="server">
                                            <asp:Label ID="lbKQ_GWCChj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_BINGJhj" runat="server">
                                            <asp:Label ID="lbKQ_BINGJhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_SHIJhj" runat="server">
                                            <asp:Label ID="lbKQ_SHIJhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_KUANGGhj" runat="server">
                                            <asp:Label ID="lbKQ_KUANGGhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_DAOXIUhj" runat="server">
                                            <asp:Label ID="lbKQ_DAOXIUhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_CHANJIAhj" runat="server">
                                            <asp:Label ID="lbKQ_CHANJIAhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_PEICHANhj" runat="server">
                                            <asp:Label ID="lbKQ_PEICHANhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_HUNJIAhj" runat="server">
                                            <asp:Label ID="lbKQ_HUNJIAhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_SANGJIAhj" runat="server">
                                            <asp:Label ID="lbKQ_SANGJIAhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_GONGShj" runat="server">
                                            <asp:Label ID="lbKQ_GONGShj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_NIANXhj" runat="server">
                                            <asp:Label ID="lbKQ_NIANXhj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_BEIYONG1hj" runat="server">
                                            <asp:Label ID="lbKQ_BEIYONG1hj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_BEIYONG2hj" runat="server">
                                            <asp:Label ID="lbKQ_BEIYONG2hj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdKQ_BEIYONG3hj" runat="server">
                                            <asp:Label ID="lbKQ_BEIYONG3hj" runat="server"></asp:Label>
                                        </td>
                                        
                                        <td align="center" id="tdBeiyong41" runat="server">
                                            <asp:Label ID="lbKQ_BEIYONG4hj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdBeiyong51" runat="server">
                                            <asp:Label ID="lbKQ_BEIYONG5hj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" id="tdBeiyong61" runat="server">
                                            <asp:Label ID="lbKQ_BEIYONG6hj" runat="server"></asp:Label>
                                        </td>
                                        
                                        <td align="center" id="tdKQ_QTJIAhj" runat="server">
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
                                        
                                    </tr>
                                </FooterTemplate>
                                
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="palNoData1" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                            没有记录!<br />
                            <br />
                        </asp:Panel>
                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                    </div>
                </div>
            </div>
            
            
            
            
            
            <div class="box-wrapper">
                <div class="box-outer">
                    <div id="divnianjia" runat="server" style="width: 100%; height: auto; overflow: scroll; display: block;" visible="false">
                        <table id="tabnianjia" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                            border="1" width="100%">
                            <asp:Repeater ID="rptKQTJ2" runat="server" OnItemDataBound="rptKQTJ2_DataBound">
                                <HeaderTemplate>
                                    <tr align="center" style="background-color: #B9D3EE;">
                                        <td>
                                            序号
                                        </td>
                                        <td>
                                            年月
                                        </td>
                                        <td>
                                            工号
                                        </td>
                                        <td>
                                            姓名
                                        </td>
                                        <td>
                                            部门
                                        </td>
                                        <td>
                                            班组
                                        </td>
                                        <td>
                                            年休假天数
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);">
                                        <td>
                                            <asp:Label runat="server" ID="lbKQ_ST_IDnj" Visible="false" Text='<%#Eval("KQ_ST_ID")%>'></asp:Label>
                                            <asp:CheckBox ID="CKBOX_SELECTnj" CssClass="checkBoxCss" runat="server" Checked="false" Onclick="checkme(this)" />
                                            <asp:Label ID="LineNumbernj" runat="server" Text='<%#Container.ItemIndex+1+(Convert.ToDouble(UCPaging2.CurrentPage)-1)*(Convert.ToDouble(UCPaging2.PageSize))%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_yfnj" runat="server" Width="80px" Text='<%#Eval("KQ_DATE")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_ghnj" runat="server" Width="90px" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_namenj" runat="server" Width="50px" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_bmnj" runat="server" Width="90px" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_bznj" runat="server" Text='<%#Eval("ST_DEPID1")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="right">
                                            <asp:TextBox ID="tb_nianxnj" runat="server" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("KQ_NIANX")%>'></asp:TextBox>
                                        </td>

                                    </tr>
                                </ItemTemplate>
                                
                                
                                <FooterTemplate>
                                    <tr>
                                        <td colspan="6" align="right">
                                        合计:
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbnjKQ_NIANXhj" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </FooterTemplate>
                                
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="palNoData2" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                            没有记录!<br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <uc1:UCPaging ID="UCPaging2" runat="server" />
</asp:Content>
