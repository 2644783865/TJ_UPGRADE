<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="FM_Desk.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_Desk" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    功能说明
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div style="width: 600px; margin-right: auto; margin-left: auto">
        <div style="width: 300px; float: left;" align="center">
            <asp:Label ID="LabelYear" runat="server" ></asp:Label>
        </div>
        <div style="width: 300px; float: left; " align="center">
            <asp:Label ID="LabelMonth" runat="server" ></asp:Label>
        </div>
        <table style="border: 1px solid #000000; width: 100%" 
            border="1" cellpadding="4" cellspacing="1">
            <tr>
                <td align="center">
                    入库核算
                </td>
                <td align="center">
                    出库核算
                </td>
                <td align="center">
                    期末关帐
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lb_rkhs" runat="server"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lb_ckhs" runat="server"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lb_qmgz" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
