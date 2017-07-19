﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_TZTHTZD.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_TZTHTZD" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    图纸替换通知单
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

        $(function() {
            $("#<%=panSPR1.ClientID%>").hide();
            $("#<%=panSPR2.ClientID%>").hide();
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").hide();
                    if ($("#<%=txtTZD_SPR1.ClientID%>").val() == "") {
                        $("#<%=txtTZD_SPR1.ClientID%>").val("李利恒");
                    }
                }
                if ($(this).val() == "2") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();
                    if ($("#<%=txtTZD_SPR1.ClientID%>").val() == "") {
                        $("#<%=txtTZD_SPR1.ClientID%>").val("李玲");
                    }
                    if ($("#<%=txtTZD_SPR2.ClientID%>").val() == "") {
                        $("#<%=txtTZD_SPR2.ClientID%>").val("李利恒");
                    }
                }
            });
        })

        function rblTZD_SPLX_onchange() {
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").hide();
                    $("#<%=txtTZD_SPR1.ClientID%>").val("李利恒");
                }
                if ($(this).val() == "2") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();
                    $("#<%=txtTZD_SPR1.ClientID%>").val("李玲");
                    $("#<%=txtTZD_SPR2.ClientID%>").val("李利恒");
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

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=txtTZD_SPR1.ClientID %>").val(r.st_name);
            }
            if (id == "person2") {
                $("#<%=txtTZD_SPR2.ClientID %>").val(r.st_name);
            }
            $('#win').dialog('close');
        }

        function btnSubmit_onclick() {
            if (window.document.readyState != null && window.document.readyState != 'complete') {
                alert("正在提交数据，请不要重复提交");
                return false;
            }
            else {
                return true;
            }
        }

        function btnRefuse_onclick() {
            if (confirm("确实要驳回吗？")) {
                return true;
            }
            else {
                return false;
            }
        }

        function btnDelete_onclick() {
            if (confirm("确实要删除吗？")) {
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
                <a id="btnRefuse" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'"
                    onclick="return btnRefuse_onclick()" onserverclick="btnRefuse_onserverclick">驳回</a>&nbsp;&nbsp;&nbsp;
                <a id="btnDelete" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'"
                    onclick="return btnDelete_onclick()" onserverclick="btnDelete_onserverclick">删除</a>&nbsp;&nbsp;&nbsp;
                <a id="btnSubmit" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                    onclick="return btnSubmit_onclick()" onserverclick="btnSubmit_onserverclick">提交</a>&nbsp;&nbsp;&nbsp;
                <a id="btnBack" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-back'"
                    onserverclick="btnBack_onserverclick">返回</a>
            </div>
        </div>
    </div>
    <asp:TabContainer runat="server" ID="TabContainer" Width="100%" BackColor="#F0F8FF">
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="表单基本信息">
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
                                <strong>图&nbsp;&nbsp;纸&nbsp;&nbsp;替&nbsp;&nbsp;换&nbsp;&nbsp;通&nbsp;&nbsp;知&nbsp;&nbsp;单</strong>
                                <asp:HiddenField runat="server" ID="hidTZD_SJID" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel runat="server" ID="panJBXX">
                        <table width="70%">
                            <tr>
                                <td colspan="2" align="right" style="font-size: larger">
                                    文件号：TJZJ-R-M-03
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="font-size: larger" width="25%">
                                    编号：<asp:TextBox runat="server" ID="txtTZD_BH" Text="" Font-Size="Medium" Width="70%"
                                        Height="25px" onblur="checknull(this)"></asp:TextBox>
                                </td>
                                <td align="right" style="font-size: larger">
                                    版本：1
                                </td>
                            </tr>
                        </table>
                        <table class="tab">
                            <tr>
                                <td width="15%">
                                    顾客名称
                                </td>
                                <td width="30%">
                                    <asp:TextBox runat="server" ID="txtTZD_GKMC"></asp:TextBox>
                                </td>
                                <td width="15%">
                                </td>
                                <td width="40%">
                                </td>
                            </tr>
                        </table>
                        <div style="width: 70%">
                            <div align="right">
                                <asp:Button ID="btnAdd" runat="server" Text="增 加" OnClick="btnAdd_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="删除" OnClick="btnCancel_OnClick" />
                            </div>
                            <table id="gr" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <asp:Repeater ID="rptNR" runat="server">
                                    <HeaderTemplate>
                                        <tr align="center" class="tableTitle headcolor">
                                            <th width="50px">
                                                <strong>序号</strong>
                                            </th>
                                            <th>
                                                <strong>图号</strong>
                                            </th>
                                            <th>
                                                <strong>图名</strong>
                                            </th>
                                            <th>
                                                <strong>备注</strong>
                                            </th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                            <td>
                                                <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                </asp:CheckBox>
                                                <asp:Label runat="server" ID="NR_ID" Text='<%#Eval("NR_ID")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="NR_TH" Text='<%#Eval("NR_TH")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="NR_TM" Text='<%#Eval("NR_TM")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="NR_BZ" Text='<%#Eval("NR_BZ")%>'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                没有记录!</asp:Panel>
                            <br />
                        </div>
                        <table class="tab">
                            <tr>
                                <td width="10%">
                                    备注：
                                </td>
                                <td width="90%">
                                    <asp:TextBox runat="server" ID="txtTZD_NR" TextMode="MultiLine" Rows="8" Font-Size="Medium"
                                        Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    超送至：
                                </td>
                                <td>
                                    <asp:CheckBoxList runat="server" ID="cbxlTZD_CSBM" RepeatDirection="Horizontal" Width="500px">
                                        <asp:ListItem Text="技术部" Value="03" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="生产部" Value="04"></asp:ListItem>
                                        <asp:ListItem Text="采购部" Value="05"></asp:ListItem>
                                        <asp:ListItem Text="财务部" Value="06"></asp:ListItem>
                                        <asp:ListItem Text="质量部" Value="12"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                     <table width="80%">
                        <tr>
                            <td width="20%">
                            </td>
                            <td width="20%">
                            </td>
                            <td width="60%">
                                <input type="hidden" runat="server" id="Hidden2" />
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
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="审批">
            <ContentTemplate>
                <div style="width: 100%; background-color: #F0F8FF" align="center" id="div2">
                    <asp:Panel runat="server" ID="panZDR">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td colspan="2">
                                </td>
                                <td style="text-align: right">
                                    审批类型：
                                </td>
                                <td id="tdSPLX">
                                    <asp:RadioButtonList runat="server" ID="rblTZD_SPLX" RepeatDirection="Horizontal"
                                        RepeatColumns="2" Width="320px" onchange="rblTZD_SPLX_onchange()">
                                        <asp:ListItem Text="部长审批" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="主管+部长审批" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
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
                                    <asp:Label runat="server" ID="lb1" Text="主管审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
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
