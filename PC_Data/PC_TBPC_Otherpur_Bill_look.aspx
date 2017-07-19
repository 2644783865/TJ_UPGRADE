<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_TBPC_Otherpur_Bill_look.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.Master" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Otherpur_Bill_look" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="PcJs/xbjhidden.js" type="text/javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script language="javascript" type="text/javascript">
    function viewCondition()
    {
       document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
    }
    </script>

    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                &nbsp; &nbsp;物料类型：<asp:Label ID="lb_shape" runat="server" Text=""></asp:Label>
                            </td>
                            <td visible="false">
                                &nbsp; &nbsp;<%--是否变更减少：--%><asp:CheckBox ID="cb_bg" runat="server" Visible="false" />
                                <asp:CheckBox ID="chkiffast" runat="server" />是否加急物料
                            </td>
                            <td>
                                <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                                    Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                            </td>
                            <td align="right">
                                <asp:Label ID="lbl_spzt" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Button ID="btn_Audit" runat="server" Text="提交审批" OnClick="btn_Audit_Click" OnClientClick="javascript:return confirm('确认提交吗？');" />
                                &nbsp;
                                <asp:Button ID="btn_confirm" runat="server" Text="下推" OnClick="btn_confirm_Click"
                                    Visible="false" /><%--此按钮暂时不用，审核通过后自动下推--%>
                                &nbsp;
                                <asp:Button ID="btnqx" runat="server" OnClick="btnqx_Click" Text="取消计划" Visible="false"
                                    Enabled="false" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_copy" runat="server" Text="复制" OnClientClick="viewCondition()" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_ExportExcel" runat="server" Text="导出" OnClick="btn_ExportExcel_OnClick"
                                    Visible="false" />
                                &nbsp;&nbsp;
                                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btn_copy"
                                    PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                    Y="40" X="-100">
                                </asp:ModalPopupExtender>
                                <asp:HyperLink ID="hyl_back" runat="server" CssClass="hand" onclick="history.go(-1);">返回上一页</asp:HyperLink>
                                &nbsp; &nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCondition" runat="server" Width="50%" Style="display: none">
                        <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="50%" style="background-color: #CCCCFF; border: solid 1px black;">
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="复制" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            项目名称：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txb_pjnm" runat="server" onchange="changepj(this)"></asp:TextBox>
                                            <asp:TextBox ID="txb_pjid" runat="server" CssClass="hidden"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txb_pjnm"
                                                ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                                CompletionInterval="10" ServiceMethod="GetPJNAME" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            工程名称：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txb_engnm" runat="server" onchange="changeeng(this)"></asp:TextBox>
                                            <asp:TextBox ID="txb_engid" runat="server" CssClass="hidden"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txb_engnm"
                                                ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="20" MinimumPrefixLength="1"
                                                CompletionInterval="10" ServiceMethod="GetENGNAME" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            物料类型:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList1" runat="server">
                                                <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                                                <asp:ListItem Text="定尺板" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="非定尺板" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="型材" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="标(组装)" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="标(发运)" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="标(工艺)" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="协A" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="协B" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="电气电料" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="油漆" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="其他" Value="10"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <asp:TabContainer ID="tab_Add_caigou" runat="server" Width="100%" TabStripPlacement="Top"
            ActiveTabIndex="1">
            <asp:TabPanel ID="Tab_audit" runat="server" HeaderText="审 核" TabIndex="0">
                <ContentTemplate>
                    <div class="box-wrapper">
                        <div style="height: 6px" class="box_top">
                        </div>
                        <div class="box-outer">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_info" runat="server" Text="* 所有审核通过后将自动下推，请仔细检查！" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rblSHDJ" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblSHDJ_Changed">
                                            <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="二级审核" Value="2" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                        采购申请
                                        <asp:Image ID="ImageAUDIT" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="box-outer">
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <asp:Panel runat="server" ID="panZDR">
                                    <tr>
                                        <td>
                                            <strong>采购理由：</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:TextBox ID="txtPA_CGLY" runat="server" TextMode="MultiLine" Width="100%" Height="42px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pal_first" runat="server">
                                    <tr>
                                        <td>
                                            <table align="center" width="100%">
                                                <tr style="height: 25px">
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                        <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand">
                                                            <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                align="absmiddle" runat="server" />
                                                            选择
                                                        </asp:HyperLink>
                                                        <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                                            Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect1"
                                                            PopupControlID="pal_select1">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pal_select1" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                            <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                <tr>
                                                                    <td style='background-color: #A8B7EC; color: white;'>
                                                                        <b>选择审核人</b>
                                                                    </td>
                                                                    <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                        <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                            text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Panel ID="pal_select1_inner" runat="server">
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <asp:Button ID="btn_shr1" runat="server" Text="确 定" OnClick="btn_shr1_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rbl_first" RepeatColumns="2" runat="server" Height="20px">
                                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核时间
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                            Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pal_second" runat="server">
                                    <tr>
                                        <td>
                                            <table align="center" width="100%">
                                                <tr>
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                        <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand">
                                                            <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                align="absmiddle" runat="server" />
                                                            选择
                                                        </asp:HyperLink>
                                                        <asp:PopupControlExtender ID="PopupControlExtender2" CacheDynamicResults="false"
                                                            Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect2"
                                                            PopupControlID="pal_select2">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pal_select2" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                            <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                <tr>
                                                                    <td style='background-color: #A8B7EC; color: white;'>
                                                                        <b>选择审核人</b>
                                                                    </td>
                                                                    <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                        <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                            text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Panel ID="pal_select2_inner" runat="server">
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <asp:Button ID="btn_shr2" runat="server" Text="确 定" OnClick="btn_shr2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rbl_second" RepeatColumns="2" runat="server" Height="20px">
                                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核时间
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="second_opinion" runat="server" Height="42px" TextMode="MultiLine"
                                                            Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pal_third" runat="server">
                                    <tr>
                                        <td>
                                            <table align="center" width="100%">
                                                <tr>
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                        <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand">
                                                            <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                align="absmiddle" runat="server" />
                                                            选择
                                                        </asp:HyperLink>
                                                        <asp:PopupControlExtender ID="PopupControlExtender3" CacheDynamicResults="false"
                                                            Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect3"
                                                            PopupControlID="pal_select3">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pal_select3" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                            <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                <tr>
                                                                    <td style='background-color: #A8B7EC; color: white;'>
                                                                        <b>选择审核人</b>
                                                                    </td>
                                                                    <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                        <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                            text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Panel ID="pal_select3_inner" runat="server">
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <asp:Button ID="btn_shr3" runat="server" Text="确 定" OnClick="btn_shr3_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rbl_third" RepeatColumns="2" runat="server" Height="20px">
                                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核时间
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="third_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                            Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="Tab_cgd" runat="server" HeaderText="采购单" TabIndex="1">
                <ContentTemplate>
                    <div class="box-wrapper">
                        <div class="box-wrapper">
                            <%-- <div style="height: 6px" class="box_top">
                            </div>--%>
                            <div class="box-outer">
                                <table width="100%">
                                    <tr>
                                        <td style="font-size: x-large; text-align: center;" colspan="4">
                                            采购申请单
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 33%;" align="left">
                                            &nbsp;&nbsp;&nbsp;使&nbsp;&nbsp;用&nbsp;&nbsp;部&nbsp;&nbsp;门：
                                            <asp:TextBox ID="tb_dep" runat="server" Text="" Enabled="false"></asp:TextBox>
                                            <asp:TextBox ID="tb_depid" runat="server" Text="" Visible="false"></asp:TextBox>
                                        </td>
                                        <td style="width: 33%;" align="left">
                                            &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：
                                            <asp:TextBox ID="Tb_shijian" runat="server" Text="" Enabled="false" Width="150px"></asp:TextBox>
                                        </td>
                                        <td style="width: 34%;" align="left">
                                            &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：
                                            <asp:TextBox ID="tb_pid" runat="server" Text="" Enabled="false" Width="200px"></asp:TextBox>
                                            <asp:TextBox ID="lb_state" runat="server" Visible="false" Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 33%;" align="left">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;任务号：
                                            <asp:TextBox ID="tb_pjinfo" runat="server" Enabled="false" Text=""></asp:TextBox>
                                            <asp:TextBox ID="tb_pj" runat="server" Visible="false" Text=""></asp:TextBox>
                                            <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                                        </td>
                                        <td style="width: 33%;" align="left">
                                            &nbsp;&nbsp;&nbsp;项目：
                                            <asp:TextBox ID="tb_enginfo" runat="server" Enabled="false" Text="" Width="150px"></asp:TextBox>
                                            <asp:TextBox ID="tb_eng" runat="server" Visible="false" Text=""></asp:TextBox>
                                            <asp:TextBox ID="tb_engid" runat="server" Visible="false" Text=""></asp:TextBox>
                                        </td>
                                        <td style="width: 34%;" align="left">
                                            &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：
                                            <asp:TextBox ID="tb_note" runat="server" Text="" Width="200px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div style="border: 1px solid #000000;">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div style="border: 1px solid #000000; height: 330px">
                                                <div class="cpbox5 xscroll">
                                                    <table id="tab" class="nowrap cptable fullwidth">
                                                        <asp:Repeater ID="tbpc_otherpurbill_lookRepeater" runat="server">
                                                            <HeaderTemplate>
                                                                <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                                    <td>
                                                                        <strong>行号</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>计划号</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>材料ID</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>名称</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>规格</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>材质</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>国标</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>图号/标识号</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>材料长度</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>材料宽度</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>数量</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>单位</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>辅助数量</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>辅助单位</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>时间要求</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>备注</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>状态</strong>
                                                                    </td>
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                        <asp:CheckBox ID="CHK" CssClass="checkBoxCss" BorderStyle="None" runat="server" Checked="false"
                                                                            onclick="checkme(this)"></asp:CheckBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_PTCODE" runat="server" Text='<%#Eval("MP_PTCODE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_MARID" runat="server" Text='<%#Eval("MP_MARID")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_MARNAME" runat="server" Text='<%#Eval("MP_MARNAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_MARNORM" runat="server" Text='<%#Eval("MP_MARNORM")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_MARTERIAL" runat="server" Text='<%#Eval("MP_MARTERIAL")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_MARGUOBIAO" runat="server" Text='<%#Eval("MP_MARGUOBIAO")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_TUHAO" runat="server" Text='<%#Eval("MP_TUHAO")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_LENGTH" runat="server" Text='<%#Eval("MP_LENGTH")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_WIDTH" runat="server" Text='<%#Eval("MP_WIDTH")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_NUMBER" runat="server" Text='<%#Eval("MP_NUMBER")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_NUNIT" runat="server" Text='<%#Eval("MP_NUNIT")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_FZNUM" runat="server" Text='<%#Eval("MP_FZNUM")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_FZNUNIT" runat="server" Text='<%#Eval("MP_FZNUNIT")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_TIMERQ" runat="server" Text='<%#Eval("MP_TIMERQ")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_NOTE" runat="server" Text='<%#Eval("MP_NOTE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MP_STATE" runat="server" Text='<%#Eval("MP_STATE")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="MP_STATETEXT" runat="server" Text='<%#get_pr_state(Eval("MP_STATE").ToString())%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <tr>
                                                            <td colspan="17" align="center">
                                                                <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                                    没有数据！</asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                负责人:<asp:TextBox ID="Tb_fuziren" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="Tb_fuzirenid" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                申请人:<asp:TextBox ID="Tb_shenqingren" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="Tb_shenqingrenid" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                制单人:<asp:TextBox ID="tb_executor" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="tb_executorid" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
</asp:Content>
