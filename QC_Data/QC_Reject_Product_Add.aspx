<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/BaseMaster.master"
    CodeBehind="QC_Reject_Product_Add.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_Reject_Product_Add"
    ValidateRequest="false" %>

<%@ Register Src="../Controls/UploadFiles.ascx" TagName="UploadFiles" TagPrefix="uf" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">

    <script type="text/javascript" language="javascript">
        //打印
        function Print() {

            window.print();

        }
        
        function OnTxtPersonInfoKeyDown()
        { 
           var jobgoal = document.getElementById("<%=tb_engId.ClientID%>").value; 
            var acNameClientId="<%=AutoCompleteExtender2.ClientID %>";
            var acName=$find(acNameClientId);
            if(acName!=null)
                   acName.set_contextKey(jobgoal);
        }
       
    </script>

    <table width="100%">
        <tr>
            <td>
                添加/修改/评审-不合格品通知单
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
                <asp:LinkButton ID="LbtnDeal" runat="server" CausesValidation="False" OnClick="LbtnDeal_Click"
                    Visible="false">
                    <asp:Image ID="Image1" Style="cursor: hand" ToolTip="已处理" ImageUrl="~/Assets/icons/edit.gif"
                        Height="17" Width="17" runat="server" />
                    已处理
                </asp:LinkButton>&nbsp; &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="chrome=1" />

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/jmeditor/jquery-1.8.3.min.js" type="text/javascript"></script>

    <script src="../JS/jmeditor/JMEditor.js" type="text/javascript"></script>

    <link href="../Contract_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: auto !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>

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
        function ChangePro() {
            var engId = $("#<%=tb_engId.ClientID %>").val();

            if (engId.indexOf("|") > -1) {
                $("#<%=tb_engId.ClientID %>").val(engId.split('|')[0]);
                $("#<%=tb_conId.ClientID %>").val(engId.split('|')[1]);
                $("#<%=tb_pjinfo.ClientID %>").val(engId.split('|')[2]);

                $.post("QC_AjaxHandler.aspx", { method: "GetTechPerson", EngId: engId.split('|')[0] }, function(data) {
                    if (data.msg) {
                        // console.log($("#<%=cbl_psr.ClientID %> [type=checkbox]"));
                        $("#<%=cbl_psr.ClientID %> [type=checkbox]").each(function() {
                            //  console.log($(this).next().html());
                            if ($(this).next().html() == data.techName) {
                                $(this).attr("checked", "true");
                                // console.log($(this).parent().siblings().find("input"));
                                $("#<%=cbl_psr.ClientID %> [type=checkbox]").not(this).removeAttr("checked");
                            }
                        });
                    } else {
                        alert("未获取技术负责人ID，请自行勾选评审人员！");
                    }
                }, "JSON");
            }

            else {
                alert('请输入正确的项目');
                return;
            }

        }

        $(function() {
            if ($("#<%=txtDeclare.ClientID %>").val() != "") {
                $("#content").html($("#<%=txtDeclare.ClientID %>").val());
            }

            //alert($("#<%=txtDeclare.ClientID %>").val());
            $("#content").blur(function() {
                $("#<%=txtDeclare.ClientID %>").val($("#content").html());
                //  alert($("#<%=txtDeclare.ClientID %>").val());
            });
        });
        
    </script>

    <div class="box-wrapper">
        <div class="box-outer">
            <table class="tabGg" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="right_bg" style="font-size: x-large; text-align: center;" colspan="6">
                        不合格品通知单<br />
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
                        抄送部门：
                    </td>
                    <td class="right_bg" colspan="5">
                        <asp:CheckBoxList runat="server" ID="cblCopyDep" RepeatColumns="12">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <asp:Panel ID="Panel1" runat="server">
                    <tr>
                        <td class="r_bg">
                            任务号：
                        </td>
                        <td class="right_bg">
                            <asp:TextBox ID="tb_engId" runat="server" Text="" onchange="ChangePro()"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionSetCount="15"
                                ServiceMethod="gcmc" FirstRowSelected="true" Enabled="true" DelimiterCharacters=""
                                ServicePath="../Ajax.asmx" TargetControlID="tb_engId" UseContextKey="True" CompletionInterval="10"
                                MinimumPrefixLength="1" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td class="r_bg">
                            项目名称：
                        </td>
                        <td class="right_bg">
                            <asp:TextBox ID="tb_pjinfo" runat="server" onfocus="this.blur()" Text=""></asp:TextBox>
                        </td>
                        <td class="r_bg">
                            合同号：
                        </td>
                        <td class="right_bg">
                            <asp:TextBox ID="tb_conId" runat="server" onfocus="this.blur()" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                            件 数：
                        </td>
                        <td class="right_bg">
                            <asp:TextBox ID="num" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td class="r_bg">
                            责任班组：
                        </td>
                        <td class="right_bg">
                            <asp:TextBox ID="txtZrbz" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td class="r_bg">
                            产品名称：
                        </td>
                        <td class="right_bg">
                            <asp:TextBox ID="tb_cpmc" runat="server" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                            图 号：
                        </td>
                        <td class="right_bg">
                            <asp:TextBox ID="tb_th" runat="server" Text="" onkeydown="return OnTxtPersonInfoKeyDown();"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionSetCount="15"
                                ServiceMethod="get_rwh_th" FirstRowSelected="true" Enabled="true" DelimiterCharacters=""
                                ServicePath="../Ajax.asmx" TargetControlID="tb_th" UseContextKey="True" CompletionInterval="10"
                                MinimumPrefixLength="1" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td class="r_bg">
                            责任方：
                        </td>
                        <td class="right_bg">
                            <asp:TextBox ID="txtZRF" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td class="r_bg">
                            编制人：
                        </td>
                        <td class="right_bg">
                            <asp:TextBox ID="tb_bzr" runat="server" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                            不合格品类型：
                        </td>
                        <td class="right_bg" colspan="5">
                            <asp:CheckBoxList ID="Cbxl_reason1" runat="server" RepeatColumns="11">
                                <asp:ListItem Text="采购" Value="0"></asp:ListItem>
                                <asp:ListItem Text="外协" Value="1"></asp:ListItem>
                                <asp:ListItem Text="厂内" Value="2"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                        </td>
                        <td class="right_bg" colspan="5">
                            <asp:CheckBoxList ID="Cbxl_reason2" runat="server" RepeatColumns="12">
                                <asp:ListItem Text="化学成分" Value="0"></asp:ListItem>
                                <asp:ListItem Text="物理性能" Value="1"></asp:ListItem>
                                <asp:ListItem Text="毛坯尺寸" Value="2"></asp:ListItem>
                                <asp:ListItem Text="钢材尺寸" Value="3"></asp:ListItem>
                                <asp:ListItem Text="铸造缺陷" Value="4"></asp:ListItem>
                                <asp:ListItem Text="锻造缺陷" Value="5"></asp:ListItem>
                                <asp:ListItem Text="制作质量" Value="6"></asp:ListItem>
                                <asp:ListItem Text="装配质量" Value="7"></asp:ListItem>
                                <asp:ListItem Text="油漆质量" Value="8"></asp:ListItem>
                                <asp:ListItem Text="外观质量" Value="9"></asp:ListItem>
                                <asp:ListItem Text="硬度问题" Value="10"></asp:ListItem>
                                <asp:ListItem Text="其他" Value="11"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                            情况描述：
                        </td>
                        <td class="right_bg" colspan="5">
                            <div id="content" contenteditable="true" class="editDemo" style="overflow: scroll;
                                height: 300px; width: 90%">
                                <%=declare%>
                            </div>
                        </td>
                    </tr>
                    <tr runat="server" id="tr_zlb">
                        <td style="width: 100px" class="r_bg">
                            质量部审批人：
                        </td>
                        <td class="right_bg" colspan="5">
                            <asp:CheckBoxList ID="cbl_zlb" runat="server" RepeatColumns="8">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr runat="server" id="tr_psr" class="r_bg">
                        <td style="width: 100px">
                            指定评审人：
                        </td>
                        <td class="right_bg" colspan="5">
                            <asp:CheckBoxList ID="cbl_psr" runat="server" RepeatColumns="8">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr runat="server" id="tr_spr">
                        <td style="width: 100px" class="r_bg">
                            技术部审批人：
                        </td>
                        <td class="right_bg" colspan="5">
                            <asp:CheckBoxList ID="cbl_spr" runat="server" RepeatColumns="8">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr runat="server" id="tr_zgld">
                        <td style="width: 100px" class="r_bg">
                            指定主管经理：
                        </td>
                        <td class="right_bg" colspan="5">
                            <asp:CheckBoxList ID="cbl_zgjl" runat="server" RepeatColumns="8">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="Panel2" runat="server">
                                <asp:Panel ID="Pal_zlfzr" runat="server">
                        <tr>
                            <td class="r_bg">
                                审 批：
                            </td>
                            <td class="right_bg" colspan="5">
                                <table width="100%">
                                    <tr>
                                        <td width="200px">
                                            质量部负责人：
                                            <asp:Label ID="lb_zlfzr" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td width="200px">
                                            审批时间：<asp:Label ID="lb_zlfzrsj" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:RadioButtonList ID="zlb_result" runat="server" RepeatColumns="2">
                                                <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:TextBox ID="tb_opinion7" runat="server" TextMode="MultiLine" Width="90%" Height="40px"
                                                Rows="5"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="Pal_qf" runat="server">
                        <tr>
                            <td class="r_bg">
                                评 审：
                            </td>
                            <td class="right_bg" colspan="5">
                                <table width="100%">
                                    <tr>
                                        <td class="right_bg">
                                            不合格分级：
                                        </td>
                                        <td class="right_bg" colspan="2">
                                            <asp:CheckBoxList ID="Cbxl_rank" runat="server" RepeatColumns="4">
                                                <asp:ListItem Text="一般不合格" Value="0" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                                                <asp:ListItem Text="严重不合格" Value="1" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="right_bg">
                                            处置方式:
                                        </td>
                                        <td class="right_bg" colspan="2">
                                            <asp:CheckBoxList ID="DealMethods" runat="server" RepeatColumns="5">
                                                <asp:ListItem Text="让步接收" Value="1" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                                                <asp:ListItem Text="返修" Value="2" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                                                <asp:ListItem Text="报废" Value="3" onclick="CheckBoxList_Click(this)"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="200px">
                                            评审人：
                                            <asp:Label ID="lb_psr" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                            审批时间：<asp:Label ID="lb_psrsj" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:TextBox ID="tb_opinion1" runat="server" TextMode="MultiLine" Width="90%" Height="40px"
                                                Rows="5"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="Pal_jsfzr" runat="server">
                        <tr>
                            <td class="r_bg">
                                审 批：
                            </td>
                            <td class="right_bg" colspan="5">
                                <table width="100%">
                                    <tr>
                                        <td width="200px">
                                            技术部负责人：
                                            <asp:Label ID="lb_jsfzr" runat="server" Text="" Font-Bold="true"></asp:Label>
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
                    <asp:Panel ID="Pal_zgld" runat="server">
                        <tr>
                            <td class="r_bg">
                                审 批：
                            </td>
                            <td class="right_bg" colspan="5">
                                <table width="100%">
                                    <tr>
                                        <td width="200px">
                                            主管经理：
                                            <asp:Label ID="lblZgld" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td width="200px">
                                            审批时间：<asp:Label ID="lb_zgldsj" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:RadioButtonList ID="zgld_result" runat="server" RepeatColumns="2">
                                                <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:TextBox ID="tb_opinion3" runat="server" TextMode="MultiLine" Width="90%" Height="40px"
                                                Rows="5"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server">
                    <asp:Panel ID="Pal_yzr" runat="server">
                        <tr>
                            <td class="r_bg">
                                验证:
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
                    </asp:Panel>
                </asp:Panel>
                <tr>
                    <td>
                        <input type="hidden" runat="server" id="hidState" />
                        <input type="hidden" runat="server" id="hidSPR_ZL_ID" />
                        <input type="hidden" runat="server" id="hidPSRID" />
                            <input runat="server" type="hidden" id="hidSPRID" />
                            <input type="hidden" runat="server" id="hidYZRID" />
                            <input runat="server" type="hidden" id="hidZGLDID" />
                        <asp:TextBox runat="server" ID="txtDeclare" Style="display: none"></asp:TextBox>
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
    <!--box-wrapper END -->
</asp:Content>
