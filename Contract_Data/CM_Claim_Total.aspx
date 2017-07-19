<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="CM_Claim_Total.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Claim_Total" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %> 
<asp:Content ID="Content1" runat="server" contentplaceholderid="RightContentTitlePlace">
  合同索赔  
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<style type="text/css"> 
     .autocomplete_completionListElement 
     {  
     	margin : 0px; 
     	background-color : #1C86EE; 
     	color : windowtext; 
     	cursor : 'default'; 
     	text-align : left; 
     	list-style:none; 
     	padding:0px;
        border: solid 1px gray; 
        width:400px!important;   
     }
     .autocomplete_listItem 
     {   
     	border-style : solid; 
     	border :#FFEFDB; 
     	border-width : 1px;  
     	background-color : #EEDC82; 
     	color : windowtext;  
     } 
     .autocomplete_highlightedListItem 
     { 
     	background-color: #1C86EE; 
     	color: black; 
     	padding: 1px; 
     } 
  </style> 
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />    
    <div class="RightContent">
  <div class="box-wrapper"> 
    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
<table width="100%">
    <tr>
    <td>索赔信息</td>
    <td style="vertical-align:middle; text-align:right;">
        <asp:HyperLink ID="hplSPHZ" runat="server" NavigateUrl="~/Contract_Data/CM_Claim_Summary.aspx" Visible="false" >
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Assets/icons/forum.gif"  />查看汇总信息
        </asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="hylSPReason" runat="server" NavigateUrl="~/Contract_Data/CM_Claim_Analysis.aspx" Visible="false" >
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/icons/gadgets.gif"  />索赔原因分析</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="hylAddClaim" CssClass="hand" runat="server"><asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/icons/add.gif" />添加索赔信息</asp:HyperLink>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:PopupControlExtender ID="PopupControlExtender1" runat="server"  TargetControlID="hylAddClaim" PopupControlID="palSPLB" Position="Bottom" OffsetY="4" OffsetX="-85">
        </asp:PopupControlExtender><br />
        
        </td>
    </tr>
    </table> 
    <asp:Panel ID="palSPLB" style="visibility:hidden; width:180px; border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>  
         <div style=" font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:5px;right: 10px;">
                      <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                  </div>
                  <br /><br />  
             <table style="width:100%; background-color:ThreeDHighlight;">
         <tr>
         <td align="left">索赔类别：</td>
         <td>
             <asp:DropDownList ID="dplSPLB_Select" runat="server">
              <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
              <asp:ListItem Text="业主&rarr;重机" Value="0"></asp:ListItem>
              <asp:ListItem Text="重机&rarr;业主" Value="1"></asp:ListItem>
             <asp:ListItem Text="重机&rarr;分包商" Value="2"></asp:ListItem>
             <asp:ListItem Text="重机&rarr;供应商" Value="3"></asp:ListItem>
            <asp:ListItem Text="分包商&rarr;重机" Value="4"></asp:ListItem>
             <asp:ListItem Text="供应商&rarr;重机" Value="5"></asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp;
         </td>
         </tr>
         <tr>
         <td align="center" colspan="2">
         <br />
             <asp:Button ID="btnConfrim" runat="server" Text="确 定" OnClick="btnConfrim_onClick" /> <br /></td>
         </tr>       
         </table></ContentTemplate></asp:UpdatePanel>
        </asp:Panel>   
