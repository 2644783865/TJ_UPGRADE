<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_MyTask_PS.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_MyTask_PS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                评审合同信息
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
                    <asp:TabPanel ID="Pan_ShenHe" runat="server" HeaderText="部门评审" TabIndex="0">
                        <ContentTemplate>
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td width="110px" height="25" align="right">
                                        评审信息：
                                    </td>
                                    <td class="category">
                                        <table style="width: 100%" cellpadding="4" class="toptable grid" cellspacing="1"
                                            border="1">
                                            <tr>
                                                <td style="width: 15%">
                                                    评审人员姓名：
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_Name" runat="server"></asp:Label>
                                                    <asp:Label ID="lb_Id" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%">
                                                    评审项目：
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_Item" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="ZZ_PS" runat="server" visible="false">
                                                <td style="width: 15%">
                                                    申请转正时间：
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_Zheng" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="HT_PS" runat="server" visible="false">
                                                <td style="width: 15%">
                                                    新增合同时间：
                                                </td>
                                                <td>
                                                    合同起始时间：<asp:Label ID="lb_Start" runat="server"></asp:Label>
                                                    <br />
                                                    合同终止时间：<asp:Label ID="lb_End" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="LD_PS" runat="server" visible="false">
                                                <td style="width: 15%">
                                                    人员流动：
                                                </td>
                                                <td>
                                                    转出部门：<asp:Label ID="lb_Zchu" runat="server"></asp:Label>
                                                    <br />
                                                    转入部门：<asp:Label ID="lb_Zru" runat="server"></asp:Label>
                                                    <asp:Label ID="lb_DepId" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="NJ_PS" runat="server" visible="false">
                                                <td style="width: 15%">
                                                    年假申请：
                                                </td>
                                                <td>
                                                    数目：<asp:Label ID="lb_Holiday" runat="server"></asp:Label>&nbsp;天
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%">
                                                    备注：
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_Remark" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    制单人：
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_Zdr" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    制单人意见：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_zdryj" Columns="100" Rows="4" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" ID="Pan_gsld" HeaderText="领导批准信息" TabIndex="1">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </div>
        </div>
</asp:Content>
