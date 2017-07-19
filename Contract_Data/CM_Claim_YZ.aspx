<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master"   AutoEventWireup="true" CodeBehind="CM_Claim_YZ.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Claim_YZ" %>
<%@ Register src="../Controls/UploadAttachments.ascx" tagname="UploadAttachments" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    <span>业主索赔</span>
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
      function ConfrimKK()
      {
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
    <asp:Button ID="btnConfirmYZ" CssClass="button-inner" runat="server" Text="提 交" 
                    onclick="btnConfirmYZ_Click"  OnClientClick="javascript:return ConfirmSubmit();" />
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
            <asp:Panel ID="palYZ" runat="server">
           
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="100%">
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>业主索赔信息</h2></td>
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
                    <asp:TextBox ID="txtSPDJRQYZ" runat="server" onchange="dateCheck(this)"></asp:TextBox>
                     <asp:CalendarExtender ID="calender_spdjrq" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="txtSPDJRQYZ"></asp:CalendarExtender>                    
                    </td><td></td><td></td>
                </tr>
                
                <tr>
                <td>接受部门</td>
                <td>
                    <asp:DropDownList ID="dplJSBMYZ" runat="server">
                    <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                    <asp:ListItem Text="市场部" Value="1"></asp:ListItem>
                    <asp:ListItem Text="技术部" Value="2"></asp:ListItem>
                    <asp:ListItem Text="生产部" Value="3"></asp:ListItem>
                    <asp:ListItem Text="采购部" Value="4"></asp:ListItem>
                    <asp:ListItem Text="质量部" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>责任班组（分包商）</td>
                <td>
                    <asp:DropDownList ID="dplZRBZ" runat="server">
                    </asp:DropDownList>
                </td>
                </tr>
                <tr>
                <td>技术负责人</td>
                <td>
                    <asp:TextBox ID="txtJSFZR" runat="server"></asp:TextBox></td>
                <td>质量负责人</td>
                <td>
                    <asp:TextBox ID="txtZZFZR" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td>业主索赔金额</td>
                <td>
                    <asp:TextBox ID="txtSPJEYZ" runat="server" onblur="javascript:check_num(this)"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:DropDownList
                        ID="DropDownList1" runat="server" Enabled="false">
                        <asp:ListItem Text="元:RMB" Value="0" ></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>处理结果</td>
                <td>
                    <asp:RadioButtonList ID="rblSFCLYZ" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                    <asp:ListItem Text="不扣款" Value="0" ></asp:ListItem>
                    <asp:ListItem Text="扣款" Value="1"></asp:ListItem>
                    <asp:ListItem Text="出具质量保证" Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                </tr>
                <tr>
                <td>索赔问题描述</td>
                <td colspan="3">
                    <asp:TextBox ID="txtSPWTMSYZ" runat="server" TextMode="MultiLine" Width="60%" Height="70px"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>公司反馈信息</h2></td>
                </tr>
                <tr>
                <td>是否回复</td>
                <td>
                    <asp:RadioButtonList ID="rblSFHFYZ" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1" ></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>回复意见</td>
                <td>
                    <asp:TextBox ID="txtHFYJYZ" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                <tr>
                <td>反馈人</td>
                <td>
                    <asp:TextBox ID="txtFKRYZ" runat="server"></asp:TextBox></td>
                <td>反馈日期</td>
                <td>
                    <asp:TextBox ID="txtFKRQYZ" runat="server" onchange="dateCheck(this)"></asp:TextBox>
                    <asp:CalendarExtender ID="Calendar_fkrq" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="txtFKRQYZ"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>最终扣款信息</h2></td>
                </tr>
                <tr>
                <td>业主索赔金额</td>
                <td>
                    <asp:TextBox ID="txtYZSPJE1" runat="server" Enabled="false" onblur="javascript:check_num(this)"></asp:TextBox></td>
                <td>最终索赔金额</td>
                <td>
                    <asp:TextBox ID="txtZZSPJEYZ" runat="server" ></asp:TextBox></td>
                </tr>
                <tr>
                <td>是否扣款</td>
                <td>
                <table width="100%">
                <tr>
                <td><asp:RadioButtonList ID="rblSSKKYZ" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                    </asp:RadioButtonList></td>
                <td> <asp:Button ID="btnKK" runat="server" OnClientClick="return ConfrimKK()" Text="扣 款" 
                        onclick="btnKK_Click" />&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:Red">提示:每条索赔记录都要进行扣款操作(包括最终索赔金额为0)</span></td>
                </tr>
                </table>
                </td>
                <td>扣款日期</td>
                <td>
                    <asp:TextBox ID="txtKKRQYZ" runat="server" Enabled="false"></asp:TextBox></td>
                </tr>
                <tr>
                <td>扣款备注</td>
                <td colspan="3">
                    <asp:TextBox ID="txtKKBZYZ" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>公司处理信息</h2></td>
                
                </tr>
                <tr>
                <td>内部处理意见</td>
                <td>
                    <asp:TextBox ID="txtNBCLYJ" runat="server"></asp:TextBox></td>
                <td>负责部门</td>
                <td>
                    <asp:CheckBoxList ID="FZBM" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                    <asp:ListItem Text="技术部" Value="0"></asp:ListItem>
                    <asp:ListItem Text="生产部" Value="1"></asp:ListItem>
                    <asp:ListItem Text="质量部" Value="2"></asp:ListItem>
                    <asp:ListItem Text="采购部" Value="3"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                </tr>
               <tr>
                <td>原因描述</td>
                <td colspan="3">
                    <asp:CheckBoxList ID="cblYYMSYZ" runat="server" RepeatColumns="5" RepeatLayout="Table" RepeatDirection="Horizontal" Width="100%" >
                    </asp:CheckBoxList>
                </td>
                </tr>
                <tr>
                <td>备注</td>
                <td colspan="3">
                    <asp:TextBox ID="txtBZ" runat="server" TextMode="MultiLine" Height="40px" Width="60%"></asp:TextBox></td>
                </tr>
                </table>
                <br />
           </asp:Panel>
           <uc1:UploadAttachments ID="at_YZ" runat="server" />
  </div>
</div>              
</asp:Content>
