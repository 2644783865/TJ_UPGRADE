<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/BaseMaster.master"
    CodeBehind="CM_ContractView_Audit.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_ContractView_Audit" %>

<%@ Register Src="../Controls/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    合同评审
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script language="javascript" type="text/javascript">
        //查看订单
        function View_PurOrder(orderid) {
            var autonum = Math.round(10000 * Math.random());
            window.open("../PC_Data/TBPC_Purordertotal_list.aspx?autonum=" + autonum + "&TotalOrder=" + orderid + "");
        }
        function View_Bid(bidnum) {
            window.open("../CM_Data/PD_DocpinshenInfo.aspx?action=look&id=" + bidnum);
        }
    </script>

    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                评审合同信息
                            </td>
                            <td align="center">
                                评审单号：<asp:Label ID="LBpsdh" runat="server"></asp:Label>
                                <%--唯一编号--%>
                                <asp:Label ID="lbl_UNID" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:LinkButton ID="LbtnYes" runat="server" OnClientClick="javascript:return confirm('确认提交吗？');"
                                    OnClick="btnYes_Click">
                                    <asp:Image ID="Image3" Style="cursor: hand" ToolTip="同意并提交" ImageUrl="~/Assets/icons/positive.gif"
                                        Height="18" Width="18" runat="server" />
                                    同意
                                </asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="LbtnNO" runat="server" OnClientClick="javascript:return confirm('确认驳回吗？');"
                                    OnClick="btnNO_Click">
                                    <asp:Image ID="Image1" Style="cursor: hand" ToolTip="驳回并提交" ImageUrl="~/Assets/icons/delete.gif"
                                        Height="18" Width="18" runat="server" />
                                    驳回
                                </asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="LbtnBack" runat="server" CausesValidation="False" OnClick="btn_back_Click">
                                    <asp:Image ID="Image7" Style="cursor: hand" ToolTip="返回" ImageUrl="~/Assets/icons/back.png"
                                        Height="17" Width="17" runat="server" />
                                    返回
                                </asp:LinkButton>&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="合同信息" TabIndex="0">
                        <HeaderTemplate>
                            合同信息</HeaderTemplate>
                        <ContentTemplate>
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td width="110px" height="25" align="right">
                                        合同基本信息：
                                    </td>
                                    <td class="category">
                                        <table style="width: 100%" cellpadding="4" class="toptable grid" cellspacing="1"
                                            border="1">
                                            <tr runat="server" id="tr_htbh">
                                                <td runat="server">
                                                    合同号：
                                                </td>
                                                <td runat="server">
                                                    <asp:Label ID="lb_HTBH" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%">
                                                    项目名称：
                                                </td>
                                                <td style="width: 85%">
                                                    <asp:Label ID="lb_XMMC" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--<tr runat="server" id="tr_gcmc">
                                                        <td style="width: 15%" runat="server">
                                                            工程名称：
                                                        </td>
                                                        <td style="width: 85%" runat="server">
                                                            <asp:Label ID="lb_GCMC" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%">
                                                            设备名称：
                                                        </td>
                                                        <td style="width: 85%">
                                                            <asp:Label ID="lb_SBMC" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                            <%--<tr>
                                                        <td>
                                                            合同范围：
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lb_FBFW" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            金额：
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lb_JE" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                            <%--<tr>
                                                        <td>
                                                            其他币种：
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lb_OtherMonunit" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                            <tr>
                                                <td>
                                                    业主名称：
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_FBS" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    制单人：
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_ZDR" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--<tr runat="server" id="tr_orderid">
                                                <td runat="server">
                                                    采购订单号：
                                                </td>
                                                <td runat="server">
                                                    <asp:Label ID="lb_Orderid" runat="server" Text="Label"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="Ltn_ViewOrder" runat="server" OnClick="btn_ViewOrder_Click">
                                                        <asp:Image ID="imgToPurorder" Style="cursor: hand" ToolTip="到订单" ImageUrl="~/Assets/icons/dindan.jpeg"
                                                            Height="17px" Width="17px" runat="server" />
                                                        到采购订单
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>--%>
                                            <%--<tr runat="server" id="tr_tbcode">
                                                <td runat="server">
                                                    投标评审编号：
                                                </td>
                                                <td runat="server">
                                                    <asp:Label ID="lb_tbcode" runat="server" Text="Label"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="Ltn_ViewBid" runat="server" OnClick="btn_ViewBid_Click">
                                                        <asp:Image ID="imgToTBPS" Style="cursor: hand" ToolTip="到投标评审" ImageUrl="~/Assets/icons/full_star.gif"
                                                            Height="17px" Width="17px" runat="server" />
                                                        到投标评审信息
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lb_bcid" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td>
                                                    备注：
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_BZ" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="right">
                                        附件：
                                    </td>
                                    <td class="category">
                                        <uc1:UploadAttachments ID="UploadAttachments1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="市场部合同评审" TabIndex="2" Visible="false">
                        <ContentTemplate>
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
                                                    <asp:TextBox runat="server" ID="CM_GHFW" TextMode="MultiLine" Height="50px" Width="90%"></asp:TextBox>
                                                </td>
                                                <td colspan="4" rowspan="10" style="vertical-align: text-top; text-align: center">
                                                    <table style="margin: auto; width: 400px">
                                                        <tr>
                                                            <td>
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
                                                            <td>
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
                                                    <asp:TextBox runat="server" ID="CM_REQUEST" TextMode="MultiLine" Height="50px" Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: middle">
                                                    质量与检验
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox runat="server" ID="CM_CHECK" TextMode="MultiLine" Height="50px" Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    质保
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox runat="server" ID="CM_ZHIBAO" Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    油漆
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox runat="server" ID="CM_YOUQI" Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    包装
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox runat="server" ID="CM_PACK" Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    运输
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox runat="server" ID="CM_YUNSHU" Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    付款
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox runat="server" ID="CM_FUKUAN" Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    其他
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox runat="server" ID="CM_QITA" Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    附件
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox runat="server" ID="CM_FUJIAN" Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="部门评审信息" TabIndex="1">
                        <ContentTemplate>
                            <div style="border: 1px solid #000000;">
                                <asp:Panel ID="Pan_shenheren" runat="server">
                                    <asp:Panel ID="Panel_zdr" runat="server" Enabled="false">
                                        <table width="100%" class="toptable grid">
                                            <tr>
                                                <td style="width: 110px;">
                                                    &nbsp;制单人意见:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_zdryj" Columns="100" Rows="4" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" ID="TabPanel3" HeaderText="领导批准信息" TabIndex="2">
                        <ContentTemplate>
                            <asp:Panel ID="Pan_gsld" runat="server">
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </div>
        </div>
    </div>
</asp:Content>
