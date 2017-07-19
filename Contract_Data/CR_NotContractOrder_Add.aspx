<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CR_NotContractOrder_Add.aspx.cs"
    MasterPageFile="~/masters/PopupBase.Master" Inherits="ZCZJ_DPF.Contract_Data.CR_NotContractOrder_Add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    <asp:Label ID="lblDDCR" runat="server" Text="---> 新建采购订单请款"></asp:Label>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">
    
function btnDDPrint_onclick() 
{   
window.showModalDialog('CR_NotContractOrder_Export.aspx?CQID=<%=lblDDCR_ID.Text %>','',"dialogWidth=800px;dialogHeight=700px");
}

function Actual_Payment_DX(str)
{
//付款总金额
var t=Arabia_to_Chinese_UserString(str);
//大写
document.getElementById('<%=txtDDFKJEDX.ClientID%>').value=t;
}
 
  //检验日期格式如：2012-01-01
function dateCheck(obj) 
{
    var value = obj.value;
    if (value != "") 
    {
        var re = new RegExp("^([0-9]{4})(-)([0-9]{2})(-)([0-9]{2})$");
        m = re.exec(value)
        if (m == null) 
        {
            obj.style.background = "yellow";
            obj.value = "";
            alert('请输入正确的时间格式如：2012-01-01');
        }
    }
} 
  function View_PurOrder(orderid)
{
      var autonum=Math.round(10000*Math.random());
      window.open("../PC_Data/TBPC_Purordertotal_list.aspx?autonum="+autonum+"&TotalOrder="+orderid+"");       
} 
//查看发票
 function View_Bill(bill,action)
{
      var autonum=Math.round(10000*Math.random());
      window.open("../Contract_Data/CR_OrderBill_Add.aspx?Action="+action+"&autonum="+autonum+"&bill="+bill+"");   
} 


    </script>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
    </div>
    <div class="RightContent">
        <div class="box-wrapper">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td style="width: 60px;">
                                <asp:Label ID="DDLabel1" runat="server" Text="操作状态："></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblDDState" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rblDDState_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">保存</asp:ListItem>
                                    <asp:ListItem Value="1">正在签字</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnDDSave" runat="server" CssClass="button-inner" Text="保 存" OnClientClick="javascript:return confirm('确认提交吗？');"
                                    OnClick="btnDDSave_Click" />
                                <asp:Label ID="lblDDRemind" runat="server" ForeColor="Red" Text="操作成功!" Visible="false"></asp:Label>
                            </td>
                            <td align="right" style="width: 20%;">
                                <input id="btnDDPrint" class="button-inner" runat="server" type="button" value="打印请款单"
                                    onclick="return btnDDPrint_onclick()" />&nbsp;&nbsp;&nbsp;
                                <input id="btnDDClose" type="button" class="button-inner" value="关 闭" onclick="javascript:window.close();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="box-outer">
                <asp:Panel ID="palDDQK" runat="server">
                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="2"
                        id="tdcr" runat="server">
                        <tr style="border-width: 100%;">
                            <td style="height: 50%" colspan="6" align="center">
                                <h2 style="vertical-align: inherit; height: 20px;" align="center">
                                    采购订单请款</h2>
                            </td>
                        </tr>
                        <tr style="border-width: 100%;">
                            <td colspan="6">
                                <span style="color: Red">提示：1.开户行及帐号根据合同中选择的厂商从基础数据厂商信息里读取，若为空，可到相应厂商处添加，也可在请款单上直接填写<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.手动添加订单号时请同时修改订单总金额，请务必确认收款单位与订单发货单位一致
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" align="left">
                                采购订单请款单号&nbsp;&nbsp;
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lblDDCR_ID" runat="server"></asp:Label>
                            </td>
                            <td colspan="3" align="right">
                                <asp:Label ID="lblDDHBDW" runat="server" Text="货币单位 元：RMB" ForeColor="blue" Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" style="width: 15%">
                                请款部门
                            </td>
                            <td colspan="2" style="width: 35%">
                                <asp:DropDownList ID="dplDDQKBM" runat="server" Width="30%">
                                </asp:DropDownList>
                            </td>
                            <td colspan="1" style="width: 15%">
                                请款时间
                            </td>
                            <td colspan="2" style="width: 35%">
                                <asp:TextBox ID="txtDDQKRQ" runat="server" Width="50%" onchange="dateCheck(this)"></asp:TextBox>
                                <asp:CalendarExtender ID="calender_sta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                    TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtDDQKRQ">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1">
                                请款人
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDDQKR" runat="server" Font-Size="Medium" Width="30%"></asp:TextBox>
                            </td>
                            <td colspan="1">
                                请款事由
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDDQKSY" runat="server" Font-Size="Medium" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="3" style="width: 15%">
                                采购订单号
                            </td>
                            <td colspan="2" rowspan="3" style="width: 35%">
                                <asp:TextBox ID="txtDDCode" runat="server" TextMode="MultiLine" Rows="4" 
                                    Width="70%"></asp:TextBox>
                                <asp:LinkButton ID="Lbtn_ViewOrder" runat="server" OnClick="btn_ViewOrder_Click"
                                    Visible="false">
                                    <asp:Image ID="imgToPurorder" Style="cursor: hand" ToolTip="到订单" ImageUrl="~/Assets/icons/dindan.jpeg"
                                        Height="17" Width="17" runat="server" />
                                    到采购订单
                                </asp:LinkButton>
                                <br />
                                <span style="color: Red">格式要求：PORD00000001,PORD00000002(","为英文逗号)</span>
                            </td>
                            <td colspan="1" width="15%">
                                收款单位名称：
                            </td>
                            <td colspan="2" style="width: 35%">
                                <asp:TextBox ID="txtDDCSName" runat="server" Enabled="false" Width="85%"></asp:TextBox>
                                <asp:TextBox ID="txtDDCSCode" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                开户银行:
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDDCSBank" runat="server" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                账 号:
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDDCSAccount" runat="server" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1">
                                采购订单总金额
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDDZJE" runat="server" ForeColor="Red" Font-Size="Medium" Width="70%"></asp:TextBox>
                            </td>
                            <td colspan="1">
                                支付方式
                            </td>
                            <td colspan="2" align="left">
                                <asp:RadioButtonList ID="rblDDZFFS" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">现金&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">转支&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="2">电汇&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="3">票汇&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="4">其他&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" rowspan="3">
                                备注
                            </td>
                            <td colspan="2" rowspan="3">
                                <asp:TextBox ID="txtNote" runat="server" Width="70%" Font-Size="Medium" Rows="3"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td colspan="1" rowspan="1">
                                付款金额（小写）￥：
                            </td>
                            <td colspan="2" rowspan="1">
                                <asp:TextBox ID="txtDDFKJE" OnTextChanged="txtDDFKJE_TextChanged" AutoPostBack="true"
                                    ForeColor="Green" runat="server" Font-Size="Medium" Width="80%" onchange="javascript:check_num(this)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" rowspan="1">
                                付款金额（大写）：
                            </td>
                            <td colspan="2" rowspan="1">
                                <asp:TextBox ID="txtDDFKJEDX" ForeColor="Green" Width="80%" Font-Size="Medium" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" rowspan="1">
                                票 证 号：
                            </td>
                            <td colspan="2" rowspan="1">
                                <asp:TextBox ID="txtDDPZH" runat="server" Font-Size="Medium" Width="65%"></asp:TextBox>
                                <asp:LinkButton ID="Lbtn_ViewBill" runat="server" OnClick="btn_ViewBill_Click" Visible="false">
                                    <asp:Image ID="ImageToBill" Style="cursor: hand" ToolTip="到发票" ImageUrl="~/Assets/icons/forum.gif"
                                        Height="17" Width="17" runat="server" />
                                    到发票
                                </asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1">
                                主管领导审核
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDDZGLD" runat="server" Font-Size="Medium" Width="30%"></asp:TextBox>
                            </td>
                            <td colspan="1">
                                部门意见
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDDBMYJ" runat="server" Font-Size="Medium" Width="30%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1">
                                领导批示
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDDLD" runat="server" Font-Size="Medium" Width="30%"></asp:TextBox>
                            </td>
                            <td colspan="1">
                                财务审核
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtDDCWSH" runat="server" Font-Size="Medium" Width="30%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
