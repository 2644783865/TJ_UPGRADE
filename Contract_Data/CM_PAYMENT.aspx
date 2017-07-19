<%@ Page Language="C#" MasterPageFile="~/Masters/PopupBase.master" AutoEventWireup="true" CodeBehind="CM_PAYMENT.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_PAYMENT" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    <link href="StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>     
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>  
<script language="javascript" type="text/javascript">

function ViewCR_onclick() {
  window.showModalDialog("CM_CHECKREQUEST.aspx?Action=View&CRid=<%=CRid %>","","dialogHeight:800px;dialogWidth:1100px");  
}
function Paid(obj){
var num=obj.value;
var patten=/^\+?[0-9][0-9]{0,9}(\.[0-9]{1,2})?$/;
if(!patten.test(num))
{ alert('请输入正确的数据格式！！！');
obj.value="0";
obj.focus();
}
else
{
 //alert("本次支付金额："+obj.value);
 //已付金额
 var yfje=document.getElementById('<%=txtYZFJE.ClientID %>').value;
 //合同金额
 var htje=document.getElementById('<%=txtHTJE.ClientID %>').value;
 //累计支付金额=已付金额+本次支付金额
 var ljzfje=parseFloat(obj.value)+parseFloat(yfje);
 //alert("累计支付金额："+ljzfje);
 document.getElementById('<%=txtLJZFJE.ClientID %>').value=ljzfje;
 //未付金额
 var wfje=parseFloat(htje)-parseFloat(ljzfje);

 document.getElementById('<%=txtWFJE.ClientID %>').value=wfje;
 if(parseFloat(wfje)<0)
 {
   alert("累计支出金额超出合同金额！\r\r请确认【本次付款金额】");
 }
 }
 
}

function Check_CHG_JE(obj)
{
  var num=obj.value;
var patten=/^(\+|\-)?[0-9][0-9]{0,9}(\.[0-9]{1,2})?$/;
if(!patten.test(num))
{ alert('请输入正确的数据格式！！！');
obj.value="0";
obj.focus();
}
else
{
 //合同金额
 var htje=document.getElementById('<%=txtHTJE.ClientID %>').value;
 //已付金额
 var yfje=document.getElementById('<%=txtYZFJE.ClientID %>').value;
 //请款金额
 var qkje=document.getElementById('<%=txtQKJE.ClientID %>').value;
 //累计支付金额=已付金额+本次支付金额
 var ljzfje=parseFloat(obj.value)+parseFloat(yfje); 
 document.getElementById('<%=txtLJZFJE.ClientID %>').value=ljzfje;
 //未付金额
 var wfje=parseFloat(htje)-parseFloat(ljzfje);
 document.getElementById('<%=txtWFJE.ClientID %>').value=wfje;
 //本期已支付
 var bqyzf=document.getElementById('<%=txtBQYZF.ClientID %>').value;
  //变更前金额
 var be_CHGJE=document.getElementById('<%=txt_Before_CHG.ClientID %>').value;
 //变更后金额
 var Aft_CHGJE =parseFloat(be_CHGJE)+parseFloat(obj.value);
  
 document.getElementById('<%=txtJE_After_CHG.ClientID %>').value=Aft_CHGJE;
 
 
 if(parseFloat(bqyzf)+parseFloat(obj.value)>parseFloat(qkje))
 {
   alert("本期已支付金额大于请款金额！");
 }
 if(parseFloat(Aft_CHGJE)<=0)
 {
     alert("支付金额必须大于0！");
 }
 
 }
}