</div>
</div>
</div>

        <div class="box-wrapper" >
        <div class="box-outer" >
        <table>
        <tr>
        <td>项目：<asp:TextBox ID="tb_pjinfo" runat="server" Width="100px" OnTextChanged="tb_pjinfo_Textchanged" AutoPostBack="True"></asp:TextBox>
            <asp:TextBox ID="tb_pjid" runat="server" Visible="false"></asp:TextBox>
            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_pjinfo"
                   ServicePath="~/Ajax.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                   ServiceMethod="GetPJNAME" FirstRowSelected="true"  CompletionListCssClass="autocomplete_completionListElement"
                 CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                </asp:AutoCompleteExtender>
           </td> <td>           
            索赔类别：<asp:DropDownList ID="dplSPLB" runat="server" AutoPostBack="true"
                         onselectedindexchanged="btnQuery_onClick">
                        <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                        <asp:ListItem Text="业主&rarr;重机" Value="0"></asp:ListItem>
                        <asp:ListItem Text="重机&rarr;业主" Value="1"></asp:ListItem>
                        <asp:ListItem Text="重机&rarr;分包商" Value="2"></asp:ListItem>
                        <asp:ListItem Text="重机&rarr;供应商" Value="3"></asp:ListItem>
                        <asp:ListItem Text="分包商&rarr;重机" Value="4"></asp:ListItem>
                        <asp:ListItem Text="供应商&rarr;重机" Value="5"></asp:ListItem>
                        </asp:DropDownList></td> <td> 
           受理部门：<asp:DropDownList ID="dplSLBM" runat="server"  AutoPostBack="true" onselectedindexchanged="btnQuery_onClick">
                            <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="市场部" Value="1"></asp:ListItem>
                            <asp:ListItem Text="技术部" Value="2"></asp:ListItem>
                            <asp:ListItem Text="生产部" Value="3"></asp:ListItem>
                            <asp:ListItem Text="采购部" Value="4"></asp:ListItem>
                            <asp:ListItem Text="质量部" Value="5"></asp:ListItem>
                    </asp:DropDownList></td> <td> 
           是否扣款：<asp:DropDownList ID="dplYN" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" AutoPostBack="true"
                    onselectedindexchanged="btnQuery_onClick">
                        <asp:ListItem Text="全部" Value="2" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="未扣款" Value="1"></asp:ListItem>
                        <asp:ListItem Text="已扣款" Value="0"></asp:ListItem>
                    </asp:DropDownList> </td> <td> 
        合同号/索赔单位：<asp:TextBox ID="txtSEARCHBOX" runat="server" Width="150px"></asp:TextBox>
             <asp:Button ID="btnQuery" runat="server" Text="查 询"  OnClick="btnQuery_onClick" />
             &nbsp;<asp:Button ID="btnReset" runat="server" Text="重 置" OnClick="btnReset_onClick"/>
        </td>
        </tr>
        </table>   
     
         <asp:Panel ID="PanelBody" runat="server"  style="overflow:auto; position:relative; margin:2px" Width="100%">
    <yyc:SmartGridView ID="grvTJ" width="100%" CssClass="toptable grid" runat="server" OnRowDataBound="grvTJ_RowDataBound"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  ShowFooter="true" >
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                  <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label> 
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="项目名称" DataField="XMMC" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="合同编号" DataField="HTBH" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="索赔单号" DataField="SPDH" ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="客户（供应商）" DataField="GYS" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="索赔金额" DataField="SPJE" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="最终索赔金额" DataField="ZZSPJE" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                 
                <asp:TemplateField HeaderText="受理部门" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("BM").ToString()=="1"?"市场部":Eval("BM").ToString()=="2"?"技术部":Eval("BM").ToString()=="3"?"生产部":Eval("BM").ToString()=="4"?"采购部":"质量部" %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="索赔类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("SPLB").ToString()=="0"?"重机&larr;业主":Eval("SPLB").ToString()=="1"?"重机&rarr;业主":Eval("SPLB").ToString()=="2"?"重机&rarr;分包商":Eval("SPLB").ToString()=="3"?"重机&rarr;供应商":Eval("SPLB").ToString()=="4"?"重机&larr;分包商":"重机&larr;供应商" %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="是否回复" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" Visible="false">
                    <ItemTemplate>
                    <asp:CheckBox ID="CheckBox2" Font-Bold="true" runat="server" Enabled="false" Checked='<%# Eval("SFHF").ToString()=="1"?false:true %>' />
                </ItemTemplate>  
              </asp:TemplateField>
                <asp:TemplateField HeaderText="是否扣款" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox3" Enabled="false" Checked='<%# Eval("SFKK").ToString()=="1"?false:true %>' runat="server" />
                </ItemTemplate>
                </asp:TemplateField>               
               <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">                    
                    <ItemTemplate>
                        <asp:HyperLink ID="hlContract_edit" CssClass="link" 
                         NavigateUrl='<%#Eval("SPLB").ToString()=="0"?"CM_Claim_YZ.aspx?Action=Edit&Splb="+Eval("SPLB").ToString()+"&Spid="+Eval("SPDH").ToString():Eval("SPLB").ToString()=="1"?"CM_Claim_ZJYZ.aspx?Action=Edit&Splb="+Eval("SPLB").ToString()+"&Spid="+Eval("SPDH").ToString():(Eval("SPLB").ToString()=="2"||Eval("SPLB").ToString()=="3")?"CM_Claim_ZJFBS.aspx?Action=Edit&Splb="+Eval("SPLB").ToString()+"&Spid="+Eval("SPDH").ToString():"CM_Claim_FBS.aspx?Action=Edit&Splb="+Eval("SPLB").ToString()+"&Spid="+Eval("SPDH").ToString()%>' runat="server">
                        <asp:Image ID="Image_edit" ImageUrl="~/Assets/images/modify.gif" 
                         runat="server" />                                
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">                    
                    <ItemTemplate>
                        <asp:HyperLink ID="hlContract_view" CssClass="link" 
                        NavigateUrl='<%#Eval("SPLB").ToString()=="0"?"CM_Claim_YZ.aspx?Action=View&Splb="+Eval("SPLB").ToString()+"&Spid="+Eval("SPDH").ToString():Eval("SPLB").ToString()=="1"?"CM_Claim_ZJYZ.aspx?Action=View&Splb="+Eval("SPLB").ToString()+"&Spid="+Eval("SPDH").ToString():(Eval("SPLB").ToString()=="2"||Eval("SPLB").ToString()=="3")?"CM_Claim_ZJFBS.aspx?Action=View&Splb="+Eval("SPLB").ToString()+"&Spid="+Eval("SPDH").ToString():"CM_Claim_FBS.aspx?Action=View&Splb="+Eval("SPLB").ToString()+"&Spid="+Eval("SPDH").ToString()%>' runat="server">
                        <asp:Image ID="Image_view" ImageUrl="~/Assets/images/search.gif" 
                         runat="server" />                                
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" HorizontalAlign="Right"/>
    <RowStyle BackColor="#EFF3FB"/>
    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False"/>
     <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2" />     
    </yyc:SmartGridView>
    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">没有记录!</asp:Panel>
    </asp:Panel>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
     </div> 
   </div>
   </div>
</asp:Content>
