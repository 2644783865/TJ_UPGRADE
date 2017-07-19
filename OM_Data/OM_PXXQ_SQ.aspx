<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_PXXQ_SQ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_PXXQ_SQ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    员工培训需求调查
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tab
        {
            width: 70%;
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
            font-size: larger;
        }
        .tab tr td input
        {
            font-size: medium;
        }
        .tab tr td input[type="text"]
        {
        }
        #rdlstSeq td
        {
            border: 0;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />

    <script src="../JS/OM.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a runat="server" id="btnSave" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                    onserverclick="btnSave_onserverclick" onclick="return btnSave_onclick();return fzdctj();">
                    保存</a> <a runat="server" id="btnBack" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                        onserverclick="btnBack_onserverclick">返回</a>
                <asp:HiddenField runat="server" ID="hidJH_SJID" />
            </div>
        </div>
    </div>
    <div style="width: 100%; background-color: #F0F8FF" align="center" id="div1">
        <table width="70%">
            <tr>
                <td align="center">
                    <asp:Image runat="server" ID="Image0" ImageUrl="~/Assets/images/OM_ZCTOP.jpg" />
                </td>
            </tr>
            <tr>
                <td align="center" style="font-size: x-large;">
                    员工培训需求调查表
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="panJBXX">
            <table class="tab">
                <tr>
                    <td>
                        填写人:&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lbDC_TXR"></asp:Label>
                        <asp:HiddenField runat="server" ID="hidTXRID" />
                    </td>
                    <td>
                        部门:&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lbDC_TXRBM"></asp:Label>
                    </td>
                    <td>
                        填写时间:&nbsp;&nbsp;<asp:Label runat="server" ID="lbDC_TXSJ"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="tab">
                <tr>
                    <td colspan="2" style="font-size: large">
                        <strong>一、调查问卷：</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>1、您认为您目前最希望通过<br />
                            培训解决哪方面的问题</strong>
                    </td>
                    <td align="center">
                        <asp:CheckBoxList runat="server" ID="cbxDC_FM" Width="90%" RepeatDirection="Horizontal"
                            RepeatColumns="2" BorderStyle="None" CellSpacing="10">
                            <asp:ListItem Text="A.增加专业知识 " Value="1"></asp:ListItem>
                            <asp:ListItem Text="B.提高综合素质" Value="2"></asp:ListItem>
                            <asp:ListItem Text="C.提升工作技能  " Value="3"></asp:ListItem>
                        </asp:CheckBoxList>
                        <br />
                        其他：
                        <asp:TextBox runat="server" ID="txtDC_FMQT"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>2、您最合适的培训时间</strong>
                    </td>
                    <td align="center">
                        <asp:CheckBoxList runat="server" ID="cbxDC_HSSJ" RepeatDirection="Horizontal" RepeatColumns="2"
                            BorderStyle="None" Width="80%" CellSpacing="10" BorderWidth="0">
                            <asp:ListItem Text="A.正常上班时间  " Value="1"></asp:ListItem>
                            <asp:ListItem Text="B.午间" Value="2"></asp:ListItem>
                            <asp:ListItem Text="C.下班后" Value="3"></asp:ListItem>
                            <asp:ListItem Text="D.周末" Value="4"></asp:ListItem>
                        </asp:CheckBoxList>
                        <br />
                        其他：
                        <asp:TextBox runat="server" ID="txtDC_HSSJQT"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>3、您最希望接受的培训类别</strong>
                    </td>
                    <td align="center">
                        <asp:CheckBoxList runat="server" ID="cbxDC_LB" Width="90%" RepeatDirection="Horizontal"
                            RepeatColumns="2" BorderStyle="None" CellSpacing="10">
                            <asp:ListItem Text="A.通用管理类（个人团队管理、执行力提升)" Value="1"></asp:ListItem>
                            <asp:ListItem Text="B.专业管理类" Value="2"></asp:ListItem>
                            <asp:ListItem Text="C.专业技术类" Value="3"></asp:ListItem>
                            <asp:ListItem Text="D.学历教育提升类 " Value="4"></asp:ListItem>
                        </asp:CheckBoxList>
                        <br />
                        其他：
                        <asp:TextBox runat="server" ID="txtDC_LBQT"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>4、您认为最为理想的培训评<br />
                            估方式是</strong>
                    </td>
                    <td align="center">
                        <asp:CheckBoxList runat="server" ID="cbxDC_FS" Width="90%" RepeatDirection="Horizontal"
                            RepeatColumns="2" BorderStyle="None" CellSpacing="10">
                            <asp:ListItem Text="A.培训组织者与受训者面谈" Value="1"></asp:ListItem>
                            <asp:ListItem Text="B.培训效果问卷调查" Value="2"></asp:ListItem>
                            <asp:ListItem Text="C.写培训心得 " Value="3"></asp:ListItem>
                            <asp:ListItem Text="D.培训考核" Value="4"></asp:ListItem>
                            <asp:ListItem Text="E.受训者直属领导或同事评价 " Value="5"></asp:ListItem>
                            <asp:ListItem Text="F.绩效考核" Value="6"></asp:ListItem>
                        </asp:CheckBoxList>
                        <br />
                        其他：
                        <asp:TextBox runat="server" ID="txtDC_FSQT"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>5、您最喜欢哪种培训形式</strong>
                    </td>
                    <td align="center">
                        <asp:CheckBoxList runat="server" ID="cbxDC_XS" Width="90%" RepeatDirection="Horizontal"
                            RepeatColumns="2" BorderStyle="None" CellSpacing="10">
                            <asp:ListItem Text="A.内部讲师主题内训" Value="1"></asp:ListItem>
                            <asp:ListItem Text="B.外聘讲师主题内训" Value="2"></asp:ListItem>
                            <asp:ListItem Text="C.外派公开课" Value="3"></asp:ListItem>
                            <asp:ListItem Text="D.专家咨询式培训" Value="4"></asp:ListItem>
                            <asp:ListItem Text="E.网络学习" Value="5"></asp:ListItem>
                        </asp:CheckBoxList>
                        <br />
                        其他：
                        <asp:TextBox runat="server" ID="txtDC_XSQT"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>6、根据实际情况，您认为您适合开展哪些培训项目</strong>
                    </td>
                    <td align="center">
                        <asp:CheckBoxList runat="server" ID="cbxDC_XM" Width="90%" RepeatDirection="Horizontal"
                            RepeatColumns="2" BorderStyle="None" CellSpacing="10">
                            <asp:ListItem Text="A.商务礼仪" Value="1"></asp:ListItem>
                            <asp:ListItem Text="B.时间管理" Value="2"></asp:ListItem>
                            <asp:ListItem Text="C.绩效管理" Value="3"></asp:ListItem>
                            <asp:ListItem Text="D.团队建设管理" Value="4"></asp:ListItem>
                            <asp:ListItem Text="E.自我发展管理" Value="5"></asp:ListItem>
                            <asp:ListItem Text="F.费用预算管理" Value="6"></asp:ListItem>
                            <asp:ListItem Text="G.公司制度培训" Value="7"></asp:ListItem>
                            <asp:ListItem Text="H.业务知识培训" Value="8"></asp:ListItem>
                            <asp:ListItem Text="I.业务技能培训" Value="9"></asp:ListItem>
                        </asp:CheckBoxList>
                        <br />
                        其他：
                        <asp:TextBox runat="server" ID="txtDC_XMQT"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <table width="70%">
                        <tr>
                            <td style="font-size: large">
                                <strong>二、具体培训课题</strong>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnAdd" runat="server" Text="增 加" OnClick="btnAdd_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_OnClick" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table id="gr" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <asp:Repeater ID="rptNR" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle headcolor">
                                                <th width="50px">
                                                    <strong>序号</strong>
                                                </th>
                                                <th>
                                                    <strong>培训项目</strong>
                                                </th>
                                                <th>
                                                    <strong>培训时间</strong>
                                                </th>
                                                <th>
                                                    <strong>培训教师</strong>
                                                </th>
                                                <th>
                                                    <strong>培训目的</strong>
                                                </th>
                                                <th>
                                                    <strong>培训方式（内训、外训）</strong>
                                                </th>
                                                <th>
                                                    <strong>备注</strong>
                                                </th>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <asp:CheckBox ID="chk" CssClass="checkBoxCss" Text='<%# Convert.ToInt32(Container.ItemIndex +1) %>'
                                                        BorderStyle="None" TextAlign="Right" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="PXXM" runat="server" Text='<%# Eval("PXXM")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="PXSJ" CssClass="zz" runat="server" Text='<%# Eval("PXSJ")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="PXJS" runat="server" Text='<%# Eval("PXJS")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="PXMD" runat="server" Text='<%#Eval("PXMD")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="PXFS" runat="server" Text='<%#Eval("PXFS")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="BZ" runat="server" Text='<%#Eval("BZ")%>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                    没有记录!</asp:Panel>
                                <br />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="click" />
                    <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
