<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_Date_silimarmarshow.aspx.cs" MasterPageFile="~/Masters/PopupBase.Master"
    Inherits="ZCZJ_DPF.PC_Data.PC_Date_silimarmarshow"  Title="相似物料查询"%>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div style="border: 1px solid #000000; overflow: auto; ">
        <div width="100%">
            <asp:TextBox ID="tb_ptcode" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="tb_marnm" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="tb_marid" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="tb_marcz" runat="server" Visible="false"></asp:TextBox>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="toptable grid" CellPadding="4" ForeColor="#333333"
                DataKeyNames="MARID" OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound"
                OnPageIndexChanging="GridView1_PageIndexChanging" Width="100%" EmptyDataText="不存在相似物料">
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
                    <asp:BoundField HeaderText="行号" SortExpression="MARID" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="marid" HeaderText="ID" SortExpression="marid" ItemStyle-Wrap="False"  ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="marnm" HeaderText="名称" ReadOnly="True" SortExpression="marnm"
                        ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="margg" HeaderText="规格" SortExpression="margg" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="marcz" HeaderText="材质" SortExpression="marcz" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="margb" HeaderText="国标" SortExpression="margb" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="num" HeaderText="库存数量" SortExpression="num" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="marunit" HeaderText="单位" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
             <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />  
            </asp:GridView>
        </div>
        
        <div>
        <table width="100%">
        <tr><td align="center"><span  style="color:Red">被代用（占用）物料</span></td></tr>
        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="toptable grid" CellPadding="4" ForeColor="#333333"
                DataKeyNames="marid"  Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="marid" HeaderText="ID"  SortExpression="marid" ItemStyle-Wrap="False"  ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="marnm" HeaderText="名称" ReadOnly="True" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="margg" HeaderText="规格"  ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="marcz" HeaderText="材质"  ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="margb" HeaderText="国标"  ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="num" HeaderText="需用数量"  ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="marunit" HeaderText="单位" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
             <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />  
            </asp:GridView>
            </table>
        </div>
        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZCZJDMPConnectionString %>"
            SelectCommand=""></asp:SqlDataSource>
    </div>
    </asp:Content>
  