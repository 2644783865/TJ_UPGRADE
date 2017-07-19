<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_KaoHe_JXGZ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoHe_JXGZ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    绩效工资
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>

    <script language="javascript" type="text/javascript">
        $(function() {
            $("#Checkbox2").click(function() {
                if ($("#Checkbox2").attr("checked")) {
                    $("#tab input[type=checkbox]").attr("checked", "true");
                }
                else {
                    $("#tab input[type=checkbox]").removeAttr("checked");
                }
            });
        })//jquery的写法，先声明一个函数，然后捕捉触发事件的对象，触发该对象时执行的事件（函数），遍历某些特定的控件，判断对象是否触发，执行事件；
    </script>

    <div class="box-wrapper">
        <div class="box-outer">
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 25%;">
                            <strong>时间：</strong>
                            <asp:DropDownList ID="dplYear" runat="server">
                            </asp:DropDownList>
                            &nbsp;年&nbsp;
                            <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplMoth_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;月&nbsp;
                        </td>
                        <td align="right" width="100px">
                            部门：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedIndexChanged="dplMoth_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 58%;">
                            <strong>按姓名查询：</strong><asp:TextBox ID="txtname" ForeColor="Gray" runat="server"
                                onfocus="DefaultTextOnFocus(this);" onblur="DefaultTextOnBlur(this);" Width="120px"></asp:TextBox>
                            <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click"></asp:Button>
                            &nbsp;
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                            &nbsp; 月度公司绩效工资基数：<asp:Label ID="lblJS" runat="server" Width="60px"></asp:Label>
                            <input type="hidden" id="hidJS" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            全选/取消<input id="Checkbox2" type="checkbox" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCreat" runat="server" Text="生成该月绩效工资" OnClick="btnCreat_Click"
                                OnClientClick="return confirm('您确定要生成当月数据吗？如果确定将清楚当月数据并重新生成！')" />
                        </td>
                          <td>
                            <asp:Button ID="btnAudit" runat="server" Text="提交部门审批" OnClick="btnAudit_Click"
                                />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptProNumCost" runat="server">
                        <HeaderTemplate>
                            <tr align="center" style="background-color: #B9D3EE;">
                                <td>
                                    序号
                                </td>
                                <td>
                                    年
                                </td>
                                <td>
                                    月
                                </td>
                                <td>
                                    部门
                                </td>
                                <td>
                                    岗位
                                </td>
                                <td>
                                    工号
                                </td>
                                <td>
                                    岗位序列
                                </td>
                                <td>
                                    姓名
                                </td>
                                <td>
                                    部门月度绩效成绩
                                </td>
                                <td>
                                    岗位系数
                                </td>
                                <td>
                                    人员月度绩效成绩
                                </td>
                                <td>
                                    月度绩效工资
                                </td>
                                <td>
                                    备注
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);">
                                <td>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id")%>' Visible="false"></asp:Label>
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                        Onclick="checkme(this)" />
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td id="Td1" runat="server" align="center">
                                    <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                </td>
                                <td id="Td2" runat="server" align="center">
                                    <asp:Label ID="lblMonth" runat="server" Text='<%#Eval("Month")%>'></asp:Label>
                                </td>
                                <td id="Td3" runat="server" align="center">
                                    <asp:Label ID="lblPart" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                </td>
                                <td id="Td4" runat="server" align="center">
                                    <asp:Label ID="lblPosition" runat="server" Text='<%#Eval("POSITION_NAME")%>'></asp:Label>
                                </td>
                                <td id="Td12" runat="server" align="center">
                                     <asp:Label ID="lblWorkNum" runat="server" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                                </td>
                                <td id="Td5" runat="server" align="center">
                                   <asp:Label ID="lblSquence" runat="server" Text='<%#Eval("ST_SEQUEN")%>'></asp:Label>
                                </td>
                                <td id="Td6" runat="server" align="center">
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                </td>
                                <td id="Td7" runat="server" align="center">
                                    <asp:Label ID="DepScore" runat="server" Text='<%#Eval("Score")%>'></asp:Label>
                                </td>
                                <td id="Td8" runat="server" align="center">
                                    <asp:Label ID="lblGWXS" runat="server" Text='<%#Eval("ST_GANGWEIXISHU")%>'></asp:Label>
                                </td>
                                <td id="Td9" align="center" runat="server">
                                    <asp:Label ID="lblScoreZong" runat="server" Text='<%#Eval("Kh_ScoreTotal")%>' name="lblScoreZong"></asp:Label>
                                </td>
                                <td id="Td10" runat="server" align="center">
                                    <asp:TextBox ID="txtMoney" runat="server" align="center" Width="100px" BorderStyle="None"
                                        BackColor="Transparent" Text='<%#Eval("Money")%>' onfocus="javascript:setToolTipGet(this);"
                                        onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                </td>
                                </td>
                                <td id="Td11" runat="server" align="center">
                                    <asp:TextBox ID="txtNote" runat="server" align="center" Width="200px" BorderStyle="None"
                                        BackColor="Transparent" Text='<%#Eval("Note")%>' onfocus="javascript:setToolTipGet(this);"
                                        onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr runat="server" id="tr_foot">
                        <td runat="server" id="foot" colspan="3">
                            <strong>合计：</strong>
                        </td>
                        <td colspan="6">
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblXiShuTotal"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblGZTotal"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblTip" runat="server" Text="提示:" Visible="false" Font-Bold="true"></asp:Label>
                <asp:Image ID="imgloading" runat="server" Visible="false" />
                <asp:Label ID="lblTip2" runat="server" Visible="false" Text="数据处理中，请稍后..."></asp:Label>
                <asp:Label ID="LabelDate" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