function AutoClose()
{
    zdgb.style.display="block";
    var t1=3;
    countDown(t1);
}
  function countDown(secs)
    {
       ctl00_PrimaryContent_tiao.innerText=secs;
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
    <table width="100%">
    <tr>
    <td> <asp:Label ID="lblAction" runat="server"></asp:Label> </td>
    <td align="center">
        <asp:Button ID="defaultBtn" runat="server" Text="NoUse"  Visible="false"/>        
        <asp:HyperLink ID="hlk_ChangeJE" runat="server" CssClass="hand">
        <asp:Image ID="Image6" ImageUrl="~/Assets/images/modify.gif" runat="server" />
        修改支出金额</asp:HyperLink>
        <asp:PopupControlExtender ID="PopupControlExtender1" runat="server"  TargetControlID="hlk_ChangeJE" PopupControlID="palJECHG" Position="Bottom" OffsetY="4">
        </asp:PopupControlExtender>
        <asp:Panel ID="palJECHG" runat="server" style="display:none; visibility:hidden; width:350px">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>  
         <table style='background-color:#f3f3f3; border: #A8B7EC 3px solid;font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>
        <tr><td  style='background-color:#A8B7EC; color:white;'><b>金额变更</b></td>
        <td style='background-color:#A8B7EC; color:white;' align='right'  valign='middle'><a onclick='document.body.click(); return false;' style='cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
        </td></tr>
         <tr>
         <td align="right"><asp:Label ID="lbl_Before_CHG" runat="server"  Text="变更前金额:"></asp:Label></td>
         <td> <asp:TextBox ID="txt_Before_CHG" runat="server" Enabled="false"></asp:TextBox></td>
         </tr>  
         <tr>
         <td align="right"><asp:Label ID="lblJE_CHG" runat="server"  Text="变更增加金额:"></asp:Label></td>
         <td><asp:TextBox ID="txtJE_CHG" runat="server" onblur="javascript:Check_CHG_JE(this)"></asp:TextBox></td>
         </tr>     
         <tr>
         <td align="right"><asp:Label ID="lblJE_Before_CHG" runat="server"  Text="变更后金额:"></asp:Label></td>
         <td> <asp:TextBox ID="txtJE_After_CHG" runat="server" Enabled="false"></asp:TextBox></td>
         </tr>         
         <tr>
         <td colspan="2" align="center"><br /><asp:Button ID="btnConfirmCHG" runat="server" Text="确 定" OnClick="btnJECHG_Click" 
         OnClientClick="javascript:return confirm('确定修改支付金额吗？\r请在备注中添加说明以便查阅')"/></td>
         </tr>         
         </table>
         </ContentTemplate></asp:UpdatePanel>
        </asp:Panel> 
        
        
    </td>     
    <td align="right"> 
    <input id="ViewCR" type="button" class="buttOver" value="查看请款单"  onclick="return ViewCR_onclick()" />
    &nbsp; &nbsp; &nbsp;      
        <asp:Button ID="btnQRZC" runat="server" CssClass="buttOver" Text="确认提交" onclick="btnQRZC_Click" OnClientClick="javascript:return confirm('确认提交吗？')" />
        &nbsp; 
        </td>  
    
    </tr>
    </table>
    

</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<hr />
<asp:Panel ID="palFKDetail" runat="server">
<table align="center" cellpadding="4" cellspacing="1" class="toptable grid" width="100%">
<tr>
<td>付款单号</td>
<td>
    <asp:Label ID="lblPRid" runat="server"></asp:Label></td>
    <td>合同编号</td>
    <td>
        <asp:Label ID="lblHTBH" runat="server" ></asp:Label></td>
</tr>
</table>
<table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="100%">
<tr>
<td>请款日期</td>
<td>
    <asp:TextBox ID="txtQKRQ" runat="server" Enabled="false"></asp:TextBox></td>
<td>支出日期</td>
<td>
    <asp:TextBox ID="txtZCRQ"  runat="server"  Text="" onchange="dateCheck(this)"/>
    <asp:CalendarExtender ID="calender_zcrq" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="txtZCRQ"></asp:CalendarExtender>
    </td>
</tr>
<tr>
<td>项款名称</td>
<td>
    <asp:TextBox ID="txtXKMC" runat="server" Enabled="false"></asp:TextBox></td>
    <td>所属项目</td>
    <td>
        <asp:TextBox ID="txtSSXM" runat="server" Enabled="false"></asp:TextBox></td>
</tr>
<tr>
<td>设备名称</td>
<td>
    <asp:TextBox ID="txtSBMC" runat="server" Enabled="false"></asp:TextBox></td>
<td>供货商名称</td>
<td>
    <asp:TextBox ID="txtGHSMC" runat="server" Enabled="false"></asp:TextBox></td>
</tr>
<tr>
<td>合同金额</td>
<td>
    <asp:TextBox ID="txtHTJE" runat="server" Enabled="false"></asp:TextBox></td>
<td>已支付金额</td>
<td>
    <asp:TextBox ID="txtYZFJE" runat="server" Enabled="false"></asp:TextBox></td>
</tr>
<tr>
<td>请款金额</td>
<td>
    <asp:TextBox ID="txtQKJE" runat="server"  Enabled="false"></asp:TextBox></td>
<td>请款人</td>
<td>
    <asp:TextBox ID="txtQKR" runat="server" Enabled="false"></asp:TextBox></td>    
</tr>
<tr>
<td>本期已支付</td>
<td>
    <asp:TextBox ID="txtBQYZF" runat="server" Enabled="false"></asp:TextBox>
    </td>
<td>累计支付金额</td>
<td> 
    <asp:TextBox ID="txtLJZFJE" runat="server" Enabled="false"></asp:TextBox></td>
</tr>
<tr> 
<td>本次支付金额</td>
<td><asp:TextBox ID="txtBCZFJE" runat="server" onblur="javascript:Paid(this)" AutoPostBack="true" OnTextChanged="txtBCZFJE_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    <asp:Label ID="lblUnit" runat="server" Text="元：RMB"></asp:Label>
    </td>
<td>合同未付金额</td>
<td>
<asp:TextBox ID="txtWFJE" runat="server" Enabled="false"></asp:TextBox>
</td>      
</tr>
<tr>
<td >备注</td>
<td colspan="3">
    <asp:TextBox ID="txtBZ" runat="server" Height="30px" Width="60%" TextMode="MultiLine"></asp:TextBox></td>
</tr>
<tr>
<td>凭证号</td>
<td colspan="3">
    <table>
    <tr>
    <td>
        <asp:RadioButtonList ID="rblPZ" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
        <asp:ListItem Text="暂无" Value="0" Selected="True"></asp:ListItem>
        <asp:ListItem Text="有" Value="1"></asp:ListItem>
        </asp:RadioButtonList>
    </td>
    <td>
     <asp:TextBox ID="txtPZH" runat="server"></asp:TextBox></td>
    </tr>
    </table>
</td>
</tr>
</table>
<table width="100%" >
<tr>
<td >
<table width="100%" style="text-align:left">
<tr>
<td colspan="2">
<asp:Label ID="lbl_PZ" runat="server" Text="若暂无凭证，可在【收/付款记录】中添加！" ForeColor="Red" Visible="false"></asp:Label>

</td>
</tr>
<tr><td> 
<asp:Label ID="lblRemind" runat="server" Text="操作成功！" ForeColor="Red" Visible="false"></asp:Label>
</td>
 <td> <div id="zdgb" style="display:none"><span id="tiao" runat="server"></span><span>秒后自动关闭...</span></div></td>
</tr></table>
</td>
<td align="right">
<asp:Panel ID="pal_yzf" runat="server" Visible="false">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/yfk.gif" />
    </asp:Panel>

</td>
</tr>

</table>
</asp:Panel>            
</asp:Content>
