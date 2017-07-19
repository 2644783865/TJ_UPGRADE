<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_TBPC_GJBJTZDetail.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_GJBJTZDetail"   MasterPageFile="~/Masters/RightCotentMaster.master" Title="关键部件台账详细信息"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
   </cc1:ToolkitScriptManager>
 <script src="../JS/DatePicker.js" type="text/javascript"></script>
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
<div class="box-inner"><div class="box_right"><div class='box-title'><table width='100%' ><tr><td> 
         关键部件台账详细信息</td></tr></table></div></div></div>
<div class="RightContent">

<div class="box-wrapper">
<div class="box-outer">
<table cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="50%">
<tr>
     <td colspan="4" style="height:auto" align="center">
        <span style="color:Red; font-size: large; font-family: 楷体_GB2312;">关键部件台账</span>
     </td>
</tr>
<tr>
    <td>
    项目名称：
    </td>
    <td>
        <asp:TextBox ID="tb_pjnm" runat="server" Enabled="false"></asp:TextBox>
    </td>
    <td>
    工程名称：
    </td>
    <td>
        <asp:TextBox ID="tb_engnm" runat="server" Enabled="false"></asp:TextBox>
    </td>
</tr>
<tr>
    <td>
       构件名称：
    </td>
    <td>
        <asp:TextBox ID="tb_gjnm" runat="server" ></asp:TextBox>
    </td>
    <td>
       炉批号：
    </td>
    <td>
        <asp:TextBox ID="tb_lph" runat="server"></asp:TextBox>
    </td>
</tr>
<tr>
    <td>
       供货厂家：
    </td>
    <td colspan="3">
        <asp:TextBox ID="tb_supply" runat="server" OnTextChanged="tb_supply_Textchanged" AutoPostBack="true" Width="250px"></asp:TextBox>
        <asp:TextBox ID="tb_supplyid" runat="server" Visible="false"></asp:TextBox>
        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_supply"
            ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
            ServiceMethod="GetCusupinfo" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
            CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
        </cc1:AutoCompleteExtender>
    </td>
</tr>
</table>


<cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top" ActiveTabIndex="0">
 <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="采购部" TabIndex="0">
      <ContentTemplate>
       <asp:Panel ID="Pan_cg" runat="server" >
        <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="50%">
          <tr>
              <td>
               进厂时间：
              </td>
              <td>
                  <asp:TextBox ID="ip_jctm" runat="server" onclick="setday(this)"></asp:TextBox>
              </td>
              <td>
               进厂状态：
              </td>
              <td>
                  <asp:TextBox ID="tb_jczt" runat="server"></asp:TextBox>
              </td>
          </tr>
          <tr>
              <td>
               采购员：
              </td>
              <td>
                  <asp:DropDownList ID="drp_cgy" runat="server">
                  </asp:DropDownList>
              </td>
              <td>
               备注：
              </td>
              <td>
                  <asp:TextBox ID="tb_cgnote" runat="server"></asp:TextBox>
              </td>
          </tr>
        </table>
        </asp:Panel>
      </ContentTemplate>
  </cc1:TabPanel>
   <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="质量部" TabIndex="1">
      <ContentTemplate>
       <asp:Panel ID="Pan_zl" runat="server" >
         <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="50%">
          <tr>
              <td>
               尺寸检查：
              </td>
              <td>
                   <asp:TextBox ID="tb_zlccjc" runat="server" ></asp:TextBox>
              </td>
              <td>
               检查时间：
              </td>
              <td>
                  
                  <asp:TextBox ID="tb_zlcctm" runat="server" onclick="setday(this)"></asp:TextBox>
              </td>
          </tr>
          <tr>
              <td>
               磁粉检查：
              </td>
              <td>
                  <asp:TextBox ID="tb_zlcfjc" runat="server"></asp:TextBox>
              </td>
              <td>
               检查时间：
              </td>
              <td>
                  
                    <asp:TextBox ID="tb_zlcftm" runat="server" onclick="setday(this)"></asp:TextBox>
              </td>
          </tr>
          <tr>
              <td>
               超声检测：
              </td>
              <td>
                  <asp:TextBox ID="tb_zlcsjc" runat="server"></asp:TextBox>
              </td>
              <td>
               检查时间：
              </td>
              <td>
                   
                   <asp:TextBox ID="tb_zlcstm" runat="server" onclick="setday(this)"></asp:TextBox>
              </td>
          </tr>
           <tr>
              <td>
               备注：
              </td>
              <td colspan="3">
                  <asp:TextBox ID="tb_zlnote" runat="server" Width="300px"></asp:TextBox>
              </td>
          </tr>
        </table>  
        </asp:Panel>      
      </ContentTemplate>
  </cc1:TabPanel>
   <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="储运部" TabIndex="2">
      <ContentTemplate>
       <asp:Panel ID="Pan_cy" runat="server" >
         <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="50%">
          <tr>
              <td>
               接收时间：
              </td>
              <td>
                  
                    <asp:TextBox ID="tb_cyjstm" runat="server" onclick="setday(this)"></asp:TextBox>
              </td>
              <td>
               接收人：
              </td>
              <td>
                  <asp:DropDownList ID="drp_cyjsr" runat="server">
                  </asp:DropDownList>
              </td>
          </tr>
          <tr>
              <td>
               入库时间：
              </td>
              <td>
                   
                    <asp:TextBox ID="tb_cyrktm" runat="server" onclick="setday(this)"></asp:TextBox>
              </td>
              <td>
               入库人：
              </td>
              <td>
                   <asp:DropDownList ID="drp_cyrkr" runat="server">
                  </asp:DropDownList>
              </td>
          </tr>
          <tr>
              <td>
               出库时间：
              </td>
              <td>
                  
                  <asp:TextBox ID="tb_cycktm" runat="server" onclick="setday(this)"></asp:TextBox>
              </td>
              <td>
               出库人：
              </td>
              <td>
                  <asp:DropDownList ID="drp_cyckr" runat="server">
                  </asp:DropDownList>
              </td>
          </tr>
           <tr>
              <td>
               卸货地点：
              </td>
              <td >
                  <asp:TextBox ID="tb_cyxhdd" runat="server"></asp:TextBox>
              </td>
              <td>
               备注：
              </td>
              <td >
                  <asp:TextBox ID="tb_cynote" runat="server"></asp:TextBox>
              </td>
          </tr>
        </table>
        </asp:Panel>
      </ContentTemplate>
  </cc1:TabPanel>
   <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="生产部" TabIndex="3">
      <ContentTemplate>
       <asp:Panel ID="Pan_sc" runat="server" >
         <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="50%">
          <tr>
              <td>
               喷砂处理：
              </td>
              <td>
                  <asp:TextBox ID="tb_scpscl" runat="server"></asp:TextBox>  
              </td>
              <td>
               完成时间：
              </td>
              <td>
                  
                   <asp:TextBox ID="tb_scpstm" runat="server" onclick="setday(this)"></asp:TextBox>
              </td>
          </tr>
          <tr>
              <td>
               存放地点：
              </td>
              <td>
                  <asp:TextBox ID="tb_sccfdd" runat="server"></asp:TextBox>
              </td>
              <td>
               备注：
              </td>
              <td>
                  <asp:TextBox ID="tb_scnote" runat="server"></asp:TextBox>
              </td>
          </tr>
        </table>  
        </asp:Panel>  
      </ContentTemplate>
  </cc1:TabPanel>
</cc1:TabContainer>
<table width="50%">
<tr>
<td align="center">
    <asp:Button ID="btn_submit" runat="server" Text="提交" OnClick="btn_submit_Click" />
    
     <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />
</td>
</tr>
</table>
</div>
 </div>
 </div>
</asp:Content>
