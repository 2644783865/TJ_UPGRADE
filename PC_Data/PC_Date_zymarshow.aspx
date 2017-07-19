<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_Date_zymarshow.aspx.cs"
    Inherits="ZCZJ_DPF.PC_Data.PC_Date_zymarshow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="border: 1px solid #000000; overflow: auto">
            <div width="100%">
                <asp:Button ID="btn_concel" runat="server" Text="关闭" OnClick="btn_concel_Click" />
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                    BorderWidth="1px" EmptyDataText="没有记录！">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <Columns>
                        <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="marid" HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="marnm" HeaderText="物料名称" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="margg" HeaderText="规格" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="marcz" HeaderText="材质" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="margb" HeaderText="国标" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="length" HeaderText="长度" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="width" HeaderText="宽度" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="num" HeaderText="数量" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="fznum" HeaderText="辅助数量" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="unit" HeaderText="单位" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="fzunit" HeaderText="辅助单位" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
