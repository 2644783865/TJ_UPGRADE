<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master"  AutoEventWireup="true" CodeBehind="CM_Contract_State.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Contract_State" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    <p>合同状态</p>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<div class="RightContent">

    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
     <table width="100%">
     <tr>
     <td style="width:30%">合同状态修改</td>
     <td align="right" style="width:auto">
         <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="请输入合同编号："></asp:Label></td>
     <td>
         <asp:TextBox ID="txtHTBH" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnQuery" runat="server" Text="查 询" CssClass="button-outer" onclick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnAll" runat="server" Text="全 部" CssClass="button-outer" onclick="btnAll_Click" />
     </td>
     </tr>
     </table>
    </div>
    </div>
    </div>
     <div class="box-wrapper" >
     <div class="box-outer">
   <asp:GridView ID="grvHT" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
             onrowcancelingedit="grvHT_RowCancelingEdit" onrowdatabound="grvHT_RowDataBound" 
             onrowediting="grvHT_RowEditing" onrowupdating="grvHT_RowUpdating"   >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB"  />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label> 
                  </ItemTemplate>

                 <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:BoundField HeaderText="合同编号" DataField="PCON_BCODE"  ReadOnly="true"
                    ItemStyle-HorizontalAlign="Center" >
           <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField HeaderText="合同名称" DataField="PCON_NAME"  ReadOnly="true"
                    ItemStyle-HorizontalAlign="Center" >
           <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
              
              <asp:TemplateField HeaderText="合同类别" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                   <asp:Label ID="Label3" runat="server" Text='<%#Eval("PCON_FORM").ToString()=="0"?"商务":Eval("PCON_FORM").ToString()=="1"?"委外":Eval("PCON_FORM").ToString()=="2"?"采购":Eval("PCON_FORM").ToString()=="3"?"发运":"其他" %>'></asp:Label>
               </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="合同状态">
               <ItemTemplate>
               <asp:HiddenField ID="hdfState" runat="server" Value='<%#Eval("PCON_STATE") %>'>
                </asp:HiddenField>
                   <asp:RadioButtonList ID="rblState" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" Height="18px"> 
                   <asp:ListItem Text="未开始" Value="0"></asp:ListItem>
                   <asp:ListItem Text="进行中" Value="1"></asp:ListItem>
                    <asp:ListItem Text="完成" Value="2"></asp:ListItem>
                   </asp:RadioButtonList>
               </ItemTemplate>
                   <ItemStyle HorizontalAlign="Center" />
               </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" HeaderText="操作">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#EEE8AA" />
            <AlternatingRowStyle BackColor="White" /> 
            <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />                  
        </asp:GridView>
        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">没有记录!</asp:Panel>
        <uc1:UCPaging ID="UCPaging1" runat="server" />
     </div>
     </div>
 </div>  
</asp:Content>
