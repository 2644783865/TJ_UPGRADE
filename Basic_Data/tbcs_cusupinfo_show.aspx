<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tbcs_cusupinfo_show.aspx.cs"
    MasterPageFile="~/Masters/BaseMaster.master" Inherits="ZCZJ_DPF.Basic_Data.tbcs_cusupinfo_show" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblShow" Text="查看厂商信息" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <%--<tr>
                        <td>
                            公司编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_CODE_Show" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            公司名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_NAME_Show" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:TextBox ID="txtCS_CODE_Show" runat="server"  Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            所在地
                        </td>
                        <td>
                            <p>
                                <asp:TextBox ID="txtCS_LOCATION_Show" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            助记码
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_HRCODE_Show" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            厂商类型
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_TYPE_Show" runat="server" ReadOnly="True"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            通讯地址
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_ADDRESS_Show" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            公司联系电话
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_PHONO_Show" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            公司联系人姓名
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_CONNAME_Show" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            公司邮箱
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_MAIL_Show" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            邮编
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_ZIP_Show" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            传真
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_FAX_Show" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            产品所属类型
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_COREBS_Show" runat="server" Height="49px" TextMode="MultiLine"
                                Width="246px"></asp:TextBox>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td>
                            供货物料代码
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_MCODE_Show" runat="server" Width="246px"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            所供产品
                        </td>
                        <td>
                            <asp:TextBox ID="TB_Scope" runat="server" Height="49px" TextMode="MultiLine" Width="246px"></asp:TextBox>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td>
                            重要等级
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_RANK_Show" runat="server" ReadOnly="True"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            开户行：
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_BANK" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            开户行帐号：
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_ACCOUNT" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            税号
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_TAX" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            维护人
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_MANCLERK_Show" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            填写日期
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_FILLDATE_Show" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注
                        </td>
                        <td>
                            <asp:TextBox ID="txtCS_NOTE_Show" runat="server" TextMode="MultiLine" Height="49px"
                                Width="246px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
