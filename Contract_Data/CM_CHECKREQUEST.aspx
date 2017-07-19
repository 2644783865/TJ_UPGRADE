<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/masters/PopupBase.Master"
    CodeBehind="CM_CHECKREQUEST.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_CHECKREQUEST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblCR" runat="server" Text="�½���"></asp:Label>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function btnPrint_onclick() {
            window.showModalDialog('CM_CHECKREQUEST_VIEW.aspx?CRid=<%=lblCR_ID.Text %>', '', "dialogWidth=800px;dialogHeight=700px");
        }

        function Actual_Payment_DX(str) {
            //ʵ����
            var t = Arabia_to_Chinese_UserString(str);
            //��д
            document.getElementById('<%=CR_BQSFKDX.ClientID%>').innerHTML = t;
            document.getElementById('<%=bqsfkdx.ClientID%>').value = t;
        }

        // ]]>
        //�������ڸ�ʽ�磺2012-01-01
        function dateCheck(obj) {
            var value = obj.value;
            if (value != "") {
                var re = new RegExp("^([0-9]{4})(-)([0-9]{2})(-)([0-9]{2})$");
                m = re.exec(value)
                if (m == null) {
                    obj.style.background = "yellow";
                    obj.value = "";
                    alert('��������ȷ��ʱ���ʽ�磺2012-01-01');
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
                                <asp:Label ID="Label1" runat="server" Text="����״̬��"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblState" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblState_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="0">����</asp:ListItem>
                                    <asp:ListItem Value="1">����ǩ��</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnSave" runat="server" CssClass="button-inner" Text="�� ��" OnClientClick="javascript:return confirm('ȷ���ύ��');"
                                    OnClick="btnSave_Click" /><asp:Label ID="lblRemind" runat="server" ForeColor="Red"
                                        Text="�����ɹ�!" Visible="false"></asp:Label>
                            </td>
                            <td align="right" style="width: 10%;">
                                <input id="btnPrint" class="button-inner" runat="server" type="button" value="��ӡ��"
                                    onclick="return btnPrint_onclick()" />&nbsp;&nbsp;&nbsp;
                                <input id="Button1" type="button" class="button-inner" value="�� ��" onclick="javascript:window.close();" />
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
                                    ��</h2>
                            </td>
                        </tr>
                        <tr style="border-width: 0;">
                            <td colspan="6">
                                <span style="color: Red">��ʾ��1.�����м��ʺŸ��ݺ�ͬ��ѡ��ĳ��̴ӻ������ݳ�����Ϣ���ȡ����Ϊ�գ��ɵ���Ӧ���̴���ӣ�Ҳ��������ֱ����д<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.��Ӻ�ͬʱֱ�����볧�����֣������Ǵ�ѡ�����ѡ��ģ����޷����������������г�����Ϣ������ѡ��һ�γ��̼��ɹ���
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" width="50%">
                                ����&nbsp;&nbsp;
                                <asp:Label ID="lblCR_ID" runat="server"></asp:Label>
                            </td>
                            <td colspan="3" align="center">
                                ���ҵ�λ&nbsp;&nbsp;<asp:Label ID="lblHBDW" runat="server" Text="Ԫ��RMB"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ����
                            </td>
                            <td>
                                <asp:DropDownList ID="dplQKBM" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                �������
                            </td>
                            <td>
                                <asp:TextBox ID="txtQKRQ" runat="server" onchange="dateCheck(this)"></asp:TextBox>
                                <asp:CalendarExtender ID="calender_sta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM��,yyyy��"
                                    TodaysDateFormat="yyyy��MM��dd��" TargetControlID="txtQKRQ">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                Ʊ֤��
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
                                    ��&nbsp;&nbsp;��&nbsp;&nbsp;ͬ&nbsp;&nbsp;��&nbsp;&nbsp;��
                                </td>
                                <td colspan="6">
                                    ��&nbsp;&nbsp;��&nbsp;&nbsp;��&nbsp;&nbsp;��
                                </td>
                            </tr>
                            <asp:Repeater ID="Det_Repeater" runat="server">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle headcolor">
                                        <td>
                                            <strong>��ͬ��</strong>
                                        </td>
                                        <td>
                                            <strong>��Ŀ����</strong>
                                        </td>
                                        <td>
                                            <strong>��Ʒ����</strong>
                                        </td>
                                        <td>
                                            <strong>����<br />
                                                ����</strong>
                                        </td>
                                        <td>
                                            <strong>��ͬ��</strong>
                                        </td>
                                        <td>
                                            <strong>���ϣ���Ʒ������</strong>
                                        </td>
                                        <td>
                                            <strong>�ܼۣ�Ԫ��</strong>
                                        </td>
                                        <td>
                                            <strong>�Ѹ�<br />
                                                ����</strong>
                                        </td>
                                        <td>
                                            <strong>����<br />
                                                ����</strong>
                                        </td>
                                        <td>
                                            <strong>����֧�����</strong>
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
                                ���ϼƣ���д����
                            </td>
                            <td colspan="2">
                                <asp:Label runat="server" ID="CR_BQSFKDX"></asp:Label>
                                <input type="hidden" id="bqsfkdx" runat="server" />
                            </td>
                            <td>
                                �����ܶ
                            </td>
                            <td colspan="2">
                                <asp:Label runat="server" ID="CR_BQSFK" Width="200px"></asp:Label>
                                <input type="hidden" id="bqsfk" runat="server" />
                                <input id="cal" type="button" value="�����ܶ�" onclick="calculate()" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                �а���Ӧ��
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtCBGYS" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                ��������
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtKFYH" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                �˺�
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtZH" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                ֧����ʽ
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rblZFFS" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">�ֽ�</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">ת֧</asp:ListItem>
                                    <asp:ListItem Value="2">���</asp:ListItem>
                                    <asp:ListItem Value="3">Ʊ��</asp:ListItem>
                                    <asp:ListItem Value="4">����</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                �����쵼
                            </td>
                            <td>
                                <asp:TextBox ID="txtZGLG" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                ���Ÿ�����
                            </td>
                            <td>
                                <asp:TextBox ID="txtBMFZR" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                ������
                            </td>
                            <td>
                                <asp:TextBox ID="txtYSR" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                �쵼
                            </td>
                            <td>
                                <asp:TextBox ID="txtLD" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                �������
                            </td>
                            <td>
                                <asp:TextBox ID="txtCWSH" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                �����
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
