<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/masters/PopupBase.Master"
    CodeBehind="CM_CHECKREQUEST.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_CHECKREQUEST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblCR" runat="server" Text="新建请款单"></asp:Label>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function btnPrint_onclick() {
            window.showModalDialog('CM_CHECKREQUEST_VIEW.aspx?CRid=<%=lblCR_ID.Text %>', '', "dialogWidth=800px;dialogHeight=700px");
        }

        function Actual_Payment_DX(str) {
            //实付款
            var t = Arabia_to_Chinese_UserString(str);
            //大写
            document.getElementById('<%=CR_BQSFKDX.ClientID%>').innerHTML = t;
            document.getElementById('<%=bqsfkdx.ClientID%>').value = t;
        }

        // ]]>
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

        function setBiLi(obj) {
            var value = obj.value;
            var tr = obj.parentNode.parentNode;
            var a = tr.getElementsByTagName('input');
            var b = a[1].value;
            var bl = value / b * 100;
            a[0].value = bl.toFixed(2);
        }

        function calculate() {
            var a = document.getElementById('gr');
            var b = a.getElementsByTagName('input');
            var n = b.length / 3;
            var sum = parseInt(0, 10);
            for (var i = 0; i < n; i++) {
                sum += parseInt(b[2 + i * 3].value, 10);
            }
            Actual_Payment_DX(sum.toString());
            document.getElementById('<%=CR_BQSFK.ClientID%>').innerHTML = sum.toFixed(2);
            document.getElementById('<%=bqsfk.ClientID%>').value = sum.toFixed(2);
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
                            <td style="width: 100px;">
                                <asp:Label ID="Label1" runat="server" Text="操作状态："></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblState" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblState_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="0">保存</asp:ListItem>
                                    <asp:ListItem Value="1">正在签字</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnSave" runat="server" CssClass="button-inner" Text="保 存" OnClientClick="javascript:return confirm('确认提交吗？');"
                                    OnClick="btnSave_Click" /><asp:Label ID="lblRemind" runat="server" ForeColor="Red"
                                        Text="操作成功!" Visible="false"></asp:Label>
                            </td>
                            <td align="right" style="width: 10%;">
                                <input id="btnPrint" class="button-inner" runat="server" type="button" value="打印请款单"
                                    onclick="return btnPrint_onclick()" />&nbsp;&nbsp;&nbsp;
                                <input id="Button1" type="button" class="button-inner" value="关 闭" onclick="javascript:window.close();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="box-outer">
                <asp:Panel ID="palQK" runat="server">
                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                        id="tdcr">
                        <tr style="border-width: 0;">
                            <td colspan="6" align="center">
                                <h2 style="margin-top: 12px">
                                    请款单</h2>
                            </td>
                        </tr>
                        <tr style="border-width: 0;">
                            <td colspan="6">
                                <span style="color: Red">提示：1.开户行及帐号根据合同中选择的厂商从基础数据厂商信息里读取，若为空，可到相应厂商处添加，也可在请款单上直接填写<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.添加合同时直接输入厂商名字（而不是从选择框中选择的）将无法关联到基础数据中厂商信息，重新选择一次厂商即可关联
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" width="50%">
                                单号&nbsp;&nbsp;
                                <asp:Label ID="lblCR_ID" runat="server"></asp:Label>
                            </td>
                            <td colspan="3" align="center">
                                货币单位&nbsp;&nbsp;<asp:Label ID="lblHBDW" runat="server" Text="元：RMB"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                请款部门
                            </td>
                            <td>
                                <asp:DropDownList ID="dplQKBM" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                请款日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtQKRQ" runat="server" onchange="dateCheck(this)"></asp:TextBox>
                                <asp:CalendarExtender ID="calender_sta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                    TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtQKRQ">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                票证号
                            </td>
                            <td>
                                <asp:TextBox ID="txtPZH" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div style="text-align: center">
                        <table id="gr" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1" style="width: 100%">
                            <tr>
                                <td colspan="4">
                                    主&nbsp;&nbsp;合&nbsp;&nbsp;同&nbsp;&nbsp;内&nbsp;&nbsp;容
                                </td>
                                <td colspan="6">
                                    请&nbsp;&nbsp;款&nbsp;&nbsp;内&nbsp;&nbsp;容
                                </td>
                            </tr>
                            <asp:Repeater ID="Det_Repeater" runat="server">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle headcolor">
                                        <td>
                                            <strong>合同号</strong>
                                        </td>
                                        <td>
                                            <strong>项目名称</strong>
                                        </td>
                                        <td>
                                            <strong>产品名称</strong>
                                        </td>
                                        <td>
                                            <strong>到款<br />
                                                比例</strong>
                                        </td>
                                        <td>
                                            <strong>合同号</strong>
                                        </td>
                                        <td>
                                            <strong>材料（产品）名称</strong>
                                        </td>
                                        <td>
                                            <strong>总价（元）</strong>
                                        </td>
                                        <td>
                                            <strong>已付<br />
                                                比例</strong>
                                        </td>
                                        <td>
                                            <strong>本次<br />
                                                比例</strong>
                                        </td>
                                        <td>
                                            <strong>申请支付金额</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                        <td class="center" width="150px">
                                            <%# Eval("CM_CONTR")%>
                                            <asp:Label runat="server" ID="ID" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td class="center" width="150px">
                                            <%# Eval("PCON_PJNAME")%>
                                        </td>
                                        <td class="center" width="100px">
                                            <%# Eval("PCON_ENGNAME")%>
                                        </td>
                                        <td class="center" width="50px">
                                            <%#string.Format("{0:N2}",(Convert.ToDouble(Eval("PCON_YFK"))/Convert.ToDouble(Convert.ToDouble(Eval("PCON_HTZJ"))==0?"1":Eval("PCON_HTZJ")))*100)+"%" %>
                                        </td>
                                        <td class="center" width="150px">
                                            <%# Eval("CONTR")%>
                                        </td>
                                        <td class="center" width="100px">
                                            <%# Eval("CM_MATERIAL")%>
                                        </td>
                                        <td class="center" width="100px">
                                            <%# Eval("CM_COUNT")%>
                                        </td>
                                        <td>
                                            <%# Eval("CM_YIFU")+"%"%>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CM_NOW" runat="server" Width="45px" Text='<%# Eval("CM_NOW")%>'
                                                CssClass="center"></asp:TextBox>%
                                        </td>
                                        <td>
                                            <input type="hidden" value='<%# Eval("CM_COUNT")%>' />
                                            <asp:TextBox ID="CM_APPLI" runat="server" Text='<%#Eval("CM_APPLI") %>' CssClass="center"
                                                onblur="javascript:check_num(this);setBiLi(this)"></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                        id="tb">
                        <tr>
                            <td>
                                金额合计（大写）：
                            </td>
                            <td colspan="2">
                                <asp:Label runat="server" ID="CR_BQSFKDX"></asp:Label>
                                <input type="hidden" id="bqsfkdx" runat="server" />
                            </td>
                            <td>
                                申请总额：
                            </td>
                            <td colspan="2">
                                <asp:Label runat="server" ID="CR_BQSFK" Width="200px"></asp:Label>
                                <input type="hidden" id="bqsfk" runat="server" />
                                <input id="cal" type="button" value="计算总额" onclick="calculate()" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                承包供应商
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtCBGYS" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                开户银行
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtKFYH" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                账号
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtZH" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                支付方式
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rblZFFS" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">现金</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">转支</asp:ListItem>
                                    <asp:ListItem Value="2">电汇</asp:ListItem>
                                    <asp:ListItem Value="3">票汇</asp:ListItem>
                                    <asp:ListItem Value="4">其他</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                主管领导
                            </td>
                            <td>
                                <asp:TextBox ID="txtZGLG" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                部门负责人
                            </td>
                            <td>
                                <asp:TextBox ID="txtBMFZR" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                验收人
                            </td>
                            <td>
                                <asp:TextBox ID="txtYSR" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                领导
                            </td>
                            <td>
                                <asp:TextBox ID="txtLD" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                财务审核
                            </td>
                            <td>
                                <asp:TextBox ID="txtCWSH" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                请款人
                            </td>
                            <td>
                                <asp:TextBox ID="txtJBR" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
