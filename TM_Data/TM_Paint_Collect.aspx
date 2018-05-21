<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="TM_Paint_Collect.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Paint_Collect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="width: 100%;">
                <asp:Button runat="server" ID="PurApply" Text="添加到采购申请" 
                    onclick="PurApply_Click" Width="100%"/>
                <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                    AutoGenerateColumns="False" CellPadding="3" ForeColor="#333333" Style="white-space: normal">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="24px">
                            <ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:BoundField DataField="PS_ENGID" HeaderText="任务号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="MNAME" HeaderText="物料名称" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sumYL" HeaderText="用量(L)" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sumXSJ" HeaderText="稀释剂(L)" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false" />
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
