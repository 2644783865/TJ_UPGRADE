<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_AllPersonSelect.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_AllPersonSelect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    公司人员结构信息表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="96%">
                        <tr>
                            <td>
                                <b>指定人员</b>
                            </td>
                            <td>
                                按岗位查询：
                                <asp:DropDownList ID="ddlSearch" AutoPostBack="true" runat="server" Width="100px"
                                    OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                按姓名：
                                <asp:TextBox ID="txtName" runat="server" Width="100px"></asp:TextBox>
                                <asp:Button ID="btnName" runat="server" Text="选 择" OnClick="btnName_Click" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" CssClass="checkBoxCss" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="ST_ID" runat="server" Text='<%# Eval("ST_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID_Num" HeaderText="序号" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="ST_NAME" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="ST_GENDER" HeaderText="性别" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="DEP_NAME" HeaderText="岗位" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:Panel ID="NoDataPanel" runat="server">
                            没有记录!</asp:Panel>
                        <asp:UCPaging ID="UCPaging1" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlSearch" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnName" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
