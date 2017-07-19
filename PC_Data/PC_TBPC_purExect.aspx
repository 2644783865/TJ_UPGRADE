<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_purExect.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_purExect"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btn_edit" runat="server" Text="编辑" OnClick="btn_edit_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btn_tijiao" runat="server" Text="提交" OnClick="btn_tijiao_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btn_fanshen" runat="server" Text="反审" OnClick="btn_fanshen_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btn_finish" runat="server" Text="完结" OnClick="btn_finish_Click" Visible="false" />&nbsp;&nbsp;
                            <asp:Button ID="btn_shangcha" runat="server" Text="上查" OnClick="btn_shangcha_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btn_xiacha" runat="server" Text="下查" OnClick="btn_xiacha_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btn_biangeng" runat="server" Text="变更查询" OnClick="btn_biangeng_Click"
                                Visible="false" />&nbsp;&nbsp;
                            <asp:Button ID="goback" runat="server" Text="返回" OnClick="goback_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btn_export" runat="server" Text="导出" OnClick="btn_export_Click" />&nbsp;&nbsp;
                            <asp:HyperLink ID="Hyp_print" runat="server" Target="_blank">
                                <asp:Image ID="Img_print" runat="server" ImageUrl="~/Assets/icon-fuction/89.gif" /></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <asp:Panel ID="HeadPanel" runat="server" Width="100%">
            <table width="100%">
                <tr align="center">
                    <td style="font-size: x-large; text-align: center;" colspan="4">
                        采购执行情况：
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%;" align="left">
                        &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode" runat="server"></asp:Label>
                    </td>
                    <td style="width: 25%;" align="left">
                        &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:Label ID="LabelDate" runat="server"></asp:Label>
                    </td>
                    <td style="width: 25%;" align="left">
                        &nbsp;&nbsp;&nbsp;供应商：<asp:Label ID="LabelSupplier" runat="server"></asp:Label>
                    </td>
                    <td style="width: 25%;" align="left">
                        &nbsp;&nbsp;&nbsp;摘&nbsp;&nbsp;&nbsp;要：<asp:Label ID="LabelAbstract" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div style="height: 400px; overflow: auto; width: 100%">
            <div class="cpbox xscroll">
            
            </div>
        </div>
    </div>
</asp:Content>
