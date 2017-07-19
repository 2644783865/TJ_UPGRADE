<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="CM_CRUndo.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_CRUndo" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    <p>待办款项</p>
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
<%--<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>--%>
<script type="text/javascript" language="javascript">
//待办请款
function DBQK(i)
{
  var ID=i.title;
  var autonum=Math.round(10000*Math.random());
  window.showModalDialog("CM_Payment.aspx?Action=AddFK&autonum="+autonum+"&CRid="+ID,obj,"dialogWidth=800px;dialogHeight=520px;status:no;");  
//  window.location.href="CM_CRUndo.aspx";
}


//编辑非合同请款
function Edit_FHTQK(i)
{
  var ID=i.title;
  var autonum=Math.round(10000*Math.random());
  sRet=window.showModalDialog("CM_FHT_Payment.aspx?Action=EditCW&autonum="+autonum+"&QKDH="+ID,"","dialogWidth=800px;dialogHeight=520px;status:no;");  
//  if(sRet=="refresh")
//   {
//  window.location.href="CM_CRUndo.aspx";
//  }
}
//商务要款
function SWYK(i)
{
  var ID=i.title;
  window.showModalDialog("CM_SW_Payment.aspx?Action=Edit&BPid="+ID,obj,"dialogWidth=800px;dialogHeight=520px;status:no;");  
//  window.location.href="CM_CRUndo.aspx";
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
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true">
      </cc1:ToolkitScriptManager> 
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
 <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        TabStripPlacement="Top" ActiveTabIndex="0">
<cc1:TabPanel ID="Tab_QK" runat="server" Width="100%" HeaderText="待办请款信息" TabIndex="0" >
<ContentTemplate>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate> 
      <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%" >
                    <tr>                    
                    <td>&nbsp;请款部门：
                    <asp:DropDownList ID="dplBM" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick">
                    </asp:DropDownList>&nbsp;&nbsp; 
                    请款人：
                    <asp:DropDownList ID="dplQKR" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick">
                    </asp:DropDownList>&nbsp;&nbsp;                    
                    合同类型：
                    <asp:DropDownList ID="dplType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick">
                   <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                   <asp:ListItem Text="委外合同" Value="1"></asp:ListItem>
                   <asp:ListItem Text="采购合同" Value="2"></asp:ListItem>
                   <asp:ListItem Text="发运合同" Value="3"></asp:ListItem>
                   <asp:ListItem Text="其他合同" Value="4"></asp:ListItem>
                   </asp:DropDownList>&nbsp;&nbsp;
                   收款单位:
                   <asp:TextBox ID="tb_CUSTMNAME" runat="server" width="200px"></asp:TextBox>
                  <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_CUSTMNAME"
                    ServicePath="~/Ajax.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                    ServiceMethod="GetCompleteProvider" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                    CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                </cc1:AutoCompleteExtender>&nbsp;
                     <asp:Button ID="Btn_QTCX" runat="server" Text="查 询" OnClick="btnQuery_OnClick"/> &nbsp;&nbsp;
              <asp:Button ID="Btn_Reset_FK" runat="server" Text="重 置"  OnClick="Btn_Reset_FK_Click"/>                                  
                    </td>
                     <td >
                     <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="../Assets/icon-fuction/388.gif" border="0" hspace="2" 
                       align="absmiddle" runat="server" />更多筛选</asp:HyperLink>
                        <cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-240"  OffsetY="8"  TargetControlID="HyperLink1" PopupControlID="palORG">
             </cc1:PopupControlExtender>
            <asp:Panel ID="palORG" Width="300px" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
             
             <div style=" font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:5px;right: 10px;">
                      <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                  </div>
                  <br /><br />  
             <table style="width:100%; background-color:ThreeDHighlight;">
             <tr>
             <td align="right" style="width:60px"><strong>项目名称:</strong></td>
             <td align="left" colspan="3">
               <asp:TextBox ID="txt_XMMC" runat="server" Width="98%"></asp:TextBox>
             </td></tr>
             <tr>
                 <td align="right" style="width:60px"><strong>合同编号:</strong></td>
                  <td  align="left" colspan="3">
                   <asp:TextBox ID="txtHTH" runat="server" Width="98%"></asp:TextBox>    
                </td>
              </tr>              
              <tr>
                 <td align="right"><strong>请款日期:</strong></td>
                  <td  align="left" colspan="3">
                    <asp:TextBox ID="txtStartTime" runat="server" onchange="dateCheck(this)"  Width="100px"/>
                    到<asp:TextBox  ID="txtEndTime" runat="server" onchange="dateCheck(this)"  Width="100px"/>
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
             </div>
         </div>
     </div> 
      <div class="box-wrapper">
        <div class="box-outer" style="display:block;" id="dbqk">        
        <asp:Panel ID="PanelBody" runat="server"  style="overflow:auto; position:relative; margin:2px" Width="100%">
           <yyc:SmartGridView ID="grvFKJL" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
        DataKeyNames="CR_ID" ShowFooter="true" OnRowDataBound="grvFKJL_RowDataBound" Width="100%">
        <FixRowColumn FixRowType="Header,Pager"  TableWidth="100%" FixColumns="0,1" />                    
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text=""></asp:Label>
                     <asp:Label ID="lbl_QKDH" runat="server" Text='<%# Eval("CR_ID") %>' Visible="false"></asp:Label>                    
                </ItemTemplate >
                <ItemStyle HorizontalAlign="Center" Wrap="False" />
            </asp:TemplateField >
            <asp:BoundField DataField="PCON_PJNAME" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="项目名称" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="PCON_ENGNAME" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="工程名称" ItemStyle-HorizontalAlign="Left" />
                           
            <asp:TemplateField HeaderText="合同编号" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
             <ItemTemplate>
                 <asp:Label ID="lbl_htbh" runat="server" Text='<%#Eval("CR_HTBH")%>' Visible="false"></asp:Label>
                 <asp:Label ID="lbl_htbh_txt" runat="server" Text='<%#Eval("CR_HTBH").ToString()==""?"非合同请款":Eval("CR_HTBH")%>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="请款期次" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lbl_QKQC" runat="server" Text="" ></asp:Label>
                </ItemTemplate>                 
                </asp:TemplateField>
            <asp:BoundField DataField="CUSTMNAME" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="收款单位" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField  ItemStyle-Wrap="false"  DataField="DEP_NAME" HeaderText="请款部门" HeaderStyle-Wrap="false"><ItemStyle HorizontalAlign="Center"></ItemStyle></asp:BoundField>
                <asp:BoundField ItemStyle-Wrap="false" DataField="CR_DATE" HeaderText="请款日期" HeaderStyle-Wrap="false" DataFormatString="{0:d}" ><ItemStyle HorizontalAlign="Center"></ItemStyle></asp:BoundField>
                <asp:BoundField ItemStyle-Wrap="false" DataField="CR_USE" HeaderText="请款用途" HeaderStyle-Wrap="false"><ItemStyle HorizontalAlign="Center"></ItemStyle></asp:BoundField>
                <asp:BoundField ItemStyle-Wrap="false" DataField="CR_BQSFK" HeaderText="请款金额" HeaderStyle-Wrap="false" DataFormatString="{0:c}">
                <ItemStyle HorizontalAlign="Right"></ItemStyle></asp:BoundField>
                <asp:TemplateField HeaderText="本期已支付" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lbl_BQYZF" runat="server" Text='<%#string.Format("{0:c}",Convert.ToDouble(Eval("CR_BQYZF").ToString()))%>' ></asp:Label>
                </ItemTemplate>                
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" HeaderText="添加付款" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:HyperLink ID="hlFK"  CssClass="hand" ToolTip='<%# Eval("CR_ID")%>' onClick='DBQK(this);' Visible='<%# Eval("CR_HTBH").ToString()!=""?true:false%>' runat="server"> 
                    <asp:Image ID="Image1" ImageUrl="~/Assets/icons/create.gif" runat="server" /></asp:HyperLink>
                    <asp:HyperLink ID="hlFHTFK"  CssClass="hand" ToolTip='<%# Eval("CR_ID")%>' onClick='Edit_FHTQK(this);' Visible='<%# Eval("CR_HTBH").ToString()==""?true:false%>' runat="server"> 
                    <asp:Image ID="Image4" ImageUrl="~/Assets/images/modify.gif" runat="server" /></asp:HyperLink>
                </ItemTemplate>              
                </asp:TemplateField>
                <asp:TemplateField HeaderText="合同信息" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:HyperLink ID="hl_view_qtht" CssClass="link" Visible='<%# Eval("CR_HTBH").ToString()!=""?true:false%>'
                             NavigateUrl='<%#"CM_Contract_Add.aspx?Action=View&condetail_id="+Eval("CR_HTBH")+"&ConForm="+Eval("CR_HTLB")%>' runat="server" Target="_blank">
                            <asp:Image ID="Imageviewht" ImageUrl="~/Assets/images/search.gif" 
                             runat="server" />                                
                    </asp:HyperLink>
                </ItemTemplate>
                </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White"  Wrap="false"/>
        <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" Wrap="false"/> 
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center"  Wrap="false"/>
        <RowStyle BackColor="#EFF3FB"  Wrap="false"/>
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"  Wrap="false"/>
    </yyc:SmartGridView>
    
            <asp:Panel ID="NoDataPanel" runat="server">无记录！
            </asp:Panel>
     <table width="100%" >
    <tr>
    <td style=" text-align:right">筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text=""></asp:Label>条记录&nbsp;&nbsp;&nbsp;
    合计请款金额：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;
    合计待付金额：<asp:Label ID="lb_select_hjdf" runat="server" Text="" ForeColor="Red"></asp:Label>
    </td>
    <td><uc1:UCPaging ID="UCPaging1" runat="server" /></td>
    </tr>
    </table></asp:Panel>             
        </div>
 </div>   
         </ContentTemplate>
    </asp:UpdatePanel>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel ID="Tab_YK" runat="server" HeaderText="待办商务要款信息" TabIndex="1">
<ContentTemplate>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>  
    
    <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%" >
                    <tr><td>&nbsp;项目或合同号：
                     <input type="text" runat="server" id="search_box" value="项目或合同号" onfocus="if(value=='项目或合同号'){value='';style.color='Black'}" 
                      onblur="if(value=='') {value='项目或合同号';style.color='Gray'}" style="color:Gray; width:150px"/>
                    &nbsp;&nbsp;
                    负 责 人：<asp:DropDownList ID="dplFZR" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Btn_Query_Click">
        </asp:DropDownList>&nbsp;&nbsp;
        客户：<asp:TextBox ID="txt_KH" runat="server" Width="200px"></asp:TextBox>
        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txt_KH"
                    ServicePath="~/Ajax.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                    ServiceMethod="GetCompleteProvider" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                    CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                </cc1:AutoCompleteExtender>
                &nbsp;&nbsp;
                         <asp:Button ID="Btn_Query" runat="server" Text="查 询" OnClick="Btn_Query_Click" /> 
                         &nbsp;&nbsp;
                        <asp:Button ID="Btn_Reset_SK" runat="server" Text="重 置"  OnClick="Btn_Reset_SK_Click"/>             
                    </td>                  
                    </tr>
                 </table>
             </div>
         </div>
     </div> 
    <div class="box-wrapper">
        <div class="box-outer" style="display:block;" id="swyk">   
           <yyc:SmartGridView ID="grvDBSWYK" width="100%" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" DataKeyNames="BP_ID" ShowFooter="true"
         AllowPaging="True" PageSize="10" OnRowDataBound="grvDBSWYK_RowDataBound">
        <FixRowColumn FixRowType="Header,Pager"  TableWidth="100%" FixColumns="0,1,2" />                    
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="PCON_PJNAME" HeaderText="项目名称" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
            
            <%--<asp:BoundField DataField="BP_ID" HeaderText="要款单号" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" Visible="false"/>
            --%><asp:BoundField DataField="PCON_BCODE" HeaderText="合同编号" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
            <asp:BoundField DataField="PCON_YZHTH" HeaderText="业主合同号" 
                    ItemStyle-HorizontalAlign="Left"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false">                    
            </asp:BoundField>
            <asp:BoundField DataField="PCON_NAME" HeaderText="合同名称" 
                ItemStyle-HorizontalAlign="Left"  ItemStyle-Wrap="false" HeaderStyle-Wrap="false">                   
            </asp:BoundField>
            <asp:BoundField DataField="PCON_RESPONSER" HeaderText="负责人"  ItemStyle-HorizontalAlign="Left" 
                 ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
            <asp:BoundField DataField="PCON_CUSTMNAME" HeaderText="客户"  ItemStyle-HorizontalAlign="Left" 
                 ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>     
            <asp:BoundField DataField="BP_KXMC" HeaderText="款项名称" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
            <asp:BoundField DataField="BP_JE" HeaderText="要款金额" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" DataFormatString="{0:c}"/>
            <asp:BoundField DataField="BP_YKRQ" HeaderText="要款日期" ItemStyle-Wrap="false" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"/>
            <asp:TemplateField  HeaderText="编辑" HeaderStyle-Wrap="false">
                <ItemTemplate>
                <asp:HyperLink ID="hlYK"  CssClass="hand" ToolTip='<%# Eval("BP_ID")%>' onClick='SWYK(this);' runat="server"> 
                <asp:Image ID="Image6" ImageUrl="~/Assets/images/modify.gif" runat="server" /></asp:HyperLink>
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
            <asp:Panel ID="Panel1" runat="server">无记录！
            </asp:Panel>
            <table width="100%" >
    <tr>
    <td style=" text-align:right">筛选结果：共<asp:Label ID="lb_select_num2" runat="server" Text=""></asp:Label>条记录&nbsp;&nbsp;&nbsp;
    合计要款金额：<asp:Label ID="lb_select_money2" runat="server" Text="" ForeColor="Red"></asp:Label></td>
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
          
</asp:Content>
