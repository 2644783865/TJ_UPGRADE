<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/masters/RightCotentMaster.Master" CodeBehind="CM_Bus_Contract.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Bus_Contract1" %>

<%@ Register src="~/Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

    <asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
   <%-- <div class="RightContentTop">
        <asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" /><div class="RightContentTitle"><table width=100%><tr><td width="15"><img  class=CloseImage title="关闭左边管理菜单" src="../assets/images/bar_hide.gif" style="CURSOR: hand" onClick="switchBar(this)"  border="0" alt=""></td><td>
            投标基本信息管理</td>
            <td width="15"><img src="../assets/images/bar_up.gif" title="隐藏" style="CURSOR: hand" onClick="switchTop(this)"  border="0" alt=""></td></tr></table></div>
    </div>--%>
 
    <div class="RightContent">      
    <div class="box-inner"><div class="box_right"><div class='box-title'><table width=100%>
    <tr>  <td>修改 / 删除投标</td>
          <td>分类查询：<asp:DropDownList ID="ddlClassify" runat="server" AutoPostBack ="true"
              onselectedindexchanged="ddlClassify_SelectedIndexChanged">
              <asp:ListItem Text="全部" Value="*"></asp:ListItem>
              <asp:ListItem Text="投标类型" Value="BP_BIDTYPE"></asp:ListItem>
              <asp:ListItem Text="业主ID" Value="BP_CUSTMID"></asp:ListItem>
              <asp:ListItem Text="商务负责人" Value="BP_BSCGCLERK"></asp:ListItem>
              <asp:ListItem Text="技术负责人" Value="BP_TCCGCLERK"></asp:ListItem>
              <asp:ListItem Text="目前状态" Value="BP_STATUS"></asp:ListItem>
              </asp:DropDownList>
              <asp:DropDownList ID="ddlDetails" runat="server" AutoPostBack="True" onselectedindexchanged="ddlDetails_SelectedIndexChanged">
              </asp:DropDownList>
          </td>
          <td align=right>
             <asp:HyperLink ID="HyperLink3" NavigateUrl="~/CM_Data/CM_Bus_ContractDetail.aspx?action=add" runat="server"><asp:Image ID="Image2" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2" align="absmiddle" runat="server" />添加投标</asp:HyperLink></td>
          </tr>
          </table></div></div></div>
          <div class="box-wrapper">
          <div class="box-outer">
          <table width='100%' align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate >
                <tr align="center" class="tableTitle"> 
                    <td ><strong>投标类型</strong></td> 
                    <td><strong>项目名称</strong></td>
                    <td ><strong>业主联系人</strong></td>                  
                    <td><strong>业主联系方式</strong></td>
                     <td><strong>商务负责人</strong></td>
                    <td ><strong>投标/报价日期</strong></td> 
                    <td ><strong>目前状态</strong></td>   
                    <td ><strong>详情信息</strong></td>                           
                    <td><strong>备注</strong></td>
                    <td><strong>修改</strong></td>  
                    <td><strong>删除</strong></td>     
                 </tr>
                </HeaderTemplate>
                 <ItemTemplate>  
                
             <tr class="baseGadget" onMouseOver="this.className='highlight'" onMouseOut="this.className='baseGadget'">
             <asp:Label ID="bp_ID"  runat="server" Visible="false"  Text='<%#Eval("BP_ID")%>'></asp:Label>
             <td ><%#Eval("BP_BIDTYPE")%>&nbsp;</td>
             <td ><%#Eval("BP_PRONAME")%>&nbsp;</td>
             <td ><%#Eval("BP_CUSTMID")%>&nbsp;</td>
              <td ><%#Eval("BP_TCCGCLERK")%>&nbsp;</td>
             <td ><%#Eval("BP_BSCGCLERK")%>&nbsp;</td>            
             <td ><%#Eval("BP_BIDDATE")%>&nbsp;</td>
             <td ><asp:Label ID="lbstate" runat="server" Text='<%#Eval("BP_STATUS")%>'></asp:Label>&nbsp;</td>
             <td><asp:HyperLink ID="HyperLink2" NavigateUrl='<%#"CM_Bus_ContractChaKan.aspx?id="+Eval("BP_ID") %>' runat="server"> 点击查看详细信息</asp:HyperLink></td> 
             <td ><%#Eval("BP_NOTE")%>&nbsp;</td>
             <td><asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"CM_Bus_ContractDetail.aspx?action=update&id="+Eval("BP_ID") %>'  runat="server" ><asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                 修改</asp:HyperLink>
             </td> 
             
             <td><asp:CheckBox ID="CheckBox1" runat="server" /></td></tr>
             </ItemTemplate> 
             </asp:Repeater>
               
          </table>
             <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red" >
                    没有记录!</asp:Panel>
             <asp:Label ID="lbl_Info" runat="server" Text=""></asp:Label>
             <div style=" text-align:right;"> <asp:Button ID="delete" runat="server" Text="删除" 
                  onclick="delete_Click" /></div><div><uc1:UCPaging ID="UCPaging1" runat="server" /></div> 
             </div> </div> </div>
   <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
       </asp:Content>
