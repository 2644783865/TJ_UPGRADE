<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="PM_Xie_Management.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Xie_Management" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    生产外协信息表</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
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
    <div class="box-inner">
        <div class="box_right">
            <table style="width: 100%; height: 24px">
                <tr>
                    <td style="width: 8%;" align="right">
                        生产外协状态：
                    </td>
                    <td style="width: 10%">
                        <asp:RadioButtonList ID="rblstate" RepeatColumns="2" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="rblstate_SelectedIndexChanged">
                            <asp:ListItem Text="正常" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="变更" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="left" style="width: 18%;">
                        <asp:RadioButtonList ID="rblproplan" RepeatColumns="3" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="rblproplan_SelectedIndexChanged">
                            <asp:ListItem Text="未制定" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="已制定" Value="1"></asp:ListItem>
                            <asp:ListItem Text="无外协" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: left; width: 10%">
                        <asp:CheckBox ID="cb_myjob" runat="server" AutoPostBack="true" OnCheckedChanged="cb_myjob_OnCheckedChanged"
                            Text="我的任务" Checked="true" />
                    </td>
                    <td align="center" style="width: 50%;">
                        合同名称：
                        <asp:DropDownList ID="ddlpjname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpjname_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 设备名称：<asp:DropDownList ID="ddlengname" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlengname_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <%--    <td style="text-align:left ;width:20px">
                    <asp:Button ID="btn_AllQuery" runat="server" Text="综合查询" BorderStyle="Groove" BorderColor="LightGray" />
                    </td>--%>
                    <td style="text-align: right; width: 10%">
                        <asp:Button ID="btn_nowx" runat="server" Text="不制定外协" OnClick="btn_nowx_OnClick" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-wrapper">
        <div>
            <table>
                <tr>
                    <td style="width: 20%; text-align: right">
                        批号：
                    </td>
                    <td style="width: 40%; text-align: left">
                        <asp:TextBox ID="txt_ph" runat="server" Width="200px"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txt_ph"
                            ServicePath="~/PM_Data/PM_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                            CompletionInterval="10" ServiceMethod="GetPID" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                        </cc1:AutoCompleteExtender>
                        合同名称：
                        <asp:TextBox ID="txt_contr" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 20%; text-align: left">
                        <asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_click" />
                    </td>
                    <td style="width: 30%;">
                    </td>
                </tr>
            </table>
        </div>
        <div class="box-outer" style="overflow: auto">
            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="check_index" runat="server" CssClass="checkBoxCss" />
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成品" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="工序" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MS_ID" HeaderText="批号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="任务号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("MS_ENGID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="合同名称" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblpj" runat="server" Text='<%# Eval("MS_PJID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblpjname" runat="server" Text='<%# Eval("MS_PJNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="设备名称" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lbleng" runat="server" Text='<%# Eval("MS_ENGNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="子设备名称" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblchildeng" runat="server" Text='<%# Eval("MS_CHILDENGNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="生产负责人" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblduy" runat="server" Text='<%# Eval("MTA_DUY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="生产外协" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlTask" CssClass="link" NavigateUrl='<%#"PM_Xie_List_Detail.aspx?mnpid="+Eval("MS_ID")%>'
                                runat="server">
                                <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                <asp:Label ID="lblwaixie" runat="server" Text="制定"></asp:Label>
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:Panel ID="NoDataPanel" runat="server">
                没有记录!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    <div>
        <asp:HiddenField ID="hfpldetail" runat="server" />
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    </div>
</asp:Content>
