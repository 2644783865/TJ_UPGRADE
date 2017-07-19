<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_XUNISHBX.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_XUNISHBX" Title="无标题页" %>

<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    重机虚拟户社险
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <link href="FixTable.css" rel="stylesheet" type="text/css" />

    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script>

    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>

    <script language="javascript" type="text/javascript">
$(function(){
$("#Checkbox2").click(function(){
if($("#Checkbox2").attr("checked")){
 $("#tab input[type=checkbox]").attr("checked","true");
}
else{
 $("#tab input[type=checkbox]").removeAttr("checked");
}
});})//jquery的写法，先声明一个函数，然后捕捉触发事件的对象，触发该对象时执行的事件（函数），遍历某些特定的控件，判断对象是否触发，执行事件；


function viewCondition() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'block';
        }
    </script>

    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    全选/取消<input id="Checkbox2" type="checkbox" />
                </td>
                <td style="width: 23%;">
                    <strong>时间：</strong>
                    <asp:DropDownList ID="dplYear" runat="server">
                    </asp:DropDownList>
                    &nbsp;年&nbsp;
                    <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplMoth_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;月&nbsp;
                </td>
                <td style="width: 35%;">
                    <strong>部门：</strong>&nbsp;
                    <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                        OnSelectedIndexChanged="dplbm_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp; <strong>姓名：</strong><asp:TextBox ID="txtname" ForeColor="Gray" runat="server"
                        onfocus="DefaultTextOnFocus(this);" onblur="DefaultTextOnBlur(this);" Width="120px"></asp:TextBox>
                    &nbsp;&nbsp;
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" />
                    &nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" Width="130px" runat="server" ToolTip="导 入" />&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnimport" Text="导入" OnClientClick="viewCondition()" />
                    <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnimport"
                        PopupControlID="PanelCondition" Drag="True" Enabled="True" DynamicServicePath=""
                        Y="80" X="900">
                    </asp:ModalPopupExtender>
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;&nbsp;&nbsp; <strong>开始年月：</strong><asp:TextBox ID="tb_CXstarttime" Width="120px"
                        runat="server"></asp:TextBox>
                    <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                        ID="CalendarExtender2" runat="server" Format="yyyy-MM" TargetControlID="tb_CXstarttime">
                    </asp:CalendarExtender>
                    &nbsp;&nbsp; <strong>结束年月：</strong><asp:TextBox ID="tb_CXendtime" Width="120px" runat="server"></asp:TextBox>
                    <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                        ID="CalendarExtender3" runat="server" Format="yyyy-MM" TargetControlID="tb_CXendtime">
                    </asp:CalendarExtender>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click"></asp:Button>
                    &nbsp;&nbsp;&nbsp;
                </td>
                <td align="right">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnexport" runat="server" Text="导出"
                        OnClick="btnexport_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:Panel ID="PanelCondition" runat="server" Width="300px" Style="display: none">
            <table width="90%" style="background-color: #CCCCFF; border: solid 1px black;">
                <tr>
                    <td colspan="8" align="center">
                        <asp:Button ID="QueryButton" runat="server" OnClick="btnimport_Click" Text="确认" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnClose" runat="server" OnClick="btnqx_import_Click" Text="取消" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="message" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>开始年月：</strong><asp:TextBox ID="tb_StartTime" Width="120px" runat="server"></asp:TextBox>
                        <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                            ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM" TargetControlID="tb_StartTime">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>结束年月：</strong><asp:TextBox ID="tb_EndTime" Width="120px" runat="server"></asp:TextBox>
                        <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                            ID="CalendarExtender1" runat="server" Format="yyyy-MM" TargetControlID="tb_EndTime">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div class="box-outer">
        <div style="height: 475px; overflow: auto; width: 100%">
            <table id="tab" class="nowrap cptable fullwidth" align="center">
                <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_itemdatabind">
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
                                一级部门
                            </td>
                            <td>
                                二级部门
                            </td>
                            <td>
                                基数
                            </td>
                            <td>
                                户口类型
                            </td>
                            <td>
                                企业养老保险（单位）
                            </td>
                            <td>
                                失业保险（单位）
                            </td>
                            <td>
                                基本医疗保险门诊大额<br />
                                医疗（单位）
                            </td>
                            <td>
                                工伤保险（单位）
                            </td>
                            <td>
                                生育保险（单位）
                            </td>
                            <td>
                                企业养老保险（单位<br />
                                补交）
                            </td>
                            <td>
                                失业保险（单位补交）
                            </td>
                            <td>
                                基本医疗保险门诊大额<br />
                                医疗（单位补缴）
                            </td>
                            <td>
                                工伤保险（单位补缴）
                            </td>
                            <td>
                                生育保险（单位补缴）
                            </td>
                            <td>
                                <strong>单位合计</strong>
                            </td>
                            <td>
                                企业养老保险（个人）
                            </td>
                            <td>
                                失业保险（个人）
                            </td>
                            <td>
                                基本医疗保险门诊大额<br />
                                医疗（个人）
                            </td>
                            <td>
                                企业养老保险（个人补交）
                            </td>
                            <td>
                                失业保险（个人补交）
                            </td>
                            <td>
                                基本医疗保险门诊大额<br />
                                医疗（个人补缴）
                            </td>
                            <td>
                                在职大额医疗救助（个人）
                            </td>
                            <td>
                                <strong>个人合计</strong>
                            </td>
                            <td>
                                其他
                            </td>
                            <td>
                                <strong>小计</strong>
                            </td>
                            <td>
                                时间区间
                            </td>
                            <td>
                                备注
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id="row" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)"
                            onclick="javascript:change(this);" ondblclick="javascript:changeback(this);">
                            <td>
                                <asp:Label ID="lb_stid" runat="server" Text='<%#Eval("SH_STID")%>' Visible="false"></asp:Label>
                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                    Onclick="checkme(this)" />
                                <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                            </td>
                            <td id="Td1" runat="server" align="center">
                                <asp:Label ID="lbdate" runat="server" align="center" Text='<%#Eval("SH_DATE")%>'></asp:Label>
                            </td>
                            <td id="Td2" runat="server" align="center">
                                <asp:Label ID="lb_gh" runat="server" align="center" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                            </td>
                            <td id="Td3" runat="server" align="center">
                                <asp:Label ID="lb_name" runat="server" align="center" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                            </td>
                            <td id="Td4" runat="server" align="center">
                                <asp:Label ID="tb_BM1" runat="server" align="center" Text='<%#Eval("ST_CONTR")%>'></asp:Label>
                            </td>
                            <td id="Td5" runat="server" align="center">
                                <asp:Label ID="tb_BM2" runat="server" align="center" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                            </td>
                            <td id="Td6" runat="server" align="center">
                                <asp:TextBox ID="tb_JS" runat="server" align="center" Width="110px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_JS")%>'></asp:TextBox>
                            </td>
                            <td id="Td7" runat="server" align="center">
                                <asp:Label ID="tb_TYPE" runat="server" align="center" Text='<%#Eval("ST_REGIST")%>'></asp:Label>
                            </td>
                            <td id="Td8" runat="server" align="center">
                                <asp:TextBox ID="tb_qyyldw" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_QYYLDW").ToString()=="0"?"":Eval("SH_QYYLDW").ToString()=="0.00"?"":Eval("SH_QYYLDW").ToString()=="0.0000"?"":Eval("SH_QYYLDW")%>'></asp:TextBox>
                            </td>
                            <td id="Td9" runat="server" align="center">
                                <asp:TextBox ID="tb_sybxdw" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_SYBXDW").ToString()=="0"?"":Eval("SH_SYBXDW").ToString()=="0.00"?"":Eval("SH_SYBXDW").ToString()=="0.0000"?"":Eval("SH_SYBXDW")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td10" runat="server" align="center">
                                <asp:TextBox ID="tb_jbyldw" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_JBYLDW").ToString()=="0"?"":Eval("SH_JBYLDW").ToString()=="0.00"?"":Eval("SH_JBYLDW").ToString()=="0.0000"?"":Eval("SH_JBYLDW")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td11" runat="server" align="center">
                                <asp:TextBox ID="tb_gsdw" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_GSDW").ToString()=="0"?"":Eval("SH_GSDW").ToString()=="0.00"?"":Eval("SH_GSDW").ToString()=="0.0000"?"":Eval("SH_GSDW")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td12" runat="server" align="center">
                                <asp:TextBox ID="tb_sydw" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_SYDW").ToString()=="0"?"":Eval("SH_SYDW").ToString()=="0.00"?"":Eval("SH_SYDW").ToString()=="0.0000"?"":Eval("SH_SYDW")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td13" runat="server" align="center">
                                <asp:TextBox ID="tb_qyyldwb" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_QYYLDWB").ToString()=="0"?"":Eval("SH_QYYLDWB").ToString()=="0.00"?"":Eval("SH_QYYLDWB").ToString()=="0.0000"?"":Eval("SH_QYYLDWB")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td14" runat="server" align="center">
                                <asp:TextBox ID="tb_sybxdwb" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_SYBXDWB").ToString()=="0"?"":Eval("SH_SYBXDWB").ToString()=="0.00"?"":Eval("SH_SYBXDWB").ToString()=="0.0000"?"":Eval("SH_SYBXDWB")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td15" runat="server" align="center">
                                <asp:TextBox ID="tb_jbyldwb" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_JBYLDWB").ToString()=="0"?"":Eval("SH_JBYLDWB").ToString()=="0.00"?"":Eval("SH_JBYLDWB").ToString()=="0.0000"?"":Eval("SH_JBYLDWB")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td16" runat="server" align="center">
                                <asp:TextBox ID="tb_gsdwb" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_GSDWB").ToString()=="0"?"":Eval("SH_GSDWB").ToString()=="0.00"?"":Eval("SH_GSDWB").ToString()=="0.0000"?"":Eval("SH_GSDWB")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td17" runat="server" align="center">
                                <asp:TextBox ID="tb_sydwb" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_SYDWB").ToString()=="0"?"":Eval("SH_SYDWB").ToString()=="0.00"?"":Eval("SH_SYDWB").ToString()=="0.0000"?"":Eval("SH_SYDWB")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td18" runat="server" align="center">
                                <asp:Label ID="lb_dwbxhj" runat="server" Width="120px" align="center" Text='<%#Eval("SH_DWBXHJ").ToString()=="0"?"":Eval("SH_DWBXHJ").ToString()=="0.00"?"":Eval("SH_DWBXHJ").ToString()=="0.0000"?"":Eval("SH_DWBXHJ")%>'></asp:Label>
                            </td>
                            <td id="Td19" runat="server" align="center">
                                <asp:TextBox ID="tb_qyylgr" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("SH_QYYLGR").ToString()=="0"?"":Eval("SH_QYYLGR").ToString()=="0.00"?"":Eval("SH_QYYLGR").ToString()=="0.0000"?"":Eval("SH_QYYLGR")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td20" runat="server" align="center">
                                <asp:TextBox ID="tb_sybxgr" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("SH_SYBXGR").ToString()=="0"?"":Eval("SH_SYBXGR").ToString()=="0.00"?"":Eval("SH_SYBXGR").ToString()=="0.0000"?"":Eval("SH_SYBXGR")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td21" runat="server" align="center">
                                <asp:TextBox ID="tb_jbylgr" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("SH_JBYLGR").ToString()=="0"?"":Eval("SH_JBYLGR").ToString()=="0.00"?"":Eval("SH_JBYLGR").ToString()=="0.0000"?"":Eval("SH_JBYLGR")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td22" runat="server" align="center">
                                <asp:TextBox ID="tb_qyylgrb" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("SH_QYYLGRB").ToString()=="0"?"":Eval("SH_QYYLGRB").ToString()=="0.00"?"":Eval("SH_QYYLGRB").ToString()=="0.0000"?"":Eval("SH_QYYLGRB")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td23" runat="server" align="center">
                                <asp:TextBox ID="tb_sybxgrb" runat="server" Width="110px" align="center" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("SH_SYBXGRB").ToString()=="0"?"":Eval("SH_SYBXGRB").ToString()=="0.00"?"":Eval("SH_SYBXGRB").ToString()=="0.0000"?"":Eval("SH_SYBXGRB")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td24" runat="server" align="center">
                                <asp:TextBox ID="tb_jbylgrb" runat="server" Width="70px" align="center" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("SH_JBYLGRB").ToString()=="0"?"":Eval("SH_JBYLGRB").ToString()=="0.00"?"":Eval("SH_JBYLGRB").ToString()=="0.0000"?"":Eval("SH_JBYLGRB")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td25" runat="server" align="center">
                                <asp:TextBox ID="tb_deylgr" runat="server" Width="70px" align="center" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("SH_DEYLGR").ToString()=="0"?"":Eval("SH_DEYLGR").ToString()=="0.00"?"":Eval("SH_DEYLGR").ToString()=="0.0000"?"":Eval("SH_DEYLGR")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td26" runat="server" align="center">
                                <asp:Label ID="lb_grbxhj" runat="server" BackColor="Yellow" Width="110px" align="center"
                                    Text='<%#Eval("SH_GRBXHJ").ToString()=="0"?"":Eval("SH_GRBXHJ").ToString()=="0.00"?"":Eval("SH_GRBXHJ").ToString()=="0.0000"?"":Eval("SH_GRBXHJ")%>'></asp:Label>
                            </td>
                            <td id="Td27" runat="server" align="center">
                                <asp:TextBox ID="tb_shqt" runat="server" Width="70px" align="center" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_QT").ToString()=="0"?"":Eval("SH_QT").ToString()=="0.00"?"":Eval("SH_QT").ToString()=="0.0000"?"":Eval("SH_QT")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td id="Td28" runat="server" align="center">
                                <asp:Label ID="lb_grxj" runat="server" Width="110px" align="center" Text='<%#Eval("SH_GRXJ").ToString()=="0"?"":Eval("SH_GRXJ").ToString()=="0.00"?"":Eval("SH_GRXJ").ToString()=="0.0000"?"":Eval("SH_GRXJ")%>'></asp:Label>
                            </td>
                            <td id="Td29" runat="server" align="center">
                                <asp:Label ID="lb_sjqj" runat="server" align="center" Text='<%#Eval("SH_SJQJ")%>'></asp:Label>
                            </td>
                            <td id="Td30" runat="server" align="center">
                                <asp:TextBox ID="tb_note" runat="server" Width="120px" align="center" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("SH_NOTE")%>' onfocus="javascript:setToolTipGet(this);"
                                    ToolTip='<%#Eval("SH_NOTE")%>' onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td colspan="8" align="right">
                                合计:
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_qyyldwhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_sybxdwhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_jbyldwhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_gsdwhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_sydwhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_qyyldwbhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_sybxdwbhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_jbyldwbhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_gsdwbhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_sydwbhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_bxdwzj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_qyylgrhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_sybxgrhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_jbylgrhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_qyylgrbhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_sybxgrbhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_jbylgrbhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_deylgrhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_bxgrzj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_shqthj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_grxjhj" runat="server"></asp:Label>
                            </td>
                            <td>
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
        <table width="100%" id="table1">
            <tr>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;筛选结果：共<asp:Label ID="lbtotalnum" Font-Bold="true"
                        ForeColor="Red" runat="server" Text=""></asp:Label>条记录
                </td>
            </tr>
        </table>
    </div>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
