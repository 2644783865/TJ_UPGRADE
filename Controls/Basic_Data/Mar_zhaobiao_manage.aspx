<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mar_zhaobiao_manage.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.Mar_zhaobiao_manage" MasterPageFile="~/masters/RightCotentMaster.Master"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="~/Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
 <asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    招标物料信息管理
    </asp:Content>
    <asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
     <div class="RightContent>      
      <div class="box-inner"><div class="box_right"><div class=box-title><table width=100%><tr><td width="10%">
             修改 / 删除招标物料</td> 
            
                         
                 <td align="left" width="13%">
                    <asp:RadioButtonList ID="rblZT" RepeatColumns="2" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="rblZT_SelectedIndexChanged">
                    <asp:ListItem Value="1">停用</asp:ListItem>
                    <asp:ListItem Selected="True" Value="0">在用</asp:ListItem>
                    </asp:RadioButtonList>
                </td>            
               <td width="23%">                   
                 <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="../Assets/icon-fuction/388.gif" border="0" hspace="2" 
                                        align="absmiddle" runat="server" />筛选</asp:HyperLink>
                     <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-50"  OffsetY="8"  TargetControlID="HyperLink1" PopupControlID="palORG">
                     </asp:PopupControlExtender>
                      <asp:Panel ID="palORG" Width="400px" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>  
                     <table width="100%" >
                     <tr>       
                     <td>
                          <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                              <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                          </div>
                          <br /><br />
                     </td>
                     </tr>
                     <tr>
                         <td >编码:</td>
                          <td >
                              <asp:TextBox ID="txt_ID" runat="server"></asp:TextBox>
                        </td> 
                         <td >名称:</td>
                          <td >
                              <asp:TextBox ID="txt_NAME" runat="server" ></asp:TextBox>
                        </td> 
                    </tr>
                      <tr>
                         <td>规格:</td>
                          <td>
                              <asp:TextBox ID="txt_GG" runat="server"></asp:TextBox>
                        </td>
                         <td>材质:</td>
                          <td>
                             <asp:TextBox ID="txt_ZJM" runat="server"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                       <td>供应商:</td>
                          <td>
                              <asp:TextBox ID="txt_CZ" runat="server"></asp:TextBox>
                        </td>
                         <td>价格:</td>
                          <td>
                              <asp:TextBox ID="txt_GB" runat="server"></asp:TextBox>
                        </td>
                      </tr>
                     <tr>
                         <td colspan="4" align="center">
                             <asp:Button ID="btn_confirm" runat="server" Text="查询"  OnClick="btn_confirm_Click"/>&nbsp;&nbsp; 
                             <asp:Button ID="btn_clear" runat="server" Text="置空"  OnClick="btn_clear_Click"/>
                         </td>
                     </tr>
                     </table>
                      </ContentTemplate>
                    </asp:UpdatePanel>
                    </asp:Panel> 
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     每页显示：<asp:DropDownList ID="ddl_pageno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                  </asp:DropDownList>&nbsp;条记录
        </td>
             <td align="right">
             <asp:HyperLink ID="HyperLink3" NavigateUrl="javascript:void window.open('Mar_zhaobiao_manageDetail.aspx?action=add','','');" runat="server">
             <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2" align="absmiddle" runat="server" />
             添加招标物料</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr></table></div></div></div>
            <div class="box-wrapper">
            <div class="box-outer">
          <table width=100% align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
    <asp:Repeater ID="Reproject1" runat="server" >
    <HeaderTemplate >    
                 <tr align="center" class="tableTitle">             
                    
                    <td><strong>物料编码</strong></td>     
                    <td><strong>物料名称</strong></td> 
                     <td><strong>材质</strong></td>     
                    <td><strong>规格</strong></td>    
                    <td><strong>供应商</strong></td>
                    <td><strong>价格</strong></td> 
                   <%-- <td><strong>长度</strong></td>
                    <td><strong>宽度</strong></td> --%>
                    <td><strong>起始日期</strong></td> 
                    <td><strong>截止日期</strong></td> 
                    <td><strong>备注</strong></td>  
                     <td><strong>修改</strong></td> 
                     <td><strong></strong></td> 
              </tr>
                </HeaderTemplate>                
                <ItemTemplate>
             <tr class="baseGadget" onMouseOver="this.className='highlight'" onMouseOut="this.className='baseGadget'">                 
               <asp:Label ID="IB_ID"  runat="server" Visible="false" Text='<%#Eval("IB_ID")%>'></asp:Label>                     
               
                <td ><%#Eval("IB_MARID")%>&nbsp;</td>
                 <td ><%#Eval("MNAME")%>&nbsp;</td> 
                 <td ><%#Eval("CAIZHI")%>&nbsp;</td>
                 <td ><%#Eval("GUIGE")%>&nbsp;</td>
               <td ><%#Eval("CS_NAME")%>&nbsp;</td> 
                <td ><%#Eval("IB_PRICE")%>&nbsp;</td> 
                 <%--<td><%#Eval("IB_LENGTH")%> &nbsp;</td>  
                   <td><%#Eval("IB_WIDTH")%> &nbsp;</td> --%>
                   <td><%#Eval("IB_DATE")%> &nbsp;</td>  
                   <td><%#Eval("IB_FIDATE")%> &nbsp;</td> 
                   <td><%#Eval("IB_NOTE")%> &nbsp;</td>                     
                 <td><asp:HyperLink ID="HyperLink1" NavigateUrl='<%# editZb(Eval("IB_MARID").ToString(),Eval("IB_ID").ToString()) %>'  runat="server" >
                 <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                     修改</asp:HyperLink></td>
                 <td><asp:CheckBox ID="CheckBox1" runat="server"/></td>
                </tr>
            </ItemTemplate> 
    </asp:Repeater>
    </table>
    <asp:Panel ID="NoDataPanel" runat="server" ForeColor="Red">没有记录!</asp:Panel>
    <asp:Label ID="lbl_Info" runat="server" Text="Label" Visible="False"></asp:Label>
       <div style=" text-align:right;padding-top:5px;padding-right:15px"> 
           <asp:Button ID="delete" runat="server" Text="删除" onclick="delete_Click" /></div>
       <div><uc1:UCPaging ID="UCPaging1" runat="server" /></div>
  </div> </div> </div>
  </asp:Content>