<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Tech_persons.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Tech_persons" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    技术部人员结构信息表</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

 <div class="RightContent">
    <div  class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="96%">
                    <tr>
                    <td><b>指定技术人员</b></td>
                    <td>按岗位查询：
                        <asp:DropDownList ID="ddlSearch" AutoPostBack="true" runat="server" 
                            Width="100px" onselectedindexchanged="ddlSearch_SelectedIndexChanged">
                        </asp:DropDownList>   
                    </td>
                    <td align="right">
                        <asp:Button ID="btnConfirm" runat="server" Text="确 定" onclick="btnConfirm_Click" />
                    </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    
    <div class="box-wrapper">
        <div class="box-outer">
          
            <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True"  ForeColor="#1E5C95" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" CssClass="checkBoxCss"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="姓名" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("ST_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="id" runat="server" Text='<%# Eval("ST_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="ST_CODE" HeaderText="ID" ItemStyle-HorizontalAlign="Center" />--%>
                <asp:BoundField DataField="ST_GENDER" HeaderText="性别" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DEP_NAME" HeaderText="岗位" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />                    
        </asp:GridView>
        <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
           
        <uc1:UCPaging ID="UCPaging1" runat="server" />

        </div>
    </div>
</div>
</asp:Content>

