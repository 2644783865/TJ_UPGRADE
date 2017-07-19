<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="CM_CHECKREQUEST_QUERY.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_CHECKREQUEST_QUERY" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 <asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">    
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
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true">
</cc1:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
   <div class="RightContent">
     <div class="box-wrapper">
     <div style="height:8px" class="box_top"></div>
        <div class="box-outer">
          <table width="100%">
            <tr>
            <td>
            <asp:Button ID="btn_search" runat="server" Text="查 询" OnClick="btn_search_Click" />
            &nbsp;|&nbsp;<asp:Button ID="btn_ViewQK" runat="server" Text="查 看"  OnClick="btn_ViewQK_Click"/>
            &nbsp;|&nbsp;<asp:Button ID="btn_EditQK" runat="server" Text="编 辑" OnClick="btn_EditQK_Click"/>
            &nbsp;|&nbsp;<asp:Button ID="btn_DelQK" runat="server" Text="删 除" OnClick="btn_DelQK_Click"  OnClientClick="javascript:return confirm('确定删除吗？')"/>
            &nbsp;|&nbsp;<asp:Button ID="btn_Print" runat="server" Text="打 印" OnClick="btn_Print_Click"/>
            &nbsp;|&nbsp;<asp:Button ID="btn_ViewHT" runat="server" Text="查看合同" OnClick="btn_ViewHT_Click"/>
            &nbsp;|&nbsp;<asp:Button ID="btn_AddFHTQK" runat="server" Text="添加非合同请款" OnClick="btn_AddFHTQK_Click"/>
            &nbsp;|&nbsp;<asp:Button ID="btn_ExportToExcel" runat="server" Text="导 出" OnClick="btn_ExportToExcel_Click" OnClientClick="javascript:return confirm('导出前请先筛选\r\r要导出当前筛选记录吗？')"/>
            </td>
            </tr>
            </table>
        
          <asp:Panel ID="Pal_Query" runat="server">
          <table width="100%">
    <tr>
    <td >合同编号：<asp:TextBox ID="txt_HTBH" runat="server" Width="205px"></asp:TextBox></td>
    <td >请款部门：<asp:DropDownList ID="dplBM" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplBM_SelectedIndexChanged">
        </asp:DropDownList></td>
    <td >请 款 人：<asp:DropDownList ID="dplQKR" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
        </asp:DropDownList></td>   
         
    </tr>
    
    <tr>
    <td>项目名称：<asp:TextBox ID="txt_PJNAME" runat="server" Width="205px"></asp:TextBox></td>
    <td>收款单位：<asp:TextBox ID="txt_SKDW" runat="server" Width="205px"></asp:TextBox>
    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txt_SKDW"
                    ServicePath="~/Ajax.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                    ServiceMethod="GetCompleteProvider" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                    CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                </cc1:AutoCompleteExtender>
    </td>
    <td>请款状态：<asp:DropDownList ID="ddl_QKZT" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
        <asp:ListItem Text="—全部—" Value="%"></asp:ListItem>
        <asp:ListItem Text="保存" Value="0"></asp:ListItem>
        <asp:ListItem Text="正在签字" Value="1"></asp:ListItem>
        <asp:ListItem Text="提交财务-未付款" Value="2"></asp:ListItem>
        <asp:ListItem Text="部分支付" Value="3"></asp:ListItem>
        <asp:ListItem Text="已支付" Value="4"></asp:ListItem>        
        </asp:DropDownList>
    </td>
    </tr>
    
    <tr>     
     <td >请款时间：<asp:TextBox ID="sta_time" runat="server" Width="90px"></asp:TextBox>
         至&nbsp;<asp:TextBox ID="end_time" runat="server" Width="90px"></asp:TextBox>
         <cc1:CalendarExtender ID="calender_sta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="sta_time"></cc1:CalendarExtender>
         <cc1:CalendarExtender ID="calender_end" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="end_time"></cc1:CalendarExtender>
     </td>
    <td>每页显示：<asp:DropDownList ID="ddl_pageno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                  </asp:DropDownList>&nbsp;条记录
        </td>
    <td>&nbsp;</td>
    </tr>
    </table>
          </asp:Panel>
    
          <asp:Panel ID="PanelQKD" runat="server"  style="width:100%; height:auto; overflow:auto;">
          <asp:GridView ID="grvQKD" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                ShowFooter="true" OnRowDataBound="grvQKD_RowDataBound">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#ffffff" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
           <Columns>
             <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
             <ItemTemplate>
             <asp:CheckBox ID="CheckBox1" runat="server" />              
             <asp:Label ID="lbl_id" runat="server" Text=""></asp:Label>
             <asp:Label ID="lbl_qkdh" runat="server" Text='<%#Eval("QKDH")%>' Visible="false"></asp:Label>
             <asp:Label ID="lbl_qkr" runat="server" Text='<%#Eval("QKR")%>' Visible="false"></asp:Label>
             <asp:Label ID="lbl_qkzt" runat="server" Text='<%#Eval("QKZT")%>' Visible="false"></asp:Label>
             <asp:Label ID="lbl_htlx" runat="server" Text='<%#Eval("HTLX")%>' Visible="false"></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
                            
            <asp:TemplateField HeaderText="合同编号" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
             <ItemTemplate>
                 <asp:Label ID="lbl_htbh" runat="server" Text='<%#Eval("HTBH")%>' Visible="false"></asp:Label>
                 <asp:Label ID="lbl_htbh_txt" runat="server" Text='<%#Eval("HTBH").ToString()==""?"非合同请款":Eval("HTBH")%>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="请款期次" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
             <ItemTemplate>
                 <asp:Label ID="lbl_qkqc" runat="server" Text=""></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="项目名称" DataField="PJNAME" ItemStyle-HorizontalAlign="Left" 
            HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
            <asp:BoundField HeaderText="请款金额" DataField="QKJE" ItemStyle-HorizontalAlign="Right" 
            HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:c}"/>
            <asp:BoundField HeaderText="本期已支付" DataField="YZF" ItemStyle-HorizontalAlign="Right" 
            HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:c}"/>
            <asp:BoundField HeaderText="请款部门" DataField="QKBM" ItemStyle-HorizontalAlign="Left" 
            HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
            <asp:BoundField HeaderText="请款人" DataField="QKR" ItemStyle-HorizontalAlign="Left" 
            HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />            
            <asp:TemplateField HeaderText="请款状态" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
             <ItemTemplate>             
                 <asp:Label ID="lbl_qkzt_txt" runat="server" Text='<%#Eval("QKZT").ToString()=="0"?"保存":Eval("QKZT").ToString()=="1"?"正在签字":Eval("QKZT").ToString()=="2"?"提交财务未付款":Eval("QKZT").ToString()=="3"?"部分支付":Eval("QKZT").ToString()=="4"?"已支付":"驳回"%>'></asp:Label>
               </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="请款日期" DataField="QKRQ" ItemStyle-HorizontalAlign="Left" 
            HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:d}"/>
            <asp:BoundField HeaderText="请款用途" DataField="QKYT" ItemStyle-HorizontalAlign="Left" 
            HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
            <asp:BoundField HeaderText="收款单位" DataField="SKDW" ItemStyle-HorizontalAlign="Left" 
            HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
           
           
           </Columns>
     </asp:GridView> 
          <asp:Panel ID="pal_NoData" runat="server" HorizontalAlign="Center">没有记录!</asp:Panel>
          </asp:Panel>
    
          <table width="100%" >
    <tr>
    <td style=" text-align:right">
    筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text=""></asp:Label>条记录&nbsp;&nbsp;&nbsp;
    合计请款：<asp:Label ID="lb_total_qkmoney" runat="server" Text="" ForeColor="Red"></asp:Label>
    &nbsp;&nbsp;&nbsp;
    合计付款：<asp:Label ID="lb_total_fkmoney" runat="server" Text="" ForeColor="Red"></asp:Label>
    </td>
    <td><uc1:UCPaging ID="UCPaging1" runat="server" /></td>
    </tr>
    </table>
        </div>
     </div>
  </div>
