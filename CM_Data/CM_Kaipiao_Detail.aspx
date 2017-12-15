<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_Kaipiao_Detail.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Kaipiao_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UploadFiles.ascx" TagName="UploadFiles" TagPrefix="uf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    开票管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">

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

        $(function() {
            $("#btnPrint").click(function() {
                console.log($("#<%=hidId.ClientID %>"));
          window.open("CM_Kaipiao_Print.aspx?Id=" + $("#<%=hidId.ClientID %>").val());

            });
        });
    </script>

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
            width: 200px !important;
            font-size: small;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
            font-size: small;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            font-size: small;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class='box-title'>
                <table width="95%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnTicket" runat="server" Text="开  票" OnClick="btnTicket_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnShenH" runat="server" Text="提交审批" OnClick="btnShenH_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_Submit" runat="server" Text="保 存" OnClick="btn_Submit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" value="打 印" id="btnPrint" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnExport" runat="server" Text="导出Excel" OnClick="btnExport_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="Back" Text="返 回" OnClick="btn_Back_Click" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper1">
        <div class="box-outer">
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="申请单信息" TabIndex="0">
                    <HeaderTemplate>
                        申请单信息
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="width: 85%; margin: 0px auto;">
                            <h2 style="text-align: center; margin-top: 20px; font-size: 25">
                                增值税发票申请单
                            </h2>
                            <asp:Panel ID="Panel" runat="server" Width="100%">
                                <table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                    <tr>
                                        <td style="width: 120px">
                                            编号：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_CODE" runat="server" Width="220px"></asp:TextBox>
                                            <input type="hidden" runat="server" id="hidTaskId" />
                                            <input type="hidden" runat="server" id="hidId" />
                                            <input type="hidden" runat="server" id="hidAction" />
                                            <input type="hidden" runat="server" id="hidSPState" />
                                            <input type="hidden" runat="server" id="hidHSState" />
                                        </td>
                                        <td style="width: 120px">
                                            制单人：
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="KP_ZDRNM"></asp:Label>
                                            <input type="hidden" runat="server" id="KP_ZDRID" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            单位名称：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_COM" runat="server" Width="220px" OnTextChanged="KP_COM_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="KP_COM"
                                                ServicePath="CM_Applica.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                                CompletionInterval="100" ServiceMethod="GetCompleteProvider" FirstRowSelected="true"
                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </asp:AutoCompleteExtender>
                                        </td>
                                        <td>
                                            单位地址：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_ADDRESS" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            设备名称：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_SHEBEI" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                        <td>
                                            帐号：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_ACCOUNT" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            税号：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_SHUIHAO" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                        <td>
                                            开户行：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_BANK" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            联系电话（纳税电话）：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_TEL" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                        <td>
                                            合同总价：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_CONZONGJIA" runat="server" Width="220px" onkeyup="value=value.replace(/[^\- \d.]/g,'')"></asp:TextBox> (万元)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            到款金额：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_DAOKUANJE" runat="server" Width="220px" onkeyup="value=value.replace(/[^\- \d.]/g,'')"></asp:TextBox> (万元)
                                        </td>
                                        <td>
                                            合同规定开票条件：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_CONDITION" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            已开票金额：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_YIKAIJE" runat="server" Width="220px" onkeyup="value=value.replace(/[^\- \d.]/g,'')"></asp:TextBox> (万元)
                                        </td>
                                        <td>
                                            本次开票金额：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_BENCIJE" runat="server" Width="220px" onkeyup="value=value.replace(/[^\- \d.]/g,'')"></asp:TextBox> (万元)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            发票交付方式：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KP_JIAOFUFS" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                        <td>
                                            是否提前开票：
                                        </td>
                                        <td>
                                            <asp:RadioButtonList runat="server" ID="KP_TIQIANKP" RepeatColumns="2">
                                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="否" Selected="True" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 220px">
                                            发票号：
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="KP_KPNUMBER" Width="220px"></asp:TextBox>
                                        </td>
                                        <td>
                                            开票日期：
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="KP_KPDATE"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            制单时间：
                                        </td>
                                        <td>
                                            <asp:Label ID="KP_ZDTIME" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 220px">
                                            开票合同号：
                                        </td>
                                        <td>
                                            <input id="txtConId" runat="server" type="text" style="width: 80px; text-align: center" />&nbsp;&nbsp;
                                            <asp:Button ID="btnAdd" runat="server" Text="增 加" OnClick="btnadd_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            备注：
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="KP_NOTE" runat="server" TextMode="MultiLine" Width="600px" Height="50px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            附件上传：
                                        </td>
                                        <td style="text-align: center;" colspan="3">
                                            <uf:UploadFiles ID="UploadAttachments1" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table width="100%">
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333">
                                            <RowStyle BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" CssClass="checkBoxCss" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="35px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                        <asp:HiddenField ID="hide" runat="server" Value='<%#Eval("Id") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TaskId" HeaderText="任务号" HeaderStyle-Wrap="false">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ConId" HeaderText="合同号" HeaderStyle-Wrap="false">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Proj" HeaderText="项目名称" HeaderStyle-Wrap="false">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </asp:BoundField>
                                          <%--      <asp:BoundField DataField="Engname" HeaderText="设备名称" HeaderStyle-Wrap="false">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </asp:BoundField>--%>
                                                <asp:TemplateField HeaderText="设备名称" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" Text='<%# Eval("Engname") %>' Width="120px" ID="txtEngname"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Map" HeaderText="图号" HeaderStyle-Wrap="false">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Number" HeaderText="数量" HeaderStyle-Wrap="false">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unit" HeaderText="单位" HeaderStyle-Wrap="false">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="开票金额（万元）" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="60px">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" Text='<%# Eval("kpmoney") %>' Width="120px" ID="kpMoney"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Money" HeaderText="合同金额（万元）" HeaderStyle-Wrap="false">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="顶发任务号" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="60px">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" Text='<%# Eval("dfTaskId") %>' Width="120px" ID="dfTaskId"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="顶发项目名称" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" Text='<%# Eval("dfProName") %>' Width="120px" ID="dfProName"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="dfConId" HeaderText="对方合同号" HeaderStyle-Wrap="false">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" Font-Size="Small" />
                                        </asp:GridView>
                                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                            没有记录!</asp:Panel>
                                        <br />
                                        <div style="float: left">
                                            <asp:Button ID="delete" runat="server" Text="删除明细" OnClick="delete_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="save" runat="server" Text="保存明细" OnClick="save_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            合计金额：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="Pan_ShenHe" HeaderText="评审信息" TabIndex="1">
                    <ContentTemplate>
                        <div class="box-wrapper">
                            <div style="height: 6px" class="box_top">
                            </div>
                            <div class="box-outer">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                            领导审批
                                            <asp:Image ID="ImageAUDIT" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="box-outer">
                                <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <tbody runat="server" id="tb1">
                                        <tr>
                                            <td>
                                                <table align="center" width="100%">
                                                    <tr style="height: 25px">
                                                        <td align="center" style="width: 10%">
                                                            审批人
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                            <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                            <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand">
                                                                <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                    align="absmiddle" runat="server" />
                                                                选择
                                                            </asp:HyperLink>
                                                            <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                                                Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect1"
                                                                PopupControlID="pal_select1">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="pal_select1" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                                <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                    font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                    <tr>
                                                                        <td style='background-color: #A8B7EC; color: white;'>
                                                                            <b>选择审核人</b>
                                                                        </td>
                                                                        <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                            <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                                text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Panel ID="pal_select1_inner" runat="server">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="right">
                                                                            <asp:Button ID="btn_shr1" runat="server" Text="确 定" OnClick="btn_shr_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核结论
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            <asp:RadioButtonList ID="rbl_first" RepeatColumns="2" runat="server" Height="20px"
                                                                Enabled="false">
                                                                <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核时间
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                                ReadOnly="true" Height="42px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tbody runat="server" id="tb2">
                                        <tr>
                                            <td>
                                                <table align="center" width="100%">
                                                    <tr>
                                                        <td align="center" style="width: 10%">
                                                            审批人
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                                            <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                            <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand">
                                                                <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                    align="absmiddle" runat="server" />
                                                                选择
                                                            </asp:HyperLink>
                                                            <asp:PopupControlExtender ID="PopupControlExtender2" CacheDynamicResults="false"
                                                                Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect2"
                                                                PopupControlID="pal_select2">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="pal_select2" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                                <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                    font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                    <tr>
                                                                        <td style='background-color: #A8B7EC; color: white;'>
                                                                            <b>选择审核人</b>
                                                                        </td>
                                                                        <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                            <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                                text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Panel ID="pal_select2_inner" runat="server">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="right">
                                                                            <asp:Button ID="btn_shr2" runat="server" Text="确 定" OnClick="btn_shr_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核结论
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            <asp:RadioButtonList ID="rbl_second" RepeatColumns="2" runat="server" Height="20px"
                                                                Enabled="false">
                                                                <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核时间
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <asp:TextBox ID="second_opinion" runat="server" Height="42px" TextMode="MultiLine"
                                                                ReadOnly="true" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="Pan_HuiShen" HeaderText="会审信息" TabIndex="1">
                    <ContentTemplate>
                        <div class="box-wrapper">
                            <div style="height: 6px" class="box_top">
                            </div>
                            <div class="box-outer">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                            会审信息
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="box-outer">
                                <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <tbody runat="server" id="tb4">
                                        <tr>
                                            <td>
                                                <table align="center" width="100%">
                                                    <tr>
                                                        <td align="center" style="width: 10%">
                                                            库房审批
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txt_fourth" runat="server" Width="80px"></asp:TextBox>
                                                            <input id="fourthid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                            <input type="hidden" id="hidNum4" runat="server" />
                                                            <asp:HyperLink ID="hlSelect4" runat="server" CssClass="hand">
                                                                <asp:Image ID="AddImage4" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                    align="absmiddle" runat="server" />
                                                                选择
                                                            </asp:HyperLink>
                                                            <asp:PopupControlExtender ID="PopupControlExtender4" CacheDynamicResults="false"
                                                                Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect4"
                                                                PopupControlID="pal_select4">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="pal_select4" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                                <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                    font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                    <tr>
                                                                        <td style='background-color: #A8B7EC; color: white;'>
                                                                            <b>选择审核人</b>
                                                                        </td>
                                                                        <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                            <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                                text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Panel ID="pal_select4_inner" runat="server">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="right">
                                                                            <asp:Button ID="btn_shr4" runat="server" Text="确 定" OnClick="btn_shr_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核结论
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            <asp:RadioButtonList ID="rbl_fourth" RepeatColumns="2" runat="server" Height="20px"
                                                                Enabled="false">
                                                                <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核时间
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="fourth_time" runat="server" Width="100%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <asp:TextBox ID="fourth_opinion" runat="server" Height="42px" TextMode="MultiLine"
                                                                ReadOnly="true" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tbody runat="server" id="tb5">
                                        <tr>
                                            <td>
                                                <table align="center" width="100%">
                                                    <tr style="height: 25px">
                                                        <td align="center" style="width: 10%">
                                                            外协审批
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txt_fifth" runat="server" Width="80px"></asp:TextBox>
                                                            <input id="fifthid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                            <input type="hidden" id="hidNum5" runat="server" />
                                                            <asp:HyperLink ID="hlSelect5" runat="server" CssClass="hand">
                                                                <asp:Image ID="AddImage5" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                    align="absmiddle" runat="server" />
                                                                选择
                                                            </asp:HyperLink>
                                                            <asp:PopupControlExtender ID="PopupControlExtender5" CacheDynamicResults="false"
                                                                Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect5"
                                                                PopupControlID="pal_select5">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="pal_select5" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                                <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                    font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                    <tr>
                                                                        <td style='background-color: #A8B7EC; color: white;'>
                                                                            <b>选择审核人</b>
                                                                        </td>
                                                                        <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                            <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                                text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Panel ID="pal_select5_inner" runat="server">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="right">
                                                                            <asp:Button ID="btn_shr5" runat="server" Text="确 定" OnClick="btn_shr_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核结论
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            <asp:RadioButtonList ID="rbl_fifth" RepeatColumns="2" runat="server" Height="20px"
                                                                Enabled="false">
                                                                <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核时间
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="fifth_time" runat="server" Width="100%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <asp:TextBox ID="fifth_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                                ReadOnly="true" Height="42px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tbody runat="server" id="tb6">
                                        <tr>
                                            <td>
                                                <table align="center" width="100%">
                                                    <tr>
                                                        <td align="center" style="width: 10%">
                                                            人工成本审批
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txt_sixth" runat="server" Width="80px"></asp:TextBox>
                                                            <input id="sixthid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                            <input type="hidden" id="hidNum6" runat="server" />
                                                            <asp:HyperLink ID="hlSelect6" runat="server" CssClass="hand">
                                                                <asp:Image ID="AddImage6" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                    align="absmiddle" runat="server" />
                                                                选择
                                                            </asp:HyperLink>
                                                            <asp:PopupControlExtender ID="PopupControlExtender6" CacheDynamicResults="false"
                                                                Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect6"
                                                                PopupControlID="pal_select6">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="pal_select6" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                                <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                    font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                    <tr>
                                                                        <td style='background-color: #A8B7EC; color: white;'>
                                                                            <b>选择审核人</b>
                                                                        </td>
                                                                        <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                            <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                                text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Panel ID="pal_select6_inner" runat="server">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="right">
                                                                            <asp:Button ID="btn_shr6" runat="server" Text="确 定" OnClick="btn_shr_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核结论
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            <asp:RadioButtonList ID="rbl_sixth" RepeatColumns="2" runat="server" Height="20px"
                                                                Enabled="false">
                                                                <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核时间
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="sixth_time" runat="server" Width="100%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <asp:TextBox ID="sixth_opinion" runat="server" Height="42px" TextMode="MultiLine"
                                                                ReadOnly="true" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tbody runat="server" id="tb7">
                                        <tr>
                                            <td>
                                                <table align="center" width="100%">
                                                    <tr>
                                                        <td align="center" style="width: 10%">
                                                            成品库审批
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txt_seventh" runat="server" Width="80px"></asp:TextBox>
                                                            <input id="seventhId" type="text" runat="server" readonly="readonly" style="display: none" />
                                                            <input type="hidden" id="hidNum7" runat="server" />
                                                            <asp:HyperLink ID="hlSelect7" runat="server" CssClass="hand">
                                                                <asp:Image ID="AddImage7" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                    align="absmiddle" runat="server" />
                                                                选择
                                                            </asp:HyperLink>
                                                            <asp:PopupControlExtender ID="PopupControlExtender7" CacheDynamicResults="false"
                                                                Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect7"
                                                                PopupControlID="pal_select7">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="pal_select7" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                                <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                    font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                    <tr>
                                                                        <td style='background-color: #A8B7EC; color: white;'>
                                                                            <b>选择审核人</b>
                                                                        </td>
                                                                        <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                            <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                                text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Panel ID="pal_select7_inner" runat="server">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="right">
                                                                            <asp:Button ID="btn_shr7" runat="server" Text="确 定" OnClick="btn_shr_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核结论
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            <asp:RadioButtonList ID="rbl_seventh" RepeatColumns="2" runat="server" Height="20px"
                                                                Enabled="false">
                                                                <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核时间
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="seventh_time" runat="server" Width="100%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <asp:TextBox ID="seventh_opinion" runat="server" Height="42px" TextMode="MultiLine"
                                                                ReadOnly="true" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tbody runat="server" id="tb8">
                                        <tr>
                                            <td>
                                                <table align="center" width="100%">
                                                    <tr>
                                                        <td align="center" style="width: 10%">
                                                            发运审批
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txt_eighth" runat="server" Width="80px"></asp:TextBox>
                                                            <input id="eighthId" type="text" runat="server" readonly="readonly" style="display: none" />
                                                            <input type="hidden" id="hidNum8" runat="server" />
                                                            <asp:HyperLink ID="hlSelect8" runat="server" CssClass="hand">
                                                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                                    runat="server" />
                                                                选择
                                                            </asp:HyperLink>
                                                            <asp:PopupControlExtender ID="PopupControlExtender8" CacheDynamicResults="false"
                                                                Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect8"
                                                                PopupControlID="pal_select8">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="pal_select8" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                                <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                    font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                    <tr>
                                                                        <td style='background-color: #A8B7EC; color: white;'>
                                                                            <b>选择审核人</b>
                                                                        </td>
                                                                        <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                            <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                                text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Panel ID="pal_select8_inner" runat="server">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="right">
                                                                            <asp:Button ID="btn_shr8" runat="server" Text="确 定" OnClick="btn_shr_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核结论
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            <asp:RadioButtonList ID="rbl_eighth" RepeatColumns="2" runat="server" Height="20px"
                                                                Enabled="false">
                                                                <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核时间
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="eighth_time" runat="server" Width="100%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <asp:TextBox ID="eighth_opinion" runat="server" Height="42px" TextMode="MultiLine"
                                                                ReadOnly="true" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tbody runat="server" id="tb3">
                                        <tr>
                                            <td>
                                                <table align="center" width="100%">
                                                    <tr style="height: 25px">
                                                        <td align="center" style="width: 10%">
                                                            财务审批
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                                            <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                            <input type="hidden" id="hidNum3" runat="server" />
                                                            <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand">
                                                                <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                    align="absmiddle" runat="server" />
                                                                选择
                                                            </asp:HyperLink>
                                                            <asp:PopupControlExtender ID="PopupControlExtender3" CacheDynamicResults="false"
                                                                Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect3"
                                                                PopupControlID="pal_select3">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="pal_select3" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                                <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                    font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                    <tr>
                                                                        <td style='background-color: #A8B7EC; color: white;'>
                                                                            <b>选择审核人</b>
                                                                        </td>
                                                                        <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                            <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                                text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Panel ID="pal_select3_inner" runat="server">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="right">
                                                                            <asp:Button ID="btn_shr3" runat="server" Text="确 定" OnClick="btn_shr_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核结论
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            <asp:RadioButtonList ID="rbl_third" RepeatColumns="2" runat="server" Height="20px"
                                                                Enabled="false">
                                                                <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核时间
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <asp:TextBox ID="third_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                                ReadOnly="true" Height="42px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </div>
    </div>
</asp:Content>
