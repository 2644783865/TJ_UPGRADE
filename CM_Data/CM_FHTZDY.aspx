<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CM_FHTZDY.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_FHTZDY" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        .tab
        {
            width: 80%;
            border: solid 1px #E5E5E5;
        }
        .tab tr
        {
        }
        .tab tr td
        {
            border: solid 1px #E5E5E5;
            border-collapse: collapse;
            text-align: center;
            font-size: small;
            height: 30px;
        }
        .tab tr td input
        {
            width: 90%;
            height: 25px;
            font-size: small;
            text-align: center;
        }
    </style>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
        <div>
            <asp:Button runat="server" ID="btnDY" OnClientClick="btnDY_OnClientClick()" Text="打印" />
        </div>
        <div id="div1" align="center" style="width: 100%;">
            <table class="tab">
                <tr>
                    <td align="center" style="font-size: x-large;">
                        <strong>发&nbsp;&nbsp;货&nbsp;&nbsp;通&nbsp;&nbsp;知</strong>
                    </td>
                </tr>
            </table>
            <table class="tab">
                <tr>
                    <td align="right" width="10%">
                        编号：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="CM_BIANHAO" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="hid_BianHao" runat="server" />
                    </td>
                    <td align="right">
                        顾客名称：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="CM_CUSNAME" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table id="gr" cellpadding="4" cellspacing="1" border="1" width="80%">
                <asp:Repeater ID="Det_Repeater" runat="server">
                    <HeaderTemplate>
                        <tr style="text-align: center">
                            <td style="font-size: small;">
                                <strong>序号</strong>
                            </td>
                            <td style="font-size: small;">
                                <strong>项目名称</strong>
                            </td>
                            <td style="font-size: small;">
                                <strong>合同号</strong>
                            </td>
                            <td style="font-size: small;">
                                <strong>任务号</strong>
                            </td>
                            <%--                            <td>
                                <strong>总序</strong>
                            </td>--%>
                            <td style="font-size: small;">
                                <strong>交货内容</strong>
                            </td>
                            <%-- <td>
                                <strong>图号</strong>
                            </td>--%>
                            <td style="font-size: small;">
                                <strong>数量</strong>
                            </td>
                            <td style="font-size: small;">
                                <strong>已发数目</strong>
                            </td>
                            <td style="font-size: small;">
                                <strong>发货数目</strong>
                            </td>
                            <%-- <td>
                                <strong>单位</strong>
                            </td>--%>
                            <%-- <td>
                                <strong>备注</strong>
                            </td>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <div>
                                    <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                    <asp:CheckBox ID="chk" Visible="false" BorderStyle="None" runat="server"></asp:CheckBox>
                                    <asp:Label runat="server" ID="lbfh" Font-Size="X-Small" Visible="false" ForeColor="Red"></asp:Label>
                                    <asp:HiddenField ID="tid" runat="server" Value='<%#Eval("ID") %>' />
                                    <asp:HiddenField ID="sid" runat="server" Value='<%#Eval("CM_ID") %>' />
                                </div>
                            </td>
                            <td align="center">
                                <asp:Label ID="CM_PROJ" runat="server" Text='<%# Eval("CM_PROJ")%>' Font-Size="Small"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="CM_CONTR" runat="server" Text='<%# Eval("CM_CONTR")%>' Font-Size="Small"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="TSA_ID" runat="server" Text='<%# Eval("TSA_ID")%>' Font-Size="Small"></asp:Label>
                            </td>
                            <%--<td>
                                <asp:Label ID="CM_ID" runat="server" Width="50px" Text='<%# Eval("ID")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="TSA_ENGNAME" runat="server" Text='<%# Eval("TSA_ENGNAME")%>' Font-Size="Small"></asp:Label>
                            </td>
                            <%--<td>
                                <asp:Label ID="TSA_MAP" runat="server" Width="100px" Text='<%# Eval("TSA_MAP")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="TSA_NUMBER" runat="server" Text='<%# Eval("TSA_NUMBER")%>' Font-Size="Small"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="TSA_YFSM" runat="server" Text='<%# Eval("TSA_YFSM")%>' Font-Size="Small"></asp:Label>
                            </td>
                            <td align="center">
                                <%--<asp:TextBox ID="CM_FHNUM" runat="server" Width="50px" Text='<%# Eval("CM_FHNUM") %>'
                                                                    CssClass="center" onblur="CheckNum(this)"></asp:TextBox>--%>
                                <asp:Label ID="CM_FHNUM" runat="server" Text='<%# Eval("CM_FHNUM") %>' Font-Size="Small"></asp:Label>
                            </td>
                            <%-- <td>
                                <asp:Label ID="TSA_UNIT" runat="server" Width="50px" Text='<%# Eval("TSA_UNIT")%>'></asp:Label>
                            </td>--%>
                            <%--                            <td>
                                <asp:Label ID="TSA_NOTE" runat="server" Width="150px" Text='<%# Eval("TSA_IDNOTE")%>'></asp:Label>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <table class="tab">
                <tr>
                    <td width="30%">
                        收货单位：
                    </td>
                    <td>
                        <asp:TextBox ID="CM_SH" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="30%">
                        交（提）货地点：
                    </td>
                    <td>
                        <asp:TextBox ID="CM_JH" TextMode="MultiLine" Rows="4" Width="90%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="30%">
                        联系人：
                    </td>
                    <td>
                        <asp:TextBox ID="CM_LXR" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="30%">
                        联系方式：
                    </td>
                    <td>
                        <asp:TextBox ID="CM_LXFS" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="30%">
                        要求发货时间：
                    </td>
                    <td>
                        <asp:TextBox ID="CM_JHTIME" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="30%">
                        备注：
                    </td>
                    <td>
                        <asp:TextBox ID="CM_BEIZHU" runat="server" TextMode="MultiLine" Width="90%" Rows="4"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table class="tab">
                <tr>
                    <td width="16%">
                        经办人：
                    </td>
                    <td width="16%">
                        <asp:TextBox runat="server" ID="CM_MANCLERK"></asp:TextBox>
                    </td>
                    <td width="16%">
                        制单日期：
                    </td>
                    <td width="16%">
                        <asp:TextBox runat="server" ID="CM_ZDTIME"></asp:TextBox>
                    </td>
                    <td width="16%">
                        部门主管：
                    </td>
                    <td width="20%">
                        <asp:TextBox runat="server" ID="ZG"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>

    <script type="text/javascript">

        function btnDY_OnClientClick() {
            document.body.innerHTML = document.getElementById('div1').innerHTML;
            window.print();
        }
    </script>

    </form>
</body>
</html>