</ContentTemplate>
 </asp:UpdatePanel>
 
 <script type="text/javascript">
//单击行变色
function RowClick(obj)
{
//判断是否单击的已选择的行，如果是则取消该行选择
    if(obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked==false)
    {
       obj.style.backgroundColor='#ffffff';
       obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
    }
    else
    {
       var table=obj.parentNode.parentNode;
       var tr=table.getElementsByTagName("tr");
       
       var ss=tr.length;
       for(var i=1;i<ss-1;i++)
       {
           tr[i].style.backgroundColor=(tr[i].style.backgroundColor == '#87CEFF') ? '#ffffff' : '#ffffff';
          tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false; 
       }
       obj.style.backgroundColor=(obj.style.backgroundColor == '#ffffff') ? '#87CEFF' : '#ffffff';
       obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
   }
}
//编辑非合同请款
function FHTQK(ID,action)
{  
  var autonum=Math.round(10000*Math.random());
  sRet=window.showModalDialog("CM_FHT_Payment.aspx?Action="+action+"&autonum="+autonum+"&QKDH="+ID,"","dialogWidth=800px;dialogHeight=520px;status:no;");  
  if(sRet=="refresh")
   {
  window.location.href="CM_CHECKREQUEST_QUERY.aspx";
  }
}
//合同请款
function HTQK(ID,action,formid){
    var autonum = Math.round(10000 * Math.random());
window.open("CM_CHECKREQUEST.aspx?Action=" + action + "&contactform="+formid+"&autonum=" + autonum + "&CRid=" + ID); 
}

//添加非合同请款
function Add_FHTQK()
{  

  var autonum=Math.round(10000*Math.random());
 var sRet= window.showModalDialog("CM_FHT_Payment.aspx?Action=Add&autonum="+autonum,"","dialogWidth=800px;dialogHeight=520px;status:no;");  
  if(sRet=="refresh")
   {
  window.location.href="CM_CHECKREQUEST_QUERY.aspx";
  }
}

//查看合同信息
function ViewHT(ID,type){
var autonum=Math.round(10000*Math.random()); 
window.open("CM_Contract_Add.aspx?Action=View&condetail_id="+ID+"&ConForm="+type+"&autonum="+autonum); 
}

//打印请款单
function PrintQK(ID){
var autonum=Math.round(10000*Math.random()); 
window.open("CM_CHECKREQUEST_VIEW.aspx?CRid="+ID+"&autonum="+autonum); 
}
</script>
</asp:Content>