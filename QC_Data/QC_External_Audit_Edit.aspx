<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="QC_External_Audit_Edit.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_External_Audit_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <%@ register src="../Controls/UploadFiles.ascx" tagname="UploadFiles" tagprefix="uf" %>

    <script type="text/javascript" language="javascript">
        //打印
        function Print() {

            window.print();

        }
       
    </script>

    <table width="100%">
        <tr>
            <td>
                外审管理
                <%--唯一编号--%>
                <asp:Label ID="lbl_UNID" runat="server" Text="" Visible="false"></asp:Label>
            </td>
            <td align="right">
                <asp:LinkButton ID="LbtnSubmit" runat="server" OnClientClick="javascript:return confirm('确认提交吗？');"
                    OnClick="btnSubmit_Click">
                    <asp:Image ID="Image3" Style="cursor: hand" ToolTip="提交" ImageUrl="~/Assets/icons/positive.gif"
                        Height="18" Width="18" runat="server" />
                    提 交&nbsp;&nbsp;
                </asp:LinkButton>
                <asp:LinkButton ID="LbtnBack" runat="server" CausesValidation="False" OnClick="LbtnBack_Click">
                    <asp:Image ID="Image7" Style="cursor: hand" ToolTip="关闭" ImageUrl="~/Assets/icons/back.png"
                        Height="17" Width="17" runat="server" />
                    关 闭
                </asp:LinkButton>&nbsp;
                <asp:HyperLink ID="HyperLink1" runat="server" onclick="javascript:Print();" CssClass="hand">
                    <asp:Image ID="Img_print" runat="server" ImageUrl="~/Assets/icon-fuction/89.gif"
                        title="打印" />
                    打 印</asp:HyperLink>&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../Contract_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function CheckBoxList_Click(sender) {
            var container = sender.parentNode;
            if (container.tagName.toUpperCase() == "TD")
            // 服务器控件设置呈现为 table 布局（默认设置），否则使用流布局即为td
            {
                container = container.parentNode.parentNode;
            }
            var chkList = container.getElementsByTagName("input");
            var senderState = sender.checked;
            for (var i = 0; i < chkList.length; i++) {
                chkList[i].checked = false;
            }
            sender.checked = senderState;
        }
    </script>

    <div class="box-wrapper">
        <div class="box-outer">
            <table class="tabGg" cellpadding="0" cellspacing="0" width="100%">
                <asp:Panel ID="Panel1" runat="server">
                    <tr>
                        <td class="right_bg" style="font-size: x-large; text-align: center;" colspan="6">
                            纠正/预防措施报告<br />
                            <div style="text-align: right;">
                                单据编号：
                                <asp:Label ID="lb_orderid" runat="server" Text=""></asp:Label></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                            主送部门：
                        </td>
                        <td class="right_bg" colspan="5">
                            <asp:CheckBoxList runat="server" ID="cblMainDep" RepeatColumns="12">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                            涉及部门：
                        </td>
                        <td class="right_bg" colspan="5">
                            <asp:CheckBoxList runat="server" ID="cblCopyDep" RepeatColumns="12">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg" style="width: 200px">
                            主题：
                        </td>
                        <td class="right_bg" style="width: 200px">
                            <asp:TextBox ID="txtTheme" runat="server"></asp:TextBox>
                        </td>
                        <td class="r_bg">
                            提出人：
                        </td>
                        <td class="right_bg" style="width: 200px">
                            <asp:Label runat="server" ID="lblSHY"></asp:Label>
                        </td>
                        <td class="r_bg">
                            开单日期：
                        </td>
                        <td class="right_bg" style="width: 200px">
                            <asp:TextBox runat="server" ID="txtKaiDanTime" class="easyui-datebox" data-options="editable:false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                            问题描述/资料：
                        </td>
                        <td class="right_bg" colspan="5">
                            <table width="100%">
                                <tr>
                                    <td class="right_bg" colspan="4">
                                        <asp:TextBox runat="server" ID="txtProblem" Width="100%" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="Panel5" runat="server">
                    <tr>
                        <td class="r_bg">
                            审 批：
                        </td>
                        <td class="right_bg" colspan="5">
                            <table width="100%">
                                <tr>
                                    <td width="200px">
                                        部门负责人：
                                        <asp:Label ID="lb_fzr1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td width="200px">
                                        审批时间：<asp:Label ID="lb_jsfzrsj1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:RadioButtonList ID="jsb_result1" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="tb_opinion21" runat="server" TextMode="MultiLine" Width="90%" Height="40px"
                                            Rows="5"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                            选择整改人：
                        </td>
                        <td class="right_bg" colspan="5">
                            <asp:CheckBoxList runat="server" ID="cblZGR" RepeatColumns="12">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server">
                    <tr>
                        <td class="r_bg">
                            原因分析：
                        </td>
                        <td class="right_bg" colspan="5">
                            <table width="100%">
                                <tr>
                                    <td class="right_bg" colspan="4">
                                        <asp:TextBox runat="server" ID="txtYYFX" Width="100%" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                            纠正措施：
                        </td>
                        <td class="right_bg" colspan="5">
                            <table width="100%">
                                <tr>
                                    <td class="right_bg" colspan="4">
                                        <asp:TextBox runat="server" ID="txtJS" Width="100%" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                            预防措施:
                        </td>
                        <td class="right_bg" colspan="5">
                            <table width="100%">
                                <tr>
                                    <td class="right_bg" colspan="4">
                                        <asp:TextBox runat="server" ID="txtCorrect" Width="100%" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="right_bg" style="width: 50px">
                                        整改人:
                                    </td>
                                    <td class="right_bg">
                                        <asp:Label ID="lblZGR" runat="server"></asp:Label>
                                    </td>
                                    <td class="right_bg" style="width: 50px">
                                        日 期:
                                    </td>
                                    <td class="right_bg">
                                        <asp:Label ID="lblZGData" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server">
                    <tr>
                        <td class="r_bg">
                            审 批：
                        </td>
                        <td class="right_bg" colspan="5">
                            <table width="100%">
                                <tr>
                                    <td width="200px">
                                        部门负责人：
                                        <asp:Label ID="lb_fzr" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td width="200px">
                                        审批时间：<asp:Label ID="lb_jsfzrsj" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:RadioButtonList ID="jsb_result" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="tb_opinion2" runat="server" TextMode="MultiLine" Width="90%" Height="40px"
                                            Rows="5"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="Panel4" runat="server">
                    <tr>
                        <td class="r_bg">
                            验证纠正措施有效性:
                        </td>
                        <td class="right_bg" colspan="5">
                            <table width="100%">
                                <tr>
                                    <td width="200px">
                                        验证人：<asp:Label ID="lb_yzr" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td width="200px">
                                        审批时间：<asp:Label ID="lb_yzrsj" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:RadioButtonList ID="check_result" runat="server" RepeatColumns="2">
                                            <asp:ListItem Text="通过" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="未通过" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="tb_opinion4" runat="server" TextMode="MultiLine" Width="90%" Height="40px"
                                            Rows="5"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                            评价总结:
                        </td>
                        <td class="right_bg" colspan="5">
                            <table width="100%">
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="90%" Height="40px"
                                            Rows="5"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td>
                        <input type="hidden" runat="server" id="hidState" />
                        <input type="hidden" runat="server" id="hidZGRID" /><input runat="server" type="hidden"
                            id="hidSPRID" />
                        <input type="hidden" runat="server" id="hidYZRID" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <table class="tabGg" cellpadding="0" cellspacing="0" width="100%">
                <asp:Panel ID="Pal_attachment" runat="server">
                    <tr>
                        <td class="r_bg" width="120px">
                            附件：
                        </td>
                        <td class="right_bg" colspan="5">
                            <uf:UploadFiles ID="UploadAttachments1" runat="server" />
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <!--box-outer END -->
    </div>
</asp:Content>
