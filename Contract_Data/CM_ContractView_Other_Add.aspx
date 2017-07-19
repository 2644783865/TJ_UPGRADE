<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/BaseMaster.master"
    CodeBehind="CM_ContractView_Other_Add.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_ContractView_Other_Add" %>

<%@ Register Src="../Controls/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="lblState" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <%--<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>--%>

    <script language="javascript" type="text/javascript">
        function CheckBoxList_Click(sender) {
            var container = sender.parentNode;
            if (container.tagName.toUpperCase() == "TD")
            // 服务器控件设置呈现为 table 布局（默认设置），否则使用流布局即为td
            {
                container = container.parentNode.parentNode; // 层次： <table><tr><td><input />
            }
            var chkList = container.getElementsByTagName("input");
            var senderState = sender.checked;
            for (var i = 0; i < chkList.length; i++) {
                chkList[i].checked = false;
            }
            sender.checked = senderState;
        }

        //检验是否填写了合同金额
        function Check_HTJE() {
            var str = "";
            var je_value = parseFloat(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel1_TextJE").value);
            if (je_value == 0) {
                str = "合同金额为0，请确定后再提交\r\r确定要提交吗？";
            }
            else {
                str = "提交后开始审批\r\r确认提交吗？";
            }
            var ok = confirm(str);
            return ok;
        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            评审合同信息
                        </td>
                        <td align="center">
                            评审单号：<asp:Label ID="LBpsdh" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="LbtnSubmit" runat="server" OnClientClick="javascript:return Check_HTJE();"
                                OnClick="btnSubmit_Click">
                                <asp:Image ID="Image3" Style="cursor: hand" ToolTip="确认并提交" ImageUrl="~/Assets/icons/positive.gif"
                                    Height="18" Width="18" runat="server" />
                                提交
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="LbtnBack" runat="server" OnClick="btnBack_Click" CausesValidation="False">
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
            <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                ActiveTabIndex="0">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="合同基本信息" TabIndex="0">
                    <ContentTemplate>
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td width="150px" align="right">
                                    合同基本信息：
                                </td>
                                <td class="category">
                                    <table style="width: 100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                        border="1">
                                        <tr>
                                            <td width="120px" align="center">
                                                合同类型:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_htlx" runat="server">
                                                    <asp:ListItem Text="-请选择-" Value="%" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="销售合同" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="生产外协" Value="1"></asp:ListItem>
                                                   <%-- <asp:ListItem Text="厂内分包" Value="2"></asp:ListItem>--%>
                                                    <asp:ListItem Text="采购合同" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="办公合同" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="其他合同" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                补充协议编号：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tb_conid" runat="server" Width="180"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tb_conid"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                                <span style="color: red">格式：主合同号-数字，如：TECTJWX13000-1</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                项目名称：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextXMMC" runat="server" Width="90%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextXMMC"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                设备名称：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextSBMC" runat="server" Width="90%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextSBMC"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                合同范围：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextFBFW" runat="server" Width="90%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextFBFW"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                金额：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextJE" runat="server" onblur="javascript:check_num(this)" Width="90%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextJE"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                业主/供应商：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextFBS" runat="server" Width="90%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextFBS"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                备注：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_BZ" runat="server" TextMode="MultiLine" Width="90%" Height="40px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    附件：
                                </td>
                                <td class="category">
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    注意事项*
                                </td>
                                <td style="color: Red" class="category">
                                    补充协议评审通过后所要更改的内容需要到相应合同信息中手动进行修改
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="指定评审人" TabIndex="1">
                    <ContentTemplate>
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="right" width="150px">
                                    制单人意见：
                                </td>
                                <td class="category">
                                    <asp:TextBox ID="txt_zdrYJ" runat="server" TextMode="MultiLine" Width="90%" Height="40px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    指定评审人员：
                                </td>
                                <td class="category">
                                    <asp:Panel ID="Panel1" runat="server" EnableViewState="False">
                                        <asp:Label ID="errorlb" runat="server" EnableViewState="False" ForeColor="Red" Visible="False"></asp:Label>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    *注意事项
                                </td>
                                <td>
                                    1.请勾选需要审批的人员，若不勾选则表示不需要此人审批;<br />
                                    2.选择人员无先后顺序;<br />
                                    3.同一部门只能选择一个人，多选无效;<br />
                                    4.此处只选择部门负责人，审批领导根据合同金额自动添加，无需选择;<br />
                                    5.制单人在提交时需要填写意见，将<span style="color: Red">不再经过经办人审批</span>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->
</asp:Content>
