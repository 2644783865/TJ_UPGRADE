<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_GJJ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GJJ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    公积金
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
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>姓名：</strong><asp:TextBox
                        ID="txtname" ForeColor="Gray" runat="server" onfocus="DefaultTextOnFocus(this);"
                        onblur="DefaultTextOnBlur(this);" Width="120px"></asp:TextBox>
                    &nbsp;
                    
                    &nbsp;
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
                    <asp:HyperLink ID="hlUpdate" CssClass="link" runat="server" NavigateUrl="OM_GJJBLEDIT.aspx">
                        <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                            ImageAlign="AbsMiddle" runat="server" />
                        修改缴费比例
                    </asp:HyperLink>
                    &nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="hlBLXGJL" CssClass="link" runat="server" NavigateUrl="OM_GJJBLJL.aspx">
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
                                年月份
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
                                身份证号
                            </td>
                            <td>
                                缴存基数
                            </td>
                            <td>
                                合计（单位+个人）
                            </td>
                            <td>
                                单位
                            </td>
                            <td>
                                个人
                            </td>
                            <td>
                                合计（补缴）
                            </td>
                            <td>
                                单位（补缴）
                            </td>
                            <td>
                                个人（补缴）
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
                                <asp:Label ID="lb_stid" runat="server" Text='<%#Eval("GJ_STID")%>' Visible="false"></asp:Label>
                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                    Onclick="checkme(this)" />
                                <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lb_date" runat="server" Text='<%#Eval("GJ_DATE")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lb_gh" runat="server" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lb_name" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="ST_CONTR" runat="server" Text='<%#Eval("ST_CONTR")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="DEP_NAME" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="ST_IDCARD" runat="server" Text='<%#Eval("ST_IDCARD")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tb_jcjs" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("GJ_JCJS")%>'></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tb_hj" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("GJ_HJ").ToString()=="0"?"":Eval("GJ_HJ").ToString()=="0.00"?"":Eval("GJ_HJ").ToString()=="0.0000"?"":Eval("GJ_HJ")%>'></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tb_dw" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("GJ_DW").ToString()=="0"?"":Eval("GJ_DW").ToString()=="0.00"?"":Eval("GJ_DW").ToString()=="0.0000"?"":Eval("GJ_DW")%>'></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tb_gr" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("GJ_GR").ToString()=="0"?"":Eval("GJ_GR").ToString()=="0.00"?"":Eval("GJ_GR").ToString()=="0.0000"?"":Eval("GJ_GR")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tb_hjb" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("GJ_HJB").ToString()=="0"?"":Eval("GJ_HJB").ToString()=="0.00"?"":Eval("GJ_HJB").ToString()=="0.0000"?"":Eval("GJ_HJB")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tb_dwb" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("GJ_DWB").ToString()=="0"?"":Eval("GJ_DWB").ToString()=="0.00"?"":Eval("GJ_DWB").ToString()=="0.0000"?"":Eval("GJ_DWB")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tb_grb" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Yellow" Text='<%#Eval("GJ_GRB").ToString()=="0"?"":Eval("GJ_GRB").ToString()=="0.00"?"":Eval("GJ_GRB").ToString()=="0.0000"?"":Eval("GJ_GRB")%>'
                                    onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:TextBox ID="tb_bz" runat="server" align="center" Width="70px" BorderStyle="None"
                                    BackColor="Transparent" Text='<%#Eval("GJ_BZ")%>' onfocus="javascript:setToolTipGet(this);"
                                    onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td colspan="8" align="right">
                                合计:
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_GJ_HJhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_GJ_DWhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_GJ_GRhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_GJ_HJBhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_GJ_DWBhj" BackColor="Yellow" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_GJ_GRBhj" BackColor="Yellow" runat="server"></asp:Label>
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
