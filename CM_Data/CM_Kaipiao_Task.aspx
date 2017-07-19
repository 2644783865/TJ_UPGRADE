<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_Kaipiao_Task.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Kaipiao_Task" %>

<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    按任务号汇总
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div style="height: 40px">
                <table>
                    <tr>
                        <td>
                            编号：<asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            任务号：<asp:TextBox ID="txtTaskId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="bthSearch" OnClick="btnSearch_Click" Text="查询" />
                        </td>
                        <td align="right">
                            <asp:Button runat="server" ID="btnExport" OnClick="BtnExport_Click" Text="导出Excel" />
                        </td>
                    </tr>
                </table>
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
                        <asp:BoundField DataField="KP_CODE" HeaderText="编号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TaskId" HeaderText="任务号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="conId" HeaderText="合同号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Proj" HeaderText="项目名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Engname" HeaderText="设备名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="number" HeaderText="数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
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
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                    没有记录!</asp:Panel>
            </div>
        </div>
        <div>
            <asp:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
