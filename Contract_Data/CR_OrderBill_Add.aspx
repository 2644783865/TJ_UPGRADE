<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/PopupBase.master"
    CodeBehind="CR_OrderBill_Add.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CR_OrderBill_Add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    <table width="100%">
        <tr>
            <td>
                &nbsp;&nbsp;<asp:Label ID="lblFP" runat="server" Font-Size="Medium" ForeColor="Blue"
                    Font-Bold="true"></asp:Label>
            </td>
            <td align="right">
                <asp:Button ID="btn_Add" runat="server" Text="保存并关闭" Font-Size="Medium" Font-Bold="true"
                    Visible="true" OnClick="btn_Add_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_CRAdd" runat="server" Text="生成请款单" Font-Size="Medium" Font-Bold="true"
                    Visible="false" OnClick="btn_CRAdd_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Text="操作成功!" ForeColor="Red" Visible="false"></asp:Label>
            </td>
            <td>
                <div id="zdgb" style="display: none">
                    <span id="tiao" runat="server"></span><span>秒后自动关闭...</span></div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">
  function AutoClose()
{
    zdgb.style.display="block";
    var t1=3;//设置3秒钟
    countDown(t1);
}

  
  function SupplierSelect()
{
    var i=window.showModalDialog('SupplierSelect.aspx?type=4','',"dialogHeight:500px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
    if(i!=null)
    {
    document.getElementById('<%=txtCSCode.ClientID%>').value=i[0];
    document.getElementById('<%=txtCSName.ClientID%>').value=i[1];
    }
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
 
// function Re_DDJE(orderzje)
// {
//    var i =window.showModalDialog(orderzje);
//    window.opener.document.getElementById('<%=txtDDZJE.ClientID%>').value=i;
// }
 
   function View_PurOrder(orderid)
{
      var autonum=Math.round(10000*Math.random());
      window.open("../PC_Data/TBPC_Purordertotal_list.aspx?autonum="+autonum+"&TotalOrder="+orderid+"");       
}

function Add_DDQK(orderid,ZJE,csinfo,bill)
    {
        var autonum=Math.round(10000*Math.random());
        window.open("../Contract_Data/CR_NotContractOrder_Add.aspx?Action=Badd&autonum="+autonum+"&orderid="+orderid+"&csinfo="+csinfo+"&ZJE="+ZJE+"&bill="+bill+"");
    }
    </script>

    <div class="RightContent">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="true"
            EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>
        <asp:Panel ID="palFP" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                        width="100%">
                        <tr style="border-width: 100%;">
                            <td style="height: 30px" colspan="6" align="center">
                                <h2>
                                    采购订单发票</h2>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" style="width: 15%">
                                发票记录编号：
                            </td>
                            <td colspan="2" style="width: 35%">
                                <asp:TextBox ID="txtJLID" Font-Size="Medium" ForeColor="Blue" runat="server" Width="70%"></asp:TextBox>
                            </td>
                            <td colspan="1" style="width: 15%">
                                开票日期
                            </td>
                            <td colspan="2" style="width: 35%">
                                <asp:TextBox ID="txtKPRQ" onchange="dateCheck(this)" Font-Size="Medium" runat="server"
                                    Width="50%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtKPRQ"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtKPRQ" ID="RegularExpressionValidator1"
                                    runat="server" ErrorMessage="YYYY-MM-DD" ValidationExpression="^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$"></asp:RegularExpressionValidator>
                                <asp:CalendarExtender ID="calender_kp" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                    TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtKPRQ">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="3" colspan="1" style="width: 15%">
                                采购订单号
                            </td>
                            <td rowspan="3" colspan="2" style="width: 35%">
                                <asp:TextBox ID="txtDDCode" runat="server" Enabled="true" TextMode="MultiLine" Rows="4"
                                    Width="70%"></asp:TextBox>
                                <asp:LinkButton ID="Lbtn_ViewOrder" runat="server" OnClick="btn_ViewOrder_Click"
                                    Visible="false">
                                    <asp:Image ID="imgToPurorder" Style="cursor: hand" ToolTip="到订单" ImageUrl="~/Assets/icons/dindan.jpeg"
                                        Height="17" Width="17" runat="server" />
                                    到采购订单
                                </asp:LinkButton>
                            </td>
                            <td colspan="1" rowspan="2">
                                开票单位
                            </td>
                            <td colspan="1" rowspan="1">
                                <asp:TextBox ID="txtCSCode" runat="server" Width="50%"></asp:TextBox>
                                <span style="color: Red">(单位编码)
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" rowspan="1">
                                <asp:TextBox ID="txtCSName" runat="server" Width="70%"></asp:TextBox>
                                <asp:HyperLink ID="hlSelect" runat="server" CssClass="hand" Visible="false" onClick="SupplierSelect()">
                                    <asp:Image ID="AddImage" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                        align="absmiddle" runat="server" />
                                    选择
                                </asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" rowspan="1">
                                发票单号
                            </td>
                            <td colspan="2" rowspan="1">
                                <asp:TextBox ID="txtFPDH" Font-Size="Medium" runat="server" Width="70%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFPDH"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                订单总金额
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDDZJE" runat="server" Enabled="true" ForeColor="Red" Font-Size="Medium"
                                    Width="70%"></asp:TextBox>
                            </td>
                            <td colspan="1" rowspan="1">
                                数量
                            </td>
                            <td colspan="2" rowspan="1">
                                <asp:TextBox ID="txtFPSL" runat="server" Text="1" Font-Size="Medium" Width="45%"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="txtFPSLRegularExpressionValidator" runat="server"
                                    ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="请输入正确的格式！"
                                    ControlToValidate="txtFPSL"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                经办人
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtJBR" runat="server" Width="70%" Font-Size="Medium"></asp:TextBox>
                            </td>
                            <td>
                                开票金额<span style="color: Red"> (含税金额)</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtKPJE" runat="server" onblur="javascript:check_num(this)" Text="0"
                                    Width="45%" Font-Size="Medium" ForeColor="Green"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" rowspan="3">
                                备注
                            </td>
                            <td colspan="2" rowspan="3">
                                <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Width="70%" Rows="4"></asp:TextBox>
                                <br />
                            </td>
                            <td colspan="1" rowspan="1">
                                收票时间<span style="color: Red"> (财务填写)
                            </td>
                            <td colspan="2" rowspan="1">
                                <asp:TextBox ID="txtSPRQ" onchange="dateCheck(this)" runat="server" Width="45%" Font-Size="Medium"></asp:TextBox>
                                <asp:RegularExpressionValidator ControlToValidate="txtSPRQ" ID="RegularExpressionValidator2"
                                    runat="server" ErrorMessage="YYYY-MM-DD" ValidationExpression="^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$"></asp:RegularExpressionValidator>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                    TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtSPRQ">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" colspan="1">
                                凭证号&nbsp;&nbsp;
                                <asp:RadioButtonList ID="rblPZ" runat="server" RepeatColumns="2" RepeatDirection="Vertical">
                                    <asp:ListItem Text="暂无" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="有" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td rowspan="2" colspan="2">
                                <asp:TextBox ID="txtPZH" runat="server" Font-Size="Medium" AutoPostBack="true" OnTextChanged="txtPZH_Changed"
                                    Width="70%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
