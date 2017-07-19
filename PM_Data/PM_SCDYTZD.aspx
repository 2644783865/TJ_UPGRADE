<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="PM_SCDYTZD.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_SCDYTZD" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    生产代用通知单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tab
        {
            width: 80%;
            border-right: solid 1px #E5E5E5;
            border-bottom: solid 1px #E5E5E5;
        }
        .tab tr
        {
        }
        .tab tr td
        {
            border-left: solid 1px #E5E5E5;
            border-top: solid 1px #E5E5E5;
            text-align: center;
            font-size: larger;
        }
        .tab tr td input
        {
            font-size: medium;
        }
        .tab tr td input[type="text"]
        {
            width: 90%;
            height: 25px;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        function btnSubmit_onclick() {
            if (window.document.readyState != null && window.document.readyState != 'complete') {
                alert("正在提交数据，请不要重复提交");
                return false;
            }
            else {
                return true;
            }
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a id="btnSubmit" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                    onclick="return btnSubmit_onclick()" onserverclick="btnSubmit_onserverclick">提交</a>&nbsp;&nbsp;&nbsp;
                <a id="btnBack" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-back'"
                    onserverclick="btnBack_onserverclick">返回</a>
            </div>
        </div>
    </div>
    <asp:TabContainer runat="server" ID="TabContainer" Width="100%" BackColor="#F0F8FF">
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="表单信息">
            <ContentTemplate>
                <div style="width: 100%; background-color: #F0F8FF" align="center" id="div1">
                    <table width="70%">
                        <tr>
                            <td align="center">
                                <asp:Image runat="server" ID="Image0" ImageUrl="~/Assets/images/OM_ZCTOP.jpg" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: x-large;">
                                <strong>生&nbsp;&nbsp;产&nbsp;&nbsp;代&nbsp;&nbsp;用&nbsp;&nbsp;通&nbsp;&nbsp;知&nbsp;&nbsp;单</strong>
                                <asp:HiddenField runat="server" ID="hidTZD_SJID" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel runat="server" ID="panJBXX">
                        <table class="tab">
                            <tr>
                                <td style="width: 5%" align="right">
                                    任务号:
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="lbTZD_RWH" runat="server" Width="100%" />
                                </td>
                                <td style="width: 5%" align="right">
                                    合同名称:
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="lbTZD_HTMC" runat="server" Width="100%" />
                                </td>
                                <td style="width: 5%" align="right">
                                    项目名称:
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="lbTZD_XMMC" runat="server" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%" align="right">
                                    设备名称:
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="lbTZD_SBMC" runat="server" Width="100%" />
                                </td>
                                <td style="width: 5%" align="right">
                                    批号:
                                </td>
                                <td style="width: 20%">
                                    <asp:Label ID="lbTZD_PH" runat="server" Width="100%" />
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 8%" align="right">
                                    编制人:
                                </td>
                                <td colspan="1">
                                    <asp:Label ID="lbTZD_BZR" runat="server" Width="100%" />
                                </td>
                                <td style="width: 8%" align="right">
                                    编制日期:
                                </td>
                                <td style="width: 14%">
                                    <asp:Label ID="lbTZD_BZRQ" runat="server" Width="100%" />
                                </td>
                                <td id="doc" style="width: 5%" align="right">
                                    <asp:Label ID="t_doc" Text="外协单号:" runat="server" Width="100%" Visible="false" />
                                    <asp:Label ID="lab_pjid" runat="server" Visible="false" />
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="txt_docnum" runat="server" Width="100%" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    总序包含：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZX1" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZX2" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZX3" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZX4" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZX5" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZX6" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZX7" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZX8" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZX9" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZX10" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZX11" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZX12" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_OnClick" Text="查找" />
                                </td>
                            </tr>
                        </table>
                        <div class="box-wrapper">
                            <div class="box-outer">
                                <div style="height: 475px; overflow: auto; width: 100%">
                                    <div class="cpbox xscroll">
                                        <table id="tab" class="nowrap cptable fullwidth" align="center">
                                            <asp:Repeater runat="server" ID="rptZZMX">
                                                <HeaderTemplate>
                                                    <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                        <th>
                                                            序号
                                                        </th>
                                                        <th>
                                                            图号
                                                        </th>
                                                        <th align="left">
                                                            总序
                                                        </th>
                                                        <th>
                                                            名称
                                                        </th>
                                                        <th>
                                                            规格
                                                        </th>
                                                        <th>
                                                            材质
                                                        </th>
                                                        <th>
                                                            单台数量
                                                        </th>
                                                        <th>
                                                            总数量
                                                        </th>
                                                        <th>
                                                            图纸单重（kg）
                                                        </th>
                                                        <th>
                                                            图纸总重（kg）
                                                        </th>
                                                        <th>
                                                            材料种类
                                                        </th>
                                                        <th>
                                                            单位
                                                        </th>
                                                        <th>
                                                            材料用量
                                                        </th>
                                                        <th>
                                                            材料总重
                                                        </th>
                                                        <th>
                                                            长度
                                                        </th>
                                                        <th>
                                                            宽度
                                                        </th>
                                                        <th>
                                                            下料备注
                                                        </th>
                                                        <th>
                                                            下料方式
                                                        </th>
                                                        <th>
                                                            工艺流程
                                                        </th>
                                                        <th>
                                                            库
                                                        </th>
                                                        <th>
                                                            备注
                                                        </th>
                                                        <th>
                                                            生产代用通知备注
                                                        </th>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                                        <td>
                                                            <asp:CheckBox runat="server" ID="cbxXuHao" />
                                                            <asp:Label ID="rownum" runat="server" Text='<%#Eval("ID_Num")%>'></asp:Label>
                                                            <asp:HiddenField runat="server" ID="MS_ID" Value='<%#Eval("MS_ID")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_TUHAO" Text='<%#Eval("MS_TUHAO")%>'></asp:Label>
                                                        </td>
                                                        <td style="text-align:left">
                                                            <asp:Label runat="server" ID="MS_ZONGXU" Text='<%#Eval("MS_ZONGXU")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_NAME" Text='<%#Eval("MS_NAME")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_GUIGE" Text='<%#Eval("MS_GUIGE")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_CAIZHI" Text='<%#Eval("MS_CAIZHI")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_UNUM" Text='<%#Eval("MS_UNUM")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_NUM" Text='<%#Eval("MS_NUM")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_TUWGHT" Text='<%#Eval("MS_TUWGHT")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_TUTOTALWGHT" Text='<%#Eval("MS_TUTOTALWGHT")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_MASHAPE" Text='<%#Eval("MS_MASHAPE")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_TECHUNIT" Text='<%#Eval("MS_TECHUNIT")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_YONGLIANG" Text='<%#Eval("MS_YONGLIANG")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_MATOTALWGHT" Text='<%#Eval("MS_MATOTALWGHT")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_LEN" Text='<%#Eval("MS_LEN")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_WIDTH" Text='<%#Eval("MS_WIDTH")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_NOTE" Text='<%#Eval("MS_NOTE")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_XIALIAO" Text='<%#Eval("MS_XIALIAO")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_PROCESS" Text='<%#Eval("MS_PROCESS")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="MS_KU" Text='<%#Eval("MS_KU")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <div style="width: 160px;">
                                                                <asp:Label runat="server" ID="MS_ALLBEIZHU" Text='<%#Eval("MS_ALLBEIZHU")%>'></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div style="width: 200px;">
                                                                <asp:TextBox runat="server" ID="MS_DYTZBZ" Width="90%" Height="100%" Text='<%#Eval("MS_DYTZBZ")%>'></asp:TextBox>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                    <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                                        没有记录!<br />
                                        <br />
                                    </asp:Panel>
                                </div>
                            </div>
                            <uc1:UCPaging ID="UCPaging1" runat="server" />
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="审批">
            <ContentTemplate>
                <div style="width: 100%; background-color: #F0F8FF" align="center" id="div2">
                    <asp:Panel runat="server" ID="panZDR">
                        <table class="tab">
                            <tr>
                                <td style="height: 30px" width="20%">
                                    制单人：
                                </td>
                                <td width="30%">
                                    <asp:TextBox runat="server" ID="txtTZD_ZDR" onfocus="this.blur()"></asp:TextBox>
                                </td>
                                <td width="20%">
                                    制单时间：
                                </td>
                                <td width="30%">
                                    <asp:Label runat="server" ID="lbTZD_ZDSJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    制单人建议：<asp:TextBox runat="server" ID="txtTZD_ZDJY" Text="" TextMode="MultiLine" Width="90%"
                                        Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panSPR1">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td colspan="6" align="center" style="height: 30px">
                                    <asp:Label runat="server" ID="lb1" Text="编制人审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" style="height: 30px;">
                                    审批人：
                                </td>
                                <td width="15%" align="left">
                                    <asp:TextBox runat="server" ID="txtTZD_SPR1" onfocus="this.blur()" Width="60%"></asp:TextBox>
                                    <asp:Image runat="server" ID="imgSHR1" ImageUrl="../Assets/images/username_bg.gif"
                                        onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
                                </td>
                                <td width="15%">
                                    审批结论：
                                </td>
                                <td width="25%" align="center">
                                    <asp:RadioButtonList runat="server" ID="rblTZD_SPR1_JL" RepeatDirection="Horizontal"
                                        Width="80%" RepeatColumns="2">
                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="15%">
                                    审批时间：
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="lbTZD_SPR1_SJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    审批建议：<asp:TextBox runat="server" ID="txtTZD_SPR1_JY" Text="" TextMode="MultiLine"
                                        Font-Size="Medium" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panSPR2">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td colspan="6" align="center" style="height: 30px;">
                                    <asp:Label runat="server" ID="lb3" Text="部长审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" style="height: 30px;">
                                    审批人：
                                </td>
                                <td width="15%" align="left">
                                    <asp:TextBox runat="server" ID="txtTZD_SPR2" onfocus="this.blur()" Width="60%"></asp:TextBox>
                                    <asp:Image runat="server" ID="Image2" ImageUrl="../Assets/images/username_bg.gif"
                                        onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" />
                                </td>
                                <td width="15%">
                                    审批结论：
                                </td>
                                <td width="25%" align="center">
                                    <asp:RadioButtonList runat="server" ID="rblTZD_SPR2_JL" RepeatDirection="Horizontal"
                                        Width="80%" RepeatColumns="2">
                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="15%">
                                    审批时间：
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="lbTZD_SPR2_SJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    审批建议：<asp:TextBox runat="server" ID="txtTZD_SPR2_JY" Text="" TextMode="MultiLine"
                                        Font-Size="Medium" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
