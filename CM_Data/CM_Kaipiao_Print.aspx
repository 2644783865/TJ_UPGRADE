<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CM_Kaipiao_Print.aspx.cs"
    Inherits="ZCZJ_DPF.CM_Data.CM_Kaipiao_Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="div1">
        <div style="width: 90%; margin: 0px auto;">
            <h2 style="text-align: center; margin-top: 20px; font-size: 20">
                增值税发票申请单
            </h2>
            <asp:Panel ID="Panel" runat="server" Width="100%">
                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1" style="font-size:xx-small">
                    <tr>
                        <td style="width: 150px">
                            编号：
                        </td>
                        <td>
                            <asp:Label ID="KP_CODE" runat="server" Width="220px"></asp:Label>
                            <input type="hidden" runat="server" id="hidTaskId" />
                            <input type="hidden" runat="server" id="hidId" />
                            <input type="hidden" runat="server" id="hidAction" />
                            <input type="hidden" runat="server" id="hidSPState" />
                            <input type="hidden" runat="server" id="hidHSState" />
                        </td>
                        <td style="width: 120px">
                            制单人：
                        </td>
                        <td>
                            <asp:Label runat="server" ID="KP_ZDRNM"></asp:Label>
                            <input type="hidden" runat="server" id="KP_ZDRID" />
                        </td>
                    </tr>
                    <tr>
                       <td style="width: 150px">
                            单位名称：
                        </td>
                        <td>
                            <asp:Label ID="KP_COM" runat="server" Width="220px" AutoPostBack="true"></asp:Label>
                        </td>
                        <td>
                            单位地址：
                        </td>
                        <td>
                            <asp:Label ID="KP_ADDRESS" runat="server" Width="220px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px">
                            设备名称：
                        </td>
                        <td>
                            <asp:Label ID="KP_SHEBEI" runat="server" Width="220px"></asp:Label>
                        </td>
                        <td>
                            帐号：
                        </td>
                        <td>
                            <asp:Label ID="KP_ACCOUNT" runat="server" Width="220px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px">
                            税号：
                        </td>
                        <td>
                            <asp:Label ID="KP_SHUIHAO" runat="server" Width="220px"></asp:Label>
                        </td>
                        <td>
                            开户行：
                        </td>
                        <td>
                            <asp:Label ID="KP_BANK" runat="server" Width="220px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px">
                            联系电话（纳税电话）：
                        </td>
                        <td>
                            <asp:Label ID="KP_TEL" runat="server" Width="220px"></asp:Label>
                        </td>
                        <td>
                            合同总价：
                        </td>
                        <td>
                            <asp:Label ID="KP_CONZONGJIA" runat="server" Width="220px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px">
                            到款金额：
                        </td>
                        <td>
                            <asp:Label ID="KP_DAOKUANJE" runat="server" Width="220px"></asp:Label>
                        </td>
                        <td>
                            合同规定开票条件：
                        </td>
                        <td>
                            <asp:Label ID="KP_CONDITION" runat="server" Width="220px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            已开票金额：
                        </td>
                        <td>
                            <asp:Label ID="KP_YIKAIJE" runat="server" Width="220px"></asp:Label>
                        </td>
                        <td>
                            本次开票金额：
                        </td>
                        <td>
                            <asp:Label ID="KP_BENCIJE" runat="server" Width="220px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            发票交付方式：
                        </td>
                        <td>
                            <asp:Label ID="KP_JIAOFUFS" runat="server" Width="220px"></asp:Label>
                        </td>
                        <td>
                            是否提前开票：
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="KP_TIQIANKP" RepeatColumns="2">
                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                <asp:ListItem Text="否" Selected="True" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 220px">
                            发票号：
                        </td>
                        <td>
                            <asp:Label runat="server" ID="KP_KPNUMBER" Width="220px"></asp:Label>
                        </td>
                        <td>
                            开票日期：
                        </td>
                        <td>
                            <asp:Label runat="server" ID="KP_KPDATE"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            制单时间：
                        </td>
                        <td>
                            <asp:Label ID="KP_ZDTIME" runat="server"></asp:Label>
                        </td>
                        <td style="width: 220px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="KP_NOTE" runat="server" TextMode="MultiLine" Width="600px" Height="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            一级审批：
                        </td>
                        <td>
                            审批人：<asp:TextBox ID="tbspr1" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            审批结论：<asp:TextBox ID="tbspjl1" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            审批时间： <asp:TextBox ID="tbsptime1" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            二级审批：
                        </td>
                        <td>
                            审批人：<asp:TextBox ID="tbspr2" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            审批结论：<asp:TextBox ID="tbspjl2" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            审批时间： <asp:TextBox ID="tbsptime2" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <table width="100%">
            <tr style="font-size:xx-small">
                <td colspan="4" align="center">
                    <asp:GridView ID="GridView1" Width="90%" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" >
                        <Columns>
                            <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    <asp:HiddenField ID="hide" runat="server" Value='<%#Eval("Id") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TaskId" HeaderText="任务号" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ConId" HeaderText="合同号" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Proj" HeaderText="项目名称" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Engname" HeaderText="设备名称" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Map" HeaderText="图号" HeaderStyle-Wrap="true" ControlStyle-BorderWidth="30px">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Number" HeaderText="数量" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Unit" HeaderText="单位" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="开票金额(万元)" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="55px">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("kpmoney") %>' Width="100px" ID="kpMoney"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Money" HeaderText="合同金额(万元)" HeaderStyle-Wrap="true">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="顶发任务号" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("dfTaskId") %>' Width="120px" ID="dfTaskId"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="顶发项目名称" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("dfProName") %>' Width="120px" ID="dfProName"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                             <asp:BoundField DataField="dfConId" HeaderText="对方合同号" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" />
                    </asp:GridView>
                    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                        没有记录!</asp:Panel>
                    <br />
                    <div style="float: left">
                        合计金额：<asp:Label ID="lb_select_money" runat="server" Text=""></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
        <a href="javascript:printme()" target="_self">打印</a>

      

    </div>
    </form>
      <script type="text/javascript" language="javascript">
          function printme() {
              document.body.innerHTML = document.getElementById('div1').innerHTML ;
              window.print();
          }
        </script>
</body>
</html>
