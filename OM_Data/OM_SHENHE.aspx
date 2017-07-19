<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="OM_SHENHE.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_SHENHE" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    审核流程信息      
</asp:Content>   
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
<div class="box-inner">
   <div class="box_right">
        <div class="box-title">
        <table style="width:98%" >
          <tr>
            <td style="width:8%"></td>
            <td style="width:8%" align="right">状态:</td>
            <td style="width:12%" align="left">
            
              <asp:RadioButtonList ID="rblstate" RepeatColumns="3" runat="server" 
                       AutoPostBack="true" onselectedindexchanged="rblstate_SelectedIndexChanged" >                
                  <asp:ListItem Text="在用" value="0" Selected="True"></asp:ListItem>
                  <asp:ListItem Text="停用" Value="1" ></asp:ListItem>
              </asp:RadioButtonList>
            </td>
            <td style="width:50%"></td>
            <td style="width:20%">
              <asp:HyperLink ID="hlkadd" runat="server" NavigateUrl="~/OM_Data/OM_SHENHE_Detail.aspx?action=add" >
               <asp:Image ID="img1" runat="server" ImageUrl="~/Assets/icons/add.gif" /> 添加类型
              </asp:HyperLink>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="btndelete" runat="server" Text="停 用" OnClick="btndelete_Click" />
            </td>
          </tr>
        </table>
       </div> 
    </div>     
 </div>
 <div class="box-wrapper">
   <div class="box-outer">
      <asp:Panel ID="NoDataPanel1" runat="server" HorizontalAlign="Center" Font-Size="Large" >
      没有记录!</asp:Panel>
      <asp:GridView ID="GridView1" runat="server" CssClass="toptable grid" Width="100%"
      AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333"  AllowPaging="false" >
      <RowStyle BackColor="#EFF3FB" />
      <Columns>
           <asp:TemplateField HeaderText="">
              <ItemTemplate>
                 <asp:CheckBox ID="cbchecked" CssClass="checkBoxCss"  BorderStyle="None" runat="server"></asp:CheckBox>
                 <asp:Label ID="lblid" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" Wrap="false" />
           </asp:TemplateField>
           <asp:TemplateField HeaderText="行号">
              <ItemTemplate>
                 <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" Wrap="false" />
           </asp:TemplateField>
           <asp:BoundField HeaderText="类型" DataField="TYPE" >
             <ItemStyle HorizontalAlign="Center"  Wrap="false" />
           </asp:BoundField>
           <asp:BoundField HeaderText="审核层级" DataField="AUDITLEVEL" >
             <ItemStyle HorizontalAlign="Center"  Wrap="false" />
           </asp:BoundField>
           <asp:BoundField HeaderText="一级" DataField="FIRSTMANNM" >
             <ItemStyle HorizontalAlign="Center"  Wrap="false" />
           </asp:BoundField>
           <asp:BoundField HeaderText="二级" DataField="SECONDMANNM" >
             <ItemStyle HorizontalAlign="Center"  Wrap="false" />
           </asp:BoundField>
           <asp:BoundField HeaderText="三级" DataField="THIRDMANNM" >
             <ItemStyle HorizontalAlign="Center"  Wrap="false" />
           </asp:BoundField>
           <asp:BoundField HeaderText="创建人" DataField="CREATERNM" >
             <ItemStyle HorizontalAlign="Center"  Wrap="false" />
           </asp:BoundField>
           <asp:BoundField HeaderText="创建时间" DataField="CREATTM" >
             <ItemStyle HorizontalAlign="Center"  Wrap="false" />
           </asp:BoundField>
           <asp:TemplateField HeaderText="状态">
              <ItemTemplate>
                 <asp:Label ID="lblstate" runat="server" Text='<%# Eval("STATE").ToString()=="0"?"在用":"停用" %>' />
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" Wrap="false" />
           </asp:TemplateField>
           <asp:TemplateField HeaderText="编辑">
              <ItemTemplate>
                 <asp:HyperLink ID="hlTask" CssClass="link" NavigateUrl='<%#"OM_SHENHE_Detail.aspx?msid="+Eval("ID")+"&action=modify" %>' runat="server">
                   <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                          修改                               
                 </asp:HyperLink>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" Wrap="false" />
           </asp:TemplateField>
      </Columns>
       <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
      </asp:GridView>
      <uc1:UCPaging ID="UCPaging1" runat="server" />
   </div>
 </div>       
</asp:Content> 
