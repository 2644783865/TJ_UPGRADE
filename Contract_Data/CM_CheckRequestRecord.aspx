<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master"  AutoEventWireup="true" CodeBehind="CM_CheckRequestRecord.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_CheckRequestRecord" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" runat="server"  contentplaceholderid="RightContentTitlePlace">收/付款记录
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true">
</cc1:ToolkitScriptManager> 
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
<%--<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>--%>
<script language="javascript" type="text/javascript">
function Edit_PZ(i)
{
  var ID=i.title;
  var autonum=Math.round(10000*Math.random());
  window.showModalDialog("CM_Payment.aspx?Action=Edit&autonum="+autonum+"&PRid="+ID,obj,"dialogWidth=800px;dialogHeight=520px;status:no;");  
  window.href="CM_CheckRequestRecord.aspx";
}

function Edit_SK(i)
{
  var ID=i.title;
  window.showModalDialog("CM_SW_Payment.aspx?Action=Edit&BPid="+ID,obj,"dialogWidth=800px;dialogHeight=520px;status:no;");  
  window.href="CM_CheckRequestRecord.aspx";
}
//检验日期格式如：2012-01-01
function dateCheck(obj)
{
    var value=obj.value;
    if(value!="")
    {
        var re = new RegExp("^([0-9]{4})(-)([0-9]{2})(-)([0-9]{2})$");
        m = re.exec(value)
        if (m == null ) 
        {
            obj.style.background="yellow";
            obj.value="";
            alert('请输入正确的时间格式如：2012-01-01');
        }       
    }
 }
  </script>
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
<div class="RightContent">   
    
  <div id="tabpal"> 
         <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        TabStripPlacement="Top" ActiveTabIndex="0">
   <!--付款记录开始-->     
