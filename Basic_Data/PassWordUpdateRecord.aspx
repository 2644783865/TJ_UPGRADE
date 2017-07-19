<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PassWordUpdateRecord.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.PassWordUpdateRecord"
    Title="基础数据" %>

<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    密码修改记录
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
            width: 100px !important;
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
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="../PC_Data/PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />

    <script src="../PC_Data/PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
    <div class="box-wrapper">
        <div class="box-outer">
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp; 姓名：<asp:TextBox ID="txtstName" runat="server" Width="80px"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtstName"
                            ServicePath="Basic_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                            CompletionInterval="10" ServiceMethod="GetNAME" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                        </asp:AutoCompleteExtender>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;修改时间从:
                        &nbsp;&nbsp;<asp:TextBox runat="server" ID="txtQsrq" class="easyui-datebox" onfocus="this.blur()"
                            Width="100px" Height="18px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp; 到:&nbsp;&nbsp;<asp:TextBox
                                runat="server" ID="txtJzrq" class="easyui-datebox" onfocus="this.blur()" Width="100px"
                                Height="18px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_Search" runat="server" Text="查询" OnClick="btn_Search_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btn_Reset" runat="server" Text="重置" OnClick="btn_Reset_Click" />
                    </td>
                    <td>
                        每页：<asp:DropDownList ID="ddlRowCount" runat="server" Width="45px" AutoPostBack="true"
                            OnSelectedIndexChanged="Query">
                            <asp:ListItem Text="30" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="60" Value="1"></asp:ListItem>
                            <asp:ListItem Text="90" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div align="center" id="div_statistcs" style="width: 100%; height: auto; overflow: scroll;
                display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptRecord" runat="server">
                        <HeaderTemplate>
                            <tr style="background-color: #B9D3EE;" height="30px">
                                <td align="center">
                                    <strong>序号</strong>
                                </td>
                                <td align="center">
                                    <strong>部门</strong>
                                </td>
                                <td align="center">
                                    <strong>姓名</strong>
                                </td>
                                <td align="center">
                                    <strong>修改时间</strong>
                                </td>
                                <td align="center">
                                    <strong>旧密码</strong>
                                </td>
                                <td align="center">
                                    <strong>新密码</strong>
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" height="30px">
                                <td>
                                    <asp:Label runat="server" ID="lbHT_ID" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblXuHao" runat="server" Text=""></asp:Label>
                                </td>
                                <td id="td1" runat="server" align="center">
                                    <%#Eval("depName")%>
                                </td>
                                <td id="td2" runat="server" align="center">
                                    <%#Eval("stName")%>
                                </td>
                                <td id="td_editTime" runat="server" align="center">
                                    <%#Eval("editTime")%>
                                </td>
                                <td id="td_oldPassword" runat="server" align="center">
                                    <%#Eval("oldPassword")%>
                                </td>
                                <td id="td_newPassword" runat="server" align="center">
                                    <%#Eval("newPassword")%>
                                </td>
                            </tr>
                        </ItemTemplate>
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
</asp:Content>
