<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_Data_addto_list.aspx.cs" MasterPageFile="~/Masters/PopupBase.Master" Inherits="ZCZJ_DPF.PM_Data.PM_Data_addto_list" %>


<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div>
        <div>
            <table width="100%">
                <tr align="center">
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="toptable grid" ForeColor="#333333" CellPadding="3" Font-Size="9pt"
                            BackColor="White" BorderColor="Black" BorderStyle="Solid" Width="100%" BorderWidth="1px"
                            EmptyDataText="没有已生成订单的记录！">
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
                                <asp:BoundField DataField="TO_DOCNUM" HeaderText="单据编号" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="制单人" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="zdrid" runat="server" Text='<%#Eval("TO_ZDR")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="zdrnm" runat="server" Text='<%#Eval("TO_ZDRNAME")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="TO_ZDTIME" HeaderText="制单日期" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="供应商" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="supplierid" runat="server" Text='<%#Eval("TO_SUPPLYID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="suppliernm" runat="server" Text='<%#Eval("TO_SUPPLYNAME")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_confirm" runat="server" Text="确定" OnClick="btn_confirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_concel" runat="server" Text="取消" OnClick="btn_concel_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>