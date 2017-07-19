<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_Data_turnto_lib.aspx.cs"
    Inherits="ZCZJ_DPF.PC_Data.PC_Data_turnto_lib" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>转备库</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:Button ID="btn_confirm" runat="server" Text="确定" OnClick="btn_confirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_concel" runat="server" Text="取消" OnClick="btn_concel_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <table width="100%" style="text-align: center;">
            <tr align="center">
                <td style="font-size: x-large; text-align: center;" colspan="4">
                    采购订单
                </td>
                <td align="right">
                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                        Visible="false" />
                </td>
            </tr>
            <tr align="center">
                <td style="width: 70%;" align="left">
                    &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:Label ID="LabelDate" runat="server"></asp:Label>
                </td>
                <td style="width: 30%;" align="left">
                    &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td style="width: 70%;" align="left">
                    &nbsp;&nbsp;&nbsp;供应商：<asp:Label ID="LabelSupplier" runat="server"></asp:Label>
                </td>
                <td style="width: 30%;" align="left">
                    &nbsp;&nbsp;&nbsp;摘&nbsp;&nbsp;&nbsp;要：<asp:Label ID="LabelAbstract" runat="server"
                        Visible="false"></asp:Label>
                </td>
            </tr>
            <tr id="ws" visible="false" runat="server">
                <td style="width: 25%;" align="left">
                    &nbsp;&nbsp;&nbsp;版本号：<asp:Label ID="LabelVersionNo" runat="server" Visible="false"></asp:Label>
                </td>
                <td style="width: 25%;" align="left">
                    变更日期：<asp:Label ID="LabelChangeDate" runat="server" Visible="false"></asp:Label>
                </td>
                <td style="width: 25%;" align="left">
                    &nbsp;&nbsp;&nbsp;变更人：<asp:Label ID="LabelChangeMan" runat="server" Visible="false"></asp:Label>
                </td>
                <td style="width: 25%;" align="left">
                    变更原因：<asp:Label ID="LabelChangeReason" runat="server" Visible="false"></asp:Label>
                    <asp:TextBox ID="tb_state" runat="server" Text="" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="tb_cstate" runat="server" Text="" Visible="false"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="height: 400px; overflow: auto; width: 100%;">
            <table width="100%">
                <tr align="center">
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" EmptyDataText="没有记录！">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" CssClass="checkBoxCss" />
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="PlanCode" HeaderText="计划跟踪号" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MaterialCode" HeaderText="材料编码" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MaterialName" HeaderText="名称" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MaterialStandard" HeaderText="标准" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MaterialTexture" HeaderText="材质" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MaterialGb" HeaderText="国标" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Unit" HeaderText="单位" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="UnitPrice" HeaderText="价格" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Number" HeaderText="数量" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="zxnum" HeaderText="执行数量" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="原因">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tb_value" runat="server" Text='<%# Eval("Comment") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="审核/到货/关闭标识">
                                    <ItemTemplate>
                                        <asp:Label ID="statetext" runat="server" Text='<%#get_order_state(Eval("PO_STATE").ToString(),Eval("pocstate").ToString())%>'></asp:Label>
                                        <asp:Label ID="state" runat="server" Text='<%# Eval("PO_STATE") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="cstate" runat="server" Text='<%# Eval("pocstate") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%">
            <tr>
                <td style="width: 25%;" align="left">
                    &nbsp;&nbsp;&nbsp;主&nbsp;&nbsp;&nbsp;管：<asp:Label ID="LabelManager" runat="server"></asp:Label>
                </td>
                <td style="width: 25%;" align="left">
                    &nbsp;&nbsp;&nbsp;部&nbsp;&nbsp;&nbsp;门：<asp:Label ID="LabelDep" runat="server"></asp:Label>
                </td>
                <td style="width: 25%;" align="left">
                    &nbsp;&nbsp;&nbsp;业务员：<asp:Label ID="LabelClerk" runat="server"></asp:Label>
                </td>
                <td style="width: 25%;" align="left">
                    &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDocument" runat="server"></asp:Label>
                    <asp:Label ID="LabelDocumentid" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
