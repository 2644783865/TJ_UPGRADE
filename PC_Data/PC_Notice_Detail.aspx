<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_Notice_Detail.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_Notice_Detail"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    看板详细信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                任务信息(带<span class="red">*</span>号的为必填项)
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <tr>
                        <td class="tdleft">
                            编 号:
                        </td>
                        <td class="tdright">
                            <asp:TextBox ID="editid" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft">
                            项目编号:
                        </td>
                        <td class="tdright">
                            <asp:TextBox ID="proid" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft">
                            项目名称:
                        </td>
                        <td class="tdright">
                            <asp:TextBox ID="proname" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft">
                            来源部门编号:
                        </td>
                        <td class="tdright">
                            <asp:TextBox ID="sdpid" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft">
                            来源部门名称:
                        </td>
                        <td class="tdright">
                            <asp:TextBox ID="sdpname" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft">
                            类 别:
                        </td>
                        <td class="tdright">
                            <asp:TextBox ID="type" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft">
                            提交时间:
                        </td>
                        <td class="tdright">
                            <asp:TextBox ID="sbtime" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft">
                            处理人ID:
                        </td>
                        <td class="tdright">
                            <asp:TextBox ID="hdid" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft">
                            处理人姓名:
                        </td>
                        <td class="tdright">
                            <asp:TextBox ID="hdperson" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft">
                            处理时间:
                        </td>
                        <td class="tdright">
                            <asp:TextBox ID="hdtime" runat="server" onclick="setday(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft">
                            状 态:
                        </td>
                        <td class="tdright">
                            <asp:RadioButtonList ID="rblstatus" RepeatColumns="2" runat="server">
                                <asp:ListItem Text="未读" Value="0"></asp:ListItem>
                                <asp:ListItem Text="已阅" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft">
                            备 注:
                        </td>
                        <td class="tdright">
                            <asp:TextBox ID="remark" runat="server" TextMode="MultiLine" Rows="3" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdleft">
                        </td>
                        <td class="tdright">
                            <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
