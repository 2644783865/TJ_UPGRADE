<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_LDBX.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_LDBX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    派遣人员保险公积金
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <link href="FixTable.css" rel="stylesheet" type="text/css" />

    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script>

    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

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

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
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
                <td align="right">
                    <asp:HyperLink ID="hlUpdate" CssClass="link" runat="server" NavigateUrl="OM_LDBLEDIT.aspx">
                        <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                            ImageAlign="AbsMiddle" runat="server" />
                        修改缴费比例
                    </asp:HyperLink>
                    &nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="hlBLXGJL" CssClass="link" runat="server" NavigateUrl="OM_LDBLJL.aspx">
                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                            ImageAlign="AbsMiddle" runat="server" />
                        查看比例修改记录
                    </asp:HyperLink>
                    &nbsp;&nbsp;&nbsp;
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
                                部门
                            </td>
                            <td>
                                工号
                            </td>
                            <td>
                                姓名
                            </td>
                            <td>
                                年月份
                            </td>
                            <td>
                                保险缴存基数
                            </td>
                            <td>
                                公积金基数
                            </td>
                            <td>
                                单位：养老保险
                            </td>
                            <td>
                                单位：失业保险
                            </td>
                            <td>
                                单位：工伤保险
                            </td>
                            <td>
                                单位：生育保险
                            </td>
                            <td>
                                单位：基本医疗
                            </td>
                            <td>
                                单位：公积金
                            </td>
                            <td>
                                单位补缴
                            </td>
                            <td>
                                单位合计
                            </td>
                            <td>
                                个人：养老保险
                            </td>
                            <td>
                                个人：失业保险
                            </td>
                            <td>
                                个人：基本医疗
                            </td>
                            <td>
                                个人：医疗大额
                            </td>
                            <td>
                                个人保险合计
                            </td>
                            <td>
                                个人公积金
                            </td>
                            <td>
                                个人补缴
                            </td>
                            <td>
                                个人合计
                            </td>
                            <td>
                                招工费用
                            </td>
                            <td>
                                补缴利息
                            </td>
                            <td>
                                小计(单位+个人+招工)
                            </td>
                            <td>
                                时间区间
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id="row" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)"
                            onclick="javascript:change(this);" ondblclick="javascript:changeback(this);">
                            <td>
                                <asp:Label ID="lbLD_STID" runat="server" Text='<%#Eval("LD_STID")%>' Visible="false"></asp:Label>
                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                    Onclick="checkme(this)" />
                                <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbDEP_NAME" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbST_WORKNO" runat="server" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbST_NAME" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbLD_DATE" runat="server" Text='<%#Eval("LD_DATE")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_JFJS" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("LD_JFJS")%>'></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_GJJS" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("LD_GJJS")%>'></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_YLBXD" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("LD_YLBXD").ToString()=="0"?"":Eval("LD_YLBXD").ToString()=="0.00"?"":Eval("LD_YLBXD").ToString()=="0.0000"?"":Eval("LD_YLBXD")%>'></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_SYBXD" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("LD_SYBXD").ToString()=="0"?"":Eval("LD_SYBXD").ToString()=="0.00"?"":Eval("LD_SYBXD").ToString()=="0.0000"?"":Eval("LD_SYBXD")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_GSBXD" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("LD_GSBXD").ToString()=="0"?"":Eval("LD_GSBXD").ToString()=="0.00"?"":Eval("LD_GSBXD").ToString()=="0.0000"?"":Eval("LD_GSBXD")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_SYD" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("LD_SYD").ToString()=="0"?"":Eval("LD_SYD").ToString()=="0.00"?"":Eval("LD_SYD").ToString()=="0.0000"?"":Eval("LD_SYD")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_YLD" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("LD_YLD").ToString()=="0"?"":Eval("LD_YLD").ToString()=="0.00"?"":Eval("LD_YLD").ToString()=="0.0000"?"":Eval("LD_YLD")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_GJJD" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("LD_GJJD").ToString()=="0"?"":Eval("LD_GJJD").ToString()=="0.00"?"":Eval("LD_GJJD").ToString()=="0.0000"?"":Eval("LD_GJJD")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_DWB" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("LD_DWB").ToString()=="0"?"":Eval("LD_DWB").ToString()=="0.00"?"":Eval("LD_DWB").ToString()=="0.0000"?"":Eval("LD_DWB")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbLD_DWH" runat="server" Text='<%#Eval("LD_DWH").ToString()=="0"?"":Eval("LD_DWH").ToString()=="0.00"?"":Eval("LD_DWH").ToString()=="0.0000"?"":Eval("LD_DWH")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_YLGR" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("LD_YLGR").ToString()=="0"?"":Eval("LD_YLGR").ToString()=="0.00"?"":Eval("LD_YLGR").ToString()=="0.0000"?"":Eval("LD_YLGR")%>'></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_SYGR" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("LD_SYGR").ToString()=="0"?"":Eval("LD_SYGR").ToString()=="0.00"?"":Eval("LD_SYGR").ToString()=="0.0000"?"":Eval("LD_SYGR")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_JBYLGR" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("LD_JBYLGR").ToString()=="0"?"":Eval("LD_JBYLGR").ToString()=="0.00"?"":Eval("LD_JBYLGR").ToString()=="0.0000"?"":Eval("LD_JBYLGR")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_YLDE" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("LD_YLDE").ToString()=="0"?"":Eval("LD_YLDE").ToString()=="0.00"?"":Eval("LD_YLDE").ToString()=="0.0000"?"":Eval("LD_YLDE")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbLD_BXGRH" runat="server" BackColor="Yellow" Text='<%#Eval("LD_BXGRH").ToString()=="0"?"":Eval("LD_BXGRH").ToString()=="0.00"?"":Eval("LD_BXGRH").ToString()=="0.0000"?"":Eval("LD_BXGRH")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_GJJGR" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("LD_GJJGR").ToString()=="0"?"":Eval("LD_GJJGR").ToString()=="0.00"?"":Eval("LD_GJJGR").ToString()=="0.0000"?"":Eval("LD_GJJGR")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_GRBJ" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("LD_GRBJ").ToString()=="0"?"":Eval("LD_GRBJ").ToString()=="0.00"?"":Eval("LD_GRBJ").ToString()=="0.0000"?"":Eval("LD_GRBJ")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbLD_HJGR" runat="server" BackColor="Yellow" Text='<%#Eval("LD_HJGR").ToString()=="0"?"":Eval("LD_HJGR").ToString()=="0.00"?"":Eval("LD_HJGR").ToString()=="0.0000"?"":Eval("LD_HJGR")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_ZGFY" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("LD_ZGFY").ToString()=="0"?"":Eval("LD_ZGFY").ToString()=="0.00"?"":Eval("LD_ZGFY").ToString()=="0.0000"?"":Eval("LD_ZGFY")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tbLD_BJLX" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("LD_BJLX").ToString()=="0"?"":Eval("LD_BJLX").ToString()=="0.00"?"":Eval("LD_BJLX").ToString()=="0.0000"?"":Eval("LD_BJLX")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbLD_ZJGR" runat="server" Text='<%#Eval("LD_ZJGR").ToString()=="0"?"":Eval("LD_ZJGR").ToString()=="0.00"?"":Eval("LD_ZJGR").ToString()=="0.0000"?"":Eval("LD_ZJGR")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbLD_SJQJ" runat="server" Text='<%#Eval("LD_SJQJ")%>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td colspan="7" align="right">
                                合计:
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_YLBXDhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_SYBXDhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_GSBXDhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_SYDhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_YLDhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_GJJDhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_DWBhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_DWHhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_YLGRhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_SYGRhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_JBYLGRhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_YLDEhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_BXGRHhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_GJJGRhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_GRBJhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_HJGRhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_ZGFYhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_BJLXhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_LD_ZJGRhj" runat="server"></asp:Label>
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
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblTip" runat="server" Text="提示:" Visible="false" Font-Bold="true"></asp:Label>
                <asp:Image ID="imgloading" runat="server" Visible="false" />
                <asp:Label ID="lblTip2" runat="server" Visible="false" Text="数据处理中，请稍后..."></asp:Label>
                <asp:Label ID="LabelDate" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
