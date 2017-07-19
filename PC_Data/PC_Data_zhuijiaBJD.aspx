<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_Data_zhuijiaBJD.aspx.cs"
    Inherits="ZCZJ_DPF.PC_Data.PC_Data_zhuijiaBJD" MasterPageFile="~/Masters/PopupBase.Master"
    Title="追加比价单" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <div class="cpbox xscroll">
        <table id="tab" class="nowrap cptable fullwidth" align="center">
            <asp:Repeater ID="purchaseplan_Repeater" runat="server">
                <HeaderTemplate>
                    <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                        <td>
                        </td>
                        <td>
                            <strong>行号</strong>
                        </td>
                        <td>
                            <strong>比价单号</strong>
                        </td>
                        <td>
                            <strong>制单人</strong>
                        </td>
                        <td>
                            <strong>制单时间</strong>
                        </td>
                        <td>
                            <strong>备注</strong>
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                        <td>
                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                            </asp:CheckBox>
                        </td>
                        <td>
                            <asp:Label ID="Lab_NUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="picno" runat="server" Text='<%#Eval("picno")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="zdrnm" runat="server" Text='<%#Eval("zdrnm")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="irqdata" runat="server" Text='<%#Eval("irqdata")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="totalnote" runat="server" Text='<%#Eval("totalnote")%>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="5" align="center">
                    <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                        没有记录！</asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btn_confirm" runat="server" Text="确定" OnClick="btn_confirm_Click" />
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btn_back" runat="server" Text="取消" OnClick="btn_back_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
