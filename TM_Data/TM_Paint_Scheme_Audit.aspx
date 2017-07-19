<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="TM_Paint_Scheme_Audit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Paint_Scheme_Audit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    油漆涂装细化方案审批
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../PC_Data/PcJs/superTables_compressed.js" type="text/javascript"></script>

    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />

    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/TM_BlukCopy.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function Submit() {
            var ok = confirm("提交后不能修改，确认提交吗？");
            if (ok == true) {
                if (document.getElementById("<%=btnsubmit.ClientID%>") == null) {
                    alert("您无权提交！！！");
                    return false;
                }
                else {
                    document.getElementById("<%=btnsubmit.ClientID%>").click();
                    document.getElementById("btnPaintsubmit").disabled = true;
                }
            }
            return ok;
        }


        function SelTechPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }

        function SelTechPersons2() {
            $("#hidPerson").val("second");
            SelPersons();
        }
        function SelTechPersons3() {
            $("#hidPerson").val("third");
            SelPersons();
        }
        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            else if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
            else if (id == "third") {
                $("#<%=txt_third.ClientID %>").val(r.st_name);
                $("#<%=thirdid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }

        $(function() {
            $("#<%=btnViewMP.ClientID %>").click(function() {
                var date = new Date();
                var time = date.getTime();
                var returnValue = window.showModalDialog("TM_Paint_Collect.aspx?nouse=" + time + "&pId=" + $("#<%= ps_no.ClientID %>").html(), '', "dialogHeight:400px;dialogWidth:700px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");

            });
        });
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ActiveTabIndex="0" AutoPostBack="false">
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="油漆涂装细化方案" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper" style="height: 472px">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td style="font-size: large; text-align: center;" colspan="7">
                                    油漆涂装细化方案
                                </td>
                                <td align="right">
                                    <asp:Image ID="Image3" ToolTip="返回上一页" CssClass="hand" Height="16" Width="16" runat="server"
                                        onclick="history.go(-1);" ImageUrl="~/Assets/icons/back.png" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 8%" align="right">
                                    合同号:
                                </td>
                                <td style="width: 15%">
                                    <asp:Label ID="tsaid" runat="server" Width="100%" />
                                </td>
                                <td style="width: 8%" align="right">
                                    项目名称:
                                </td>
                                <td style="width: 14%">
                                    <asp:Label ID="proname" runat="server" Width="100%" />
                                    <input id="proid" type="text" runat="server" readonly="readonly" style="display: none" />
                                </td>
                                <td style="width: 8%" align="right">
                                    设备名称:
                                </td>
                                <td style="width: 14%">
                                    <asp:Label ID="engname" runat="server" Width="100%" />
                                </td>
                                <td style="width: 8%" align="right">
                                    计划编号:
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="ps_no" runat="server" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 8%" align="right">
                                    油漆品牌:
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="paint_brand" runat="server" Width="100%" />
                                </td>
                                <td style="width: 8%" align="right">
                                    编制日期:
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="plandate" runat="server" Width="100%" />
                                </td>
                                <td>
                                    <a href="#" id="btnViewMP" runat="server">
                                        <img id="Img1" src="~/Assets/images/create.gif" style="border: 0px;" runat="server" />查看材料汇总
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-outer">
                        <asp:Panel ID="Panel1" runat="server" Style="height: 385px; width: 100%; position: static">
                            <div style="border: 1px solid #000000; height: 480px">
                                <div class="cpbox4 xscroll">
                                    <table id="tab" class="nowrap cptable fullwidth">
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr align="center" style="background-color: #B9D3EE" id="row">
                                                    <td rowspan="2">
                                                        任务号
                                                    </td>
                                                    <td rowspan="2">
                                                        部件名称
                                                    </td>
                                                    <td rowspan="2">
                                                        图号
                                                    </td>
                                                    <td rowspan="2">
                                                        除锈级别
                                                    </td>
                                                    <td rowspan="2">
                                                        涂装面积(m2)
                                                    </td>
                                                    <td colspan="4">
                                                        底漆
                                                    </td>
                                                    <td colspan="4">
                                                        中间漆
                                                    </td>
                                                    <td colspan="6">
                                                        面漆
                                                    </td>
                                                    <td rowspan="2">
                                                        总厚度
                                                    </td>
                                                    <td rowspan="2">
                                                        备注
                                                    </td>
                                                    <td rowspan="2">
                                                        变更备注
                                                    </td>
                                                </tr>
                                                <tr align="center" style="background-color: #B9D3EE" id="Tr1">
                                                    <td>
                                                        种类
                                                    </td>
                                                    <td>
                                                        厚度(um)
                                                    </td>
                                                    <td>
                                                        用量(L)
                                                    </td>
                                                    <td>
                                                        稀释剂(L)
                                                    </td>
                                                    <td>
                                                        种类
                                                    </td>
                                                    <td>
                                                        厚度(um)
                                                    </td>
                                                    <td>
                                                        用量(L)
                                                    </td>
                                                    <td>
                                                        稀释剂(L)
                                                    </td>
                                                    <td>
                                                        种类
                                                    </td>
                                                    <td>
                                                        厚度(um)
                                                    </td>
                                                    <td>
                                                        用量(L)
                                                    </td>
                                                    <td>
                                                        稀释剂(L)
                                                    </td>
                                                    <td>
                                                        颜色
                                                    </td>
                                                    <td>
                                                        色号
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" id="row" runat="server">
                                                    <td>
                                                        <%# Eval("PS_ENGID") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_NAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_TUHAO") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_LEVEL") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_MIANJI") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_BOTSHAPE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_BOTHOUDU") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_BOTYONGLIANG") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_BOTXISHIJI") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_MIDSHAPE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_MIDHOUDU") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_MIDYONGLIANG") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_MIDXISHIJI") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_TOPSHAPE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_TOPHOUDU") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_TOPYONGLIANG") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_TOPXISHIJI") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_TOPCOLOR") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_TOPCOLORLABEL") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_TOTALHOUDU") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PS_BEIZHU") %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBGBeiZhu" runat="server" Text=' <%# Eval("PS_BGBEIZHU") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="审  核" TabIndex="1">
            <ContentTemplate>
                <div class="box-wrapper" style="height: 472px">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;
                                    <input id="btnPaintsubmit" type="button" value="提 交" onclick="return Submit();" />
                                    <asp:Button ID="btnsubmit" runat="server" CssClass="hidden" OnClick="btnsubmit_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox ID="ckbMessage" Checked="true" runat="server" />&nbsp;Notes邮件提醒
                                </td>
                                <td align="right">
                                    <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="二级审核" Value="2" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: large; text-align: center; height: 43px">
                                    油漆涂装细化方案
                                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-outer">
                        <asp:Panel ID="Panel2" runat="server" Style="height: 376px; width: 100%; position: static">
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td style="width: 10%" align="center">
                                        项目名称
                                    </td>
                                    <td style="width: 40%">
                                        <asp:Label ID="pro_name" runat="server" Width="100%" />
                                    </td>
                                    <td style="width: 10%" align="center">
                                        设备名称
                                    </td>
                                    <td style="width: 40%">
                                        <asp:Label ID="eng_name" runat="server" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        编制
                                    </td>
                                    <td>
                                        <asp:Label ID="editor" runat="server" Width="100%"></asp:Label>
                                        <input id="editorid" type="text" runat="server" readonly="readonly" style="display: none" />
                                    </td>
                                    <td align="center">
                                        编制日期
                                    </td>
                                    <td>
                                        <asp:Label ID="plan_date" runat="server" Width="100%" />
                                    </td>
                                    <input id="state" type="text" runat="server" style="display: none" />
                                </tr>
                                <tr>
                                    <td align="center">
                                        主管审核
                                    </td>
                                    <td colspan="3">
                                        <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                            <tr style="height: 25px">
                                                <td align="center" style="width: 8%">
                                                    审批人
                                                </td>
                                                <td style="width: 24%">
                                                    <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                    <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                </td>
                                                <td align="center" style="width: 8%">
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
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <div>
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
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="return savePick();">
                    保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                        onclick="javascript:$('#win').dialog('close')">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidPerson" value="" />
    </div>
</asp:Content>
