<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_Kaipiao_Contract.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Kaipiao_Contract" %>

<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    按合同号汇总
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div style="height: 40px">
                合同号：
                <asp:TextBox runat="server" ID="txtContract"></asp:TextBox>
                <asp:Button runat="server" ID="btnQuery" Text="查询" OnClick="btnQuery_OnClick" />
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%;">
            <div style="width: 100%; overflow: scroll">
                <asp:GridView ID="GridView1" Width="1600px" CssClass="toptable grid" Style="white-space: normal"
                    runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Height="21px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="conId" HeaderText="合同号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Proj" HeaderText="项目名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Map" HeaderText="图号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--      <asp:BoundField DataField="number" HeaderText="数量" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="Unit" HeaderText="单位" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="kpZongMoney" HeaderText="开票金额" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ZongMoney" HeaderText="合同金额" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="KP_KPNUMBER" HeaderText="发票号" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="KP_KPDATE" HeaderText="开票日期" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <table id="Table1" width="100%">
                    <tr align="center">
                        <td align="center">
                            <asp:Label runat="server" ID="Label1" Text="开票金额总数："></asp:Label>
                            <asp:Label runat="server" ID="KPJE" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label runat="server" ID="Label2" Text="合同金额总数："></asp:Label>
                            <asp:Label runat="server" ID="HTJE" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                    没有记录!</asp:Panel>
            </div>
        </div>
        <div>
            <asp:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
