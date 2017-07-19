<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_BGYP_Apply_Detail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_BGYP_Applay_Detail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
办公用品申请单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
     <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ActiveTabIndex="0" AutoPostBack="false">
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="申请明细" TabIndex="0">
            <ContentTemplate>
              
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="审  核" TabIndex="1">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td>
                                    <input id="btnMssubmit" type="button" value="提 交" onclick="return Submit();" />
                                    <asp:Button ID="btnsubmit" runat="server" CssClass="hidden" OnClick="btnsubmit_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" />
                                </td>
                                <td align="center">
                                    <asp:CheckBox ID="ckbMessage" Checked="true" runat="server" />&nbsp;邮件提醒
                                </td>
                                <td align="right">
                                    <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="二级审核" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: large; text-align: center; height: 43px" colspan="2">
                                    制作明细表
                                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-outer">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td style="width: 10%" align="center">
                                    项目名称
                                </td>
                                <td style="width: 40%">
                                    <asp:Label ID="proname" runat="server" Width="100%" />
                                </td>
                                <td style="width: 10%" align="center">
                                    设备名称
                                </td>
                                <td style="width: 40%">
                                    <asp:Label ID="engname" runat="server" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    编制
                                </td>
                                <td>
                                    <asp:Label ID="txt_editor" runat="server" Width="100%"></asp:Label>
                                </td>
                                <input id="editorid" type="text" runat="server" readonly="readonly" style="display: none" />
                                <td align="center">
                                    编制日期
                                </td>
                                <td>
                                    <asp:Label ID="plandate" runat="server" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    主管审核
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()"
                                                    Visible="false">
                                                    <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Height="20px">
                                                    <asp:ListItem Text="同意" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
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
                            <tr>
                                <td align="center">
                                    部门领导
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                                <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()">
                                                    <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Height="20px">
                                                    <asp:ListItem Text="同意" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="5"></asp:ListItem>
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
                            <tr>
                                <td align="center">
                                    主管经理
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                                <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelTechPersons3()">
                                                    <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblthird" RepeatColumns="2" runat="server" Height="20px">
                                                    <asp:ListItem Text="同意" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="7"></asp:ListItem>
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
                        </table>
                        <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
                        <input id="hidPerson" type="hidden" value="" />
                    </div>
                    <div>
                        <div id="win" visible="false">
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            按部门查询：
                                        </td>
                                        <td>
                                            <input id="dep" name="dept" value="03">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: 430px; height: 230px">
                                <table id="dg">
                                </table>
                            </div>
                        </div>
                        <div id="buttons" style="text-align: right" visible="false">
                            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
                                保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                                    onclick="javascript:$('#win').dialog('close')">取消</a>
                        </div>
                    </div>
                  
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
