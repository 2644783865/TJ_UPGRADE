<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_NDPXJH_ZSP.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_NDPXJH_ZSP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    年度培训计划表
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
            text-align: center;
            font-size: larger;
        }
        .tab tr td input
        {
            font-size: medium;
        }
        .tab tr td input[type="text"]
        {
            height: 25px;
        }
        #rdlstSeq td
        {
            border: 0;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />

    <script src="../JS/OM.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#<%=panSPR1.ClientID%>").hide();
            $("#<%=panSPR2.ClientID%>").hide();
            $("#<%=panSPR3.ClientID%>").hide();
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=panSPR1.ClientID%>").show();
                    //                    if ($("#<%=txtSPR1.ClientID%>").val() == "") {
                    //                        $("#<%=txtSPR1.ClientID%>").val("周文轶");
                    //                        $("#<%=hidSPR1ID.ClientID%>").val("1");
                    //                    }
                }
                else if ($(this).val() == "2") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();
                }
                else if ($(this).val() == "3") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();
                    $("#<%=panSPR3.ClientID%>").show();
                }
            });
        })

        function rblSPJB_onchange() {
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").hide();
                    $("#<%=panSPR3.ClientID%>").hide();
                    //                    if ($("#<%=txtSPR1.ClientID%>").val() == "") {
                    //                        $("#<%=txtSPR1.ClientID%>").val("周文轶");
                    //                        $("#<%=hidSPR1ID.ClientID%>").val("1");
                    //                    }
                }
                else if ($(this).val() == "2") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();
                    $("#<%=panSPR3.ClientID%>").hide();
                }
                else if ($(this).val() == "3") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();
                    $("#<%=panSPR3.ClientID%>").show();
                }
            });
        }

        function xr1() {
            $("#hidPerson").val("person1");
            SelPersons();
        }

        function xr2() {
            $("#hidPerson").val("person2");
            SelPersons();
        }

        function xr3() {
            $("#hidPerson").val("person3");
            SelPersons();
        }

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=txtSPR1.ClientID %>").val(r.st_name);
                $("#<%=hidSPR1ID.ClientID%>").val(r.st_id);
            }
            if (id == "person2") {
                $("#<%=txtSPR2.ClientID %>").val(r.st_name);
                $("#<%=hidSPR2ID.ClientID%>").val(r.st_id);
            }
            if (id == "person3") {
                $("#<%=txtSPR3.ClientID %>").val(r.st_name);
                $("#<%=hidSPR3ID.ClientID%>").val(r.st_id);
            }
            $('#win').dialog('close');
        }

        function checknull(obj) {
            if (obj.value == "") {
                alert("此项不能为空！！！");
                $(obj).focus();
                return false;
            }
            else {
                return true;
            }
        }

        function btnSave_onclick() {
            if ($("#<%=txtPX_YEAR.ClientID %>").val() == "") {
                alert("请填写年份");
            }
        }

        function btnDelete_OnClientClick() {
            if (confirm("驳回后，该条数据将会重新进入审批流程，确实要驳回吗？")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a runat="server" id="btnSubmit" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                    onserverclick="btnSubmit_onserverclick">提交审批</a> <a runat="server" id="btnSave" href="#"
                        class="easyui-linkbutton" data-options="iconCls:'icon-add'" onserverclick="btnSave_onserverclick"
                        onclick="return btnSave_onclick();fzdctj();">保存</a> <a runat="server" id="btnBack"
                            href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'" onserverclick="btnBack_onserverclick">
                            返回</a>
                <asp:HiddenField runat="server" ID="hidJH_SJID" />
            </div>
        </div>
    </div>
    <asp:TabContainer runat="server" ID="TabContainer1">
        <asp:TabPanel runat="server" ID="TabPanel0" HeaderText="培训计划">
            <ContentTemplate>
                <div style="width: 100%; background-color: #F0F8FF" align="center" id="div1">
                    <asp:Panel runat="server" ID="panJBXX">
                        <table width="70%">
                            <tr>
                                <td align="center">
                                    <asp:Image runat="server" ID="Image0" ImageUrl="~/Assets/images/OM_ZCTOP.jpg" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="font-size: x-large;">
                                    天津中材重机
                                    <asp:TextBox runat="server" ID="txtPX_YEAR" onblur="nfyz(this)" Width="60px" Height="25px"
                                        Font-Size="Medium"></asp:TextBox><span style="color: Red">*</span> 年度培训计划申报表
                                </td>
                            </tr>
                        </table>
                        <table class="tab">
                            <tr>
                                <td colspan="2" height="25px">
                                    单位/部门：中材（天津）重型机械有限公司
                                </td>
                                <td colspan="2">
                                    联系电话：86890105
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="cbxQX" Text="全选" TextAlign="Right" AutoPostBack="true"
                                        OnCheckedChanged="cbxQX_OnCheckedChanged" />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnAdd" Text="增加行" BackColor="LightGreen"  OnClick="btnAdd_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnDelete" Text="驳回" OnClientClick="return btnDelete_OnClientClick()" OnClick="btnDelete_OnClick" />
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <div style="overflow: auto; width: 100%">
                                    <table id="gr" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                        border="1">
                                        <asp:Repeater runat="server" ID="rptPXJH" OnItemDataBound="rptPXJH_OnItemDataBound">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                    <td>
                                                        <strong>序号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>申报部门</strong>
                                                    </td>
                                                    <td>
                                                        <strong>培训方式</strong>
                                                    </td>
                                                    <td>
                                                        <strong>项目名称</strong>
                                                    </td>
                                                    <td>
                                                        <strong>培训时间</strong>
                                                    </td>
                                                    <td>
                                                        <strong>培训地点</strong>
                                                    </td>
                                                    <td>
                                                        <strong>主讲人</strong>
                                                    </td>
                                                    <td>
                                                        <strong>培训对象</strong>
                                                    </td>
                                                    <td>
                                                        <strong>培训人数</strong>
                                                    </td>
                                                    <td>
                                                        <strong>学时</strong>
                                                    </td>
                                                    <td>
                                                        <strong>培训费预算</strong>
                                                    </td>
                                                    <td>
                                                        <strong>备注</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                                    <td>
                                                        <asp:CheckBox runat="server" ID="cbxXuHao" Text='<%# Convert.ToInt32(Container.ItemIndex +1) %>'
                                                            TextAlign="Right" CssClass="checkBoxCss" />
                                                        <asp:HiddenField runat="server" ID="PX_ID" Value='<%#Eval("PX_ID")%>' />
                                                        <asp:HiddenField runat="server" ID="PX_SJID" Value='<%#Eval("PX_SJID")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="PX_BM" Width="80px" onfocus="this.blur()" Text='<%#Eval("PX_BM")%>'></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="PX_FS">
                                                            <asp:ListItem Value="n" Text="内部培训"></asp:ListItem>
                                                            <asp:ListItem Value="w" Text="外部培训"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="PX_XMMC" onblur="checknull(this)" Text='<%#Eval("PX_XMMC")%>'></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="PX_SJ">
                                                            <asp:ListItem Text="第一季度" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="第二季度" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="第三季度" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="第四季度" Value="4"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="PX_DD" onblur="checknull(this)" Text='<%#Eval("PX_DD")%>'></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="PX_ZJR" onblur="checknull(this)" Width="60px" Text='<%#Eval("PX_ZJR")%>'>
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="PX_DX" Text='<%#Eval("PX_DX")%>'></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="PX_RS" Width="50px" onblur="checknull(this);szgsyz(this)"
                                                            Text='<%#Eval("PX_RS")%>'></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="PX_XS" Width="40px" onblur="checknull(this);zsxsyz(this)"
                                                            Text='<%#Eval("PX_XS")%>'></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="PX_FYYS" Width="50px" onblur="zsxsyz(this)" Text='<%#Eval("PX_FYYS")%>'></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="PX_BZ" Text='<%#Eval("PX_BZ")%>'></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                    没有记录!</asp:Panel>
                                <br />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cbxQX" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <table width="70%">
                            <tr>
                                <td>
                                    备注：<br />
                                    1.2015年度要求管理技术岗位员工“基础管理类”最低2学时，“岗位技能提升类”培训最低2学时；<br />
                                    2.2015年度要求一线岗位员工“岗位技能类”培训，要求一线岗位员工最低2学时。
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="审批信息">
            <ContentTemplate>
                <div style="width: 100%; background-color: #F0F8FF" align="center" id="div2">
                    <asp:Panel runat="server" ID="panZDR">
                        <table class="tab">
                            <tr>
                                <td colspan="4">
                                    <span style="color: Red">注： 默认一级审批---总经理审批，请正确选择审批人。</span>
                                </td>
                            </tr>
                            <tr style="background-color: #79CDCD">
                                <td style="text-align: right">
                                    审批类型：
                                </td>
                                <td id="tdSPLX" colspan="3">
                                    <asp:RadioButtonList runat="server" ID="rblSPJB" RepeatDirection="Horizontal" RepeatColumns="3"
                                        Width="320px" onchange="rblSPJB_onchange()" Style="margin: auto">
                                        <asp:ListItem Text="一级审批" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="二级审批" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="三级审批" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 30px" width="20%">
                                    制单人：
                                </td>
                                <td width="30%">
                                    <asp:TextBox runat="server" ID="txtZDR" onfocus="this.blur()"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hidZDRID" />
                                </td>
                                <td width="20%">
                                    制单时间：
                                </td>
                                <td width="30%">
                                    <asp:Label runat="server" ID="lbZDR_SJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    制单人建议：<asp:TextBox runat="server" ID="txtZDR_JY" Text="" TextMode="MultiLine" Width="90%"
                                        Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panSPR1">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td colspan="6" align="center" style="height: 30px">
                                    <asp:Label runat="server" ID="lb1" Text="第一级审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" style="height: 30px;">
                                    审批人：
                                </td>
                                <td width="15%" align="left">
                                    <asp:TextBox runat="server" ID="txtSPR1" onfocus="this.blur()" Width="60%"></asp:TextBox>
                                    <input type="hidden" runat="server" id="hidSPR1ID" />
                                    <asp:Image runat="server" ID="imgSHR1" ImageUrl="../Assets/images/username_bg.gif"
                                        onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
                                </td>
                                <td width="15%">
                                    审批结论：
                                </td>
                                <td width="25%" align="center">
                                    <asp:RadioButtonList runat="server" ID="rblSPR1_JL" RepeatDirection="Horizontal"
                                        Style="margin: auto" Width="80%" RepeatColumns="2">
                                        <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="15%">
                                    审批时间：
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="lbSPR1_SJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    审批建议：<asp:TextBox runat="server" ID="txtSPR1_JY" Text="" TextMode="MultiLine" Font-Size="Medium"
                                        Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panSPR2">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td colspan="6" align="center" style="height: 30px;">
                                    <asp:Label runat="server" ID="lb3" Text="第二级审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" style="height: 30px;">
                                    审批人：
                                </td>
                                <td width="15%" align="left">
                                    <asp:TextBox runat="server" ID="txtSPR2" onfocus="this.blur()" Width="60%"></asp:TextBox>
                                    <input type="hidden" runat="server" id="hidSPR2ID" />
                                    <asp:Image runat="server" ID="Image2" ImageUrl="../Assets/images/username_bg.gif"
                                        onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" />
                                </td>
                                <td width="15%">
                                    审批结论：
                                </td>
                                <td width="25%" align="center">
                                    <asp:RadioButtonList runat="server" ID="rblSPR2_JL" RepeatDirection="Horizontal"
                                        Style="margin: auto" Width="80%" RepeatColumns="2">
                                        <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="15%">
                                    审批时间：
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="lbSPR2_SJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    审批建议：<asp:TextBox runat="server" ID="txtSPR2_JY" Text="" TextMode="MultiLine" Font-Size="Medium"
                                        Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panSPR3">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td colspan="6" align="center" style="height: 30px;">
                                    <asp:Label runat="server" ID="Label1" Text="第三级审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" style="height: 30px;">
                                    审批人：
                                </td>
                                <td width="15%" align="left">
                                    <asp:TextBox runat="server" ID="txtSPR3" onfocus="this.blur()" Width="60%"></asp:TextBox>
                                    <input type="hidden" runat="server" id="hidSPR3ID" />
                                    <asp:Image runat="server" ID="Image1" ImageUrl="../Assets/images/username_bg.gif"
                                        onclick="xr3()" align="middle" Style="cursor: pointer" title="选择" />
                                </td>
                                <td width="15%">
                                    审批结论：
                                </td>
                                <td width="25%" align="center">
                                    <asp:RadioButtonList runat="server" ID="rblSPR3_JL" RepeatDirection="Horizontal"
                                        Style="margin: auto" Width="80%" RepeatColumns="2">
                                        <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="15%">
                                    审批时间：
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="lbSPR3_SJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    审批建议：<asp:TextBox runat="server" ID="txtSPR3_JY" Text="" TextMode="MultiLine" Font-Size="Medium"
                                        Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
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
                        <input id="dep" name="dept" value="07">
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
            保存</a> &nbsp;&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;<a class="easyui-linkbutton"
                    data-options="iconCls:'icon-ok',plain:true" onclick="xiuGai();">修改</a>
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>
