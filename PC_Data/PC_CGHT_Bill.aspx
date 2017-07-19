<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="PC_CGHT_Bill.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_CGHT_Bill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblFP" runat="server"></asp:Label>
            </td>
            <td align="right">
                <asp:Button ID="btnConfirm" runat="server" Text="保存" OnClick="btnConfirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Text="操作成功!" ForeColor="Red" Visible="false"></asp:Label>
            </td>
            <td>
                <div id="zdgb" style="display: none">
                    <span id="tiao" runat="server"></span><span>秒后自动关闭...</span></div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script type="text/javascript" language="javascript">
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
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="true"
            EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>
        <asp:Panel ID="palFP" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                        width="100%">
                        <tr>
                            <td width="100px">
                                合同号
                            </td>
                            <td>
                                <asp:TextBox ID="txtHTBH" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td width="100px">
                                对方合同号
                            </td>
                            <td>
                                <asp:TextBox ID="txtDFBH" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                开票单位
                            </td>
                            <td>
                                <asp:TextBox ID="txtKPDW" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                产品名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtEng" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                开票金额<span style="color: Red">（含税）</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtKPJE" runat="server" onblur="javascript:checkNum(this)" Text="0"></asp:TextBox>（万元）
                            </td>
                            <td>
                                数量
                            </td>
                            <td>
                                <asp:TextBox ID="txtSL" runat="server" Text="1" Width="40px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="txtSLRegularExpressionValidator" runat="server"
                                    ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="请输入正确的格式！"
                                    ControlToValidate="txtSL" Font-Size="Smaller"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                发票号
                            </td>
                            <td>
                                <asp:TextBox ID="txtFPDH" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFPDH"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 50px">
                                开票日期<br />
                                确认销售
                            </td>
                            <td valign="middle">
                                <asp:TextBox ID="txtKPRQ" onchange="dateCheck(this)" runat="server"></asp:TextBox>
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
                            <td style="width: 50px">
                                开票时间
                            </td>
                            <td style="vertical-align: middle">
                                <asp:TextBox ID="txtLPSJ" onchange="dateCheck(this)" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLPSJ"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtLPSJ" ID="RegularExpressionValidator2"
                                    runat="server" ErrorMessage="YYYY-MM-DD" ValidationExpression="^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$"></asp:RegularExpressionValidator>
                                <asp:CalendarExtender ID="calender_lp" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                    TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtLPSJ">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                经办人
                            </td>
                            <td>
                                <asp:TextBox ID="txtJBR" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                备注
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtBZ" runat="server" TextMode="MultiLine" Width="60%" Height="40px"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
