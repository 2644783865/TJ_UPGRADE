﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_ChangePS.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_ChangePS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    计划单变更评审
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <asp:HiddenField runat="server" ID="DepID" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class='box-title'>
                        <table width="100%">
                            <tr>
                                <td>
                                    变更筛选
                                </td>
                                <asp:Panel runat="server" ID="depchange" Visible="false">
                                    <td align="right" style="width: 60px" valign="middle">
                                        审核状态:
                                    </td>
                                    <td style="width: 150px" valign="top">
                                        <asp:RadioButtonList ID="rbl_mytask" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged">
                                            <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="我的任务" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="left">
                                        <asp:RadioButtonList ID="rbl_status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text="待审核" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="已通过" Selected="True" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </asp:Panel>
                                <td>
                                    <asp:RadioButtonList runat="server" ID="rbl_confirm" RepeatDirection="Horizontal"
                                        AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged"
                                        Visible="false">
                                        <asp:ListItem Text="未处理" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="已处理" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 100px">
                                    请选择查询类型：
                                </td>
                                <td valign="middle" style="width: 120px">
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                        <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="合同编号" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td valign="middle">
                                    <asp:TextBox ID="searchcontent" runat="server"></asp:TextBox>
                                    <asp:Button ID="btn_Search" runat="server" Text="查  询" OnClick="btn_Search_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer" style="width: 99%;">
                    <div style="width: 100%; overflow: scroll">
                        <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                            OnRowDataBound="GridView1_OnRowDataBound" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text=' <%#Eval("ID_Num")%>'></asp:Label>
                                        <asp:HiddenField ID="chid" runat="server" Value='<%#Eval("CH_ID") %>' />
                                        <asp:HiddenField ID="status" runat="server" Value='<%#Eval("CM_STATE") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Height="21px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="变更项目" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="width: 400px; white-space: normal">
                                            <%#Eval("CM_ITEM") %>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="450px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CM_CONTR" HeaderText="合同号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CM_ZDTIME" HeaderText="制单日期" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="制单人">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_zdr" runat="server" Text='<%# Eval("ST_NAME")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="审核状态">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_status" runat="server" Text='<%# Eval("CM_STATE").ToString()=="1"?"审核中":Eval("CM_STATE").ToString()=="2"?"审核通过":"被驳回"%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Task_look" CssClass="link" NavigateUrl='<%#"CM_ChangeView.aspx?action=look&id="+Eval("ID")+"&chid="+Eval("CH_ID") %>'
                                            runat="server">
                                            <asp:Image ID="InfoImage_look" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />
                                            查看
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Task_ed" CssClass="link" NavigateUrl='<%#"CM_AddTask.aspx?action=change&id="+Eval("ID")+"&chid="+Eval("CH_ID") %>'
                                            runat="server">
                                            <asp:Image ID="InfoImage_ed" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />
                                            修改
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="评审" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Task_ps" CssClass="link" NavigateUrl='<%#"CM_ChangeView.aspx?action=ps&id="+Eval("ID")+"&chid="+Eval("CH_ID") %>'
                                            runat="server">
                                            <asp:Image ID="InfoImage_ps" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />
                                            评审
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="确定" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Confirm" runat="server" CommandArgument='<%#Eval("CH_ID") %>'
                                            Text="确定" OnClick="btn_Confirm_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                            没有记录!</asp:Panel>
                    </div>
                </div>
                <div>
                    <asp:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_Search" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="rbl_status" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
