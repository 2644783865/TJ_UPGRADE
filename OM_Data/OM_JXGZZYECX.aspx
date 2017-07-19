<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_JXGZZYECX.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.JXGZZYECX" Title="绩效工资总余额" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-wrapper">
        <div class="box-outer">
            <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rpt1" runat="server">
                        <HeaderTemplate>
                            <tr style="background-color: #B9D3EE;" >
                                <td align="center">
                                    序号
                                </td>
                                <td align="center">
                                    部门
                                </td>
                                <td align="center">
                                    总余额
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);">
                                <td>
                                    <asp:Label ID="ID" runat="server" Text='<%#Eval("Id")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbdepartment" runat="server" Width="50px" Text='<%#Eval("DepName")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="jixiaozye" runat="server" Width="90px" Text='<%#Eval("JiXiaoYE")%>'></asp:Label>
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
