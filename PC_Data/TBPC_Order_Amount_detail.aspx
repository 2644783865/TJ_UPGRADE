<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TBPC_Order_Amount_detail.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.master" Inherits="ZCZJ_DPF.PC_Data.TBPC_Order_Amount_detail" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    材料合计
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                    <td style="width: 50%">
                    </td>
                    <td style="width: 25%" align="left">
                    预算材料总金额：
                    <asp:Label ID="lab_YS_AMOUNT_B" Text="" runat="server"></asp:Label>
                    </td>
                    <td style="width: 25%" align="left">
                    订单材料总金额
                    <asp:Label ID="lab_YS_AMOUNT_O" Text="" runat="server"></asp:Label>
                    </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="width: 100%; overflow: auto; overflow-x: yes; overflow-y: hidden;">
                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="nowrap toptable grid "
                    border="1">
                    <asp:Repeater ID="tbpc_otherpurbillRepeater" runat="server">
                        <HeaderTemplate>
                            <tr align="center" style="background-color: #5CACEE">
                                <td>
                                    <strong>名称</strong>
                                </td>
                                <td>
                                    <strong>预算总金额</strong>
                                </td>
                                <td>
                                    <strong>订单总金额</strong>
                                </td>
                                <td>
                                    <strong>百分比</strong>
                                </td>
                                <td>
                                    <strong>明细</strong>
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id="row" class="baseGadget" onmouseout="this.className='baseGadget'" runat="server"
                                align="center">
                                <td>
                                    <asp:Label ID="lab_YS_NAME" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lab_YS_MAR_BG" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lab_YS_MAR" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lab_YS_MAR_Percent" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:HyperLink ID="Hpl_detail" runat="Server" ForeColor="Red" NavigateUrl="">
                                        <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" ForeColor="Red"
                                            hspace="2" align="absmiddle" runat="server" />
                                        <asp:Label ID="check_look" runat="server" Text="查看明细"></asp:Label></asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
