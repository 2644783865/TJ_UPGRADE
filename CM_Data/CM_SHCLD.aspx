<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_SHCLD.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_SHCLD" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    售后质量问题处理单
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
        $(function() {
            $("#<%=panSPR4.ClientID%>").hide();
            $("#<%=panSPR5.ClientID%>").hide();
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=panSPR4.ClientID%>").show();
                    $("#<%=panSPR5.ClientID%>").hide();
                    if ($("#<%=txtCLD_SPR4.ClientID%>").val() == "") {
                        $("#<%=txtCLD_SPR4.ClientID%>").val("王福泉");
                    }
                }
                if ($(this).val() == "2") {
                    $("#<%=panSPR4.ClientID%>").show();
                    $("#<%=panSPR5.ClientID%>").show();
                    if ($("#<%=txtCLD_SPR4.ClientID%>").val() == "") {
                        $("#<%=txtCLD_SPR4.ClientID%>").val("王福泉");
                    }
                    if ($("#<%=txtCLD_SPR5.ClientID%>").val() == "") {
                        $("#<%=txtCLD_SPR5.ClientID%>").val("王自清");
                    }
                }
            });
        })

        function txtCLD_HTH_onchange() {
            var txtCLD_HTH = $("#<%=txtCLD_HTH.ClientID%>").val().substring(5, 10);
            $("#<%=txtCLD_BH.ClientID%>").val("ZLCL" + txtCLD_HTH);
        }

        function rblCLD_SPLX_onchange() {
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=panSPR4.ClientID%>").show();
                    $("#<%=panSPR5.ClientID%>").hide();
                    if ($("#<%=txtCLD_SPR4.ClientID%>").val() == "") {
                        $("#<%=txtCLD_SPR4.ClientID%>").val("王福泉");
                    }
                }
                if ($(this).val() == "2") {
                    $("#<%=panSPR4.ClientID%>").show();
                    $("#<%=panSPR5.ClientID%>").show();
                    if ($("#<%=txtCLD_SPR4.ClientID%>").val() == "") {
                        $("#<%=txtCLD_SPR4.ClientID%>").val("王福泉");
                    }
                    if ($("#<%=txtCLD_SPR5.ClientID%>").val() == "") {
                        $("#<%=txtCLD_SPR5.ClientID%>").val("王自清");
                    }
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

        function xr4() {
            $("#hidPerson").val("person4");
            SelPersons();
        }

        function xr5() {
            $("#hidPerson").val("person5");
            SelPersons();
        }

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=txtCLD_SPR1.ClientID %>").val(r.st_name);
            }
            if (id == "person2") {
                $("#<%=txtCLD_SPR2.ClientID %>").val(r.st_name);
            }
            if (id == "person4") {
                $("#<%=txtCLD_SPR4.ClientID %>").val(r.st_name);
            }
            if (id == "person5") {
                $("#<%=txtCLD_SPR5.ClientID %>").val(r.st_name);
            }
            $('#win').dialog('close');
        }

        function FY_FYJE_onchange() {
            var CLD_FWZFY = 0;
            $("#gr .zz").each(function() {
                CLD_FWZFY += parseFloat($(this).val());
            });
            $("#<%=txtCLD_FWZFY.ClientID%>").val(CLD_FWZFY.toFixed(2));
        }

        function check_num(obj) {
            var num = obj.value;
            var patten = /^(\+|\-)?[0-9][0-9]{0,9}(\.[0-9]{1,2})?$/;
            if (!patten.test(num)) {
                alert('请输入正确的数据格式！！！');
                obj.value = "0.00";
                obj.focus();
            }
        }

        function btnQC_onclick() {
            $("#tdSPLX input:radio:checked").each(function() {
                $(this).attr("checked", "");
            })
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div style="width: 100%;" align="right">
        <a runat="server" id="btnSubmit" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'"
            onserverclick="btnSubmit_OnClick">提交</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <a runat="server" id="btnBack" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-back'"
            onserverclick="btnBack_OnClick">返回</a>
    </div>
    <asp:TabContainer runat="server" ID="TabContainer" Width="100%" BackColor="#F0F8FF">
        <asp:TabPanel ID="TabPanel0" HeaderText="表单信息" runat="server">
            <ContentTemplate>
                <div style="width: 100%; background-color: #F0F8FF" align="center" id="div1">
                    <table width="80%">
                        <tr>
                            <td align="center">
                                <asp:Image runat="server" ID="Image0" ImageUrl="~/Assets/images/OM_ZCTOP.jpg" />
                                <asp:HiddenField ID="hidCLD_SJID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: x-large;">
                                <strong>售&nbsp;&nbsp;后&nbsp;&nbsp;质&nbsp;&nbsp;量&nbsp;&nbsp;问&nbsp;&nbsp;题&nbsp;&nbsp;处&nbsp;&nbsp;理&nbsp;&nbsp;单</strong>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel runat="server" ID="pan0">
                        <table width="80%">
                            <tr>
                                <td colspan="2" align="right" style="font-size: larger">
                                    文件号：TJZJ-R-M-12
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="font-size: larger" width="25%">
                                    编号：<asp:TextBox runat="server" ID="txtCLD_BH" Text="" Font-Size="Medium" Width="70%"
                                        Height="25px"></asp:TextBox>
                                </td>
                                <td align="right" style="font-size: larger">
                                    版本：1
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panJBXX">
                        <table class="tab">
                            <tr>
                                <td width="10%">
                                    项目名称
                                </td>
                                <td width="20%">
                                    <asp:TextBox runat="server" ID="txtCLD_XMMC" Text=""></asp:TextBox>
                                </td>
                                <td width="10%">
                                    合同号
                                </td>
                                <td width="20%">
                                    <%--<asp:TextBox runat="server" ID="txtCLD_HTH" Text="" onchange="txtCLD_HTH_onchange()"></asp:TextBox>--%>
                                    <asp:TextBox runat="server" ID="txtCLD_HTH" Text=""></asp:TextBox>
                                </td>
                                <td width="10%">
                                    设备名称
                                </td>
                                <td width="30%">
                                    <asp:TextBox runat="server" ID="txtCLD_SBMC" Text=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    顾客名称
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCLD_GKMC" Text=""></asp:TextBox>
                                </td>
                                <td>
                                    任务号
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCLD_RWH" Text=""></asp:TextBox>
                                </td>
                                <td>
                                    信息简介
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCLD_XXJJ" Text=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    质量问题描述<br />
                                    及业主要求
                                </td>
                                <td colspan="5">
                                    <asp:TextBox runat="server" ID="txtCLD_WTMS" TextMode="MultiLine" Rows="12" Text=""
                                        Width="96%" Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    业主地址
                                </td>
                                <td colspan="5">
                                    <asp:TextBox runat="server" ID="txtCLD_YZDZ" Text=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    联系人
                                </td>
                                <td colspan="2">
                                    <asp:TextBox runat="server" ID="txtCLD_LXR"></asp:TextBox>
                                </td>
                                <td>
                                    联系方式
                                </td>
                                <td colspan="2">
                                    <asp:TextBox runat="server" ID="txtCLD_LXFS" Text=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    时间要求
                                </td>
                                <td colspan="5">
                                    <asp:TextBox runat="server" ID="txtCLD_SJYQ" Text=""></asp:TextBox>
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
                    <asp:Panel runat="server" ID="panYYFX">
                        <table class="tab">
                            <tr>
                                <td rowspan="2" width="20%">
                                    原因分析（质量部）
                                </td>
                                <td colspan="2" width="80%">
                                    <asp:TextBox runat="server" ID="txtCLD_YYFX" TextMode="MultiLine" Text="" Rows="12"
                                        Font-Size="Medium" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%">
                                    填写人:<asp:TextBox runat="server" ID="txtCLD_YYFX_TXR" Width="50%" onfocus="this.blur()"></asp:TextBox>
                                </td>
                                <td>
                                    填写时间：<asp:Label runat="server" ID="lbCLD_YYFX_TXSJ" Width="50%"></asp:Label>
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
                                <input type="hidden" runat="server" id="Hidden3" />
                                <asp:FileUpload ID="FileUpload3" runat="server" />
                                <asp:Button ID="btnFU2" runat="server" Text="上传文件" OnClick="btnFU2_OnClick" CausesValidation="False" />
                                <br />
                                <asp:Label ID="filesError3" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                                <table width="60%">
                                    <asp:Repeater runat="server" ID="rptYYFX">
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
                                                    <asp:LinkButton runat="server" ID="lbtndelete3" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtndelete3_OnClick" CausesValidation="False" Text="删除">
                                                        <asp:Image runat="server" ID="imgdelete1" ImageUrl="~/Assets/images/erase.gif" Width="15px"
                                                            Height="15px" />
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbtnonload3" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtnonload3_OnClick" CausesValidation="False" Text="下载">
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
                    <asp:Panel runat="server" ID="panCLYJ">
                        <table class="tab">
                            <tr>
                                <td rowspan="2" width="20%">
                                    处理意见（技术部）
                                </td>
                                <td colspan="2" width="80%">
                                    <asp:TextBox runat="server" ID="txtCLD_CLYJ" TextMode="MultiLine" Text="" Rows="12"
                                        Font-Size="Medium" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%">
                                    填写人:<asp:TextBox runat="server" ID="txtCLD_CLYJ_TXR" Width="50%" onfocus="this.blur()"></asp:TextBox>
                                </td>
                                <td>
                                    填写时间：<asp:Label runat="server" ID="lbCLD_CLYJ_TXSJ" Width="50%"></asp:Label>
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
                                <input type="hidden" runat="server" id="hidFile" />
                                <asp:FileUpload ID="FileUp" runat="server" /><asp:Button ID="btnAddFU" runat="server"
                                    Text="上传文件" OnClick="btnUp_Click" CausesValidation="False" />
                                <br />
                                <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                                <table width="60%">
                                    <asp:Repeater runat="server" ID="rptCLYJ">
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
                                                    <asp:LinkButton runat="server" ID="lbtndelete" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtndelete_OnClick" CausesValidation="False" Text="删除">
                                                        <asp:Image runat="server" ID="imgdelete1" ImageUrl="~/Assets/images/erase.gif" Width="15px"
                                                            Height="15px" />
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbtnonload" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtnonload_OnClick" CausesValidation="False" Text="下载">
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
                    <asp:Panel runat="server" ID="panCLFA">
                        <table class="tab">
                            <tr>
                                <td rowspan="2" width="20%">
                                    处理方案（市场部）
                                </td>
                                <td colspan="2" width="80%">
                                    <asp:TextBox runat="server" ID="txtCLD_CLFA" TextMode="MultiLine" Text="" Rows="12"
                                        Font-Size="Medium" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%">
                                    填写人:<asp:TextBox runat="server" ID="txtCLD_CLFA_TXR" Width="50%" onfocus="this.blur()"></asp:TextBox>
                                </td>
                                <td>
                                    填写时间：<asp:Label runat="server" ID="lbCLD_CLFA_TXSJ" Width="50%"></asp:Label>
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
                                <input type="hidden" runat="server" id="Hidden4" />
                                <asp:FileUpload ID="FileUpload4" runat="server" />
                                <asp:Button ID="btnFU3" runat="server" Text="上传文件" OnClick="btnFU3_OnClick" CausesValidation="False" />
                                <br />
                                <asp:Label ID="filesError4" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                                <table width="60%">
                                    <asp:Repeater runat="server" ID="rptCLFA">
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
                                                    <asp:LinkButton runat="server" ID="lbtndelete4" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtndelete4_OnClick" CausesValidation="False" Text="删除">
                                                        <asp:Image runat="server" ID="imgdelete1" ImageUrl="~/Assets/images/erase.gif" Width="15px"
                                                            Height="15px" />
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbtnonload4" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtnonload4_OnClick" CausesValidation="False" Text="下载">
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
                    <asp:Panel runat="server" ID="panCLJG">
                        <table class="tab">
                            <tr>
                                <td rowspan="2" width="20%">
                                    处理过程及<br />
                                    结果（负责部门）
                                </td>
                                <td colspan="2" width="80%">
                                    <asp:TextBox runat="server" ID="txtCLD_CLJG" TextMode="MultiLine" Text="" Rows="12"
                                        Font-Size="Medium" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%">
                                    填写人:<asp:TextBox runat="server" ID="txtCLD_CLJG_TXR" Width="50%" onfocus="this.blur()"></asp:TextBox>
                                </td>
                                <td>
                                    填写时间：<asp:Label runat="server" ID="lbCLD_CLJG_TXSJ" Width="50%"></asp:Label>
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
                                <input type="hidden" runat="server" id="Hidden1" />
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:Button ID="btnFU" runat="server" Text="上传文件" OnClick="btnFU_OnClick" CausesValidation="False" />
                                <br />
                                <asp:Label ID="Label6" runat="server" ForeColor="Red" Visible="False" EnableViewState="False"></asp:Label>
                                <table width="60%">
                                    <asp:Repeater runat="server" ID="rptCLJG">
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
                                                    <asp:LinkButton runat="server" ID="lbtndelete1" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtndelete1_OnClick" CausesValidation="False" Text="删除">
                                                        <asp:Image runat="server" ID="imgdelete1" ImageUrl="~/Assets/images/erase.gif" Width="15px"
                                                            Height="15px" />
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbtnonload1" CommandArgument='<%#Eval("FILE_ID")%>'
                                                        OnClick="lbtnonload1_OnClick" CausesValidation="False" Text="下载">
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
                    <asp:UpdatePanel runat="server" ID="UpdatePanel">
                        <ContentTemplate>
                            <asp:Panel ID="panFYTJ" runat="server">
                                <table class="tab">
                                    <tr>
                                        <td rowspan="4" width="20%">
                                            服务费用统计
                                        </td>
                                        <td width="40%" align="right">
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
                                                <asp:Repeater ID="rptFWFY" runat="server">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle headcolor">
                                                            <th width="50px">
                                                                <strong>序号</strong>
                                                            </th>
                                                            <th>
                                                                <strong>费&nbsp;用&nbsp;名&nbsp;称</strong>
                                                            </th>
                                                            <th>
                                                                <strong>费&nbsp;用&nbsp;（元）</strong>
                                                            </th>
                                                            <th>
                                                                <strong>备&nbsp;注</strong>
                                                            </th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                            <td>
                                                                <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                                <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                                </asp:CheckBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="FY_FYMC" runat="server" Text='<%# Eval("FY_FYMC")%>' Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="FY_FYJE" CssClass="zz" runat="server" Text='<%# Eval("FY_FYJE")%>'
                                                                    Width="200px" onchange="FY_FYJE_onchange()" onblur="check_num(this)"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="FY_BZ" runat="server" Text='<%# Eval("FY_BZ")%>' Width="150px"></asp:TextBox>
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
                                    <tr>
                                        <td width="40%">
                                        </td>
                                        <td align="right">
                                            合计：<asp:TextBox runat="server" ID="txtCLD_FWZFY" Text="" Width="50%" onfocus="this.blur()"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="40%">
                                            统计人：<asp:TextBox runat="server" ID="txtCLD_FWFY_TJR" Width="50%" onfocus="this.blur()"></asp:TextBox>
                                        </td>
                                        <td width="40%">
                                            填写时间：<asp:Label runat="server" ID="lbCLD_FWFY_TJSJ" Width="50%"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="审批">
            <ContentTemplate>
                <div style="width: 100%; background-color: #F0F8FF" align="center" id="div2">
                    <asp:Panel runat="server" ID="panZDR">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td style="height: 30px" width="20%">
                                    制单人：
                                </td>
                                <td width="30%">
                                    <asp:TextBox runat="server" ID="txtCLD_ZDR" onfocus="this.blur()"></asp:TextBox>
                                </td>
                                <td width="20%">
                                    制单时间：
                                </td>
                                <td width="30%">
                                    <asp:Label runat="server" ID="lbCLD_ZDSJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    制单人建议：<asp:TextBox runat="server" ID="txtCLD_ZDJY" Text="" TextMode="MultiLine" Width="90%"
                                        Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panSPR1">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td colspan="6" align="center" style="height: 30px;">
                                    <asp:Label runat="server" ID="lb3" Text="市场部长第一次审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" style="height: 30px;">
                                    审批人：
                                </td>
                                <td width="15%">
                                    <asp:TextBox runat="server" ID="txtCLD_SPR1" onfocus="this.blur()" Width="70%"></asp:TextBox>
                                    <asp:Image runat="server" ID="Image1" ImageUrl="../Assets/images/username_bg.gif"
                                        onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
                                </td>
                                <td width="15%">
                                    审批结论：
                                </td>
                                <td width="25%" align="center">
                                    <asp:RadioButtonList runat="server" ID="rblCLD_SPR1_JL" RepeatDirection="Horizontal"
                                        Width="80%" RepeatColumns="2">
                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="15%">
                                    审批时间：
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="lbCLD_SPR1_SJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    审批建议：<asp:TextBox runat="server" ID="txtCLD_SPR1_JY" Text="" TextMode="MultiLine"
                                        Font-Size="Medium" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panSPR2">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td colspan="6" align="center" style="height: 30px">
                                    <asp:Label runat="server" ID="Label3" Text="市场部长第二次审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" style="height: 30px">
                                    审批人：
                                </td>
                                <td width="15%">
                                    <asp:TextBox runat="server" ID="txtCLD_SPR2" onfocus="this.blur()" Width="70%"></asp:TextBox>
                                    <asp:Image runat="server" ID="Image2" ImageUrl="../Assets/images/username_bg.gif"
                                        onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" />
                                </td>
                                <td width="15%">
                                    审批结论：
                                </td>
                                <td width="25%">
                                    <asp:RadioButtonList runat="server" ID="rblCLD_SPR2_JL" RepeatDirection="Horizontal"
                                        Width="80%" RepeatColumns="2">
                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="15%">
                                    审批时间：
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="lbCLD_SPR2_SJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    审批建议：<asp:TextBox runat="server" ID="txtCLD_SPR2_JY" Text="" TextMode="MultiLine"
                                        Font-Size="Medium" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table class="tab">
                            <tr>
                                <td width="20%">
                                </td>
                                <td align="center" style="height: 30px;" width="20%">
                                    勾选负责部门：
                                </td>
                                <td width="50%">
                                    <asp:CheckBoxList runat="server" ID="cbxlCLD_FZBM" RepeatDirection="Horizontal" RepeatColumns="4"
                                        Width="80%">
                                        <asp:ListItem Text="生产部" Value="04"></asp:ListItem>
                                        <asp:ListItem Text="质量部" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="技术部" Value="03"></asp:ListItem>
                                        <asp:ListItem Text="采购部" Value="05"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">
                                </td>
                                <td style="height: 30px;" align="center" width="20%">
                                    审批类型：
                                </td>
                                <td id="tdSPLX" align="center" width="50%">
                                    <asp:RadioButtonList runat="server" ID="rblCLD_SPLX" RepeatDirection="Horizontal"
                                        Width="80%" onchange="rblCLD_SPLX_onchange()">
                                        <asp:ListItem Text="主管领导" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="主管领导+总经理" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <%--<input type="button" id="btnQC" onclick="btnQC_onclick()" style="background-color: ButtonHighlight"
                                        value="清除" visible="false" />--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panSPR4">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td colspan="6" align="center" style="height: 30px">
                                    <asp:Label runat="server" ID="Label1" Text="主管领导审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" style="height: 30px">
                                    审批人：
                                </td>
                                <td width="15%">
                                    <asp:TextBox runat="server" ID="txtCLD_SPR4" onfocus="this.blur()" Width="70%"></asp:TextBox>
                                    <asp:Image runat="server" ID="Image3" ImageUrl="../Assets/images/username_bg.gif"
                                        onclick="xr4()" align="middle" Style="cursor: pointer" title="选择" />
                                </td>
                                <td width="15%">
                                    审批结论：
                                </td>
                                <td width="25%">
                                    <asp:RadioButtonList runat="server" ID="rblCLD_SPR4_JL" RepeatDirection="Horizontal"
                                        Width="80%" RepeatColumns="2">
                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="15%">
                                    审批时间：
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="lbCLD_SPR4_SJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    审批建议：<asp:TextBox runat="server" ID="txtCLD_SPR4_JY" Text="" TextMode="MultiLine"
                                        Font-Size="Medium" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panSPR5">
                        <table class="tab">
                            <tr style="background-color: #79CDCD">
                                <td colspan="6" align="center" style="height: 30px;">
                                    <asp:Label runat="server" ID="Label2" Text="总经理审批" ForeColor="Red" Font-Size="Larger"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" style="height: 30px;">
                                    审批人：
                                </td>
                                <td width="15%">
                                    <asp:TextBox runat="server" ID="txtCLD_SPR5" onfocus="this.blur()" Width="70%"></asp:TextBox>
                                    <asp:Image runat="server" ID="Image4" ImageUrl="../Assets/images/username_bg.gif"
                                        onclick="xr5()" align="middle" Style="cursor: pointer" title="选择" />
                                </td>
                                <td width="15%">
                                    审批结论：
                                </td>
                                <td width="25%">
                                    <asp:RadioButtonList runat="server" ID="rblCLD_SPR5_JL" RepeatDirection="Horizontal"
                                        Width="80%" RepeatColumns="2">
                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="15%">
                                    审批时间：
                                </td>
                                <td width="15%">
                                    <asp:Label runat="server" ID="lbCLD_SPR5_SJ" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    审批建议：<asp:TextBox runat="server" ID="txtCLD_SPR5_JY" Text="" TextMode="MultiLine"
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
