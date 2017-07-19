<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Otherpur_Bill_edit.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Otherpur_Bill_edit"
    Title="新增物料管理" SmartNavigation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../JS/Datetime.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/xbjhidden.js" type="text/javascript"></script>

    <script language="javascript" type="text/jscript">
        function dakai() {
            window.open('../Basic_Data/WL_Material_List.aspx');
        }
        function viewCondition() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'block';
        }
    </script>

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

    <script type="text/javascript">
        //        $(function() {
        //            $("input[name*=MP_NUMBER],input[name*=MP_FZNUM]").keyup(function() {
        //                alert("a");
        //                var type = $("#<%=lb_shape.ClientID %>").html();
        //                alert(type);
        //                if (type == "定尺半" || type == "非定尺半") {
        //                    var guige = $(this).parent().parent().find("input[name*=MP_MARNORM]").val();
        //                    alert(guige);
        //                }
        //            });
        //        });
        function CalWeight(input) {
            var type = $("#<%=lb_shape.ClientID %>").html();

            if (type == "定尺板" || type == "非定尺板") {
                var guige = $(input).parent().parent().find("input[name*=MP_MARNORM]").val();
                var num = $(input).parent().parent().find("input[name*=MP_NUMBER]").val();
                var fznum = $(input).parent().parent().find("input[name*=MP_FZNUM]").val();
                var unit = $(input).parent().parent().find("input[name*=MP_NUNIT]").val();
                var fzunit = $(input).parent().parent().find("input[name*=MP_FZNUNIT]").val();
                var mweight = $(input).parent().parent().find("input[name*=MWEIGHT]").val();
                if (unit == "平米" || unit == "m2" || unit == "M2") {
                    var weight = parseFloat(guige) * parseFloat(num) * mweight;
                    if (!isNaN(weight)) {
                        if (fzunit == "T") {
                            $(input).parent().parent().find("input[name*=MP_FZNUM]").val((weight / 1000).toFixed(4));
                        }
                        else if (fznum == "kg") {
                            $(input).parent().parent().find("input[name*=MP_FZNUM]").val((weight).toFixed(4));
                        }
                    }

                }
                else if (fzunit == "平米" || fzunit == "m2" || fzunit == "M2") {
                    var weight = parseFloat(guige) * parseFloat(fznum) * mweight;
                    if (!isNaN(weight)) {
                        if (unit == "T") {
                            $(input).parent().parent().find("input[name*=MP_NUMBER]").val((weight / 1000).toFixed(4));
                        }
                        else if (unit == "kg") {
                            $(input).parent().parent().find("input[name*=MP_NUMBER]").val((weight).toFixed(4));
                        }
                    }

                }

            }


            if (type == "型材") {
               
                var num = $(input).parent().parent().find("input[name*=MP_NUMBER]").val();
                var fznum = $(input).parent().parent().find("input[name*=MP_FZNUM]").val();
                var unit = $(input).parent().parent().find("input[name*=MP_NUNIT]").val();
                var fzunit = $(input).parent().parent().find("input[name*=MP_FZNUNIT]").val();
                var mweight = $(input).parent().parent().find("input[name*=MWEIGHT]").val();
                if (unit == "米" || unit == "m" || unit == "M") {
                    var weight =  parseFloat(num) * mweight;
                    if (!isNaN(weight)) {
                        if (fzunit == "T") {
                            $(input).parent().parent().find("input[name*=MP_FZNUM]").val((weight / 1000).toFixed(4));
                        }
                        else if (fznum == "kg") {
                            $(input).parent().parent().find("input[name*=MP_FZNUM]").val((weight).toFixed(4));
                        }
                    }

                }
                else if (fzunit == "米" || fzunit == "m" || fzunit == "M") {
                    var weight =  parseFloat(fznum) * mweight;
                    if (!isNaN(weight)) {
                        if (unit == "T") {
                            $(input).parent().parent().find("input[name*=MP_NUMBER]").val((weight / 1000).toFixed(4));
                        }
                        else if (unit == "kg") {
                            $(input).parent().parent().find("input[name*=MP_NUMBER]").val((weight).toFixed(4));
                        }
                    }

                }

            }
        }

    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="RightContent" style="overflow: hidden">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                &nbsp; &nbsp;物料类型：<asp:Label ID="lb_shape" runat="server" Text=""></asp:Label>
                            </td>
                            <td visible="false">
                                &nbsp; &nbsp;<%--是否变更减少：--%><asp:CheckBox ID="cb_bg" runat="server" Visible="false" />
                                <asp:CheckBox ID="chkiffast" runat="server" />是否加急物料
                            </td>
                            <td align="right">
                                <asp:Button ID="btn_ycl" runat="server" Text="隐藏列" OnClientClick="viewCondition()" />
                                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btn_ycl"
                                    PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                    Y="80">
                                </asp:ModalPopupExtender>
                                <asp:Button ID="Button1" runat="server" Text="查看物料信息" OnClientClick="dakai()" />
                                &nbsp;
                                <asp:Button ID="btn_addrow" runat="server" Text="增加行" OnClick="btn_addrow_Click"
                                    Visible="false" />
                                &nbsp;
                                <asp:Button ID="btn_insert" runat="server" Text="插入行" OnClick="btn_insert_Click" />
                                &nbsp;
                                <asp:Button ID="btn_delectrow" runat="server" Text="删除行" OnClick="btn_delectrow_Click" />
                                &nbsp;
                                <asp:Button ID="btn_save" runat="server" Text="保存" OnClick="btn_save_Click" />
                                &nbsp;
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClientClick="history.go(-1)" />
                                &nbsp; &nbsp;&nbsp; &nbsp; 批量增加<asp:TextBox ID="TextBox1" runat="server" Width="50px"></asp:TextBox>
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                             ControlToValidate="TextBox1"  ValidationExpression="^[0-9]*[1-9][0-9]*$" display="Static"></asp:RegularExpressionValidator>--%>
                                行<asp:Button ID="btn_add" runat="server" Text="增加" OnClick="btn_add_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCondition" runat="server" Width="60%" Style="display: none">
                        <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="60%" style="background-color: #CCCCFF; border: solid 1px black;" border="1">
                                    <tr>
                                        <td colspan="4" align="center">
                                            选择要隐藏的列
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="计划号" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox2" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="名称" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox3" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="规格" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox4" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="材质" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox5" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="国标" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox6" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="图号/标识号" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox7" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="时间要求" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox8" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="备注" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="确定" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div>
                    <table width="100%">
                        <tr>
                            <td style="font-size: x-large; text-align: center;" colspan="3">
                                采购申请单
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 33%;" align="left">
                                &nbsp;&nbsp;&nbsp;使&nbsp;&nbsp;用&nbsp;&nbsp;部&nbsp;&nbsp;门：
                                <asp:TextBox ID="tb_dep" runat="server" Text="" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="tb_depid" runat="server" Text="" Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 33%;" align="left">
                                &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：
                                <asp:TextBox ID="Tb_shijian" runat="server" Text="" Enabled="false" Width="150px"></asp:TextBox>
                            </td>
                            <td style="width: 34%;" align="left">
                                &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：
                                <asp:TextBox ID="TextBox_pid" runat="server" Text="" Width="200px" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="Label_view" runat="server" Visible="false" Text=""></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 33%;" align="left">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;任务号：
                                <asp:TextBox ID="tb_pjinfo" runat="server" Text="" OnTextChanged="tb_pjinfo_Textchanged"
                                    AutoPostBack="True"></asp:TextBox>
                                <asp:TextBox ID="tb_pj" runat="server" Visible="false" Text=""></asp:TextBox>
                                <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_pjinfo"
                                    ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                    CompletionInterval="10" ServiceMethod="GetTask" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                </cc1:AutoCompleteExtender>
                            </td>
                            <td style="width: 33%;" align="left">
                                &nbsp;&nbsp;&nbsp;项目名称：
                                <asp:TextBox ID="tb_enginfo" runat="server" Text="" Width="150px"></asp:TextBox>
                                <asp:TextBox ID="tb_htid" runat="server" Text="" Width="150px" Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 34%;" align="left">
                                &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：
                                <asp:TextBox ID="tb_note" runat="server" Text="" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                    ActiveTabIndex="0" BorderStyle="None">
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="采购单" TabIndex="0" BorderStyle="None">
                        <ContentTemplate>
                            <div style="border: 1px solid #000000; height: 300px">
                                <div class="cpbox6 xscroll">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div>
                                                <table id="tab" width="100%" class="nowrap cptable fullwidth">
                                                    <asp:Repeater ID="tbpc_otherpurbillRepeater" runat="server" OnItemDataBound="tbpc_otherpurbillRepeater_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                                <td>
                                                                    <strong>行号</strong>
                                                                </td>
                                                                <td id="td1" runat="server">
                                                                    <strong>计划号</strong>
                                                                </td>
                                                                <td>
                                                                    <strong>材料ID</strong>
                                                                </td>
                                                                <td id="td2" runat="server">
                                                                    <strong>名称</strong>
                                                                </td>
                                                                <td id="td3" runat="server">
                                                                    <strong>规格</strong>
                                                                </td>
                                                                <td id="td4" runat="server">
                                                                    <strong>材质</strong>
                                                                </td>
                                                                <td id="td5" runat="server">
                                                                    <strong>国标</strong>
                                                                </td>
                                                                <td id="td6" runat="server">
                                                                    <strong>图号/标识号</strong>
                                                                </td>
                                                                <td>
                                                                    <strong>材料长度</strong>
                                                                </td>
                                                                <td>
                                                                    <strong>材料宽度</strong>
                                                                </td>
                                                                <td>
                                                                    <strong>数量</strong>
                                                                </td>
                                                                <td>
                                                                    <strong>单位</strong>
                                                                </td>
                                                                <td>
                                                                    <strong>辅助数量</strong>
                                                                </td>
                                                                <td>
                                                                    <strong>辅助单位</strong>
                                                                </td>
                                                                <td id="td7" runat="server">
                                                                    <strong>时间要求</strong>
                                                                </td>
                                                                <td id="td8" runat="server">
                                                                    <strong>备注</strong>
                                                                </td>
                                                                <td>
                                                                    <strong>状态</strong>
                                                                </td>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                                                runat="server" align="center">
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                    <asp:CheckBox ID="CHK" CssClass="checkBoxCss" BorderStyle="None" runat="server" Checked="false"
                                                                        onclick="checkme(this)"></asp:CheckBox>
                                                                </td>
                                                                <td id="td9" runat="server">
                                                                    <asp:Label ID="MP_PTCODE" runat="server" Text='<%#Eval("MP_PTCODE")%>'></asp:Label>
                                                                    <input type="hidden" runat="server" id="MWEIGHT" name="MWEIGHT" value='<%#Eval("MWEIGHT")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="MP_MARID" runat="server" Text='<%#Eval("MP_MARID")%>' OnTextChanged="Tb_marid_Textchanged"
                                                                        AutoPostBack="true" Width="100px"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="MP_MARID"
                                                                        ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="15" MinimumPrefixLength="1"
                                                                        CompletionInterval="10" ServiceMethod="GetCompletemarbyhc" FirstRowSelected="true"
                                                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td id="td10" runat="server">
                                                                    <asp:TextBox ID="MP_MARNAME" runat="server" BorderStyle="None" Text='<%#Eval("MP_MARNAME")%>'></asp:TextBox>
                                                                    <%--<input id="MP_MARNAME" type="text"  runat="server" value='<%#Eval("MP_MARNAME")%>' readonly="readonly" style="width:100px"/>--%>
                                                                </td>
                                                                <td id="td11" runat="server">
                                                                    <asp:TextBox ID="MP_MARNORM" name="MP_MARNORM" runat="server" Text='<%#Eval("MP_MARNORM")%>'
                                                                        BorderStyle="None"></asp:TextBox>
                                                                    <%--<input id="MP_MARNORM" type="text" runat="server" value='<%#Eval("MP_MARNORM")%>' readonly="readonly" style="width:150px" />--%>
                                                                </td>
                                                                <td id="td12" runat="server">
                                                                    <asp:TextBox ID="MP_MARTERIAL" runat="server" Text='<%#Eval("MP_MARTERIAL")%>' BorderStyle="None"></asp:TextBox>
                                                                    <%-- <input id="MP_MARTERIAL" type="text" runat="server"  value='<%#Eval("MP_MARTERIAL")%>' readonly="readonly" style="width:100px"/>--%>
                                                                </td>
                                                                <td id="td13" runat="server">
                                                                    <asp:TextBox ID="MP_MARGUOBIAO" runat="server" Text='<%#Eval("MP_MARGUOBIAO")%>'
                                                                        BorderStyle="None"></asp:TextBox>
                                                                    <%-- <input id="MP_MARGUOBIAO" type="text" runat="server" value='<%#Eval("MP_MARGUOBIAO")%>' readonly="readonly" style="width:140px"/>--%>
                                                                </td>
                                                                <td id="td14" runat="server">
                                                                    <asp:TextBox ID="MP_TUHAO" runat="server" Text='<%#Eval("MP_TUHAO")%>' Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="MP_LENGTH" runat="server" Text='<%#Eval("MP_LENGTH")%>' Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="MP_WIDTH" runat="server" Text='<%#Eval("MP_WIDTH")%>' Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td bgcolor="#98FB98" align="center">
                                                                    <asp:TextBox ID="MP_NUMBER" name="MP_NUMBER" onkeyup="CalWeight(this)" runat="server"
                                                                        Text='<%#Eval("MP_NUMBER")%>' Width="50px"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
                                                                        ControlToValidate="MP_NUMBER" ValidationExpression="^(-?\d+)(\.\d+)?" Display="Static"></asp:RegularExpressionValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="MP_NUNIT" name="MP_NUNIT" runat="server" Text='<%#Eval("MP_NUNIT")%>'
                                                                        BorderStyle="None"></asp:TextBox>
                                                                    <%-- <input id="MP_NUNIT" type="text" runat="server" value='<%#Eval("MP_NUNIT")%>' readonly="readonly" style="width:30px"/>--%>
                                                                </td>
                                                                <td bgcolor="#98FB98" align="center">
                                                                    <asp:TextBox ID="MP_FZNUM" name="MP_FZNUM" runat="server" onkeyup="CalWeight(this)"
                                                                        Text='<%#Eval("MP_FZNUM")%>' Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="MP_FZNUNIT" name="MP_FZNUNIT" runat="server" Text='<%#Eval("MP_FZNUNIT")%>'
                                                                        BorderStyle="None"></asp:TextBox>
                                                                    <%--<input id="MP_FZNUNIT" type="text" runat="server"  value='<%#Eval("MP_FZNUNIT")%>' readonly="readonly" style="width:50px"/>--%>
                                                                </td>
                                                                <td align="center" id="td15" runat="server">
                                                                    <asp:TextBox ID="MP_TIMERQ" runat="server" Text='<%#Eval("MP_TIMERQ")%>' onclick="setday(this)"></asp:TextBox>
                                                                </td>
                                                                <td align="center" id="td16" runat="server">
                                                                    <asp:TextBox ID="MP_NOTE" runat="server" Text='<%#Eval("MP_NOTE")%>'></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="MP_STATE" runat="server" Text='<%#Eval("MP_STATE")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="MP_STATETEXT" runat="server" Text='<%#get_pr_state(Eval("MP_STATE").ToString())%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr>
                                                        <td colspan="16" align="center">
                                                            <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                                没有数据！</asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
                <div>
                    <table width="100%" style="text-align: center">
                        <tr>
                            <td>
                                负责人:
                                <asp:DropDownList ID="cob_fuziren" runat="server" Width="120px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                申请人:
                                <asp:DropDownList ID="cob_sqren" runat="server" Width="120px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                制单人:<asp:TextBox ID="TextBoxexecutor" runat="server" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="TextBoxexecutorid" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div align="center">
                    <asp:Label ID="Labelerror" runat="server" Text="" Visible="False"></asp:Label><%--显示操作结果成功/失败--%>
                    <asp:Label ID="container" runat="server" Text="" Visible="false"></asp:Label><%--所有工程是否完成保存--%>
                    <asp:HiddenField ID="dvscrollX" runat="server" />
                    <asp:HiddenField ID="dvscrollY" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
