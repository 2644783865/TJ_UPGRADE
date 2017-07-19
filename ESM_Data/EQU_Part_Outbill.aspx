<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="EQU_Part_Outbill.aspx.cs" Inherits="ZCZJ_DPF.ESM_Data.EQU_Part_Outbill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    设备备件出库&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="button-outer" OnClick="btnSave_OnClick" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReturn" runat="server" Text="返 回" CausesValidation="false" OnClick="btnReturn_OnClick"
                                            CssClass="button-outer" />
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <table width="100%">
                            <tr align="center">
                                <td align="center" colspan="5">
                                    <asp:Label ID="lbltitle1" runat="server" Text="设备备件领料单" Font-Bold="true" Font-Size="Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="5">
                                    出库单号：<asp:Label ID="lblOutCode" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    制单人：<asp:Label ID="lblOutDoc" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td align="right">
                                    发料员：
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txt_giver" runat="server" Width="80px"></asp:TextBox>
                                    <input id="giverid" type="text" runat="server" readonly="readonly" style="display: none" />
                                    <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand">
                                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        选择
                                    </asp:HyperLink>
                                    <cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                        Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect1"
                                        PopupControlID="pal_select1">
                                    </cc1:PopupControlExtender>
                                    <asp:Panel ID="pal_select1" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                        <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                            font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                            <tr>
                                                <td style='background-color: #A8B7EC; color: white;'>
                                                    <b>选择发料员</b>
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
                                                    <asp:Button ID="btn_giver" runat="server" Text="确 定" OnClick="btn_giver_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                <td align="left" style="white-space: nowrap">
                                    领料部门：<asp:DropDownList ID="ddlDep" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    领料日期：<asp:Label ID="lblOutDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            CellPadding="4" ForeColor="#333333" EmptyDataText="没有相关数据！">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="设备备件名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("ParName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备件型号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("ParType")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="库存数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNumstore" runat="server" Text='<%#Eval("ParNumSto")%>' BorderStyle="None"
                                            Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="领料数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOutNum" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOutNote" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
