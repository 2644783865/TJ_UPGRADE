<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EQU_tzsbop.aspx.cs" Inherits="ZCZJ_DPF.ESM.EQU_tzsbop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <base id="goDownload" target="_self" />
    <title>特种设备管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="pragma" content="no-cache"/>
    <meta http-equiv="cache-control" content="no-cache"/>
    <meta http-equiv="expires" content="0"/>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                添加特种设备(带<span class="Error">*</span>号的为必填项)
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-wrapper">
            <div class="box-outer">
                <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <tr>
                        <td align="right" style="width: 100px">
                            内部编号：
                        </td>
                        <td>
                            <asp:TextBox ID="Code" runat="server"></asp:TextBox><span class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必须输入内部编号"
                                ControlToValidate="Code"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            设备名称：
                        </td>
                        <td>
                            <asp:TextBox ID="Name" runat="server"></asp:TextBox><span class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必须输入设备名称"
                                ControlToValidate="Name"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            设备类别：
                        </td>
                        <td>
                            <asp:TextBox ID="Type" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            型号规格：
                        </td>
                        <td>
                            <asp:TextBox ID="Specification" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            出厂编号：
                        </td>
                        <td>
                            <asp:TextBox ID="Ocode" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            注册编号：
                        </td>
                        <td>
                            <asp:TextBox ID="Rcode" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            使用证号：
                        </td>
                        <td>
                            <asp:TextBox ID="Ucode" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            制造商：
                        </td>
                        <td>
                            <asp:TextBox ID="Manufa" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            安装位置：
                        </td>
                        <td>
                            <asp:TextBox ID="Position" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            使用状态：
                        </td>
                        <td>
                            <asp:TextBox ID="Ustate" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            再检日期：
                        </td>
                        <td>
                            <asp:TextBox  ID="Redate" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" ValueToCompare="1900-01-01" Operator="GreaterThan" runat="server"  ControlToValidate="Redate"  Type="Date"  Text="输入日期格式不正确例：1900-01-01"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            备注：
                        </td>
                        <td>
                            <asp:TextBox  ID="Remark" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                        <asp:Button ID="btnupdate" runat="server" Text="修改" OnClick="btnupdate_Click" />
                        <input type="button" id="btn_Back" value="返回" onclick="window.close()" />
                        </td>
                     </tr>
                     </table>
                     </div>
                     </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