<cc1:TabPanel ID="Tab_QK" runat="server" Width="100%" HeaderText="付款记录查询" TabIndex="0" >
<ContentTemplate>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
<div class="box-wrapper"> 
    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
    <!--付款记录筛选条件开始-->
    <table width="100%">
    <tr>
    <td>&nbsp;请款部门：
        <asp:DropDownList ID="dplBM" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnFKQuery_Click">
        </asp:DropDownList>&nbsp;&nbsp; 
        请款人：
        <asp:DropDownList ID="dplQKR" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnFKQuery_Click">
        </asp:DropDownList>&nbsp;&nbsp;
        
        合同类型：
        <asp:DropDownList ID="dplType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnFKQuery_Click">
       <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
       <asp:ListItem Text="委外合同" Value="1"></asp:ListItem>
       <asp:ListItem Text="采购合同" Value="2"></asp:ListItem>
       <asp:ListItem Text="发运合同" Value="3"></asp:ListItem>
       <asp:ListItem Text="其他合同" Value="4"></asp:ListItem>
       </asp:DropDownList>&nbsp;&nbsp;                  
         <input type="text" runat="server" id="search_Pay" value="项目或合同号" onfocus="if(value=='项目或合同号'){value='';style.color='Black'}" 
                      onblur="if(value=='') {value='项目或合同号';style.color='Gray'}" style="color:Gray;"/>
                    &nbsp;&nbsp;         
         有无凭证：<asp:DropDownList ID="ddl_pz" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnFKQuery_Click">
         <asp:ListItem Text="-全部-" Value="%"></asp:ListItem>
       <asp:ListItem Text="有" Value="1"></asp:ListItem>
       <asp:ListItem Text="无" Value="0"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnQuery" CssClass="button-outer" runat="server" Text="查 询" OnClick="btnFKQuery_Click" />&nbsp;
        <asp:Button ID="btn_reset_fk" runat="server" Text="重 置"  OnClick="btn_reset_fk_Click"/>&nbsp;
        <asp:Button ID="btn_Export" runat="server" Text="导 出"  OnClick="btn_Export_Click" OnClientClick="return confirm('导出前请筛选\r要导出当前筛选结果吗？')"/>
          </td> <td >
             <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="../Assets/icon-fuction/388.gif" border="0" hspace="2" 
             align="absmiddle" runat="server" />更多筛选</asp:HyperLink>
            <cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-300"  OffsetY="8"  TargetControlID="HyperLink1" PopupControlID="palORG">
            </cc1:PopupControlExtender>
     <!--付款更多筛选panel-->
            <asp:Panel ID="palORG" Width="375px" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
             
             <div style=" font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:5px;right: 10px;">
                      <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                  </div>
                  <br /><br />  
             <table style="width:100%; background-color:ThreeDHighlight;">
            <tr>
            <td align="right"><strong>收款单位:</strong></td>
              <td  align="left" >
                  <asp:TextBox ID="txtSKDW" runat="server" Width="300px"></asp:TextBox>
                  <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSKDW"
                    ServicePath="~/Ajax.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                    ServiceMethod="GetCompleteProvider" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                    CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                </cc1:AutoCompleteExtender>
              </td>
            </tr>
              <tr>              
                 <td align="right"><strong>付款日期:</strong></td>
                  <td  align="left" >
                    <asp:TextBox ID="txtStartTime" runat="server"  onchange="dateCheck(this)"/>
                    到<asp:TextBox ID="txtEndTime" runat="server"  onchange="dateCheck(this)"/>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="txtStartTime"></cc1:CalendarExtender>
                         <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年" 
                          TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtEndTime"></cc1:CalendarExtender>
                </td>                
              </tr>
                         
             </table>             
              
            </asp:Panel>                                        
       </td>
    </tr>
    </table>
    <!--付款记录筛选条件结束-->
    </div>
    </div>
    </div>
     <div class="box-wrapper">
       <div class="box-outer">
       <!--付款记录数据表-->
           <asp:Panel ID="Panel1" runat="server" style="width:100%; height:auto; overflow:auto; display:block;">
         <div style="width:100%">
             <yyc:SmartGridView ID="grvQKJL" width="100%" CssClass="toptable grid" runat="server" OnRowDataBound="grvQKJL_RowDataBound"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  ShowFooter="true">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <FixRowColumn FixRowType="Header,Pager"  TableWidth="100%" FixColumns="0,1,2" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                  <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblQKDH" runat="server" Text='<%# Eval("QKDH") %>' Visible="false"></asp:Label>  
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="HTBH" HeaderText="合同编号" ItemStyle-HorizontalAlign="Left"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
              <asp:BoundField DataField="PJNAME" HeaderText="项目名称" ItemStyle-HorizontalAlign="Left"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
              <asp:BoundField DataField="ENGNAME" HeaderText="工程名称" ItemStyle-HorizontalAlign="Left"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
              <asp:BoundField DataField="SKDW" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="收款单位" ItemStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="QKJE" HeaderText="请款金额" ItemStyle-HorizontalAlign="Right"  ItemStyle-Wrap="false" DataFormatString="{0:c}"
                HeaderStyle-Wrap="false"/>
              <asp:BoundField DataField="QKRQ" HeaderText="请款日期"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="ZCDH" HeaderText="付款单号" ItemStyle-HorizontalAlign="Left"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" Visible="false"/>
              <asp:BoundField DataField="ZFJE" HeaderText="支付金额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}"
              ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
              <asp:BoundField DataField="ZCRQ" HeaderText="支付日期"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="QKYT" HeaderText="请款用途" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
              <asp:BoundField DataField="QKBM" HeaderText="请款部门" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
              <asp:BoundField DataField="QKR" HeaderText="请款人"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
              <asp:TemplateField HeaderText="合同类别"  ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                 <ItemTemplate>
                     <asp:Label ID="lblHTLB" runat="server" Text='<%#Eval("HTLB").ToString()=="0"?"销售":Eval("HTLB").ToString()=="1"?"委外":Eval("HTLB").ToString()=="2"?"采购":Eval("HTLB").ToString()=="3"?"发运":"其他" %>'></asp:Label> 
                 </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="有无凭证" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:CheckBox ID="Cbx_PZ" runat="server" Enabled="false" Checked='<%#Eval("PZ").ToString()=="0"?false:true %>' />
                        </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField  HeaderText="编辑" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                        <asp:HyperLink ID="hlFK"  CssClass="hand" ToolTip='<%# Eval("ZCDH")%>'  onClick='Edit_PZ(this);' runat="server"
                         Visible='<%# Eval("ZCDH").ToString()==""?false:true%>' > 
                        <asp:Image ID="img_editfk" ImageUrl="~/Assets/images/modify.gif" runat="server" /></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
              </asp:TemplateField>
              
               <asp:TemplateField HeaderText="合同信息" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
                <ItemTemplate>
                   <asp:HyperLink ID="hl_view_qtht" CssClass="link" Visible='<%# Eval("ZCDH").ToString()==""?false:true%>'  
                         NavigateUrl='<%#"CM_Contract_Add.aspx?Action=View&condetail_id="+Eval("HTBH")+"&ConForm="+Eval("HTLB")%>'
                          runat="server" Target="_blank">
                        <asp:Image ID="Imageview" ImageUrl="~/Assets/images/search.gif" 
                         runat="server" />                                
                   </asp:HyperLink>
                </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false">                    
                    <ItemTemplate>
                        <asp:LinkButton ID="Lbtn_Del" runat="server" ForeColor="Red" CommandArgument='<%#Eval("ZCDH") %>' OnClick="Lbtn_Del_OnClick"
                       CommandName='<%#Eval("QKDH") %>'   OnClientClick="javascript:return confirm('确定要删除吗？\r删除后该记录返回到【待办款项】');" >
                         删除
                         </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" /> 
            <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />                  
        </yyc:SmartGridView>
      <asp:Panel ID="palNoData" runat="server" HorizontalAlign="Center">没有记录!</asp:Panel>
     <br />
     </div></asp:Panel>
     <table width="100%" >
    <tr>
    <td style=" text-align:right">筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text=""></asp:Label>条记录&nbsp;&nbsp;&nbsp;
    合计付款金额：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    <td><uc1:UCPaging ID="UCPaging1" runat="server" /></td>
    </tr>
    </table>
     </div>
     </div>
 </div>   
