<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_Person_Holtzjl.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_Person_Holtzjl" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
年假调整记录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .show
        {
            display: block;
        }
        .completionListElement
        {
            margin: 0px;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 150px !important;
            background-color: White;
            font-size: small;
        }
        .listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            color: windowtext;
            font-size: small;
        }
        .highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            font-size: small;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div>
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                style="cursor: pointer">
                <asp:Repeater ID="rptHoliday" runat="server">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor" style="height: 40px; border: solid 1px">
                            <td width="55px" style="border: solid 1px">
                                <strong>序号</strong>
                            </td>
                            <td width="100px" style="border: solid 1px">
                                <strong>姓名</strong>
                            </td>
                            <td width="100px" style="border: solid 1px">
                                <strong>工号</strong>
                            </td>
                            <td width="150px" style="border: solid 1px">
                                <strong>部门</strong>
                            </td>
                            <td style="border: solid 1px">
                                <strong>调整天数</strong>
                            </td>
                            <td style="border: solid 1px">
                                <strong>调整人</strong>
                            </td>
                            <td style="border: solid 1px">
                                <strong>调整时间</strong>
                            </td>
                            <td style="border: solid 1px">
                                <strong>调整原因</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                            <td>
                                <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                <asp:CheckBox ID="cbxNumber" runat="server" Visible="false"/>
                                <asp:Label ID="lbNJ_ST_ID" runat="server" Visible="false" Text='<%#Eval("TZJL_ST_ID")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbNJ_NAME" runat="server" Text='<%#Eval("TZJL_NAME") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbNJ_WORKNUMBER" runat="server" Text='<%#Eval("TZJL_WORKNUMBER") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbNJ_BUMEN" runat="server" Text='<%#Eval("TZJL_BUMEN") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbNJ_RUZHITIME" runat="server" Text='<%#Eval("TZJL_TZTS") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbNJ_TZTS" runat="server" Text='<%#Eval("TZJL_TZR") %>'></asp:TextBox>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lbNJ_YSY" runat="server" Text='<%#Eval("TZJL_TZTIME") %>'></asp:Label>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lbTZJL_TZREASON" runat="server" Text='<%#Eval("TZJL_TZREASON") %>'></asp:Label>
                                &nbsp;
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            </div>
            <asp:Panel ID="palNoData" runat="server" HorizontalAlign="Center" ForeColor="Red">
                没有记录!</asp:Panel>
            <asp:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
