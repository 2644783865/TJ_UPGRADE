<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_Config.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Config" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
        <Columns>
            <asp:TemplateField ItemStyle-Wrap="false">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField  HeaderText="用户名"   DataField="" />
            <asp:BoundField  HeaderText="发料人"   DataField=""/>
            <asp:BoundField  HeaderText="单据缩写"   DataField=""/>
        </Columns>
        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
        <RowStyle BackColor="#EFF3FB" Wrap="False" />
        <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
    </asp:GridView>
</asp:Content>
