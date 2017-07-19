<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_Data_DYDdaochu.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_Data_DYDdaochu" MasterPageFile="~/Masters/PopupBase.Master"  Title="代用单批量导出"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
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
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
   </cc1:ToolkitScriptManager>
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
 <div>
    <table width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
    <tr>
    <td colspan="6" align="center" style="font-size:larger; color:Red;">
       采购代用单的批量导出
    </td>
    </tr>
     <tr>
        <td align="right">
            单据编号：
        </td>
        <td>
            <asp:TextBox ID="tb_orderno" runat="server"></asp:TextBox>
        </td>
        <td align="right">
            开始日期：
        </td>
        <td>
            <input type="text" ID="txtStartTime" runat="server" onclick="setday(this);"  readonly="readonly"  />
        </td>
        <td align="right">
            结束日期：
        </td>
        <td>
            <input type="text" ID="txtEndTime" runat="server" onclick="setday(this);"  readonly="readonly"  />
        </td>
    </tr>
    
   <tr>
    <td align="right">
        名称：
    </td>
    <td>
        <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
    </td>
    <td align="right">
         材质：
    </td>
     <td >
         <asp:TextBox ID="tb_cz" runat="server"></asp:TextBox>
    </td>
     <td align="right">
         规格：
    </td>
     <td >
         <asp:TextBox ID="tb_gg" runat="server"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right">
        项目：
    </td>
    <td >
        <asp:TextBox ID="tb_pj" runat="server" OnTextChanged="tb_pj_Textchanged" AutoPostBack="true"></asp:TextBox>
         <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="tb_pj"
           ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
           ServiceMethod="GetPJNAME" FirstRowSelected="true"  CompletionListCssClass="autocomplete_completionListElement"
         CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
        </cc1:AutoCompleteExtender>
    </td>
    <td align="right">
        工程：
    </td>
    <td>
        <asp:TextBox ID="tb_eng" runat="server" OnTextChanged="tb_eng_Textchanged" AutoPostBack="true"></asp:TextBox>
         <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="tb_eng"
           ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="20" MinimumPrefixLength="1" CompletionInterval="10"
           ServiceMethod="GetENGNAME" FirstRowSelected="true"  CompletionListCssClass="autocomplete_completionListElement"
          CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
        </cc1:AutoCompleteExtender>
    </td>
    <td align="right">
        制单人：
    </td>
    <td>
        <asp:DropDownList ID="drp_stu" runat="server"></asp:DropDownList>
    </td>
</tr>
    </table>
    <table width="100%">
    <tr align="center">
    <td>
        <asp:Button ID="btn_daochu" runat="server" Text="导出"  OnClick="btn_daochu_Click"/>
    </td>
    </tr>
    </table>
    </div>
</asp:Content>
 

