<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="PC_CGHT_Payment.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_CGHT_Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    付款记录单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">
        function AutoClose() {
            zdgb.style.display = "block";
            var t1 = 3; //设置5秒钟
            countDown(t1);
        }
        function countDown(secs) {
            ctl00_PrimaryContent_tiao.innerText = secs;
            if (--secs >= 0) {
                setTimeout("countDown(" + secs + ")", 1000);
            }
            else {
                window.opener = null;
                window.open('', '_self');
                window.close();
            }
        }
        //检验日期格式如：2012-01-01
        function dateCheck(obj) {
            var value = obj.value;
            if (value != "") {
                var re = new RegExp("^([0-9]{4})(-)([0-9]{2})(-)([0-9]{2})$");
                m = re.exec(value)
                if (m == null) {
                    obj.style.background = "yellow";
                    obj.value = "";
                    alert('请输入正确的时间格式如：2012-01-01');
                }
            }
        }

        function checkNum(obj) {
            var num = obj.value;
            var patten = /^(\+|\-)?[0-9][0-9]{0,9}(\.[0-9]{1,6})?$/;
            if (!patten.test(num)) {
                alert('请输入正确的数据格式！！！');
                obj.value = "0";
                obj.focus();
            }
        }
    </script>

    <div class="RightContent">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePartialRendering="false"
            runat="server">
        </asp:ToolkitScriptManager>
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="lblAction" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnConfirm" CssClass="button-inner" runat="server" Text="提 交" OnClick="btnConfirm_Click" />
                                <asp:Label ID="lblState" runat="server" ForeColor="Red" Visible="False">操作成功!</asp:Label>
                            </td>
                            <td>
                                <div id="zdgb" style="display: none">
                                    <span id="tiao" runat="server"></span><span>秒后自动关闭...</span></div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="height: 8px" class="box_top">
            <div class="box-wrapper">
                <div class="box-outer">
                    <asp:Panel ID="palYKD" runat="server">
                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                            width="100%">
                            <tr>
                                <td colspan="4" align="center" style="height: 30px;">
                                    <h2>
                                        付款记录单</h2>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    付款单号：<asp:Label ID="lblYKDH" runat="server"></asp:Label>
                                </td>
                                <td colspan="2" align="center">
                                    货币单位：<asp:Label ID="lblhHBDW" runat="server" Text="万元：RMB"></asp:Label>
                                </td>
                            </tr>
                         
                            <tr>
                                <td>
                                    买方合同编号
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHTBH" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    款项名称
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="lblKXMC">
                                        <asp:ListItem Text="预付款" Value="预付款"></asp:ListItem>
                                        <asp:ListItem Text="进度款" Value="进度款"></asp:ListItem>
                                        <asp:ListItem Text="发货款" Value="发货款"></asp:ListItem>
                                        <asp:ListItem Text="质保金" Value="质保金"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                               <tr>
                                <td>
                                    市场部合同号：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSCBHTH" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    设备名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtShebei" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    付款金额
                                </td>
                                <td>
                                    <asp:TextBox ID="txtJE" runat="server" onblur="javascript:checkNum(this)" Text="0"></asp:TextBox>
                                    <input type="hidden" runat="server" id="hidJE" />
                                </td>
                                <td>
                                    付款日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtYKRQ" runat="server" onchange="dateCheck(this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtYKRQ"
                                        runat="server" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ControlToValidate="txtYKRQ" ID="RegularExpressionValidator1"
                                        runat="server" ErrorMessage="YYYY-MM-DD" ValidationExpression="^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$"></asp:RegularExpressionValidator>
                                </td>
                                <asp:CalendarExtender ID="calender_filldate" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                    TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtYKRQ">
                                </asp:CalendarExtender>
                            </tr>
                            <tr>
                                <td>
                                    付款方式
                                </td>
                                <td>
                                    <asp:DropDownList ID="dplSKFS" runat="server">
                                        <asp:ListItem Text="-请选择-" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="现金" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="转支" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="电汇" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="票汇" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="其他" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    支付比例:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSWZ" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    备注
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtCWB" runat="server" Text="" TextMode="MultiLine" Height="40px"
                                        Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
