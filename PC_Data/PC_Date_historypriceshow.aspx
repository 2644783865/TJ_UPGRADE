<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_Date_historypriceshow.aspx.cs"
    MasterPageFile="~/Masters/PopupBase.Master" Inherits="ZCZJ_DPF.PC_Data.PC_Date_historypriceshow"
    Title="历史价格查询" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div style="border: 1px solid #000000; overflow: auto">
        <div width="100%">
            <asp:TextBox ID="tb_marid" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="tb_marguige" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="tb_marcaizhi" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="tb_marshijian" runat="server" Visible="false"></asp:TextBox>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CssClass="toptable grid" CellPadding="4" ForeColor="#333333" DataKeyNames="MARID"
                OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
                Width="100%" EmptyDataText="暂无报价">
                <PagerTemplate>
                    <table width="100%" style="border: 0px; border-style: ridge;" align="center">
                        <tr>
                            <td style="border-bottom-style: ridge; width: 100%; text-align: center">
                                <asp:Label ID="lblCurrrentPage" runat="server" ForeColor="#CC3300"></asp:Label>
                                <span>跳转至</span>
                                <asp:DropDownList ID="page_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="page_DropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <span>页</span>
                                <asp:LinkButton ID="lnkBtnFirst" CommandArgument="First" CommandName="page" runat="server">第一页</asp:LinkButton>
                                <asp:LinkButton ID="lnkBtnPrev" CommandArgument="prev" CommandName="page" runat="server">上一页</asp:LinkButton>
                                <asp:LinkButton ID="lnkBtnNext" CommandArgument="Next" CommandName="page" runat="server">下一页</asp:LinkButton>
                                <asp:LinkButton ID="lnkBtnLast" CommandArgument="Last" CommandName="page" runat="server">最后一页</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </PagerTemplate>
                <Columns>
                    <asp:BoundField HeaderText="行号" SortExpression="MARID" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MARID" HeaderText="ID" SortExpression="MARID" ItemStyle-Wrap="False"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MARNM" HeaderText="名称" ReadOnly="True" SortExpression="MARNM"
                        ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MARGG" HeaderText="规格" SortExpression="MARGG" ItemStyle-Wrap="False"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MARCZ" HeaderText="材质" SortExpression="MARCZ" ItemStyle-Wrap="False"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PRICE" HeaderText="价格" SortExpression="PRICE" ItemStyle-Wrap="False"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="shuilv" HeaderText="税率(%)" SortExpression="shuilv" HeaderStyle-ForeColor="BlueViolet"
                        ItemStyle-ForeColor="Red" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="picno" HeaderText="比价单号" SortExpression="picno" ItemStyle-Wrap="False"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="SUPPLIERRESNM" HeaderText="供应商" SortExpression="SUPPLIERRESNM"
                        ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="IRQDATA" HeaderText="比价时间" SortExpression="IQRDATA" ItemStyle-Wrap="False"
                        ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZCZJDMPConnectionString %>"
            SelectCommand=""></asp:SqlDataSource>
    </div>
</asp:Content>
