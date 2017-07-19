<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_YOUQI.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_YOUQI"
    MasterPageFile="~/Masters/RightCotentMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    油漆涂装细化方案
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
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
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td style="width: 12%">
                            <b>涂装方案查看</b>
                        </td>
                        <td>
                            <asp:CheckBox ID="cb_myjob" runat="server" Text="我的任务" CssClass="checkBoxCss" AutoPostBack="true"
                                OnCheckedChanged="cb_myjob_occ" />
                        </td>
                        <td>
                            批号查询：
                            <asp:TextBox ID="txt_psid" runat="server"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txt_psid"
                                ServicePath="~/PM_Data/PM_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="2"
                                CompletionInterval="10" ServiceMethod="GetPjID" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </cc1:AutoCompleteExtender>
                            按项目名称查询：
                            <asp:TextBox ID="txt_pjname" runat="server"></asp:TextBox>
                            <asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_OnClick" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnexport" runat="server" Text="导 出" OnClick="btnexport_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="NoDataPanel" runat="server">
                没有记录!</asp:Panel>
            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="gridview_OnDataBound">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="cb_1" runat="server" CssClass="checkBoxCss" />
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            <asp:Label ID="PS_LOOKSTATUS" runat="server" Visible="false" Text='<%#Eval("PS_LOOKSTATUS")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="批号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="PS_ID" runat="server" Text='<%#Eval("PS_ID")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PS_PJNAME" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="PS_ENGNAME" HeaderText="工程名称" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="MTA_DUY" HeaderText="调度员" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <%--<asp:BoundField DataField="PS_SUBMITTM" HeaderText="提交日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />--%>
                    <asp:BoundField DataField="PS_ADATE" HeaderText="下推日期" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <asp:TemplateField HeaderText="详细信息" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Button ID="lbtn_look" runat="server" Text="点击查看" Font-Underline="true" CommandArgument='<%#Eval("PS_ID")%>'
                                OnClick="lbtn_look_oc" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
