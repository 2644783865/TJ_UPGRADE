<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EQU_operate.aspx.cs" Inherits="ZCZJ_DPF.ESM.equipment_operate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <base id="goDownload" target="_self" />
    <title>PMS项目管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="pragma" content="no-cache"/>
    <meta http-equiv="cache-control" content="no-cache"/>
    <meta http-equiv="expires" content="0"/>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                添加设备(带<span class="Error">*</span>号的为必填项)
                            </td>
                        </tr>
                    </table>
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
                        <td align="right" style="width: 150px">
                            卡片编号：
                        </td>
                        <td>
                            <asp:TextBox ID="CNum" runat="server" Width="200px"></asp:TextBox><span class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必须输入编号"
                                ControlToValidate="CNum"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                      <tr>
                        <td align="right" style="width: 150px">
                            开始使用日期：
                        </td>
                        <td>
                            <asp:TextBox ID="SDate" runat="server" Width="200px"></asp:TextBox><label class="Error">日期格式：1900.01.01</label>
<%--                                <asp:CompareValidator
                                    ID="CompareValidator1" runat="server" ErrorMessage="请输入正确日期格式" ControlToValidate ="SDate"></asp:CompareValidator>--%>
                                
                        </td>
                    </tr>
                      <tr>
                        <td align="right" style="width: 150px">
                            使用年限（月）：
                        </td>
                        <td>
                            <asp:TextBox ID="Dmonth" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           原值：
                        </td>
                        <td>
                            <asp:TextBox ID="Oval" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           固定资产编号：
                        </td>
                        <td width="200">
                            <asp:TextBox ID="ANum" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           净残值率：
                        </td>
                        <td>
                            <asp:TextBox ID="ReRate" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           累计折旧：
                        </td>
                        <td width="200px">
                            <asp:TextBox ID="AcDep" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           使用部门：
                        </td>
                        <td>
                            <asp:TextBox ID="UsDe" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           类别名称：
                        </td>
                        <td>
                            <asp:TextBox ID="CName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           固定资产名称：
                        </td>
                        <td width="200px">
                            <asp:TextBox ID="AName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           净值：
                        </td>
                        <td>
                            <asp:TextBox ID="NetVal" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align="right" style="width: 150px">
                           规格型号：
                        </td>
                        <td>
                            <asp:TextBox ID="Spec" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           折旧方法：
                        </td>
                        <td>
                            <asp:TextBox ID="Depre" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           工作量单位：
                        </td>
                        <td>
                            <asp:TextBox ID="Unit" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           存放地点：
                        </td>
                        <td>
                            <asp:TextBox ID="Stor" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           对应折旧科目：
                        </td>
                        <td>
                            <asp:TextBox ID="CorAccount" runat="server" Width="200px"></asp:TextBox>
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