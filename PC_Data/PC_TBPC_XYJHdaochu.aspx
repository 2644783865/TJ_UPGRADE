<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_TBPC_XYJHdaochu.aspx.cs"
    Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_XYJHdaochu" MasterPageFile="~/Masters/PopupBase.Master"
    Title="批量导出" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
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
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <div>
        <table width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
            <tr>
                <td colspan="8" align="center" style="font-size: larger; color: Red;">
                    需用计划的批量导出
                </td>
            </tr>
            <tr>
                <td align="right">
                    批号：
                </td>
                <td>
                    <asp:TextBox ID="tb_pihao" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    物料编码：
                </td>
                <td>
                    <asp:TextBox ID="tb_marid" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    开始日期：
                </td>
                <td>
                    <input type="text" id="txtStartTime" runat="server" onclick="setday(this);" readonly="readonly" />
                </td>
                <td align="right">
                    结束日期：
                </td>
                <td>
                    <input type="text" id="txtEndTime" runat="server" onclick="setday(this);" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <%--<td align="right">
        项目：
    </td>
    <td >
        <asp:TextBox ID="tb_pj" runat="server" OnTextChanged="tb_pj_Textchanged" AutoPostBack="true"></asp:TextBox>
        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="tb_pj"
           ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
           ServiceMethod="GetPJNAME" FirstRowSelected="true"  CompletionListCssClass="autocomplete_completionListElement"
         CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
        </cc1:AutoCompleteExtender>
    </td>
    <td align="right">
        工程：
    </td>
    <td>
        <asp:TextBox ID="tb_eng" runat="server" OnTextChanged="tb_eng_Textchanged" AutoPostBack="true"></asp:TextBox>
        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="tb_eng"
           ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="20" MinimumPrefixLength="1" CompletionInterval="10"
           ServiceMethod="GetENGNAME" FirstRowSelected="true"  CompletionListCssClass="autocomplete_completionListElement"
          CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
        </cc1:AutoCompleteExtender>
    </td>--%>
                <td align="right">
                    图号：
                </td>
                <td>
                    <asp:TextBox ID="tb_th" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    物料类型:
                </td>
                <td>
                    <asp:TextBox ID="tb_shape" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    名称：
                </td>
                <td>
                    <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    材质：
                </td>
                <td>
                    <asp:TextBox ID="tb_cz" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    规格：
                </td>
                <td>
                    <asp:TextBox ID="tb_gg" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    国标：
                </td>
                <td>
                    <asp:TextBox ID="tb_gb" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    申请部门：
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList3" runat="server">
                        <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                        <asp:ListItem Text="公司领导" Value="01"></asp:ListItem>
                        <asp:ListItem Text="综合办公室" Value="02"></asp:ListItem>
                        <asp:ListItem Text="技术部" Value="03"></asp:ListItem>
                        <asp:ListItem Text="质量部" Value="12"></asp:ListItem>
                        <asp:ListItem Text="生产管理部" Value="04"></asp:ListItem>
                        <asp:ListItem Text="采购部" Value="05"></asp:ListItem>
                        <asp:ListItem Text="财务部" Value="06"></asp:ListItem>
                        <asp:ListItem Text="市场部" Value="07"></asp:ListItem>
                        <asp:ListItem Text="机加工车间" Value="08"></asp:ListItem>
                        <asp:ListItem Text="结构车间" Value="09"></asp:ListItem>
                        <asp:ListItem Text="安全环保部" Value="10"></asp:ListItem>
                        <asp:ListItem Text="工程师办公室" Value="11"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right">
                    申请人：
                </td>
                <td>
                    <asp:TextBox ID="tb_sqr" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    状态：
                </td>
                <td colspan="3">
                    <asp:RadioButton ID="rad_quanbu" runat="server" Text="全部" TextAlign="Right" GroupName="select"
                        Checked="true" />&nbsp; &nbsp;
                    <asp:RadioButton ID="rad_weidaohuo" runat="server" Text="未下推" TextAlign="Right" GroupName="select" />&nbsp;
                    &nbsp;
                    <asp:RadioButton ID="rad_bfdaohuo" runat="server" Text="已下推" TextAlign="Right" GroupName="select" />&nbsp;
                    &nbsp;
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr align="center">
                <td>
                    <asp:Button ID="btn_daochu" runat="server" Text="导出" OnClick="btn_daochu_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
