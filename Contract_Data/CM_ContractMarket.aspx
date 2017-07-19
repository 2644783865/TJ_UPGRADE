<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_ContractMarket.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_ContractMarket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <strong>合同/订单&nbsp;&nbsp;评审表</strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper1">
        <div class="box-outer">
            <asp:Panel ID="PanelAll" runat="server">
                <div style="text-align: center; padding-top: 10px; margin-bottom: 20px">
                    <h2 style="height: 20px">
                        合同/订单&nbsp;&nbsp;评审表</h2>
                </div>
                <table width="90%" style="margin: auto;">
                    <tr>
                        <td style="text-align: left; font-size: small">
                            编号：<asp:TextBox runat="server" ID="CM_BIANHAO"></asp:TextBox>
                        </td>
                        <td style="text-align: right; font-size: small">
                            文件号：TJZJ-R-M-01&nbsp;&nbsp;&nbsp;&nbsp;版 本：1
                        </td>
                    </tr>
                </table>
                <table width="90%" cellpadding="4" cellspacing="1" class="grid" border="1" style="margin: auto;
                    text-align: center;">
                    <tr>
                        <td rowspan="13" width="50px" style="vertical-align: middle">
                            合<br />
                            <br />
                            同<br />
                            <br />
                            信<br />
                            <br />
                            息<br />
                            <br />
                            及<br />
                            <br />
                            成<br />
                            <br />
                            本<br />
                            <br />
                            核<br />
                            <br />
                            算<br />
                            <br />
                            部<br />
                            <br />
                            分
                        </td>
                        <td>
                            顾客名称：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_CUSNAME"></asp:TextBox>
                        </td>
                        <td>
                            设备名称：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_EQUIP"></asp:TextBox>
                        </td>
                        <td>
                            设备图号：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_MAP"></asp:TextBox>
                        </td>
                        <td>
                            合同单价：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_PAY"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            项目名称：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_PROJ"></asp:TextBox>
                        </td>
                        <td>
                            设备重量：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_SBZL"></asp:TextBox>
                        </td>
                        <td>
                            评审时间：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_PSTIME"></asp:TextBox>
                        </td>
                        <td>
                            图纸提供：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_TZ"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            订货数量,地点：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_NUMPLACE"></asp:TextBox>
                        </td>
                        <td>
                            交货期：
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CM_JHTIME"></asp:TextBox>
                        </td>
                        <td colspan="4">
                            成本核算
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle">
                            供货范围
                        </td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="CM_GHFW" TextMode="MultiLine" Height="50px" Width="100%"></asp:TextBox>
                        </td>
                        <td colspan="4" rowspan="10" style="vertical-align: text-top; text-align: center">
                            <table style="margin: auto; width: 400px">
                                <asp:Panel runat="server" ID="panel">
                                    <tr>
                                        <td align="right">
                                            行数：
                                        </td>
                                        <td width="60px">
                                            <asp:TextBox runat="server" ID="num" onblur="CheckNum(this)" Width="50px" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Button runat="server" ID="btn_Add" Text="确定" OnClick="btn_add_Click" />
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView runat="server" ID="GridHeSuan" AutoGenerateColumns="False" CellPadding="4"
                                            CssClass="toptable grid" ForeColor="#333333" Style="margin: auto; width: 400px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="项目">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt0" runat="server" Text='<%#Eval("CM_XM") %>' Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="重量/t">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt1" runat="server" Text='<%#Eval("CM_ZL") %>' Width="50px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="单价/万元/t">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt2" runat="server" Text='<%#Eval("CM_DJ") %>' Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="总价/万元">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt3" runat="server" Text='<%#Eval("CM_ZJ") %>' Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Delete" runat="server" OnClick="Delete_Click" CommandArgument='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'>
                                    <img src="../Assets/images/no.gif" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="#EFF3FB" />
                                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        说明：
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox runat="server" ID="CM_SHUOM" TextMode="MultiLine" Height="50px" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle">
                            工艺/技术要求
                        </td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="CM_REQUEST" TextMode="MultiLine" Height="50px" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle">
                            质量与检验
                        </td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="CM_CHECK" TextMode="MultiLine" Height="50px" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            质保
                        </td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="CM_ZHIBAO" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            油漆
                        </td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="CM_YOUQI" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            包装
                        </td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="CM_PACK" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            运输
                        </td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="CM_YUNSHU" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            付款
                        </td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="CM_FUKUAN" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            其他
                        </td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="CM_QITA" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            附件
                        </td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="CM_FUJIAN" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
