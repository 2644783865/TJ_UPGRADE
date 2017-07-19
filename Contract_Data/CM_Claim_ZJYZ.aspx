<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="CM_Claim_ZJYZ.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Claim_ZJYZ" %>
<%@ Register src="../Controls/UploadAttachments.ascx" tagname="UploadAttachments" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    <span>重机对业主索赔</span>
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
    <asp:Button ID="btnConfirmZJYZ" runat="server" CssClass="button-inner" Text="提 交"  OnClientClick="javascript:return ConfirmSubmit();"
                    onclick="btnConfirmZJYZ_Click" />
                    &nbsp;&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="hand" ToolTip="返回到 合同索赔界面" NavigateUrl="~/Contract_Data/CM_Claim_Total.aspx">
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
        
                    <asp:Panel ID="palZJYZ" runat="server">            
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
                    <asp:TextBox ID="txtSPDJRQZJYZ" runat="server" onchange="dateCheck(this)"></asp:TextBox>
                    <asp:CalendarExtender ID="calender_spdjrq" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="txtSPDJRQZJYZ"></asp:CalendarExtender>
                    </td><td></td><td></td>
                </tr>
                <tr>
                <td>提出部门</td>
                <td>
                    <asp:DropDownList ID="dplJSBMZJYZ" runat="server">
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
                  <asp:RadioButtonList ID="rbLSSCLZJYZ" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                   <asp:ListItem Text="不扣款" Value="0" ></asp:ListItem>
                    <asp:ListItem Text="扣款" Value="1"></asp:ListItem>
                    <asp:ListItem Text="出具质量保证" Value="2"></asp:ListItem>
                  </asp:RadioButtonList>
                </td>
                </tr>
                <tr>
                <td>索赔金额</td>
                <td>
                    <asp:TextBox ID="txtSPJEZJYZ" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:DropDownList
                        ID="DropDownList7" runat="server" Enabled="false">
                        <asp:ListItem Text="元:RMB" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>

                </tr>
                <tr>
                <td>索赔问题描述</td>
                <td colspan="3">
                    <asp:TextBox ID="txtSPWTMSZJYZ" runat="server" TextMode="MultiLine" Width="60%" Height="70px"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>业主反馈信息</h2></td>
                </tr>
                <tr>
                <td>是否回复</td>
                <td>
                    <asp:RadioButtonList ID="rblSFHFZJYZ" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>回复意见</td>
                <td>
                    <asp:TextBox ID="txtHFYJZJYZ" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                <tr>
                <td>反馈人</td>
                <td>
                    <asp:TextBox ID="txtFKRZJYZ" runat="server"></asp:TextBox></td>
                <td>反馈日期</td>
                <td>
                    <asp:TextBox ID="txtFKRQZJYZ" runat="server"  onchange="dateCheck(this)"></asp:TextBox>
                    <asp:CalendarExtender ID="Calendar_fkrq" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="txtFKRQZJYZ"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>最终扣款信息</h2></td>
                </tr>
                <tr>
                <td>索赔金额</td>
                <td>
                    <asp:TextBox ID="txtSPJEZJYZ1" runat="server" Enabled="false"></asp:TextBox></td>
                <td>最终索赔金额</td>
                <td>
                    <asp:TextBox ID="txtZZSPJEZJYZ" runat="server" ></asp:TextBox></td>
                </tr>
                <tr>
                <td>是否扣款</td>
                <td>
                <table width="100%">
                <tr>
                <td><asp:RadioButtonList ID="rblSFKKZJYZ" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                    </asp:RadioButtonList></td>
                <td> <asp:Button ID="btnKKZJYZ" runat="server" Text="扣 款" OnClientClick="return ConfrimKK()" 
                        onclick="btnKKZJYZ_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:Red">提示:每条索赔记录都要进行扣款操作(包括最终索赔金额为0)</span></td>
                </tr>
                </table>
                </td>
                <td>扣款日期</td>
                <td>
                    <asp:TextBox ID="txtKKRQZJYZ" runat="server" Enabled="false"></asp:TextBox></td>
                </tr>
                <tr>
                <td>扣款备注</td>
                <td colspan="3">
                    <asp:TextBox ID="txtKKBZZJYZ" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                </table>
                <br />
               
            </asp:Panel> 
            <uc1:UploadAttachments ID="AT_ZJYZ" runat="server" />
        </div>
        </div>
        
</asp:Content>
