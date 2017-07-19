<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/PopupBase.master" CodeBehind="CM_Bill.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Bill" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    
    <table width="100%">
    <tr>
    <td>    
    <asp:Label ID="lblFP" runat="server"></asp:Label>
    </td>
    <td align="right"> 
    <asp:Button ID="btnConfirm" runat="server"  Text="保存并关闭"  
            onclick="btnConfirm_Click" />&nbsp;&nbsp;|&nbsp;&nbsp;       
        <asp:Button ID="btn_Add" runat="server" Text="保存并添加"  Visible="false" OnClick="btn_Add_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        
        <asp:Label ID="lblRemind" runat="server" Text="操作成功！"  ForeColor="Red" Visible="false"></asp:Label>
            </td>
            <td>
            <div id="zdgb" style="display:none"><span id="tiao" runat="server"></span><span>秒后自动关闭...</span></div>
            </td>
    </tr>
    </table>
    
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
<script  type="text/javascript" language="javascript">

function SupplierSelect()
{
    var i=window.showModalDialog('SupplierSelect.aspx?type=4','',"dialogHeight:500px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
    if(i!=null)
    {
    document.getElementById('<%=txtKPDW.ClientID%>').value=i[1];
    }
}

function ButtonJudge(obj)
{
    window.returnValue = "refresh";    
    if(obj.value=="修 改")
    {
       return confirm("确认修改吗？");
    }
    else if(obj.value="添加并关闭")
    {
       return confirm("确认添加吗？");
    }

}
function AutoClose()
{
    zdgb.style.display="block";
    var t1=3;
//  var t2=ctl00_RightContentTitlePlace_tiao;
    countDown(t1);                         
}

    function countDown(secs)
    {
       ctl00_RightContentTitlePlace_tiao.innerText=secs;
       if(--secs>=0)
       {
         setTimeout("countDown("+secs+")",1000);
       }
       else
       {
         window.opener=null;
         window.open('','_self');
         window.close();
       }
   }

</script>
<script type="text/javascript" language="javascript">
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
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="true" EnableScriptGlobalization="true">
</asp:ToolkitScriptManager>
 <div class="RightContent">
     <asp:Panel ID="palFP" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
              <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="100%" >
<tr>
<td>合同编号</td>
<td>
    <asp:TextBox ID="txtHTBH" runat="server" Enabled="false"></asp:TextBox>
 </td>
 <td>生产制号</td>
 <td>
     <asp:TextBox ID="txtSCZH" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td>开票单位</td>
<td>
 <input type="text" id="txtKPDW" runat="server" readonly="readonly"/>
  <asp:HyperLink ID="hlSelect" runat="server" CssClass="hand" onClick="SupplierSelect()">
     <asp:Image ID="AddImage" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                       选择 </asp:HyperLink>
</td>
<td>开票日期</td>
<td>
    <asp:TextBox ID="txtKPRQ" onchange="dateCheck(this)" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtKPRQ" ErrorMessage="*"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ControlToValidate="txtKPRQ" ID="RegularExpressionValidator2" runat="server" ErrorMessage="YYYY-MM-DD" ValidationExpression="^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$"></asp:RegularExpressionValidator>
    <asp:CalendarExtender ID="calender_kp" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="txtKPRQ"></asp:CalendarExtender>
    
    </td>
</tr>
<tr>
<td style="width:100px">开票金额<span style="color:Red">（含税）</span></td>
<td >
    <asp:TextBox ID="txtKPJE" runat="server" onblur="javascript:check_num(this)" Text="0"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtKPJE" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td> 
    <td>收票日期</td>
    <td><asp:TextBox ID="txtSPRQ" onchange="dateCheck(this)" runat="server"></asp:TextBox>(财务填写)
    <asp:RegularExpressionValidator ControlToValidate="txtSPRQ" ID="RegularExpressionValidator1" runat="server"
     ErrorMessage="YYYY-MM-DD" ValidationExpression="^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$"></asp:RegularExpressionValidator>
     <asp:CalendarExtender ID="calender_sp" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="txtSPRQ"></asp:CalendarExtender>
     </td>   
</tr>
<tr>
<td>发票单号</td>
<td>
    <asp:TextBox ID="txtFPDH" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtFPDH" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
   <td>数量</td>
    <td>
        <asp:TextBox ID="txtSL" runat="server" Text="1"></asp:TextBox>
        <asp:RegularExpressionValidator ID="txtSLRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="请输入正确的格式！" ControlToValidate="txtSL" ></asp:RegularExpressionValidator>
    </td> 
</tr>
<tr>    
    <td>凭证号</td>
   <td colspan="3">
   <table><tr><td>
       <asp:RadioButtonList ID="rblPZ" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
       <asp:ListItem Text="暂无" Value="0" Selected="True"></asp:ListItem>
       <asp:ListItem Value="1" Text="有"></asp:ListItem>
       </asp:RadioButtonList></td><td>
     <asp:TextBox ID="txtPZH" runat="server" AutoPostBack="true" OnTextChanged="txtPZH_Changed"></asp:TextBox></td></tr></table>
   </td>
</tr>
<tr>
<td>经办人</td>
<td>
<asp:TextBox ID="txtJBR" runat="server"></asp:TextBox>
</td>
 <td>备注</td>
 <td>
    <asp:TextBox ID="txtBZ" runat="server" TextMode="MultiLine" Height="50px" Width="60%"></asp:TextBox>
   
    </td>
    </tr>
</table>
           </ContentTemplate>
     </asp:UpdatePanel>
</asp:Panel>
 </div>
 <div>
   
    </div>           
</asp:Content>