</ContentTemplate>
            </asp:UpdatePanel>
</ContentTemplate>
</cc1:TabPanel>

<!--收款记录开始-->
<cc1:TabPanel ID="Tab_YK" runat="server" HeaderText="收款记录查询" TabIndex="1">
<ContentTemplate>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">

            <ContentTemplate>             
<div class="box-inner">
         <div class="box_right">
             <div class="box-title">
             <!--收款记录筛选条件开始-->
                 <table width="100%" >
                    <tr>
                    <td>&nbsp;项目或合同号                  
                     <input type="text" runat="server" id="search_Get" value="项目或合同号" onfocus="if(value=='项目或合同号'){value='';style.color='Black'}" 
                      onblur="if(value=='') {value='项目或合同号';style.color='Gray'}" style="color:Gray;"/>
                    &nbsp;&nbsp;负 责 人：<asp:DropDownList ID="dplFZR" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnSKQuery_Click">
        </asp:DropDownList>&nbsp;&nbsp;
         有无凭证：<asp:DropDownList ID="ddl_skpz" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnSKQuery_Click">
         <asp:ListItem Text="-全部-" Value="%"></asp:ListItem>
       <asp:ListItem Text="有" Value="1"></asp:ListItem>
       <asp:ListItem Text="无" Value="0"></asp:ListItem>
        </asp:DropDownList>
                    &nbsp;&nbsp;
                         <asp:Button ID="Btn_Query" runat="server" Text="查 询" OnClick="btnSKQuery_Click" /> 
                         &nbsp;&nbsp;
                        <asp:Button ID="Btn_Reset_SK" runat="server" Text="清除筛选"  OnClick="Btn_Reset_SK_Click"/>             
                    </td>
                    <td >
             <asp:HyperLink ID="HyperLink2" CssClass="hand" runat="server"><asp:Image ID="Image1" ImageUrl="../Assets/icon-fuction/388.gif" border="0" hspace="2" 
             align="absmiddle" runat="server" />更多筛选</asp:HyperLink>
            <cc1:PopupControlExtender ID="PopupControlExtender2" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-300"  OffsetY="8"  TargetControlID="HyperLink2" PopupControlID="palORG2">
            </cc1:PopupControlExtender>
            <!--收款更多筛选panel-->
            <asp:Panel ID="palORG2" Width="375px" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
             
             <div style=" font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:5px;right: 10px;">
                      <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                  </div>
                  <br /><br />  
             <table style="width:100%; background-color:ThreeDHighlight;">
            <tr>
            <td align="right"><strong>客户名称:</strong></td>
              <td  align="left" >
                  <asp:TextBox ID="txtKHMC" runat="server" Width="300px"></asp:TextBox>
                  <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtKHMC"
                    ServicePath="~/Ajax.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                    ServiceMethod="GetCompleteProvider" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                    CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                </cc1:AutoCompleteExtender>--%>
              </td>
            </tr>
              <tr>              
                 <td align="right"><strong>收款日期:</strong></td>
                  <td  align="left" >
                   <asp:TextBox ID="txt_SK_starttime" runat="server"   onchange="dateCheck(this)"></asp:TextBox>到
                        <asp:TextBox ID="txt_SK_endtime" runat="server"  onchange="dateCheck(this)"></asp:TextBox>
                        <cc1:CalendarExtender ID="calender_start" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="txt_SK_starttime"></cc1:CalendarExtender>
                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年" 
                          TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txt_SK_endtime"></cc1:CalendarExtender>
                </td>                
              </tr>
                        
             </table>             
              
            </asp:Panel> 
            </td>
                    </tr>
                 </table>
             </div>
         </div>
     </div> 
 <div class="box-wrapper">
        <div class="box-outer" style="display:block;" id="swyk"> 
        <!--收款记录数据表-->  
           <yyc:SmartGridView ID="grvSKJL" width="100%" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" DataKeyNames="BP_ID" ShowFooter="true" AllowPaging="True" PageSize="10"
         OnRowDataBound="grvSKJL_RowDataBound">
        <FixRowColumn FixRowType="Header,Pager"  TableWidth="100%" FixColumns="0,1,2,3" />                    
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="序号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="PCON_BCODE" HeaderText="合同编号" ItemStyle-HorizontalAlign="Left" 
                   ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                   <asp:BoundField DataField="PCON_YZHTH" HeaderText="业主合同号" 
                    ItemStyle-HorizontalAlign="Left"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false">                    
                    </asp:BoundField>
                   <asp:BoundField DataField="PCON_PJNAME" HeaderText="项目名称" ItemStyle-HorizontalAlign="Left" 
                   ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                   <asp:BoundField DataField="PCON_NAME" HeaderText="合同名称" 
                  ItemStyle-HorizontalAlign="Left"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false">                   
                   </asp:BoundField>
                <asp:BoundField DataField="PCON_RESPONSER" HeaderText="负责人"  ItemStyle-HorizontalAlign="Left" 
                     ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField DataField="PCON_CUSTMNAME" HeaderText="客户"  ItemStyle-HorizontalAlign="Left" 
                 ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                   <asp:BoundField DataField="BP_KXMC" HeaderText="款项名称" ItemStyle-HorizontalAlign="Left" 
                   ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                   <asp:BoundField DataField="BP_JE" HeaderText="要款金额" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" 
                   ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                   <asp:BoundField DataField="BP_YKRQ" HeaderText="要款日期" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" 
                   ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="BP_SFJE" HeaderText="实付金额" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" 
                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="BP_SKRQ" HeaderText="收款日期" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" 
                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:TemplateField HeaderText="有无凭证" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:CheckBox ID="Cbx_YKPZ" runat="server" Enabled="false" Checked='<%#Eval("BP_PZ").ToString()=="0"?false:true %>' />
                        </ItemTemplate>
              </asp:TemplateField>
                     <asp:TemplateField  HeaderText="编辑" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                        <asp:HyperLink ID="hlSK"  CssClass="hand" ToolTip='<%# Eval("BP_ID")%>'  onClick='Edit_SK(this);' runat="server"> 
                        <asp:Image ID="img_editsk" ImageUrl="~/Assets/images/modify.gif" runat="server" /></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
              </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="合同信息" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                <ItemTemplate>
                <asp:HyperLink ID="hl_view_swht" CssClass="link" 
                        NavigateUrl='<%#"CM_Contract_SW_Add.aspx?Action=View&condetail_id="+Eval("PCON_BCODE")%>' runat="server" Target="_blank">
                    <asp:Image ID="img_view_swht" ImageUrl="~/Assets/images/search.gif" runat="server" />                                   
                        </asp:HyperLink>
                </ItemTemplate>
                </asp:TemplateField>           
        </Columns>
        <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" /> 
        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
        <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" BorderStyle="None" BorderWidth="0px" ForeColor="#330099" />
        <PagerSettings Visible="False" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </yyc:SmartGridView>
            <asp:Panel ID="Panel_nodate" runat="server">无记录！
            </asp:Panel>
            <!--收款合计-->
            <table width="100%" >
    <tr>
    <td style=" text-align:right">筛选结果：共<asp:Label ID="lb_select_num2" runat="server" Text=""></asp:Label>条记录&nbsp;&nbsp;&nbsp;
    合计收款(实付)金额：<asp:Label ID="lb_select_money2" runat="server" Text="" ForeColor="Red"></asp:Label>
   </td>
     <td>
    <asp:Panel ID="Pal_page" runat="server">            
        <div style="text-align:center">
        第<asp:Label ID="lbl_currentpage" runat="server" Text=""></asp:Label>/<asp:Label ID="lbl_totalpage" runat="server" Text=""></asp:Label>
        <asp:LinkButton ID="lnkbtnFrist" runat="server" OnClick="lnkbtnFrist_Click">首页</asp:LinkButton> 
        <asp:LinkButton ID="lnkbtnPre" runat="server" OnClick="lnkbtnPre_Click">上一页</asp:LinkButton>         
        <asp:LinkButton ID="lnkbtnNext" runat="server" OnClick="lnkbtnNext_Click">下一页</asp:LinkButton> 
        <asp:LinkButton ID="lnkbtnLast" runat="server" OnClick="lnkbtnLast_Click">尾页</asp:LinkButton> 
        跳转到第<asp:TextBox ID="txt_goto" runat="server" Width="50px"></asp:TextBox>页
        <asp:LinkButton ID="lnkbtnGoto" runat="server" OnClick="lnkbtnGoto_Click">GO</asp:LinkButton> 
            <asp:RegularExpressionValidator ControlToValidate="txt_goto" ID="RegularExpressionValidator1" runat="server" ErrorMessage="请输入正确的数据格式！" ValidationExpression="^[0-9]*[1-9][0-9]*$"></asp:RegularExpressionValidator>
       </div>
       </asp:Panel>
    </td>
    </tr>
    </table>
        </div>
     </div>
  </ContentTemplate>
            </asp:UpdatePanel>
</ContentTemplate>
</cc1:TabPanel>

</cc1:TabContainer>
        </div> 
   
</div>    
</asp:Content>