<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="QC_Task_Persons.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_Task_Persons" Title="人员信息" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
质量部人员结构信息表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
 <div class="RightContent">
    <div  class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                    <td width="20%"></td>
                    <td>按部门查询：
                        <asp:DropDownList ID="ddlSearch" AutoPostBack="true" runat="server" 
                            Width="100px" onselectedindexchanged="ddlSearch_SelectedIndexChanged">
                        </asp:DropDownList>   
                    </td>
                    <td align="center" width="35%">
                        <asp:Button ID="btnConfirm" runat="server" Text="提 交"  BorderStyle="None" 
                                BackColor="Transparent"  onclick="btnConfirm_Click" />
                            
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_back" runat="server" Text="返 回"  BorderStyle="None" 
                                BackColor="Transparent" onclick="btn_back_Click" />
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
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                         <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" CssClass="checkBoxCss"/>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="编号"  ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ST_ID") %>'>&gt;</asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="姓名" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("ST_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="ST_NAME" HeaderText="姓名" ItemStyle-HorizontalAlign="Center" />--%>
                <asp:BoundField DataField="ST_GENDER" HeaderText="性别" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ST_DEPID1" HeaderText="岗位" ItemStyle-HorizontalAlign="Center" />
            </Columns>
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
</div>
</asp:Content>
