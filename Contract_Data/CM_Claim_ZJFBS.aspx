<%@ Page Language="C#"  MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="CM_Claim_ZJFBS.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Claim_ZJFBS" %>
<%@ Register src="../Controls/UploadAttachments.ascx" tagname="UploadAttachments" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace"><span><asp:Label ID="Lbl_splb1" runat="server" Text=""></asp:Label>索赔</span>
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
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <script type="text/javascript" language="javascript">
      function ConfrimKK(){
      var a=confirm("扣款金额以【最终扣款金额】为准，确认扣款吗？\r\r提示：扣款后扣款金额将不能修改，请确认！！！");
      return a;
      }
      function ConfirmSubmit()
      {
        return confirm("确认提交吗？");
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
  <div class="box-wrapper"> 
    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
    <tr>
       <td><strong>提示：带<span class="red">*</span>号的为必填项（添加后不能修改）</strong>
       <%--唯一编号--%> <asp:Label ID="lbl_UNID" runat="server" Text="" Visible="false"></asp:Label>
       </td>
    <td align="right">
    <asp:Button ID="btnConfirmZJFBS" CssClass="button-inner" runat="server"  Text="提 交" OnClick="btnConfirmZJFBS_Click" OnClientClick="javascript:return ConfirmSubmit();" /> 
          &nbsp;&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="hand" ToolTip="返回到合同索赔界面" NavigateUrl="~/Contract_Data/CM_Claim_Total.aspx">
        返 回<asp:Image ID="Image3" Height="16" Width="16" runat="server" ImageUrl="~/Assets/icons/back.png" /></asp:HyperLink>
     &nbsp;</td>
    </tr>
    </table>
     </div>
    </div>
    </div>
    </div>
        <div class="box-wrapper">
        <div class="box-outer">        
                    <asp:Panel ID="palZJFBS" runat="server">            
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="100%">
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>索赔信息</h2></td>
                </tr>
                <tr>
                <td>索赔合同号</td>
                <td>                   
                    <asp:TextBox ID="HTBH" runat="server" Width="50%" OnTextChanged="HTBH_Textchanged" AutoPostBack="True"></asp:TextBox>
            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="HTBH"
                   ServicePath="~/Ajax.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                   ServiceMethod="GetContractNO" FirstRowSelected="true"  CompletionListCssClass="autocomplete_completionListElement"
                 CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                </asp:AutoCompleteExtender>
                   <span class="red">*</span></td>
               <td>索赔单号</td>
                <td>
                    
                 <asp:TextBox ID="txtSPDH" runat="server" Enabled="false" Width="50%"></asp:TextBox></td> </tr>
                <tr>
                <td>项目名称</td>
                <td><asp:TextBox ID="txtXMMC" runat="server" Enabled="false"  Width="50%"></asp:TextBox></td>
                <td>工程名称</td>
                <td><asp:TextBox ID="txtGCMC" runat="server" Enabled="false"  Width="50%" ></asp:TextBox></td>
                </tr>
                <tr>                
                <td>索赔登记日期</td>
                <td>
                    <asp:TextBox ID="txtSPDJRQZJFBS" runat="server" onchange="dateCheck(this)"  Width="50%"></asp:TextBox>
                    <asp:CalendarExtender ID="calender_spdjrq" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="txtSPDJRQZJFBS"></asp:CalendarExtender>
                    </td><td></td><td></td>
                </tr>
                <tr>
                <td>提出部门</td>
                <td>
                    <asp:DropDownList ID="dplJSBMZJFBS" runat="server">
                    <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                    <asp:ListItem Text="市场部" Value="1"></asp:ListItem>
                    <asp:ListItem Text="技术部" Value="2"></asp:ListItem>
                    <asp:ListItem Text="生产部" Value="3"></asp:ListItem>
                    <asp:ListItem Text="采购部" Value="4"></asp:ListItem>
                    <asp:ListItem Text="质量部" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>处理结果</td><%--是否处理改为处理结果--%>
                <td>
                  <asp:RadioButtonList ID="rblCLJGZJFBS" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                     <asp:ListItem Text="不扣款" Value="0" ></asp:ListItem>
                    <asp:ListItem Text="扣款" Value="1"></asp:ListItem>
                    <asp:ListItem Text="出具质量保证" Value="2"></asp:ListItem>
                  </asp:RadioButtonList>
                </td>         
                </tr>
                <tr>
                                
                <td>索赔金额</td>
                <td>
                    <asp:TextBox ID="txtSPJEZJFBS" runat="server" onblur="javascript:check_num(this)"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList10" runat="server" Enabled="false" >
                        <asp:ListItem Text="元:RMB" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>  
                </tr>
                <tr>
                <td>索赔问题描述</td>
                <td colspan="3">
                    <asp:TextBox ID="txtSPWTMSZJFBS" runat="server" TextMode="MultiLine" Width="60%" Height="70px"></asp:TextBox></td>
                </tr>
               <%-- <tr>
                <td colspan="4" align="center" valign="middle"><h2><asp:Label ID="Lbl_splb2" runat="server" Text=""></asp:Label>反馈信息</h2></td>
                </tr>
                <tr>
                <td>是否回复</td>
                <td>
                    <asp:RadioButtonList ID="rblSFHFZJFBS" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1" ></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>回复意见</td>
                <td>
                    <asp:TextBox ID="txtHFYJZJFBS" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                <tr>
                <td>登记人</td>
                <td>
                    <asp:TextBox ID="txtDJRZJFBS" runat="server"></asp:TextBox></td>
                <td>登记日期</td>
                <td>
                    <asp:TextBox ID="txtDJRQZJFBS" runat="server" onClick="setday(this)"></asp:TextBox></td>
                </tr>--%>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>最终扣款信息</h2></td>
                </tr>
                <tr>
                <td>索赔金额</td>
                <td>
                    <asp:TextBox ID="txtSPJEZJFBS1" runat="server" onblur="javascript:check_num(this)" Enabled="false"></asp:TextBox></td>
                <td>最终索赔金额</td>
                <td>
                    <asp:TextBox ID="txtZZSPJEZJFBS" runat="server" ></asp:TextBox></td>
                </tr>
                <tr>
                <td>是否扣款</td>
                <td>
                <table width="100%">
                <tr>
                <td><asp:RadioButtonList ID="rblSFKKZJFBS" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                    </asp:RadioButtonList></td>
                <td> <asp:Button ID="btnKKZJFBS" runat="server" Text="扣 款" OnClientClick="return ConfrimKK()" 
                        onclick="btnKKZJFBS_Click" /><span style="color:Red">提示:每条索赔记录都要进行扣款操作(包括最终索赔金额为0)</span></td>
                </tr>
                </table>
                </td>
                <td>扣款日期</td>
                <td>
                    <asp:TextBox ID="txtKKRQZJFBS" runat="server" Enabled="false"></asp:TextBox>
                    
                    </td>
                </tr>
                <tr>
                <td>扣款备注</td>
                <td colspan="3">
                    <asp:TextBox ID="txtKKBZZJFBS" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                </table>
                <br />
            </asp:Panel>
               <uc1:UploadAttachments ID="AT_ZJFBS" runat="server" />
        </div>
        </div>
</asp:Content>
