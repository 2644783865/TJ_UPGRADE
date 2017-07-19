<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_Finished_out_look.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.Master" Inherits="ZCZJ_DPF.PM_Data.PM_Finished_out_look" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Label ID="lbl_spzt" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Button ID="btn_Audit" runat="server" Text="提交审批" OnClick="btn_Audit_Click" OnClientClick="javascript:return confirm('确认提交吗？');" />
                                &nbsp;
                                <asp:Button ID="btn_confirm" runat="server" Text="下推" Visible="false" /><%--此按钮暂时不用，审核通过后自动下推--%>
                                <%-- <asp:TextBox ID="lb_state" runat="server" Visible="false" Text=""></asp:TextBox>--%>
                                &nbsp;
                                <asp:HyperLink ID="hyl_back" runat="server" CssClass="hand" onclick="history.go(-1);">返回上一页</asp:HyperLink>
                                &nbsp; &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:TabContainer ID="tab_Add_finished" runat="server" Width="100%" TabStripPlacement="Top"
            ActiveTabIndex="0">
            <asp:TabPanel ID="Tab_audit" runat="server" HeaderText="审 核" TabIndex="0">
                <ContentTemplate>
                    <div class="box-wrapper">
                        <div style="height: 6px" class="box_top">
                        </div>
                        <div class="box-outer">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_info" runat="server" Text="* 一级审批为项目负责人，二级审批为成品库管员！" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rblSHDJ" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblSHDJ_Changed">
                                            <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="二级审核" Value="2" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="三级审核" Value="3" ></asp:ListItem>
                                        </asp:RadioButtonList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                        成品出库申请
                                        <asp:Image ID="ImageAUDIT" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="box-outer">
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <asp:Panel ID="pal_first" runat="server">
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
                                                                        <asp:Button ID="btn_shr1" runat="server" Text="确 定" OnClick="btn_shr1_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rbl_first" RepeatColumns="2" runat="server" Height="20px">
                                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
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
                                                            Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pal_second" runat="server">
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
                                                                        <asp:Button ID="btn_shr2" runat="server" Text="确 定" OnClick="btn_shr2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rbl_second" RepeatColumns="2" runat="server" Height="20px">
                                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
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
                                                            Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pal_third" runat="server">
                                    <tr>
                                        <td>
                                            <table align="center" width="100%">
                                                <tr>
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
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
                                                                        <asp:Button ID="btn_shr3" runat="server" Text="确 定" OnClick="btn_shr3_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rbl_third" RepeatColumns="2" runat="server" Height="20px">
                                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
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
                                                            Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="Tab_rud" runat="server" HeaderText="成品出库单" TabIndex="1">
                <ContentTemplate>
                    <div class="box-wrapper">
                        <div class="box-wrapper">
                            <%-- <div style="height: 6px" class="box_top">
                            </div>--%>
                            <div class="box-outer">
                                <table width="100%">
                                    <tr>
                                        <td style="font-size: x-large; text-align: center;" colspan="3">
                                            成品出库单
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 33%;" align="left">
                                            &nbsp;&nbsp;&nbsp;出库&nbsp;&nbsp;单号
                                            <asp:TextBox ID="txt_docnum" runat="server" Text="" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td style="width: 33%;" align="left">
                                            &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：
                                            <asp:TextBox ID="Tb_shijian" runat="server" Text="" Enabled="false" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            备&nbsp;&nbsp;&nbsp;注：
                                            <asp:TextBox ID="txt_note" runat="server" Text="" Enabled="false" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div style="border: 1px solid #000000;">
                                    <div style="border: 1px solid #000000; height: 330px">
                                        <div class="cpbox5 xscroll">
                                            <table id="tab" class="nowrap cptable fullwidth">
                                                <asp:Repeater ID="PM_Finished_lookRepeater" runat="server">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                            <td>
                                                                <strong>序号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>编号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>项目名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>合同号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>任务单号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>总序</strong>
                                                            </td>
                                                            <td>
                                                                <strong>图号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>设备名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>出库数量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td>
                                                                <strong>出库时间</strong>
                                                            </td>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                            <td>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex+1%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblbianhao" runat="server" Text='<%#Eval("CM_BIANHAO")%>'></asp:Label>
                                                                <asp:Label ID="lblfid" runat="server" Text='<%#Eval("CM_FID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="CM_ID" runat="server" Text='<%#Eval("CM_ID")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblProj" runat="server" Text='<%#Eval("KC_PROJ")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblContr" runat="server" Text='<%#Eval("CM_CONTR")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTsa" runat="server" Text='<%#Eval("TSA_ID")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="TFO_ZONGXU" runat="server" Text='<%#Eval("TFO_ZONGXU")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblMap" runat="server" Text='<%#Eval("KC_MAP")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEngname" runat="server" Text='<%#Eval("KC_NAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblNumber" runat="server" Text='<%#Eval("TFO_CKNUM")%>'></asp:Label>
                                                                <asp:Label ID="lblkcnum" runat="server" Text='<%#Eval("TFO_KCNUM")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblFHdate" runat="server" Text='<%#Eval("CM_JHTIME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblIndate" runat="server" Text='<%#Eval("OUTDATE")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="17" align="center">
                                                        <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                            没有数据！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                 <div>
                    <table width="100%">
                        <tr>
                            <td width="20%">
                            </td>
                            <td width="20%">
                            </td>
                            <td width="60%">
                                <input type="hidden" runat="server" id="hidsj" />
                                <asp:FileUpload ID="FileUpload2" runat="server" />
                                <asp:Button ID="btnFU1" runat="server" Text="上传文件" OnClick="btnFU1_OnClick" CausesValidation="False" />
                                <br />
                                <asp:Label ID="filesError2" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                                <table width="60%">
                                    <asp:Repeater runat="server" ID="rptJBXX">
                                        <HeaderTemplate>
                                            <tr style="background-color: #71C671;">
                                                <td width="40%" align="center">
                                                    <strong>文件名称</strong>
                                                </td>
                                                <td width="30%" align="center">
                                                    <strong>文件上传时间</strong>
                                                </td>
                                                <td width="15%" align="center">
                                                    <strong>删除</strong>
                                                </td>
                                                <td width="15%" align="center">
                                                    <strong>下载</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="FILE_SHOWNAME" Text='<%#Eval("FILE_SHOWNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="FILE_UPDATE" Text='<%#Eval("FILE_UPDATE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbtndelete2" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtndelete2_OnClick" CausesValidation="False" Text="删除">
                                                        <asp:Image runat="server" ID="imgdelete1" ImageUrl="~/Assets/images/erase.gif" Width="15px"
                                                            Height="15px" />
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbtnonload2" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtnonload2_OnClick" CausesValidation="False" Text="下载">
                                                        <asp:Image runat="server" ID="imgonload" ImageUrl="~/Assets/images/erase.gif" Height="15px"
                                                            Width="15px" />
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                负责人:<asp:TextBox ID="Tb_fuziren" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="Tb_fuzirenid" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                申请人:<asp:TextBox ID="Tb_shenqingren" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="Tb_shenqingrenid" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                制单人:<asp:TextBox ID="tb_executor" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="tb_executorid" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
</asp:Content>
