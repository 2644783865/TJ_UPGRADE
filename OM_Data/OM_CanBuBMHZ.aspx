<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_CanBuBMHZ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CanBuBMHZ" Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    餐补按部门汇总
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="20">
    </asp:ToolkitScriptManager>
    <div class="box_right">
        <div class="box-inner">
            <table style="width: 100%">
                <tr>
                    <td>
                        <strong>时间：</strong>
                        <asp:DropDownList ID="dplYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth">
                        </asp:DropDownList>
                        &nbsp;年&nbsp;
                        <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth">
                        </asp:DropDownList>
                        &nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>部门：</strong>&nbsp;
                        <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                            OnSelectedIndexChanged="dplbm_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="daochu" runat="server" Text="导出" OnClick="daochu_clicked" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box_right">
        <div class="box-wrapper">
            <div style="width: 100%; height: auto; overflow: scroll; display: block">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" border="1" class="toptable grid nowrap">
                    <asp:Repeater ID="rptProNumCost" runat="server">
                        <HeaderTemplate>
                            <tr align="center" style="background-color: #B9D3EE;">
                                <td>
                                    序号
                                </td>
                                <td>
                                    年月份
                                </td>
                                <td>
                                    部门
                                </td>
                                <td>
                                    月度餐补
                                </td>
                                <td>
                                    补发
                                </td>
                                <td>
                                    合计
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCB_YearMonth" runat="server" Width="50px" Text='<%#Eval("CB_YearMonth")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbDEP_NAME" runat="server" Width="50px" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCB_MonthCB" runat="server" Width="50px" Text='<%#Eval("CB_MonthCBhj")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCB_BuShangYue" runat="server" Width="50px" Text='<%#Eval("CB_BuShangYuehj")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCB_HeJi" runat="server" Width="50px" Text='<%#Eval("CB_HeJihj")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Label ID="zonge" runat="server" Text="总额:"></asp:Label>
                            <asp:Label ID="heji" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="palNoData" runat="server" ForeColor="Red" Visible="false" HorizontalAlign="Center">
                    <br />
                    没有记录<br />
                </asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
