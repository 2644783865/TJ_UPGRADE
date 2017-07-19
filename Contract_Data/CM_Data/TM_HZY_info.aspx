<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_HZY_info.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.TM_HZY_info" Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="~/Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    任务单管理       
</asp:Content> 

    <asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        
     <div class="RightContent"> 
             
      <table width="100%">
           <tr>
              <td></td>
                 <td align="right">按项目编号:</td>
                 <td style="height:42px" valign="top">
                    <asp:ComboBox ID="ComboBox1" runat="server" DropDownStyle="DropDownList"
                            AutoCompleteMode="SuggestAppend" Width="100px"
                       onselectedindexchanged="ComboBox1_SelectedIndexChanged"  AutoPostBack="true">
                    </asp:ComboBox>
                 </td>
                <td>按分类：
                <asp:DropDownList ID="ddl_type" runat="server"  AutoPostBack="true"
                        onselectedindexchanged="ddl_type_SelectedIndexChanged">
                    <asp:ListItem Value="">-全部-</asp:ListItem>
                    <asp:ListItem Value="0">项目部</asp:ListItem>
                    <asp:ListItem Value="1">中财重机</asp:ListItem>
                    <asp:ListItem Value="2">港口</asp:ListItem>
                </asp:DropDownList>       
                </td>
                <td>
                    按状态：
                    <asp:DropDownList ID="ddl_state" runat="server" AutoPostBack="true"
                    onselectedindexchanged="ddl_state_SelectedIndexChanged">
                    <asp:ListItem Value="">-全部-</asp:ListItem>
                    <asp:ListItem Value="0">正在执行</asp:ListItem>
                    <asp:ListItem Value="1">已结算</asp:ListItem>
                    <asp:ListItem Value="2">未分类</asp:ListItem>
                    
                    </asp:DropDownList>
                </td>
                <td width="30%" align="right"><asp:HyperLink ID="hpTask"  NavigateUrl="CM_task_detail.aspx?action=add"  runat="server">新建任务单</asp:HyperLink></td>
            </tr>
         </table>
            
         <div class="box-wrapper">
            <div class="box-outer">
            
             <asp:Label ID="ControlFinder" runat="server" Text="" Visible="false"></asp:Label>
            
            
              <table width='100%' align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
              
                 <asp:Repeater ID="Repeater1" runat="server" onitemdatabound="Repeater1_ItemDataBound"  >
                    
                    <HeaderTemplate >
                     <tr align="center" class="tableTitle">   
                        <td><strong>序号</strong></td>         
                        <td><strong>生产任务编号</strong></td>
                        <td ><strong>项目名称</strong></td>                     
                        <td><strong>业主经办人</strong></td>                          
                        <td><strong>计划完成时间</strong></td>
                        <td ><strong>负责部门</strong></td>
                        <td ><strong>结算金额</strong></td> 
                        <td ><strong>是否结算</strong></td>     
                        <td><strong>结算</strong></td>   
                          <td><strong>修改</strong></td>                      
                        <td><strong>详细信息</strong></td>         
                      </tr>
                    </HeaderTemplate>
                    
                    <ItemTemplate> 
                     <tr class="baseGadget" onMouseOver="this.className='highlight'" onMouseOut="this.className='baseGadget'">
                          <asp:Label ID="PT_CODE"  runat="server" Visible="false"  Text='<%#Eval("PT_CODE")%>'></asp:Label>
                          
                          <td> <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.ItemIndex +1) %>'></asp:Label>
                          <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"/></td>
                              
                          <td ><%#Eval("PT_CODE")%>&nbsp;</td>               
                          <td ><%#Eval("PT_PJNAME")%>&nbsp;</td>              
                          <td><%#Eval("PT_PMCHARGERNM")%> &nbsp;</td>
                          <td><%#Eval("PT_DATE")%> &nbsp;</td>
                          <td><%#Eval("PT_ICKCOMNM")%> &nbsp;</td>
                          <td><%#Eval("PT_JSJE")%> &nbsp;</td>
                          <td>
                              <asp:Label ID="lb_isjs" runat="server" Text='<%#Eval("PT_ISJS").ToString()=="0"?"否":Eval("PT_ISJS").ToString()=="1"?"是":""%>'></asp:Label></td>
                              
                          <td><asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"CM_task_detail.aspx?action=js&id="+Eval("PT_CODE") %>'  runat="server" ><asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                             结算</asp:HyperLink>
                         </td> 
                         <td><asp:HyperLink ID="HyperLink3" NavigateUrl='<%#"CM_task_detail.aspx?action=update&id="+Eval("PT_CODE") %>'  runat="server" ><asp:Image ID="Image1" ImageUrl="~/Assets/images/create.gif"  border="0" hspace="2" align="absmiddle" runat="server" />
                             修改</asp:HyperLink>
                         </td> 
                         <td><asp:HyperLink ID="HyperLink2" NavigateUrl='<%#"CM_task_detail.aspx?action=view&id="+Eval("PT_CODE") %>'  runat="server" ><asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                             查看详细</asp:HyperLink></td>
                      </tr>
                  </ItemTemplate>
                </asp:Repeater>
             </table>
             <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red" >没有记录!</asp:Panel>
                <asp:Button ID="btn_del" runat="server" Text="删 除" onclick="btn_del_Click" 
                    onclientclick="return confirm('你确定删除吗?');" />
            <asp:Label ID="lbl_Info" runat="server" Text=""></asp:Label>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
      </div>
  </div>
  </asp:Content>
