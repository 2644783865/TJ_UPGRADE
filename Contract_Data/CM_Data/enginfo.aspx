<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/masters/BaseMaster.master" CodeBehind="enginfo.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.enginfo" %>

<%@ Register src="~/Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

    <asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="RightContentTop">
        <asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" /><div class="RightContentTitle"><table width=100%><tr><td width="15"><img  class=CloseImage title="关闭左边管理菜单" src="../assets/images/bar_hide.gif" style="CURSOR: hand" onClick="switchBar(this)"  border="0" alt=""></td><td>
            工程基本信息管理</td>
            <td width="15"><img src="../assets/images/bar_up.gif" title="隐藏" style="CURSOR: hand" onClick="switchTop(this)"  border="0" alt=""></td></tr></table></div>
    </div>
 
     <div class="RightContent>      
      <div class="box-inner"><div class="box_right"><div class=box-title><table width=100%><tr><td>
             修改 / 删除工程</td>
              <td>按项目名称查询：<asp:DropDownList ID="dllENG_PJNAME" runat="server" AutoPostBack="true" 
                      onselectedindexchanged="dllENG_PJNAME_SelectedIndexChanged">
              <%--<asp:ListItem Text="全部" Value="全部"></asp:ListItem>
              <asp:ListItem Text="进行中" Value="0"></asp:ListItem>
              <asp:ListItem Text="完工" Value="1"></asp:ListItem>
              <asp:ListItem Text="停工" Value="2"></asp:ListItem>--%>
              </asp:DropDownList>
              </td>
               <td>按工程状态查询：<asp:DropDownList ID="dllENG_STA" runat="server" AutoPostBack="true"
               onselectedindexchanged="dllENG_STA_SelectedIndexChanged" >
              <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
              <asp:ListItem Text="进行中" Value="0"></asp:ListItem>
              <asp:ListItem Text="完工" Value="1"></asp:ListItem>
              <asp:ListItem Text="停工" Value="2"></asp:ListItem>
              </asp:DropDownList>
              </td>
             <td align="right">
             <asp:HyperLink ID="HyperLink3" NavigateUrl="~/CM_Data/enginfoDetail.aspx?action=add" runat="server">添加工程</asp:HyperLink></td></tr></table></div></div></div>
            <div class="box-wrapper">
            <div class="box-outer">
          <table width=100% align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
    <asp:Repeater ID="Reproject1" runat="server" >
    <HeaderTemplate >
    
                 <tr align="center" class="tableTitle">              
                    <td><strong>工程名称</strong></td>
                 <td ><strong>项目编号</strong></td>
                  <td ><strong>项目名称</strong></td>
                    <td><strong>工程全名</strong></td>                          
                 <%--   <td><strong>工程编号</strong></td>--%>
                    <td ><strong>开始日期</strong></td> 
                    <td ><strong>交工日期</strong></td>   
                    <td ><strong>实际完成日期</strong></td>   
                <%--    <td ><strong>项目状态</strong></td>   --%>
                <td ><strong>工程状态</strong></td>                       
                    <td><strong>备注</strong></td>  
                    <td><strong>查看</strong></td>
              <%--       <td><strong>删除</strong></td>      --%>         
                  </tr>
                </HeaderTemplate>
                
                <ItemTemplate>  
                
             <tr class="baseGadget" onMouseOver="this.className='highlight'" onMouseOut="this.className='baseGadget'" onDblClick="javascript:var win=window.open('user_show.aspx?id=','员工详细信息','width=853,height=470,top=176,left=161,toolbar=no,location=no,directories=no,status=no,menubar=no,resizable=no,scrollbars=yes'); win.focus()">
                 
               <asp:Label ID="eng_ID"  runat="server" Visible="false" Text='<%#Eval("ENG_ID")%>'></asp:Label>
               <%--  <td ><%#Eval("PJ_ID")%>&nbsp</td>    --%>           
                <td ><%#Eval("ENG_NAME")%>&nbsp;</td>
               <td ><%#Eval("ENG_PJID")%>&nbsp;</td> 
                <td ><%#Eval("ENG_PJNAME")%>&nbsp;</td> 
                 <td><%#Eval("ENG_FULLNAME")%> &nbsp;</td>         
                 <%-- <td><%#Eval("ENG_CODE")%> &nbsp;</td>--%>
                 <td ><%#Eval("ENG_STARTDATE")%>&nbsp;</td>
                <td><%#Eval("ENG_CONTRACTDATE")%> &nbsp;</td>
                <td ><%#Eval("ENG_REALFINISHDATE")%>&nbsp;</td>
                <td ><asp:Label ID="lbstate" runat="server" Text='<%#Eval("ENG_STATE")%>'></asp:Label>
                    &nbsp;</td>
             <%--   <td><%#Eval("PJ_STA")%> &nbsp</td>--%>
                 <td ><%#Eval("ENG_NOTE")%>&nbsp;</td>                          
                 <td><asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"enginfoDetail.aspx?action=update&id="+Eval("ENG_ID")+"&&ENG_PJID="+Eval("ENG_PJID") %>'  runat="server" ><asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                     查看</asp:HyperLink></td>
             <%--    <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>--%>
                </tr>
            </ItemTemplate>
      
         
    </asp:Repeater>
    <asp:Panel ID="NoDataPanel" runat="server">
                没有记录!</asp:Panel>
    
    </table>
                <asp:Label ID="lbl_Info" runat="server" Text="Label" Visible="False"></asp:Label>
       <div style=" text-align:right;padding-top:5px;padding-right:15px"> 
           <asp:Button ID="delete" runat="server" Text="删除" onclick="delete_Click" /></div>
           
       <div><uc1:UCPaging ID="UCPaging1" runat="server" /></div>
        
           
  </div> </div> </div>
  </asp:Content>