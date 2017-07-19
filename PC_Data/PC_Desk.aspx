<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_Desk.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_Desk" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购部消息面板
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Visible="false">
        <div style="font-size: larger;" visible="false" id="kz">
            <table>
                <tr>
                    <td>
                        采购计划管理
                    </td>
                    <td>
                        <asp:HyperLink ID="Hyp1" Target="right" runat="server">
                            <p style="color: Red">
                                下推
                                <asp:Label ID="Lbxiatui" runat="server" Text=""></asp:Label></p>
                        </asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink1" Target="right" runat="server">
                            审核
                            <asp:Label ID="Lbshenhe1" runat="server" Text=""></asp:Label></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        物料占用管理
                    </td>
                    <td>
                        <asp:HyperLink ID="Hyp3" Target="right" runat="server">
                            提交
                            <asp:Label ID="Lbtijiao" runat="server" Text=""></asp:Label></asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="Hyp4" Target="right" runat="server">
                            审核
                            <asp:Label ID="Lbshenhe2" runat="server" Text=""></asp:Label></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        任务分工
                    </td>
                    <td>
                        <asp:HyperLink ID="Hyp5" Target="right" runat="server">
                            提交
                            <asp:Label ID="Lbfengong" runat="server" Text=""></asp:Label></asp:HyperLink>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        我的任务
                    </td>
                    <td>
                        <asp:HyperLink ID="Hyp6" Target="right" runat="server">
                            提交
                            <asp:Label ID="Lbmyrenwu" runat="server" Text=""></asp:Label></asp:HyperLink>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        比价单管理
                    </td>
                    <td>
                        <asp:HyperLink ID="Hyp7" Target="right" runat="server">
                            提交
                            <asp:Label ID="Lbbijia" runat="server" Text=""></asp:Label></asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="Hyp8" Target="right" runat="server">
                            审核
                            <asp:Label ID="Lbbijiashenhe" runat="server" Text=""></asp:Label></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        订单管理
                    </td>
                    <td>
                        <asp:HyperLink ID="Hyp9" Target="right" runat="server">
                            提交
                            <asp:Label ID="Lbdd" runat="server" Text=""></asp:Label></asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="Hyp10" Target="right" runat="server">
                            审核
                            <asp:Label ID="Lbddshenhe" runat="server" Text=""></asp:Label></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        代用单
                    </td>
                    <td>
                        <asp:HyperLink ID="Hyp11" Target="right" runat="server">
                            提交
                            <asp:Label ID="Lbdy" runat="server" Text=""></asp:Label></asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="Hyp12" Target="right" runat="server">
                            审核
                            <asp:Label ID="Lbdyshenhe" runat="server" Text=""></asp:Label></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
