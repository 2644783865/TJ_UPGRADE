<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_KaoHe_Deadline.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoHe_Deadline"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    考核日期设定
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                <tr>
                <td align="right">
                         <asp:Button ID="Submit" runat="server" Text="保存" OnClick="btn_Submit_Click"/>&nbsp;
                </td>
                </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box-outer" style="width: 99%; overflow: auto;">
            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            <input id="Hidden2" type="hidden" value='<%# Eval("Id") %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="kh_Type" HeaderText="考核类型" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="开始日期" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                        <asp:TextBox runat="server" ID='txtStart'  class="easyui-datebox"  Text='<%# Eval("kh_Start") %>'></asp:TextBox>
                      
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="截止日期" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                             <asp:TextBox runat="server" ID='txtEnd' class="easyui-datebox"   Text='<%# Eval("kh_End") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
</asp:Content>
