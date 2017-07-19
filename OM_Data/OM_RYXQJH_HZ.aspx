<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_RYXQJH_HZ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_RYXQJH_HZ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    人员需求计划
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
            width: 400px !important;
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
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />
    <link href="SM_JS/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/rowcolor.js" type="text/javascript"></script>

    <script src="SM_JS/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#<%=panSPR1.ClientID%>").hide();
            $("#<%=panSPR2.ClientID%>").hide();
            $("#<%=panSPR3.ClientID%>").hide();
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=panSPR1.ClientID%>").show();
                }
                else if ($(this).val() == "2") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();
                    if ($("#<%=txtSPR1.ClientID%>").val() == "") {
                        $("#<%=txtSPR1.ClientID%>").val("蔡伟疆");
                    }
                    if ($("#<%=txtSPR2.ClientID%>").val() == "") {
                        $("#<%=txtSPR2.ClientID%>").val("王自清");
                    }
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
                }
                else if ($(this).val() == "2") {
                    $("#<%=panSPR1.ClientID%>").show();
                    $("#<%=panSPR2.ClientID%>").show();
                    $("#<%=panSPR3.ClientID%>").hide();
                    if ($("#<%=txtSPR1.ClientID%>").val() == "") {
                        $("#<%=txtSPR1.ClientID%>").val("蔡伟疆");
                    }
                    if ($("#<%=txtSPR2.ClientID%>").val() == "") {
                        $("#<%=txtSPR2.ClientID%>").val("王自清");
                    }
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
            }
            if (id == "person2") {
                $("#<%=txtSPR2.ClientID %>").val(r.st_name);
            }
            if (id == "person3") {
                $("#<%=txtSPR3.ClientID %>").val(r.st_name);
            }
            $('#win').dialog('close');
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a runat="server" id="btnSubmit" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                    onserverclick="btnSubmit_onserverclick">提交审批</a> <a runat="server" id="btnSave" href="#"
                        class="easyui-linkbutton" data-options="iconCls:'icon-add'" onserverclick="btnSave_onserverclick">
                        保存</a><a runat="server" id="btnBack" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                            onserverclick="btnBack_onserverclick"> 返回</a>
            </div>
        </div>
    </div>
    <asp:TabContainer runat="server" ID="TabContainer">
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="基本信息">
            <ContentTemplate>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title" align="left">
                            <a runat="server" id="btnDelete" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                                onserverclick="btnDelete_onserverclick">驳回</a>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div style="height: 475px; overflow: auto; width: 100%">
                            <div class="cpbox xscroll">
                                <table id="tab1" class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater runat="server" ID="rptZPJH" OnItemDataBound="rptZPJH_OnItemDataBound">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                <td rowspan="2">
                                                    <strong>序号</strong>
                                                </td>
                                                <td rowspan="2">
                                                    <strong>部门</strong>
                                                </td>
                                                <td colspan="4">
                                                    <strong>需求计划</strong>
                                                </td>
                                                <td>
                                                    <strong>岗位描述</strong>
                                                </td>
                                                <td colspan="7">
                                                    <strong>任职要求</strong>
                                                </td>
                                                <td rowspan="2">
                                                    <strong>希望到岗时间</strong>
                                                </td>
                                                <td rowspan="2">
                                                    <strong>拟工作地点</strong>
                                                </td>
                                                <td rowspan="2">
                                                    <strong>其他</strong>
                                                </td>
                                            </tr>
                                            <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                <td>
                                                    <strong>岗位名称</strong>
                                                </td>
                                                <td>
                                                    <strong>需求来源<br />
                                                        （校园/招聘）</strong>
                                                </td>
                                                <td>
                                                    <strong>招聘方式</strong>
                                                </td>
                                                <td>
                                                    <strong>需求人数</strong>
                                                </td>
                                                <td>
                                                    <strong>岗位名称及工作职责</strong>
                                                </td>
                                                <td>
                                                    <strong>所学专业</strong>
                                                </td>
                                                <td>
                                                    <strong>毕业院校要求</strong>
                                                </td>
                                                <td>
                                                    <strong>学历</strong>
                                                </td>
                                                <td>
                                                    <strong>性别</strong>
                                                </td>
                                                <td>
                                                    <strong>年龄</strong>
                                                </td>
                                                <td>
                                                    <strong>资格能力要求<br />
                                                        （经验、资历、工作技能等）</strong>
                                                </td>
                                                <td>
                                                    <strong>其他要求</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                                <td>
                                                    <asp:CheckBox runat="server" ID="cbxXuHao" CssClass="checkBoxCss" />
                                                    <asp:Label runat="server" ID="lbXuHao" Text='<%# Convert.ToInt32(Container.ItemIndex +1) %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="JH_ID" Value='<%#Eval("JH_ID")%>' />
                                                   <%-- <asp:HiddenField runat="server" ID="JH_GWMCID" Value='<%#Eval("JH_GWMCID")%>' />--%>
                                                </td>
                                                <td id="tdBM" runat="server">
                                                    <asp:Label runat="server" BorderStyle="None" Width="60px" ID="JH_ZPBM" Text='<%#Eval("JH_ZPBM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_GWMC" Text='<%#Eval("JH_GWMC")%>'></asp:Label>
                                                    <%--<asp:DropDownList runat="server" ID="ddlJH_GWMC" Height="98%">
                                                    </asp:DropDownList>--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_XQLY" Text='<%#Eval("JH_XQLY")%>'></asp:Label>
                                                    <%--<asp:RadioButtonList runat="server" ID="rblJH_XQLY" RepeatDirection="Horizontal"
                                                        BorderWidth="0" Style="margin: auto">
                                                        <asp:ListItem Text="校园" Value="校园"></asp:ListItem>
                                                        <asp:ListItem Text="社会" Value="社会"></asp:ListItem>
                                                    </asp:RadioButtonList>--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_ZPFS" Text='<%#Eval("JH_ZPFS") %>'></asp:Label>
                                                    <%--<asp:RadioButtonList runat="server" ID="rblJH_ZPFS" RepeatDirection="Horizontal"
                                                        Style="margin: auto">
                                                        <asp:ListItem Text="集团统一" Value="集团统一"></asp:ListItem>
                                                        <asp:ListItem Text="自主" Value="自主"></asp:ListItem>
                                                    </asp:RadioButtonList>--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_ZPRS" name="JH_ZPRS" BorderStyle="None" Text='<%#Eval("JH_ZPRS")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_ZPGW" BorderStyle="None" Text='<%#Eval("JH_ZPGW")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_ZPZY" BorderStyle="None" Text='<%#Eval("JH_ZPZY")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_ZPYX" BorderStyle="None" Text='<%#Eval("JH_ZPYX")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_ZPXL" BorderStyle="None" Text='<%#Eval("JH_ZPXL")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_ZPXB" Text='<%#Eval("JH_ZPXB") %>'></asp:Label>
                                                    <%--<asp:RadioButtonList runat="server" ID="rblJH_ZPXB" RepeatColumns="2" RepeatDirection="Horizontal"
                                                        Style="margin: auto">
                                                        <asp:ListItem Text="男" Value="男"></asp:ListItem>
                                                        <asp:ListItem Text="女" Value="女"></asp:ListItem>
                                                    </asp:RadioButtonList>--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_ZPNL" BorderStyle="None" Text='<%#Eval("JH_ZPNL")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_ZPYQ" BorderStyle="None" Text='<%#Eval("JH_ZPYQ")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_QTYQ" BorderStyle="None" Text='<%#Eval("JH_QTYQ")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_XWDGSJ" BorderStyle="None" Text='<%#Eval("JH_XWDGSJ")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_NGZDD" BorderStyle="None" Text='<%#Eval("JH_NGZDD")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="JH_QT" BorderStyle="None" Text='<%#Eval("JH_QT")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                                <td colspan="5">
                                                    <strong>人数合计</strong>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbZRS"></asp:Label>
                                                </td>
                                                <td colspan="12">
                                                </td>
                                            </tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                             <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                    没有待汇总的招聘计划!</asp:Panel>
                                <br />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="审批信息">
            <ContentTemplate>
                <div style="width: 100%; background-color: #F0F8FF" align="center" id="div2">
                    <asp:Panel runat="server" ID="panZDR">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td style="text-align: right">
                                    审批类型：
                                </td>
                                <td id="tdSPLX" colspan="3">
                                    <asp:RadioButtonList runat="server" ID="rblSPJB" RepeatDirection="Horizontal" RepeatColumns="3"
                                        Width="320px" onchange="rblSPJB_onchange()" Style="margin: auto">
                                        <asp:ListItem Text="一级审批" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="二级审批" Value="2" Selected="True"></asp:ListItem>
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
                                    <asp:Image runat="server" ID="Image1" ImageUrl="../Assets/images/username_bg.gif"
                                        onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" />
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
