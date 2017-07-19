<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Notice_Main.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Notice_Main" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="~/Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
联系单管理 
</asp:Content> 
 <asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
       
  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
  </asp:ToolkitScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <ContentTemplate> 
    <div class="box-wrapper">
  <div style="height: 6px" class="box_top"></div>
  <div class="box-outer">
      <table width="96%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
      <tr>
        <td>分类查询：</td>
        <td align="right"> 
            <asp:Label ID="Label3" runat="server" Text="按项目编号："></asp:Label>
        </td>
        <td style="height:42px" valign="top">
             <asp:ComboBox ID="ComboBox2" runat="server" AutoCompleteMode="SuggestAppend" 
                 Width="120px" DropDownStyle="DropDownList"  AutoPostBack="true"
                 onselectedindexchanged="ComboBox2_SelectedIndexChanged">
             </asp:ComboBox>
        </td>
        
        <td align="right"> 
            <asp:Label ID="Label4" runat="server" Text="按生产制号："></asp:Label>
        </td>
        <td style="height:42px" valign="top">
             <asp:ComboBox ID="ComboBox1" runat="server" AutoCompleteMode="SuggestAppend" 
                 Width="150px" DropDownStyle="DropDownList"  AutoPostBack="true"
                 onselectedindexchanged="ComboBox1_SelectedIndexChanged">
             </asp:ComboBox>
        </td>
                    
        <td>
            <asp:Label ID="Label2" runat="server" Text="按所属类别："></asp:Label>
            <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ddlstatus_SelectedIndexChanged" >
                 <asp:ListItem Value="0" Selected="True">全部</asp:ListItem>
                 <asp:ListItem Value="1">合同信息</asp:ListItem>
                 <asp:ListItem Value="2">任务单信息</asp:ListItem>
                 <asp:ListItem Value="3">其他信息</asp:ListItem>                                   
            </asp:DropDownList>
        </td>
        
        <td>
            <asp:Label ID="Label1" runat="server" Text="按部门人员："></asp:Label>
            <asp:DropDownList ID="ddlpersons" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ddlpersons_SelectedIndexChanged" >                                 
            </asp:DropDownList>
        </td>
                           
        <td width="30%" align="right">
            <asp:HyperLink ID="hpTask"  NavigateUrl="TM_Contact_List.aspx?action=add"  runat="server">
            新建联系单</asp:HyperLink>
        </td>
     </tr>
     </table>
            
            
    <table width='100%' align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
    <asp:Repeater ID="Repeater1" runat="server"  >
    <HeaderTemplate >
         <tr align="center" class="tableTitle">                  
            <td><strong>联系单编号</strong></td>
            <td ><strong>项目名称</strong></td>
            <td ><strong>工程名称</strong></td>   
            <%--<td ><strong>项目编号</strong></td>--%>                     
            <td><strong>所属类别</strong></td>
            <td><strong>编制人</strong></td> 
            <td><strong>编制时间</strong></td> 
            <td><strong>修改</strong></td>                         
            <td><strong>详细信息</strong></td>         
          </tr>
    </HeaderTemplate>
                
    <ItemTemplate>         
      <tr class="baseGadget" onMouseOver="this.className='highlight'" onMouseOut="this.className='baseGadget'" onDblClick="javascript:var win=window.open('user_show.aspx?id=','员工详细信息','width=853,height=470,top=176,left=161,toolbar=no,location=no,directories=no,status=no,menubar=no,resizable=no,scrollbars=yes'); win.focus()">
        <asp:Label ID="BSC_ID"  runat="server" Visible="false"  Text='<%#Eval("DCS_ID")%>'></asp:Label>
                <td ><%#Eval("DCS_ID")%>&nbsp;</td>               
                <td ><%#Eval("DCS_PROJECT")%>&nbsp;</td>
                <td ><%#Eval("DCS_ENGNAME")%>&nbsp;</td>              
                <%--<td><%#Eval("DCS_PROJECTID")%> &nbsp;</td>--%> 
                <td>
                    <asp:Label ID="BCSTYPE" runat="server" Text='<%#Eval("DCS_TYPE")%>' ></asp:Label>
                </td> 
                <td><%#Eval("DCS_EDITOR")%> &nbsp;</td>
                 <td><%#Eval("DCS_DATE")%> &nbsp;</td> 
                 <td>
                 <asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"TM_Contact_List.aspx?action=update&id="+Eval("DCS_ID") %>'  runat="server" >
                 <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                     修改</asp:HyperLink>
                 </td> 
                 <td>
                 <asp:HyperLink ID="HyperLink2" NavigateUrl='<%#"TM_Contact_List.aspx?action=view&id="+Eval("DCS_ID") %>'  runat="server" >
                 <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                     查看详细</asp:HyperLink>
                 </td>
         </tr>
    </ItemTemplate>
    </asp:Repeater>
     </table>
    
    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red" >
        没有记录!
    </asp:Panel>
    <asp:Label ID="lbl_Info" runat="server" Text=""></asp:Label>
    <div style=" text-align:right;"> </div>
    <div><uc1:UCPaging ID="UCPaging1" runat="server" /></div> 
  </div> </div> 
</ContentTemplate>
 </asp:UpdatePanel>
  </asp:Content>

