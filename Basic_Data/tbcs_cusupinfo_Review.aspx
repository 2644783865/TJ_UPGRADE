<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="tbcs_cusupinfo_Review.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.tbcs_cusupinfo_Review" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="~/Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
     添加/停用/启用厂商信息-审批</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="PrimaryContent"> 
<script  type="text/javascript" language="javascript">
  
  function DefaultTextOnFocus(obj)
  {
     if(obj.value=="请输入厂商名称！")
     {
        obj.value="";
        obj.style.color="Black";
     }
     else
     {
       obj.style.color="Black";
     }
  }
  
  function DefaultTextOnBlur(obj)
  {
     if(obj.value=="")
     {
        obj.value="请输入厂商名称！";
        obj.style.color="Gray";
     }
  }
</script> 
     <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>    
  <div class="RightContent"> 
  
      <div class="box-inner">
        <div class="box_right">
          <div class="box-title">     
          <table width="100%">
          <tr>
            <td style="text-align:center">                     
          <asp:RadioButtonList ID="rblZT" runat="server" RepeatColumns="6" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblZT_OnSelectedIndexChanged">
          <asp:ListItem Text="全部" Value="0"></asp:ListItem>
          <asp:ListItem Text="最近审批" Value="1"></asp:ListItem>
          <asp:ListItem Text="待审批" Value="2" Selected="True"></asp:ListItem>
          <asp:ListItem Text="审批中" Value="3"></asp:ListItem>
           <asp:ListItem Text="审批通过" Value="5"></asp:ListItem>
          <asp:ListItem Text="驳回" Value="4" ></asp:ListItem>
          </asp:RadioButtonList></td>
          <td>所属类型&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="dropdownlist1" runat="server" AutoPostBack="true" 
                  onselectedindexchanged="dropdownlist1_SelectedIndexChanged">
          <asp:ListItem Text="-请选择-" Value="0" Selected="True"></asp:ListItem>
           <asp:ListItem Text="客户" Value="1"></asp:ListItem>
           <asp:ListItem Text="采购供应商" Value="2"></asp:ListItem>
            <asp:ListItem Text="运输公司" Value="3"></asp:ListItem>
           <asp:ListItem Text="技术外协分包商" Value="4"></asp:ListItem>
           <asp:ListItem Text="生产外协分包商" Value="5"></asp:ListItem>
           <asp:ListItem Text="原材料销售供应商" Value="6"></asp:ListItem>
           <asp:ListItem Text="其它" Value="7"></asp:ListItem>   
          </asp:DropDownList></td>
          <td>
              名称：<asp:TextBox ID="txtCS_Name" ForeColor="Gray" runat="server"  onfocus="DefaultTextOnFocus(this);" onblur="DefaultTextOnBlur(this);" Width="180px"></asp:TextBox>
               &nbsp;&nbsp;  
                <asp:Button ID="Btn_Query" runat="server" Text="查 询" OnClick="Btn_Query_Click"/> 
                <td>
                <asp:Button ID="Btn_daochu" runat="server" Text="导 出" OnClick="btnExport_OnClick" OnClientClick="return confirm('确认导出吗？')"/>
                
                </td>     
            </td>
          </tr>
       </table> 
      </div>      
       </div> 
       </div>
 <div class="box-wrapper">
 <div class="box-outer">
   <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="rptTBCS_CUSUPINFO" runat="server"  OnItemDataBound="rptTBCS_CUSUPINFO_ItemDataBound">
        <HeaderTemplate >    
                 <tr align="center" >                  
                   <th><strong>序号</strong></th>
                  <%--<th ><strong>公司编号</strong></th> --%>
                    <th><strong>公司名称</strong></th>                          
                    <th><strong>所在地</strong></th>
                    <th ><strong>所属类型</strong></th>
                    <th><strong>供货范围</strong></th>
                    <th><strong>申请人</strong></th>
                    <th><strong>审批类别</strong></th>
                    <th><strong>审批状态</strong></th>
                    <th><strong>审批通过时间</strong></th>
                    <th id="tdView" runat="server"><strong>查看</strong></th>
                    <th id="tdReview" runat="server"><strong>审批</strong></th>
                    <th><strong>修改</strong></th>
                    <th><strong>删除</strong></th>
                  </tr>
                </HeaderTemplate>          
                <ItemTemplate>  
                
             <tr class="baseGadget" onmouseover="this.className='highlight'"  onclick="this.className='clickback'" 
             onmouseout="this.className='baseGadget'"  style="text-align:left">
                 
                 <td ><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>&nbsp;
                     <asp:Label ID="lbl_id" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                     <asp:Label ID="lbl_app_Per" runat="server" Text='<%#Eval("CS_MANCLERK")%>' Visible="false"></asp:Label>
                 <asp:Label ID="lblCS_CODE" runat="server"  Text='<%#Eval("CS_CODE")%>' Visible="false"></asp:Label></td>         
                 <td><%#Eval("CS_NAME")%> &nbsp;</td>
                 <td ><%#Eval("CS_LOCATION")%>&nbsp;</td>
                 <td ><%#Eval("CS_TYPE")%>&nbsp;</td>
                 <td><%#Eval("CS_Scope")%></td>                
                 <td><%#Eval("CS_MANCLERK")%></td>                
                  <td>
                      <asp:Label ID="lbl_action" runat="server" Text='<%#Eval("CS_ACTION").ToString()=="0"?"添加/启用":"停用"%>' ForeColor='<%#Eval("CS_ACTION").ToString()=="0"?System.Drawing.Color.Green :System.Drawing.Color.Red %>'></asp:Label>
                  </td> 
                  <td>
                      <asp:Label ID="lbl_spzt" runat="server" Text='<%#Eval("CS_SPJG").ToString()=="0"?"未审批":Eval("CS_SPJG").ToString()=="1"?"审批中":Eval("CS_SPJG").ToString()=="2"?"审批通过":"已驳回"%>'></asp:Label>
                  </td>
                  <td>
                      <asp:Label ID="lbl_passtime" runat="server" Text=""></asp:Label>
                  </td>
                  <td id="tdView" runat="server">
                  <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# viewSp(Eval("CS_ACTION").ToString(),Eval("ID").ToString()) %>'  runat="server" >
                  <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />查看
                     </asp:HyperLink>
                 </td>                   
                 <td id="tdReview" runat="server"><asp:HyperLink ID="HyperLink1" NavigateUrl='<%# reviewSp(Eval("CS_ACTION").ToString(),Eval("ID").ToString()) %>'  runat="server" >
                     <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />审核</asp:HyperLink>
                 </td>
                 <td>
                      <asp:HyperLink ID="Hlk_Edit" Visible="false"  NavigateUrl='<%# editSp(Eval("CS_ACTION").ToString(),Eval("ID").ToString()) %>'  runat="server" >
                      <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />修改
                      </asp:HyperLink>                     
                 </td>
                 <td>
                   <asp:LinkButton ID="Lbtn_Del" runat="server" ForeColor="Red" CommandArgument='<%#Eval("ID") %>' OnClick="lnkAction_OnClick"
                         OnClientClick="javascript:return confirm('确定要删除吗？');"  CommandName="del" >
                         <asp:Image ID="ImageVoid" ImageUrl="~/Assets/images/erase.gif" 
                         runat="server" />
                   </asp:LinkButton>
                 </td>
                </tr>
            </ItemTemplate>
    </asp:Repeater>
                  
    </table>
       <asp:Panel ID="NoDataPanel" runat="server"><p style="color:Red; text-align:center">
           没有记录! </p> 
                  </asp:Panel>
                  <uc1:UCPaging ID="UCPaging1" runat="server" />       
          <p style="text-align:center">
              <asp:Label ID="lblTotalNumber" runat="server" Text=""></asp:Label></p>
          </div>
          </div>
             </div>
</asp:Content>
