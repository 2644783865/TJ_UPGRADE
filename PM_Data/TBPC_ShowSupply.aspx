<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TBPC_ShowSupply.aspx.cs" MasterPageFile="~/Masters/PopupBase.Master"
 Inherits="ZCZJ_DPF.PM_Data.TBPC_ShowSupply" Title="厂商查询" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

<div style="border: 1px solid #000000; overflow: auto">
        <div width="100%">
            
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="toptable grid" CellPadding="4" ForeColor="#333333"
                DataKeyNames="CS_CODE" OnRowDataBound="GridView1_RowDataBound" 
                 Width="100%" EmptyDataText="暂无对应供应商，请完善供应商供货范围，或者增加新供应商">
               
                <Columns>
                    <asp:BoundField HeaderText="行号"  ItemStyle-Wrap="False" />
                    <asp:BoundField DataField="CS_NAME" HeaderText="供货商名称"  ItemStyle-Wrap="False"/>
                    <asp:BoundField DataField="CS_HRCODE" HeaderText="助记码" ReadOnly="True" 
                        ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="CS_Scope" HeaderText="供货范围"  ItemStyle-Wrap="False" />
                </Columns>
                 <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left"/>
             <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />  
            </asp:GridView>
             <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
        
    </div>
 
</asp:Content>
